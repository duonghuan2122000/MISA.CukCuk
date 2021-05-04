<template>
  <div>
    <div v-if="!isLoading && !isSuccess" class="content error">
      Error. Vui lòng thử lại.
    </div>
    <div v-else class="content">
      <div class="title-box">
        <div class="title">DANH SÁCH KHÁCH HÀNG</div>
        <Button
          text="Thêm khách hàng"
          iconLeft="user-plus"
          @click="onClickBtnAddCustomer"
        />
      </div>

      <div class="toolbar-box" style="margin-top: 16px">
        <div class="toolbar-left">
          <div class="input-box">
            <span class="fas fa-search icon-left"></span>
            <Input
              placeholder="Nhập mã, họ tên hoặc số điện thoại khách hàng"
              style="width: 300px"
              class="has-icon"
              v-model="customerFilter"
              @input="onChangeInputCustomerFilter"
            />
          </div>
          <Combobox
            style="margin-left: 8px; width: 150px"
            :options="customerGroupOptions"
            v-model="selectedCustomerGroupId"
            @input="onChangeComboboxCustomerGroup"
          />
        </div>
        <div class="toolbar-right">
          <Button
            v-if="customersDel && customersDel.length > 0"
            styleBtn="background-color: #f20"
            styleIcon="color: #fff"
            :color="null"
            :onlyIcon="true"
            icon="trash"
            @click="onClickDeleteCustomer"
          />
          <Button
            style="margin-left: 8px"
            :color="null"
            :onlyIcon="true"
            icon="sync"
            @click="refreshData"
          />
        </div>
      </div>

      <div class="scroll">
        <div v-if="isLoading" class="loading">
          <div class="loader"></div>
        </div>
        <div v-else-if="isSuccess">
          <Table v-if="customers.length > 0">
            <template v-slot:head>
              <tr>
                <th>
                  <input
                    type="checkbox"
                    @change="onSelectAllCustomers"
                    :checked="isCheckedAll"
                  />
                </th>
                <th>Mã khách hàng</th>
                <th>Họ và tên</th>
                <th>Ngày sinh</th>
                <th>Giới tính</th>
                <th>Nhóm khách hàng</th>
                <th>Email</th>
                <th>Số điện thoại</th>
              </tr>
            </template>
            <template v-slot:body>
              <tr
                v-for="c in customers"
                :key="c.customerId"
                @dblclick="onDblClickTrCustomer(c.customerId)"
              >
                <td>
                  <CheckBox :value="c.customerId" v-model="customersDel" />
                </td>
                <td>{{ c.customerCode }}</td>
                <td>{{ c.fullName }}</td>
                <td>{{ c.dateOfBirth | formatDate }}</td>
                <td>{{ c.genderName }}</td>
                <td>{{ c.customerGroupName }}</td>
                <td>{{ c.email }}</td>
                <td>{{ c.phoneNumber }}</td>
              </tr>
            </template>
          </Table>
          <div v-else>Không có dữ liệu</div>
        </div>
      </div>

      <div class="footer">
        <div>
          Hiển thị {{ startRecord }} - {{ endRecord }}/{{ totalRecord }} khách
          hàng
        </div>
        <Pagination :page="page" :totalPages="totalPages" />
        <div>{{ pageSize }} khách hàng / trang</div>
      </div>
    </div>
    <CustomerDialog
      :show="isShowCustomerDialog"
      :customer.sync="customerModify"
      :customerGroupOptions="customerGroupOptions.slice(1)"
      @onChange="setStateCustomerDialog"
    />
    <ConfirmDialog
      :show="isShowConfirmDialog"
      :message="msgConfirmDialog"
      @onChange="setStateConfirmDialog"
      @onOk="delCustomer"
      @onCancel="cancelDelCustomer"
    />
    <AlertDialog
      :show="isShowAlertDialog"
      :message="msgAlertDialog"
      @onChange="setStateAlertDialog"
    />
  </div>
</template>

