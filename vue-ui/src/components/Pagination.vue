<template>
  <div v-if="totalPages > 1" class="pagination">
    <router-link
      v-if="page > 1"
      class="pagination-item prev-page"
      :to="'?page=' + (page - 1)"
    >
      <i class="fas fa-chevron-left"></i>
    </router-link>

    <router-link
      v-for="p in pages"
      :key="p"
      class="pagination-item"
      :class="{ active: p == page }"
      :to="'?page=' + p"
    >
      {{ p }}
    </router-link>
    <router-link
      v-if="page < totalPages"
      class="pagination-item next-page"
      :to="'?page=' + (page + 1)"
    >
      <i class="fas fa-chevron-right"></i>
    </router-link>
  </div>
</template>

<script>
import { toRefs, computed } from "vue";
export default {
  props: {
    /**
     * trang hiện tại.
     * Mặc định là 1.
     * CreatedBy: dbhuan (27/04/2021)
     */
    page: {
      type: Number,
      default: 1,
    },

    /**
     * Tổng số trang.
     * Mặc định là 0.
     * CreatedBy: dbhuan (27/04/2021)
     */
    totalPages: {
      type: Number,
      default: 0,
    },
  },
  setup(props) {
    const { page, totalPages } = toRefs(props);

    /**
     * Xác định đoạn trang cần hiển thị.
     * CreatedBy: dbhuan (27/04/2021)
     */
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
  text-decoration: none;
}

.pagination .pagination-item.active {
  background-color: green;
  color: #fff;
}

.pagination .pagination-item:not(.active):hover {
  background-color: #e5e5e5;
}
</style>