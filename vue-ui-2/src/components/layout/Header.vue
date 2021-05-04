<template>
  <div class="header">
    <div class="logo-box">
      <div class="toggle fas fa-bars" @click="$emit('toggleNavbar')"></div>
      <div class="logo"></div>
    </div>

    <div class="header-toolbar-box">
      <Dropdown
        class="header-toolbar-left"
        :content="['Nhà hàng biển đông', 'Nhà hàng biển đông chi nhánh Hà Nội']"
      >
        <div style="margin-left: 16px">Nhà hàng biển đông</div>
        <div class="fas fa-chevron-down" style="margin-left: 10px"></div>
      </Dropdown>

      <div class="header-toolbar-right">
        <div class="fas fa-bell" style="margin-right: 16px; font-size: 16px"></div>
        <div>Dương Bằng Huân</div>
        <Dropdown
          style="margin-left: 16px"
          :content="['Thông tin cá nhân', 'Đăng xuất']"
          dropdownPosition="right"
        >
          <i style="font-size: 16px" class="fas fa-ellipsis-h"></i>
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
};
</script>

<style>
.header {
  height: 48px;
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
}

.header .logo-box .toggle {
  height: 24px;
  width: 24px;
  margin-left: 16px;
  font-size: 24px;
  cursor: pointer;
  flex-shrink: 0;
}

.header .logo-box .logo {
  margin-left: 10px;
  background-image: url('../../assets/img/cukcuk-logo.png');
  background-position-y: center;
  background-size: contain;
  background-repeat: no-repeat;
  height: 30px;
  width: 200px;
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