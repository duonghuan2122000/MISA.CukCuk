<template>
  <div v-if="totalPages > 1" class="pagination">
    <div class="pagination-item prev-page">
      <i class="fas fa-chevron-left"></i>
    </div>

    <div
      v-for="p in pages"
      :key="p"
      class="pagination-item"
      :class="{ active: p == page }"
    >
      {{ p }}
    </div>
    <div class="pagination-item next-page">
      <i class="fas fa-chevron-right"></i>
    </div>
  </div>
</template>

<script>
import { toRefs, computed } from "vue";
export default {
  props: {
    /**
     * trang hiện tại.
     * Mặc định là 1.
     */
    page: {
      type: Number,
      default: 1,
    },

    /**
     * Tổng số trang.
     * Mặc định là 0.
     */
    totalPages: {
      type: Number,
      default: 0,
    },
  },
  setup(props) {
    const { page, totalPages } = toRefs(props);
    const pages = computed(() => {
      let pages = [];
      let start = page.value >= 3 ? page.value - 1 : 1;
      let end =
        page.value <= totalPages.value - 2 ? page.value + 1 : totalPages.value;
      for (let i = start; i <= end; i++) pages.push(i);
      return pages;
    });

    return { pages };
  },
};
</script>

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