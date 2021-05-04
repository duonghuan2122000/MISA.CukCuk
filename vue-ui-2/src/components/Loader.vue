<template>
  <div class="loader"></div>
</template>

<style scoped>
/* Loader */
.loader {
  border: 2px solid #f3f3f3; /* Light grey */
  border-top: 2px solid #019160; /* Blue */
  border-radius: 50%;
  width: 30px;
  height: 30px;
  animation: spin 2s linear infinite;
}

.error {
  text-align: center;
  font-size: 15px;
}

@keyframes spin {
  0% {
    transform: rotate(0deg);
  }
  100% {
    transform: rotate(360deg);
  }
}
</style>