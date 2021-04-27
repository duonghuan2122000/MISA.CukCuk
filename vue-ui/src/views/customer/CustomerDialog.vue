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
        <div class="row">
          <div class="col-3"></div>
          <div class="col-9">
            <div class="row">
              <div class="col-6">
                <div class="field">
                  <label>Mã khách hàng (<span style="color: red">*</span>)</label>
                  <Input />
                </div>
              </div>
              <div class="col-6">
                <div class="field">
                  <label>Họ và tên (<span style="color: red">*</span>)</label>
                  <Input />
                </div>
              </div>
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
export default {
  components: {
    Button,
    Input
  },
  props: {
    show: {
      type: Boolean,
    },
  },
  emits: ['onChange'],
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