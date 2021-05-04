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
  height: 48px;
  border-bottom: 1px solid #e5e5e5;
}

.table tbody tr:last-child {
  border: none;
}

.table tbody tr {
  cursor: pointer;
}

.table tbody tr:hover {
  background-color: #e5e5e5;
}

.table th,
.table td {
  padding-left: 10px;
}

.table th:first-child,
.table td:first-child {
  padding-left: 16px;
}

.table th:last-child,
.table td:last-child {
  padding-right: 16px;
}
</style>