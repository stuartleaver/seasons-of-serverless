<template>
  <div id="app">
    <TheMagicChocolateBox msg="The Magic Chocolate Box"/>
  </div>
</template>

<script>
import TheMagicChocolateBox from './components/TheMagicChocolateBox.vue'


export default {
  name: 'App',
  components: {
    TheMagicChocolateBox
  }
}
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
