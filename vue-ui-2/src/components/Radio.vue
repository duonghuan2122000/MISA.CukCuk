<template>
  <input
    type="radio"
    :name="name"
    :value="value"
    @change="$emit('input', $event.target.value)"
  />
</template>

<script>
export default {
  props: {
    /**
     * Tên của radio.
     * CreatedBy: dbhuan (27/04/2021)
     */
    name: String,

    /**
     * Giá trị của radio.
     * CreatedBy: dbhuan (27/04/2021)
     */
    value: String,
  },
};
</script>

