<template>
  <input
    type="radio"
    :name="name"
    :value="value"
    @change="$emit('update:modelValue', $event.target.value)"
  />
</template>

<script>
export default {
  props: {
    name: String,
    value: String,
  },
  emits: ["update:modelValue"],
};
</script>

