<template>
  <input type="checkbox" :checked="checkValues.includes(value)" :value="value" @change="onChange" />
</template>

<script>
export default {
  model: {
    prop: "checkValues",
  },
  props: {
    /**
     * Giá trị của radio.
     * CreatedBy: dbhuan (27/04/2021)
     */
    value: {
      type: String,
      default: null,
    },

    /**
     * Giá trị hiện tại của radio.
     * CreatedBy: dbhuan (29/04/2021)
     */
    checkValues: {
      type: Array,
      default: null,
    },
  },
  methods: {
    onChange(e) {
      let value = e.target.value;
      let isChecked = e.target.checked;
      if (!isChecked) {
        let newCheckValues = this.checkValues.filter((item) => item !== value);
        this.$emit("input", newCheckValues);
      } else {
        this.$emit("input", [...this.checkValues, value]);
      }
    },
  },
};
</script>

