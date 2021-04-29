<template>
  <table class="table">
    <thead>
      <slot name="head" />
    </thead>
    <tbody>
      <slot name="body" />
    </tbody>
  </table>
</template>

<style>
.table {
  width: 100%;
  border-collapse: collapse;
}

.table tr {
  text-align: left;
  height: 40px;
  border-bottom: 1px solid #e5e5e5;
}

.table tbody tr:last-child {
  border: none;
}

.table tbody tr {
  cursor: pointer;
}

.table tbody tr:hover,
.table tbody tr.selected {
  background-color: #e5e5e5;
}
</style>