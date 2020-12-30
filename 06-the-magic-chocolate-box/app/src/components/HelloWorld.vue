<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <family-load v-if="!family" @familyLoaded="onFamilyLoaded" />
    <chocolate-box-details v-if="family" v-bind:family="family" />
  </div>
</template>

<script>
import ChocolateBoxDetails from './ChocolateBoxDetails.vue';
import FamilyLoad from './FamilyLoad.vue'

export default {
  components: { FamilyLoad, ChocolateBoxDetails },
  name: "HelloWorld",
  data: function () {
    return {
      family: null,
    };
  },
  props: {
    msg: String,
  },
  methods: {
    onFamilyLoaded (value) {
      this.family = value
      console.log(this.family)
    }
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
