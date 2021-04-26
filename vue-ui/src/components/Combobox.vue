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
  height: 40px;
  padding-left: 16px;
  border-radius: 4px;
  border: 1px solid #e5e5e5;
  outline: none;
}

.input:focus {
  border-color: green;
}
</style>