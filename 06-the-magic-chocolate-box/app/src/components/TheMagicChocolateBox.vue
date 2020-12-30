<template>
  <div class="">
    <b-jumbotron
      class="header"
      header="The Magic Chocolate Box"
      lead="In some families, around the holiday season, there is always a big box of chocolates on the table, and there's always a little cousin who grabs your favorite one! This year, we can't be together in person but maybe we can prepare little individual boxes for each person. All we have to do is choose the right chocolate for the right person!"
    >
      <!-- <img alt="Tasty ladoos" src="../assets/ladoo.png" /> -->
      <br />
      <br />
      <p>
        Have you made the perfect Ladoo? Have a URL to a photo of your Ladoos?
        Enter it below and watch as Azure Functions and Machine Learning work
        their magic to predict if you have made the perfect ladoo.
      </p>
      <p>
        Brought to you by
        <a
          href="https://twitter.com/search?q=%23SeasonsOfServerless"
          target="_blank"
          >#SeasonsOfServerless</a
        >, with a seasoning of
        <a
          href="https://azure.microsoft.com/en-gb/services/functions"
          target="_blank"
          >Azure Functions</a
        >, <a href="https://vuejs.org/" target="_blank">Vue.js</a>,
        <a
          href="https://azure.microsoft.com/en-gb/services/app-service/static/"
          target="_blank"
          >Static Web Apps</a
        >
        and
        <a
          href="https://docs.microsoft.com/en-us/azure/azure-functions/durable/durable-functions-entities"
          target="_blank"
          >Durable Entities</a
        >
      </p>
      <p>
        View the code on
        <a
          href="https://github.com/stuartleaver/seasons-of-serverless/tree/main/06-the-magic-chocolate-box"
          target="_blank"
          >GitHub</a
        >
      </p>
    </b-jumbotron>
    <FamilyDetails v-if="!family" @familyLoaded="onFamilyLoaded" />
    <ChocolateBoxDetails v-if="family" v-bind:family="family" />
  </div>
</template>

<script>
import ChocolateBoxDetails from "./ChocolateBoxDetails.vue";
import FamilyDetails from "./FamilyDetails.vue";

export default {
  components: { FamilyDetails, ChocolateBoxDetails },
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
    onFamilyLoaded(value) {
      this.family = value;
      console.log(this.family);
    },
  },
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
