<template>
  <input
    :type="inputType"
    class="input"
    :placeholder="placeholder"
    :value="value"
    @input="$emit('input', $event.target.value)"
  />
</template>

<script>
export default {
  props: {
    /**
     * Text hint của input.
     * Mặc đinh là "".
     * CreatedBy: dbhuan (27/04/2021)
     */
    placeholder: {
      type: String,
      default: "",
    },

    /**
     * Type của input.
     * Mặc định: text
     * CreatedBy: dbhuan (27/04/2021)
     */
    inputType: {
      type: String,
      default: "text",
    },

    /**
     * Biến dùng cho 2 way binding. v-model
     * CreatedBy: dbhuan (27/04/2021)
     */
    value: String,
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
  border-color: #019160;
}
</style>