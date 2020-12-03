<template>
  <div class="">
    <b-jumbotron
      class="ladooHeader"
      header="Lovely Ladoo's"
      lead="It's Diwali season in India! Diwali is a festival that celebrates the
      victory of light over darkness. Families celebrate with fireworks and
      light up every house in the country with diyas, a type of Indian candles.
      A very popular delicacy that Indians eat during Diwali is ladoos."
    >
      <img alt="Tasty ladoos" src="../assets/ladoo.png" />
      <br />
      <br />
      <p>
        Have you made the perfect Ladoo? Have a URL to a photo of your Ladoo's?
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
        and the machine learning elves of
        <a
          href="https://azure.microsoft.com/en-us/services/cognitive-services/custom-vision-service/"
          target="_blank"
          >Azure Custom Vision</a
        >
      </p>
      <p>
        View the code on
        <a
          href="https://github.com/stuartleaver/seasons-of-serverless/tree/main/02-lovely-ladoos"
          target="_blank"
          >GitHub</a
        >
      </p>
      <input
        class="col-6"
        v-model="imageLink"
        placeholder="Enter image url..."
      />
      <br />
      <div v-if="showLoading">
        <strong class="m-5">Loading...</strong>
        <br />
        <b-spinner></b-spinner>
      </div>
    </b-jumbotron>
    <div class="container">
      <div class="row">
        <div class="col"></div>
        <b-alert
          class="col-6 center-block"
          show
          variant="danger"
          v-if="showError"
        >
          Opps, something went wrong - {{ errorMessage }}
        </b-alert>
        <div class="col"></div>
      </div>
    </div>
    <div class="container">
      <div class="row">
        <div class="col"></div>
        <div class="col-12" v-if="showResults">
          <b-card
            :img-src="imageLink"
            img-alt="Card image"
            img-left
            class="mb-3"
            title="Ladoo prediction results"
            sub-title="Here are your predictions on if your image is of ladoo's"
          >
            <b-card-text>
              <div
                class="columns medium-4"
                v-for="result in data"
                :key="result"
              >
                <p>
                  {{ result.tagName | titleize }} -
                  {{ parseFloat(result.probability) | percentage }}
                </p>
              </div>
            </b-card-text>
          </b-card>
        </div>
        <div class="col"></div>
      </div>
    </div>
  </div>
</template>

<script>
const axios = require("axios");

export default {
  name: "LadooPredictor",
  data() {
    return {
      data: {},
    };
  },
  props: {
    imageLink: String,
    showLoading: Boolean,
    showError: Boolean,
    errorMessage: String,
    showResults: String,
  },
  methods: {
    onChange() {
      this.showLoading = true;
      this.showError = false;
      this.showResults = false;

      axios
        .get(
          `http://localhost:7071/api/ladoopredictor?imageUrl=${this.imageLink}`
        )
        .then((response) => {
          this.data = response.data;

          this.showResults = true;
          this.showLoading = false;
        })
        .catch((error) => {
          this.showLoading = false;
          this.showError = true;

          if (error.response) {
            // Request made and server responded
            console.log(error.response.data);
            console.log(error.response.status);
            console.log(error.response.headers);

            this.errorMessage = error.response.data;
          } else if (error.request) {
            // The request was made but no response was received
            console.log(error.request);

            this.errorMessage = error.request;
          } else {
            // Something happened in setting up the request that triggered an Error
            console.log("Error", error.message);

            this.errorMessage = error.message;
          }
        });
    }
  },
  watch: {
    imageLink: function () {
      this.onChange();
    },
  },
  filters: {
    titleize(value) {
      return value.replace(/(?:^|\s|-)\S/g, (x) => x.toUpperCase());
    },
    percentage(value) {
      return parseFloat(value * 100).toFixed(2) + "%";
    },
  },
};
</script>

<style scoped>
.ladooHeader {
  background-color: #42b983;
}
.ladooHeader p {
  color: white;
}
.ladooHeader a {
  color: blue;
}
</style>