<template>
  <input
    type="radio"
    :name="name"
    :value="value.toString()"
    :checked="checkValue == value"
    @change="$emit('input', parseInt($event.target.value))"
  />
</template>

<script>
export default {
  model: {
    prop: "checkValue",
  },
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
    value: Number,

    /**
     * Giá trị hiện tại của radio.
     * CreatedBy: dbhuan (29/04/2021)
     */
    checkValue: {
      type: Number,
      default: null,
    },
  },
};
</script>

