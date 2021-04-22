<template>
  <input
    type="text"
    class="input"
    placeholder="Tìm kiếm nhân viên theo Mã, họ tên hoặc SĐT"
  />
</template>

<style scoped>
.input {
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