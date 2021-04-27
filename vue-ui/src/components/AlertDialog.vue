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
import { toRefs, watchEffect } from "vue";
export default {
  props: {
    show: Boolean,
    message: String,
  },
  emits: ["onChange"],
  setup(props, { emit }) {
    const { show } = toRefs(props);

    watchEffect(() => {
      if (show.value == true) {
        setTimeout(() => {
          emit("onChange", false);
        }, 5000);
      }
    });
  },
};
</script>

<style scoped>
.dialog {
  position: fixed;
  bottom: 0;
  left: 0;
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
  width: 300px;
  background-color: #fff;
  position: relative;
  display: flex;
  flex-direction: column;
  overflow: hidden;
  border: 1px solid #ccc;
  margin-bottom: 20px;
  margin-left: 20px;
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