<template>
  <select
    :value="modelValue"
    @input="$emit('update:modelValue', $event.target.value)"
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
    options: Array,

    modelValue: String,
  },
  emits: ["update:modelValue"],
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