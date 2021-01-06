<template>
  <div id="app">
    <b-jumbotron
      class="header"
      header="The Recipe Connector"
      lead="In Africa, city cooks want to reconnect with their country roots. In Kenya, there are many traditional dishes, but no one in Nairobi remembers how to make them! In Lagos, Nigeria, cooks are eager to recreate their grandmother's famous jollof rice, but they can't quite remember the proportions of ingredients. Let's set up a City/Country hotline!"
    >
      <img alt="rice" src="./assets/rice.png" />
      <br />
      <br />
      <p>
        Use the map below to add a pin for you location just by clicking. This
        will then bring up your regional center. From there, you can enter your
        email to get the perfect jollof rice recipe for the area.
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
          href="https://azure.microsoft.com/en-gb/services/azure-maps/"
          target="_blank"
          >Azure Maps</a
        >
      </p>
      <p>
        View the code on
        <a
          href="https://github.com/stuartleaver/seasons-of-serverless/tree/main/07-the-recipe-notifier"
          target="_blank"
          >GitHub</a
        >
      </p>
    </b-jumbotron>
    <RecipeConnector />
  </div>
</template>

<script>
import RecipeConnector from "./components/RecipeConnector.vue";

export default {
  name: "App",
  components: {
    RecipeConnector,
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

<style scoped>
.header {
  background-color: #00843e;
}
.header h1 {
  color: white;
}
.header p {
  color: white;
}
.header a {
  color: blue;
}
</style>
