<template>
  <div @click="$emit('click')">
    <button
      v-if="!onlyIcon"
      class="button"
      :class="[color ? 'is-' + color : '', { 'has-icon': iconLeft }]"
    >
      <span v-if="iconLeft" class="icon fas" :class="'fa-' + iconLeft"></span>
      <span>{{ text }}</span>
    </button>

    <button v-else class="button only-icon" :style="styleBtn">
      <span :style="styleIcon" class="fas" :class="'fa-' + icon"></span>
    </button>
  </div>
</template>

<script>
export default {
  props: {
    /**
     * Xác định loại button. Chỉ có icon hoặc có chữ.
     * CreatedBy: dbhuan (27/04/2021)
     */
    onlyIcon: {
      type: Boolean,
      default: false,
    },

    /**
     * style cho button.
     * CreatedBy: dbhuan (27/04/2021)
     */
    styleBtn: {
      type: String,
      default: "",
    },

    /**
     * Style cho icon của button icon.
     * CreatedBy: dbhuan (27/04/2021)
     */
    styleIcon: {
      type: String,
      default: "",
    },

    /**
     * Button text.
     * CreatedBy: dbhuan (27/04/2021)
     */

    /**
     * Text của button.
     * Mặc định là "".
     * CreatedBy: dbhuan (27/04/2021)
     */
    text: {
      type: String,
      default: "",
    },

    /**
     * Icon left của button. Lấy giá trị theo fontawesome5.
     * Mặc định là "".
     * CreatedBy: dbhuan (27/04/2021)
     */
    iconLeft: {
      type: String,
      default: "",
    },

    /**
     * Màu nền của button.
     * Hỗ trợ primary, secondary và null.
     * Mặc định là primary
     * CreatedBy: dbhuan (27/04/2021)
     */
    color: {
      type: String,
      default: "primary",
    },

    /**
     * Button icon
     * CreatedBy: dbhuan (27/04/2021)
     */
    icon: {
      type: String,
      default: "",
    },
  },
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
  padding-left: 24px;
  padding-right: 24px;
  cursor: pointer;
  border-radius: 4px;
}

.button.has-icon {
  padding-left: 16px;
  padding-right: 16px;
}

.button.has-icon .icon {
  margin-right: 8px;
}

/* Button color primary */
.button.is-primary {
  background-color: #019160;
  color: #fff;
}

.button.is-primary span {
  color: #fff;
}

/* Button hover */
.button.is-primary:hover {
  background-color: #2fbe8e;
}

/* Button only icon */
.button.only-icon {
  padding-left: 16px;
  padding-right: 16px;
}
</style>