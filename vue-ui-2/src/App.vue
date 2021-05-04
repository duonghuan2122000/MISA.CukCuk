<template>
  <div>
    <HeaderSection @toggleNavbar="toggleNavbar" />
    <NavbarSection :toggle="isToggleNavbar" />
    <ContentSection />
  </div>
</template>

<script>
import HeaderSection from "./components/layout/Header";
import NavbarSection from "./components/layout/Navbar";
import ContentSection from "./components/layout/Content";
export default {
  components: {
    HeaderSection,
    NavbarSection,
    ContentSection,
  },
  data() {
    return {
      /**
       * Xác định trạng thái toggle của navbar.
       * CreatedBy: dbhuan (29/04/2021)
       */
      isToggleNavbar: false,
    };
  },
  methods: {
    /**
     * Hàm toggle navbar.
     * CreatedBy: dbhuan (29/04/2021)
     */
    toggleNavbar() {
      this.isToggleNavbar = !this.isToggleNavbar;
    },
  },
};
</script>

<style>
@import url("./assets/font/fontawesome-5.15.1/css/fontawesome.min.css");
@import url("./assets/font/fontawesome-5.15.1/css/solid.min.css");

* {
  box-sizing: border-box;
  font-family: Google-Sans;
  font-size: 13px;
  color: #000;
}

body {
  margin: 0;
  padding: 0;
}

a {
  text-decoration: none;
}

/* width */
::-webkit-scrollbar {
  width: 5px;
  height: 5px;
}

/* Track */
::-webkit-scrollbar-track {
  background: #f1f1f1;
}

/* Handle */
::-webkit-scrollbar-thumb {
  background: #888;
}

/* Handle on hover */
::-webkit-scrollbar-thumb:hover {
  background: #555;
}

@font-face {
  font-family: Google-Sans;
  src: url("./assets/font/GoogleSans-Regular.otf");
}
</style>
