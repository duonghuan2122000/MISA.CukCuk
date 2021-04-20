<template>
  <div class="pagination">
    <div class="pagination-item prev-page">
      <i class="fas fa-chevron-left"></i>
    </div>
    <div class="pagination-item active">1</div>
    <div class="pagination-item">2</div>
    <div class="pagination-item">3</div>
    <div class="pagination-item next-page">
      <i class="fas fa-chevron-right"></i>
    </div>
  </div>
</template>

<style scoped>
.pagination {
  display: flex;
  align-items: center;
}

.pagination .pagination-item {
  display: inline-block;
  height: 40px;
  line-height: 40px;
  width: 40px;
  border-radius: 50%;
  border: 1px solid #ccc;
  text-align: center;
  margin-left: 4px;
  margin-right: 4px;
  cursor: pointer;
}

.pagination .pagination-item.active {
  background-color: green;
  color: #fff;
}

.pagination .pagination-item:not(.active):hover {
  background-color: #e5e5e5;
}
</style>