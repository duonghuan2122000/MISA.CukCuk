<template>
  <div @click="$emit('click')">
    <button v-if="!onlyIcon" class="button" :class="color ? 'is-' + color : ''">
      <span v-if="iconLeft" class="icon fas" :class="'fa-' + iconLeft"></span>
      <span>{{ text }}</span>
    </button>

    <button v-else class="button" :style="styleBtn">
      <span :style="styleIcon" class="fas" :class="'fa-' + icon"></span>
    </button>
  </div>
</template>

<script>
export default {
  props: {
    /**
     * Xác định loại button. Chỉ có icon hoặc có chữ.
     */
    onlyIcon: {
      type: Boolean,
      default: false,
    },

    /**
     * style cho button.
     */
    styleBtn: {
      type: String,
      default: "",
    },

    /**
     * Style cho icon của button icon.
     */
    styleIcon: {
      type: String,
      default: "",
    },

    /**
     * Button text.
     */

    /**
     * Text của button.
     * Mặc định là "".
     */
    text: {
      type: String,
      default: "",
    },

    /**
     * Icon left của button. Lấy giá trị theo fontawesome5.
     * Mặc định là "".
     */
    iconLeft: {
      type: String,
      default: "",
    },

    /**
     * Màu nền của button.
     * Hỗ trợ primary, secondary và null.
     * Mặc định là primary
     */
    color: {
      type: String,
      default: "primary",
    },

    /**
     * Button icon
     */
    icon: {
      type: String,
      default: "",
    },
  },
  emits: ["click"],
};
</script>

<style scoped>
div {
  display: inline-block;
}
.button {
  display: inline-block;
  height: 40px;
  outline: none;
  background-color: transparent;
  border: 1px solid #ccc;
  padding-left: 16px;
  padding-right: 16px;
  cursor: pointer;
  border-radius: 4px;
}

/* Button icon */
.button .icon {
  margin-right: 8px;
}

/* Button color primary */
.button.is-primary {
  background-color: green;
  color: #fff;
}

.button.is-primary span {
  color: #fff;
}

/* Button hover */
.button.is-primary:hover {
  background-color: rgb(0, 100, 0);
}
</style>