<template>
  <div class="dialog" :class="{ hide: !show }">
    <div class="dialog-content">
      <div class="dialog-header">
        <div class="dialog-header-title">THÔNG BÁO</div>
        <div class="dialog-close-button" @click="$emit('onChange', false)">
          <i class="fas fa-times"></i>
        </div>
      </div>
      <div class="dialog-body">
        {{ message }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    /**
     * Biến xác định trạng thái dialog.
     * CreatedBy: dbhuan (27/04/2021)
     */
    show: Boolean,

    /**
     * Lời thông báo cần hiển thị.
     * CreatedBy: dbhuan (27/04/2021)
     */
    message: String,
  },
  watch: {
    show: function (newVal) {
      if (newVal == true) {
        setTimeout(() => {
          this.$emit("onChange", false);
        }, 5000);
      }
    },
  },
};
</script>

<style scoped>
.dialog {
  position: fixed;
  top: 0;
  left: calc(50% - 150px);
}

.dialog .dialog-content {
  z-index: 40;
  width: 300px;
  background-color: #fff;
  position: relative;
  display: flex;
  flex-direction: column;
  overflow: hidden;
  box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16);
  border-radius: 4px;
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
  padding-bottom: 16px;
}

.dialog.hide {
  display: none;
}
</style>