<template>
  <div class="dialog" :class="{ hide: !show }">
    <div class="dialog-background" @click="$emit('onChange', false)"></div>
    <div class="dialog-content">
      <div class="dialog-header">
        <div class="dialog-header-title">THÔNG TIN KHÁCH HÀNG</div>
        <div class="dialog-close-button" @click="$emit('onChange', false)">
          <i class="fas fa-times"></i>
        </div>
      </div>
      <div class="dialog-body">
        <div
          v-if="isLoading"
          style="
            height: 366px;
            display: flex;
            align-items: center;
            justify-content: center;
          "
        >
          <Loader />
        </div>
        <div class="row" v-if="isSuccess">
          <div class="col-4 avatar-box">
            <div class="avatar"></div>
            <div style="text-align: center">
              Vui lòng chọn ảnh có định dạng jpg, jpeg, png, gif
            </div>
          </div>

          <div class="col-8">
            <div class="row">
              <div class="col-6">
                <div class="field">
                  <label
                    >Mã khách hàng (<span style="color: red">*</span>)</label
                  >
                  <Input
                    :value="customer && customer.customerCode"
                    @input="
                      $emit('update:customer', {
                        ...customer,
                        customerCode: $event,
                      })
                    "
                  />
                </div>
              </div>

              <div class="col-6">
                <div class="field">
                  <label>Họ và tên (<span style="color: red">*</span>)</label>
                  <Input
                    :value="customer && customer.fullName"
                    @input="
                      $emit('update:customer', {
                        ...customer,
                        fullName: $event,
                      })
                    "
                  />
                </div>
              </div>

              <div class="col-6">
                <div class="field">
                  <label>Mã thẻ thành viên</label>
                  <Input
                    :value="customer && customer.memberCardCode"
                    @input="
                      $emit('update:customer', {
                        ...customer,
                        memberCardCode: $event,
                      })
                    "
                  />
                </div>
              </div>

              <div class="col-6">
                <div class="field">
                  <label>Nhóm khách hàng</label>
                  <Combobox
                    :options="customerGroupOptions"
                    :value="customer && customer.customerGroupId"
                    @input="
                      $emit('update:customer', {
                        ...customer,
                        customerGroupId: $event,
                      })
                    "
                  />
                </div>
              </div>

              <div class="col-6">
                <div class="field">
                  <label>Ngày sinh</label>
                  <Input
                    inputType="date"
                    :value="customer && customer.dateOfBirth"
                    @input="
                      $emit('update:customer', {
                        ...customer,
                        dateOfBirth: $event,
                      })
                    "
                  />
                </div>
              </div>

              <div class="col-6">
                <div class="field">
                  <label>Giới tính</label>
                  <div class="flex-horizontal">
                    <div>
                      <Radio
                        name="gender"
                        :value="0"
                        :checkValue="customer && customer.gender"
                        @input="
                          $emit('update:customer', {
                            ...customer,
                            gender: $event,
                          })
                        "
                      />
                      <label>Nữ</label>
                    </div>
                    <div>
                      <Radio
                        name="gender"
                        :value="1"
                        :checkValue="customer && customer.gender"
                        @input="
                          $emit('update:customer', {
                            ...customer,
                            gender: $event,
                          })
                        "
                      />
                      <label>Nam</label>
                    </div>
                    <div>
                      <Radio
                        name="gender"
                        :value="2"
                        :checkValue="customer && customer.gender"
                        @input="
                          $emit('update:customer', {
                            ...customer,
                            gender: $event,
                          })
                        "
                      />
                      <label>Khác</label>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="col-8">
            <div class="field">
              <label>Email</label>
              <Input
                :value="customer && customer.email"
                @input="
                  $emit('update:customer', { ...customer, email: $event })
                "
              />
            </div>
          </div>

          <div class="col-4">
            <div class="field">
              <label>Số điện thoại (<span style="color: red">*</span>)</label>
              <Input
                :value="customer && customer.phoneNumber"
                @input="
                  $emit('update:customer', { ...customer, phoneNumber: $event })
                "
              />
            </div>
          </div>

          <div class="col-8">
            <div class="field">
              <label>Tên công ty</label>
              <Input
                :value="customer && customer.companyName"
                @input="
                  $emit('update:customer', { ...customer, companyName: $event })
                "
              />
            </div>
          </div>

          <div class="col-4">
            <div class="field">
              <label>Mã số thuế</label>
              <Input
                :value="customer && customer.companyTaxCode"
                @input="
                  $emit('update:customer', {
                    ...customer,
                    companyTaxCode: $event,
                  })
                "
              />
            </div>
          </div>

          <div class="col-12">
            <div class="field">
              <label>Địa chỉ</label>
              <Input
                :value="customer && customer.address"
                @input="
                  $emit('update:customer', { ...customer, address: $event })
                "
              />
            </div>
          </div>
        </div>
      </div>

      <div class="dialog-footer">
        <Button text="Lưu" />
        <Button
          style="margin-left: 8px"
          text="Hủy"
          :color="null"
          @click="$emit('onChange', false)"
        />
      </div>
    </div>
  </div>
