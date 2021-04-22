<template>
  <div class="header">
    <div class="logo-box">
      <div class="toggle fas fa-bars" @click="toggleNavbar"></div>
      <div class="logo">Misa CukCuk</div>
    </div>

    <div class="header-toolbar-box">
      <Dropdown
        class="header-toolbar-left"
        :content="['Nhà hàng biển đông', 'Nhà hàng biển đông chi nhánh Hà Nội']"
      >
        <div style="margin-left: 16px">Nhà hàng biển đông</div>
        <div class="fas fa-chevron-down" style="margin-left: 8px"></div>
      </Dropdown>

      <div class="header-toolbar-right">
        <div class="fas fa-bell" style="margin-right: 8px"></div>
        <div>Dương Bằng Huân</div>
        <Dropdown style="margin-left:8px" :content="['Thông tin cá nhân', 'Đăng xuất']" dropdownPosition="right">
          <i class="fas fa-ellipsis-h"></i>
        </Dropdown>
      </div>
    </div>
  </div>
</template>

<script>
import Dropdown from "../Dropdown.vue";
export default {
  components: {
    Dropdown,
  },
  setup(_, {emit}){
    const toggleNavbar = () => {
      emit('toggleNavbar');
    }

    return {toggleNavbar}
  }
};
</script>

<style>
.header {
  height: 56px;
  display: flex;
  align-items: center;
  border-bottom: 1px solid #e5e5e5;
}

/* Logo box */
.header .logo-box {
  height: 100%;
  width: 226px;
  display: flex;
  align-items: center;
  border-right: 1px solid #e5e5e5;
}

.header .logo-box .toggle {
  margin-left: 16px;
  font-size: 24px;
  cursor: pointer;
}

.header .logo-box .logo {
  margin-left: 16px;
  font-size: 24px;
}

/* Header toolbar box */
.header .header-toolbar-box {
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: space-between;
  height: 100%;
}

/* .header .header-toolbar-left {
    
} */

.header .header-toolbar-right {
  height: 100%;
  display: flex;
  align-items: center;
  padding-right: 16px;
}
</style>