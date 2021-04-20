<template>
  <select class="input">
    <option v-for="(opt, i) in options" :key="i" :value="opt.value">{{opt.text}}</option>
  </select>
</template>

<script>
export default {
  props: {
    options: {
      type: Array
    }
  },
  setup() {
    
  },
}
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