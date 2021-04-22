<template>
  <input :type="inputType" class="input" :placeholder="placeholder" />
</template>

<script>
export default {
  props: {
    /**
     * Text hint của input.
     * Mặc đinh là "".
     */
    placeholder: {
      type: String,
      default: "",
    },

    /**
     * Type của input.
     * Mặc định: text
     */
    inputType: {
      type: String,
      default: "text",
    },
  },
};
</script>

<style scoped>
.input {
  display: inline-block;
  height: 40px;
  padding-left: 16px;
  border-radius: 4px;
  border: 1px solid #e5e5e5;
  outline: none;
  width: 100%;
}

.input:focus {
  border-color: green;
}
</style>