</template>

<script>
import Button from "../../components/Button.vue";
import Input from "../../components/Input.vue";
import Combobox from "../../components/Combobox.vue";
import Radio from "../../components/Radio.vue";
import Loader from "../../components/Loader.vue";

import StateEnum from "../../store/StateEnum.js";
export default {
  components: {
    Button,
    Input,
    Combobox,
    Radio,
    Loader,
  },
  props: {
    /**
     * Biến xác định trạng thái dialog.
     * CreatedBy: dbhuan (27/04/2021)
     */
    show: Boolean,

    /**
     * Danh sách nhóm khách hàng.
     * CreatedBy: dbhuan (27/04/2021)
     */
    customerGroupOptions: Array,

    /**
     * Thông tin khách hàng. Mặc định là null.
     * CreatedBy: dbhuan (27/04/2021)
     */
    customer: {
      type: Object,
      default: null,
    },

    /**
     * Trạng thái của dilaog: LOADING, SUCCESS, ERROR.
     * CreatedBy: dbhuan (04/05/2021)
     */
    state: {
      type: Number,
      default: StateEnum.LOADING,
    },
  },

  computed: {
    isLoading: function () {
      return this.state == StateEnum.LOADING;
    },
    isSuccess: function () {
      return this.state == StateEnum.SUCCESS;
    },
  },
};
</script>

<style scoped>
.dialog {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
}
.dialog .dialog-background {
  position: absolute;
  height: 100%;
  width: 100%;
  background-color: #000;
  opacity: 0.4;
}

.dialog .dialog-content {
  z-index: 40;
  width: 640px;
  max-height: calc(100% - 40px);
  margin: 20px auto;
  background-color: #fff;
  position: relative;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}

.dialog .dialog-header {
  padding-left: 24px;
  padding-right: 24px;
  height: 40px;
  line-height: 40px;
}

.dialog .dialog-header-title {
  font-size: 15px;
  font-weight: bold;
}

.dialog .dialog-close-button {
  position: absolute;
  right: 0;
  top: 0;
  width: 40px;
  text-align: center;
  cursor: pointer;
  border-bottom-left-radius: 4px;
}

.dialog .dialog-close-button:hover {
  background-color: #e5e5e5;
}

.dialog .dialog-body {
  flex: 1;
  overflow: auto;
  padding-left: 24px;
  padding-right: 24px;
}

.dialog .dialog-body .avatar-box {
  display: flex;
  align-items: center;
  flex-direction: column;
  cursor: pointer;
}

.dialog .dialog-body .avatar {
  height: 150px;
  width: 150px;
  border-radius: 50%;
  border: 1px solid #ccc;
  background-image: url("../../assets/img/default-avatar.jpg");
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
}

.dialog .dialog-footer {
  height: 56px;
  flex-shrink: 0;
  display: flex;
  justify-content: flex-end;
  align-items: center;
  padding-right: 24px;
}

.dialog.hide {
  display: none;
}

.field {
  padding-left: 4px;
  padding-right: 4px;
}

.field > label {
  display: inline-block;
  margin-bottom: 4px;
}

.field .flex-horizontal {
  display: flex;
  flex-direction: row;
  align-items: center;
  height: 40px;
}

/* Grid layout */
.row {
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
}
.col-1 {
  width: 8.33%;
  flex: none;
}
.col-2 {
  width: 16.66%;
  flex: none;
}
.col-3 {
  width: 25%;
  flex: none;
}
.col-4 {
  width: 33.33%;
  flex: none;
}
.col-5 {
  width: 41.66%;
  flex: none;
}
.col-6 {
  width: 50%;
  flex: none;
}
.col-7 {
  width: 58.33%;
  flex: none;
}
.col-8 {
  width: 66.66%;
  flex: none;
}
.col-9 {
  width: 75%;
  flex: none;
}
.col-10 {
  width: 83.33%;
  flex: none;
}
.col-11 {
  width: 91.66%;
  flex: none;
}
.col-12 {
  width: 100%;
  flex: none;
}
</style>