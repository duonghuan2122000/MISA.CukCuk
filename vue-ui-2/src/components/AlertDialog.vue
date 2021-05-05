<template>
  <div class="dialog" :class="{ hide: !show }">
    <div class="dialog-content">
      <div class="dialog-body">
        <div class="dialog-body-title">
          <span :style="'background-color:' + colorAlert" class="icon-alert">
            <span class="fas fa-check" style="color: #fff"></span>
          </span>
          {{ message }}
        </div>
        <div class="dialog-close-button" @click="$emit('onChange', false)">
          <i
            class="fas fa-times"
            style="font-size: 20px; line-height: 48px"
            :style="'color:' + colorAlert"
          ></i>
        </div>
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

    /**
     * Màu của dialog alert. danger, warning, success và info
     * CreatedBy: dbhuan (05/05/2021)
     */
    color: {
      type: String,
      default: "success",
    },
  },
  computed: {
    /**
     * Compute colorAlert.
     * CreatedBy: dbhuan (05/05/2021)
     */
    colorAlert: function () {
      switch (this.color) {
        case "danger":
          return "#F65454";
        case "warning":
          return "#FF6900";
        case "success":
          return "#019160";
        default:
          return "#0075FF";
      }
    },
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
  box-shadow: 0 3px 6px rgba(0, 0, 0, 0.16);
  border-radius: 4px;
}

.dialog .dialog-body {
  padding-left: 24px;
  padding-right: 24px;
  height: 48px;
  line-height: 48px;
}

.dialog .dialog-body-title {
  font-size: 15px;
  font-weight: bold;
}

.dialog .dialog-body-title .icon-alert {
  display: inline-block;
  height: 24px;
  width: 24px;
  line-height: 24px;
  border-radius: 50%;
  text-align: center;
}

.dialog .dialog-close-button {
  position: absolute;
  right: 0;
  top: 0;
  width: 48px;
  text-align: center;
  cursor: pointer;
  border-bottom-left-radius: 4px;
}

.dialog .dialog-close-button:hover {
  background-color: #e5e5e5;
}

.dialog.hide {
  display: none;
}
</style>