<template>
  <div v-if="totalPages > 1" class="pagination">
    <router-link
      v-if="page > 1"
      class="pagination-item first-page"
      to="?page=1"
    />
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
    <router-link
      v-if="page < totalPages"
      class="pagination-item last-page"
      :to="'?page=' + totalPages"
    />
  </div>
</template>

<script>
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
  computed: {
    /**
     * Xác định đoạn trang cần hiển thị.
     * CreatedBy: dbhuan (29/04/2021)
     */
    pages: function () {
      // 1 2 3
      let pages = [];
      let start = this.page >= 3 ? this.page - 1 : 1;
      let end =
        this.page <= this.totalPages - 2 ? this.page + 1 : this.totalPages;
      for (let i = start; i <= end; i++) pages.push(i);
      return pages;
    },
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
  height: 30px;
  line-height: 30px;
  width: 30px;
  border-radius: 50%;
  border: 1px solid #ccc;
  text-align: center;
  margin-left: 4px;
  margin-right: 4px;
  cursor: pointer;
  text-decoration: none;
}

.pagination .first-page {
  background-image: url("../assets/icon/btn-firstpage.svg");
  background-position: center;
  background-repeat: no-repeat;
  background-size: 20px 20px;
}

.pagination .last-page {
  background-image: url("../assets/icon/btn-lastpage.svg");
  background-position: center;
  background-repeat: no-repeat;
  background-size: 20px 20px;
}

.pagination .pagination-item.active {
  background-color: #019160;
  color: #fff;
}

.pagination .pagination-item:not(.active):hover {
  background-color: #e5e5e5;
}
</style>