<script>
import Button from "../../components/Button.vue";
import Input from "../../components/Input.vue";
import Combobox from "../../components/Combobox.vue";
import Table from "../../components/Table.vue";
import Pagination from "../../components/Pagination.vue";
import ConfirmDialog from "../../components/ConfirmDialog.vue";
import CustomerDialog from "./CustomerDialog.vue";
import AlertDialog from "../../components/AlertDialog.vue";
import CheckBox from "../../components/CheckBox.vue";

import StateEnum from "../../store/StateEnum.js";

import axios from "../../helpers/axios";
import dayjs from "dayjs";

export default {
  components: {
    Button,
    Input,
    Combobox,
    Table,
    Pagination,
    ConfirmDialog,
    CustomerDialog,
    AlertDialog,
    CheckBox,
  },
  data() {
    return {
      /**
       * Trạng thái trang web.
       * CreatedBy: dbhuan (29/04/2021)
       */
      state: StateEnum.LOADING,

      /**
       * Tổng số trang.
       * CreatedBy: dbhuan (29/04/2021)
       */
      totalPages: 0,

      /**
       * Tổng số khách hàng.
       * CreatedBy: dbhuan (29/04/2021)
       */
      totalRecord: 0,

      /**
       * trang hiện tại.
       * CreatedBy: dbhuan (29/04/2021)
       */
      page: 1,

      /**
       * Số khách hàng trên một trang.
       * CreatedBy: dbhuan (29/04/2021)
       */
      pageSize: 10,

      /**
       * Danh sách khách hàng.
       * CreatedBy: dbhuan (04/05/2021)
       */
      customers: [],

      /**
       * Danh sách nhóm khách hàng.
       * CreatedBy: dbhuan (29/04/2021)
       */
      customerGroupOptions: [],

      /**
       * Id nhóm khách hàng đang được chọn.
       * CreatedBy: dbhuan (29/04/2021)
       */
      selectedCustomerGroupId: "",

      /**
       * Bộ lọc danh sách khách hàng theo họ tên hoặc số điện thoại.
       * CreatedBy: dbhuan (29/04/2021)
       */
      customerFilter: "",

      /**
       * Danh sách khách hàng cần xóa.
       * CreatedBy: dbhuan (04/05/2021)
       */
      customersDel: [],

      /**
       * Biến xác định trạng thái checkbox all.
       * CreatedBy: dbhuan (04/05/2021)
       */
      isCheckedAll: false,

      /**
       * Khách hàng đang được thêm mới hoặc chính sửa.
       * CreatedBy: dbhuan (29/04/2021)
       */
      customerModify: null,

      /**
       * Biến xác định trạng thái dialog khách hàng.
       * CreatedBy: dbhuan (2/04/2021)
       */
      isShowCustomerDialog: false,

      /**
       * Biến xác định trạng thái dialog confirm
       * CreatedBy: dbhuan (29/04/2021)
       */
      isShowConfirmDialog: false,

      /**
       * Biến xác định trạng thái dialog alert.
       * CreatedBy: dbhuan (29/04/2021)
       */
      isShowAlertDialog: false,

      /**
       * Lời nhắn cần hiển thị dialog confirm.
       * CreatedBy: dbhuan (29/04/2021)
       */
      msgConfirmDialog: "",

      /**
       * Lời nhắn cần hiển thị dialog alert.
       * CreatedBy: dbhuan (29/04/2021)
       */
      msgAlertDialog: "",

      /**
       * Biến timeout.
       * CreatedBy: dbhuan (29/04/2021)
       */
      timeOut: null,
    };
  },
  computed: {
    /**
     * Trạng thái trang web đang loading.
     * CreatedBy: dbhuan (29/04/2021)
     */
    isLoading: function () {
      return this.state == StateEnum.LOADING;
    },

    /**
     * Trạng thái trang web đã load thành công.
     * CreatedBy: dbhuan (29/04/2021)
     */
    isSuccess: function () {
      return this.state == StateEnum.SUCCESS;
    },

    /**
     * Thứ tự khách hàng bắt đầu.
     * CreatedBy: dbhuan (29/04/2021)
     */
    startRecord: function () {
      return (this.page - 1) * this.pageSize + 1;
    },

    /**
     * Thứ tự khách hàng kết thúc.
     * CreatedBy: dbhuan (29/04/2021)
     */
    endRecord: function () {
      return this.startRecord + this.pageSize - 1;
    },
  },

  mounted() {
    this.initialData();
    this.fetchCustomers();
    this.fetchCustomerGroups();
  },
  methods: {
    /**
     * Khởi tạo data.
     * CreatedBy: dbhuan (29/04/2021)
     */
    initialData() {
      if (this.$route.query && this.$route.query.page) {
        this.page = parseInt(this.$route.query.page);
      }
    },

    /**
     * Hàm refresh dữ liệu.
     * CreatedBy: dbhuan (29/04/2021)
     */
    refreshData() {
      this.selectedCustomerGroupId = "";
      this.customersDel = [];
      this.isCheckedAll = false;
      this.$router.push({ query: { page: 1 } }).catch(() => {});
    },

    /**
     * Hàm lấy danh sách khách hàng từ api.
     * CreatedBy: dbhuan (29/04/2021)
     */
    fetchCustomers() {
      // set trạng thái trang web là loading.
      this.state = StateEnum.LOADING;

      // call api get dữ liệu danh sách khách hàng.
      axios
        .get(
          `/api/v1/customers?page=${this.page}&pageSize=${this.pageSize}&filter=${this.customerFilter}&customerGroupId=${this.selectedCustomerGroupId}`
        )
        .then((res) => res.data)
        .then((data) => {
          // thành công lấy dữ liệu.

          // set dữ liệu vào data.
          this.totalPages = data.totalPages || 0;
          this.totalRecord = data.totalRecord || 0;
          this.customers = data.data || [];

          // set trang thái trang web là success.
          this.state = StateEnum.SUCCESS;
        })
        .catch(() => {
          // có lỗi xảy ra.

          // set trạng thái trang web là error.
          this.state = StateEnum.ERROR;
        });
    },

    /**
     * Hàm lấy danh sách nhóm khách hàng.
     * CreatedBy: dbhuan (29/04/2021)
     */
    fetchCustomerGroups() {
      // call api lấy danh sách khách hàng.
      axios
        .get("/api/v1/customer-groups")
        .then((res) => res.data)
        .then((data) => {
          // thành công.
          let options = [{ value: "", text: "Nhóm khách hàng" }];
          for (let cg of data) {
            options.push({
              value: cg.customerGroupId,
              text: cg.customerGroupName,
            });
          }
          this.customerGroupOptions = options;
        })
        .catch(() => {
          this.state = StateEnum.ERROR;
        });
    },

    /**
     * Hàm lọc danh sách khách hàng theo giá trị input nhập vào.
     */
    onChangeInputCustomerFilter() {
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        this.fetchCustomers();
      }, 1000);
    },

    /**
     * Hàm set trạng thái dialog khách hàng.
     * CreatedBy: dbhuan (29/04/2021)
     */
    setStateCustomerDialog(state) {
      this.isShowCustomerDialog = state;
    },

    /**
     * Hàm set trạng thái dialog confirm.
     * CreatedBy: dbhuan (29/04/2021)
     */
    setStateConfirmDialog(state) {
      this.isShowConfirmDialog = state;
    },

    /**
     * Hàm set trạng thái dialog alert.
     * CreatedBy: dbhuan (29/04/2021)
     */
    setStateAlertDialog(state) {
      this.isShowAlertDialog = state;
    },

    /**
     * Hàm click button thêm khách hàng.
     * CreatedBy: dbhuan (29/04/2021)
     */
    onClickBtnAddCustomer() {
      this.customerModify = null;
      this.setStateCustomerDialog(true);
    },

    /**
     * Hàm được gọi khi combobox nhóm khách hàng thay đổi.
     * CreatedBy: dbhuan (29/04/2021)
     */
    onChangeComboboxCustomerGroup() {
      this.fetchCustomers();
    },

    /**
     * Hàm chọn tất cả khách hàng trên table.
     * CreatedBy: dbhuan (04/05/2021)
     */
    onSelectAllCustomers(e) {
      let isChecked = e.target.checked;
      if (!isChecked) {
        this.isCheckedAll = false;
        this.customersDel = [];
      } else {
        this.isCheckedAll = true;
        this.customersDel = this.customers.map((item) => item.customerId);
      }
    },

    /**
     * Hàm dblclick vào tr table.
     * CreatedBy: dbhuan (29/04/2021)
     */
    onDblClickTrCustomer(customerId) {
      // call api lấy thông tin khách hàng.
      axios
        .get(`/api/v1/customers/${customerId}`)
        .then((res) => res.data)
        .then((data) => {
          this.customerModify = data;
          this.setStateCustomerDialog(true);
        })
        .catch();
    },

    /**
     * Hàm click button xóa khách hàng.
     * CreatedBy: dbhuan (29/04/2021)
     */
    onClickDeleteCustomer() {
      // Hiển thị dialog confirm xác nhận xóa.
      this.msgConfirmDialog = `Bạn có chắc muốn xóa khách hàng đã chọn khỏi hệ thống không ?`;
      this.setStateConfirmDialog(true);
    },

    /**
     * Hàm xóa khách hàng.
     * CreatedBy: dbhuan (29/04/2021)
     */
    delCustomer() {
      // Ẩn dialog confirm và set lời nhắn confirm là "".
      this.setStateConfirmDialog(false);
      this.msgConfirmDialog = "";

      axios({
        method: "DELETE",
        url: "/api/v1/customers",
        data: {
          ids: this.customersDel,
        },
      })
        .then((res) => res.data)
        .then(() => {
          // thành công thì hiển thị thông báo thành công trên dialog alert.
          this.fetchCustomers();
          this.msgAlertDialog = "Xóa thành công.";
          this.setStateAlertDialog(true);
        })
        .catch(() => {
          // thất bại thì hiển thị thông báo thất bại trên dialog alert.
          this.msgAlertDialog = "Xóa thất bại.";
          this.setStateAlertDialog(true);
        })
        // thực hiện reload lại dữ liệu sau khi call api.
        .finally(() => this.cancelDelCustomer());
    },

    /**
     * Hàm hủy xóa khách hàng.
     * CreatedBy: dbhuan (29/04/2021)
     */
    cancelDelCustomer() {
      this.setStateConfirmDialog(false);
      this.msgConfirmDialog = "";
      this.customersDel = [];
    },
  },

  watch: {
    "$route.query.page": function () {
      // Thực hiện load lại dữ liệu khi có sự thay đổi.
      this.initialData();
      this.fetchCustomers();
    },
  },
  filters: {
    /**
     * Hàm format date về dạng DD/MM/YYYY.
     * CreatedBy: dbhuan (29/04/2021)
     */
    formatDate: function (dateStr) {
      if (!dateStr) {
        return "Không rõ";
      }
      return dayjs(dateStr).format("DD/MM/YYYY");
    },
  },
};
</script>

