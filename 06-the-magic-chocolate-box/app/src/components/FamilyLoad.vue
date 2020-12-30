<template>
  <div class="hello">
      <p v-if="errors.length">
    <b>Please correct the following error(s):</b>
    <ul>
      <li v-for="error in errors" v-bind:key="error">{{ error }}</li>
    </ul>
  </p>
    <div>
      <h1>Create Family</h1>
      <div class="field">
        <label class="label">Name</label>
        <input
          type="text"
          class="input"
          name="name"
          v-model="createFamilyName"
        />
      </div>

      <select
        name="chocolateBoxSize"
        id="chocolateBoxSize"
        v-model="chocolateBoxSize"
      >
        <option value="1">Small</option>
        <option value="2">Medium</option>
        <option value="3">Large</option>
      </select>
      <div class="field has-text-right">
        <button type="submit" class="button is-danger" @click="onCreateFamily">
          Create Family
        </button>
      </div>
    </div>
    <div>
      <h1>Load Family</h1>
      <div class="field">
        <label class="label">Name</label>
        <input type="text" class="input" name="name" v-model="loadFamilyName" />
      </div>
      <div class="field has-text-right">
        <button type="submit" class="button is-danger" @click="onLoadFamily">
          Load Family
        </button>
      </div>
    </div>
  </div>
</template>

<script>
const axios = require("axios");

export default {
  name: "FamilyLoad",
  data: function () {
    return {
      createFamilyName: "",
      loadFamilyName: "",
      chocolateBoxSize: 1,
      info: null,
      errors: [],
    };
  },
  props: {
    msg: String,
  },
  methods: {
    onCreateFamily: function () {
      console.log({
        familyName: this.createFamilyName,
        chocolateBoxSize: this.chocolateBoxSize,
      });

      this.errors = [];

      if (!this.createFamilyName) {
        this.errors.push("Name required.");
      } else {
        axios
          .post("http://localhost:7071/api/createfamily", {
            familyName: this.createFamilyName,
            chocolateBoxSize: 3,
          })
          .then((response) => {
            console.log(response);
            this.$emit("familyCreated", this.familyName);
          })
          .catch(function (error) {
            // handle error
            console.log(error.response.data);
          })
          .then(function () {
            // always executed
          });
      }
    },
    onLoadFamily: function () {
      console.log({
        familyName: this.loadFamilyName,
      });

      this.errors = [];

      if (!this.loadFamilyName) {
        this.errors.push("Name required.");
      } else {
        axios
          .post("http://localhost:7071/api/getfamily", {
            familyName: this.loadFamilyName,
          })
          .then((response) => {
            console.log(response.data);
            this.$emit("familyLoaded", response.data);
          })
          .catch(function (error) {
            // handle error
            console.log(error.response.data);
          })
          .then(function () {
            // always executed
          });
      }
    },
  },
};
</script>

<style scoped>
</style>