<template>
  <div class="dropdown">
    <div class="dropdown-toggle" @click="toggleDropdown">
      <slot />
    </div>

    <div class="dropdown-content" :class="[{ show }, dropdownPosition]">
      <div v-for="(c, index) in content" :key="index" class="dropdown-item">
        {{ c }}
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from "@vue/composition-api";
export default {
  props: {
    /**
     * Nội dung của dropdown content.
     * Kiểu dữ liệu là một mảng string. ['Dropdown item 1', 'Dropdown item 2']
     */
    content: {
      type: Array,
    },

    /**
     * Vị trí hiển thị dropdown content. 
     * Hỗ trợ: left, right
     * Mặc định là left.
     */
    dropdownPosition: {
        type: String,
        default: "left"
    }
  },
  setup() {
    const show = ref(false);

    const toggleDropdown = () => {
      show.value = !show.value;
    };

    return { show, toggleDropdown };
  },
};
</script>

<style scoped>
.dropdown {
  position: relative;
  height: 100%;
}

.dropdown .dropdown-toggle {
  display: flex;
  align-items: center;
  height: 100%;
  cursor: pointer;
}

.dropdown .dropdown-content {
  display: none;
  position: absolute;
  z-index: 40;
  /* left: 0; */
  background-color: #fff;
  white-space: nowrap;
  border: 1px solid #ccc;
}

.dropdown .dropdown-content .dropdown-item {
  height: 40px;
  line-height: 40px;
  min-width: 100%;
  padding-left: 16px;
  padding-right: 16px;
  cursor: pointer;
}

.dropdown .dropdown-content .dropdown-item:hover {
  background-color: #e5e5e5;
}

/* Show dropdown right position */
.dropdown .dropdown-content.right {
  right: 0;
}

/* Show dropdown content */
.dropdown .dropdown-content.show {
  display: block;
}
</style>