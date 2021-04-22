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
import { ref } from "vue";
export default {
  components: {
    HeaderSection,
    NavbarSection,
    ContentSection,
  },

  setup() {
    const isToggleNavbar = ref(false);
    const toggleNavbar = () => {
      isToggleNavbar.value = !isToggleNavbar.value;
    };
    return { isToggleNavbar, toggleNavbar };
  },
};
</script>

<style>
@import url("./assets/fontawesome5/css/fontawesome.min.css");
@import url("./assets/fontawesome5/css/solid.min.css");

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
  src: url("./assets/font/OpenSans-Regular.ttf");
}
</style>