<style>
.content {
  position: absolute;
  top: 56px;
  left: 226px;
  right: 0;
  bottom: 0;
  padding-left: 16px;
  padding-right: 16px;
  display: flex;
  flex-direction: column;
}

.content .title-box {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.content .title-box .title {
  font-size: 20px;
  font-weight: bold;
}

.content .toolbar-box {
  display: flex;
  align-items: center;
  justify-content: space-between;
}

.content .toolbar-left {
  display: flex;
  flex-direction: row;
}

.content .toolbar-left .input-box {
  display: inline-block;
  position: relative;
}

.content .toolbar-left .input-box .icon-left {
  position: absolute;
  left: 16px;
  top: 13px;
  color: #bbbbbb;
}

.content .toolbar-left .input-box .has-icon {
  padding-left: 40px;
}

.content .scroll {
  flex: 1;
  margin-top: 16px;
  margin-bottom: 16px;
  overflow: auto;
}

.content .footer {
  height: 56px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  flex-wrap: wrap;
  border-top: 5px solid #ccc;
}

.loading {
  display: flex;
  align-items: center;
  justify-content: center;
}

/* Loader */
.loader {
  border: 2px solid #f3f3f3; /* Light grey */
  border-top: 2px solid #019160; /* Blue */
  border-radius: 50%;
  width: 30px;
  height: 30px;
  animation: spin 2s linear infinite;
}

.error {
  text-align: center;
  font-size: 15px;
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>