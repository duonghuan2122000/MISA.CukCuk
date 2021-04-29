<template>
  <select
    :value="value"
    @input="$emit('input', $event.target.value)"
    class="input"
  >
    <option v-for="(opt, i) in options" :key="i" :value="opt.value">
      {{ opt.text }}
    </option>
  </select>
</template>

<script>
export default {
  props: {
    /**
     * Danh sách các lựa chọn trong thẻ option.
     * CreatedBy: dbhuan (27/04/2021)
     */
    options: Array,

    /**
     * Biến dùng để sử dụng cho 2 way binding. v-model.
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