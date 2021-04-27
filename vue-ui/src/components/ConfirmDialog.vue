<template>
  <div class="dialog" :class="{ hide: !show }">
    <div class="dialog-background" @click="$emit('onChange', false)"></div>
    <div class="dialog-content">
      <div class="dialog-header">
        <div class="dialog-header-title">XÁC NHẬN</div>
        <div class="dialog-close-button" @click="$emit('onChange', false)">
          <i class="fas fa-times"></i>
        </div>
      </div>
      <div class="dialog-body">
        {{ message }}
      </div>

      <div class="dialog-footer">
        <Button text="Ok" @click="$emit('onOk', false)" />
        <Button
          style="margin-left: 8px"
          text="Hủy"
          :color="null"
          @click="$emit('onCancel')"
        />
      </div>
    </div>
  </div>
</template>

<script>
import Button from "./Button.vue";
export default {
  components: {
    Button,
  },
  props: {
    /**
     * Biến xác định trạng thái dialog.
     * CreatedBy: dbhuan (27/04/2021)
     */
    show: Boolean,

    /**
     * Lời nhắn cần hiển thị trên dialog.
     * CreatedBy: dbhuan (27/04/2021)
     */
    message: String,
  },
  emits: ["onChange", "onOk", "onCancel"],
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
</style>