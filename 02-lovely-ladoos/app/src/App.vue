<template>
  <div id="app">
    <LadooPredictor class="text-center" />
  </div>
</template>

<script>
import LadooPredictor from "./components/LadooPredictor.vue";

export default {
  name: "App",
  components: {
    LadooPredictor,
  },
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
