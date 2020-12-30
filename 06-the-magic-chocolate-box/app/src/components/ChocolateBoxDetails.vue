<template>
  <div class="container">
    <p v-if="errors.length">
      <b>Please correct the following error(s):</b>
      <ul>
        <li v-for="error in errors" v-bind:key="error">{{ error }}</li>
      </ul>
    </p>
    <div class="row justify-content-center">
      <div class="col-md-6 create-family">
        <h2 class=" text-center">Family Details</h2>
        <p>Family name: {{ this.family.familyName }}</p>
        <div>
          <h2>Reserve</h2>
          <input type="text" class="input" name="name" v-model="name" />
          <select class="form-control" v-model="selectedChocolate">
            <option v-for="chocolate in chocolates" v-bind:value="chocolate" v-bind:key="chocolate">
              {{ chocolate }}
            </option>
          </select>
          <input type="text" class="input" name="quantity" v-model="quantity" />
          <button type="submit" class="button is-danger" @click="onReserve">Reserve chocolate</button>
        </div>
        <div>
          <h2>Details</h2>
          <p v-if="this.family.chocolateBox.chocolateBoxSize === 1">Chocolate box size: Small</p>
          <p v-if="this.family.chocolateBox.chocolateBoxSize === 2">Chocolate box size: Medium</p>
          <p v-if="this.family.chocolateBox.chocolateBoxSize === 3">Chocolate box size: Large</p>
          <ul id="chocolate-box">
            <li v-for="chocolate in this.family.chocolateBox.chocolates" v-bind:key="chocolate.name">
              {{ chocolate.name }} {{ chocolate.quantity }}
            </li>
          </ul>
          <p>Chocolates remaining: {{ this.family.chocolateBox.remainingChocolates }}</p>
        </div>
      </div>
      <div class="col-md-6 load-family">
        <h2 class="text-center">Reservations</h2>
        <ul id="reservations">
          <li v-for="reservation in this.family.reservations" v-bind:key="reservation.reservedDateTime">
            {{ reservation.reservedDateTime }} {{ reservation.name }}:
            {{ reservation.chocolateName }} - {{ reservation.quantity }}
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
const axios = require("axios");

export default {
  name: "ChocolateBoxDetails",
  data: function () {
    return {
      chocolates: [],
      name: "",
      selectedChocolate: "",
      quantity: 0,
      errors: [],
      isValid: Boolean,
    };
  },
  props: {
    family: null,
  },
  computed: {
    chocolatesToReserve: function () {
      return Number(this.quantity);
    },
  },
  methods: {
    onReserve: function () {
      console.log({
        familyName: this.family.familyName,
        name: this.name,
        chocolateName: this.selectedChocolate,
        quantity: this.chocolatesToReserve,
      });

      this.errors = [];

      if (this.isValidReservation()) {
        // axios
        //   .post("http://localhost:7071/api/reserve", {
        //     familyName: this.family.familyName,
        //     name: this.name,
        //     chocolatename: this.selectedChocolate,
        //     quantity: this.quantity,
        //   })
        //   .then((response) => {
        //     console.log(response);
        //     this.$emit("familyCreated", this.familyName);
        //   })
        //   .catch(function (error) {
        //     // handle error
        //     console.log(error.response.data);
        //   })
        //   .then(function () {
        //     // always executed
        //   });
      }
    },
    isValidReservation: function () {
      this.isValid = true;

      if (!this.name) {
        this.errors.push("Name required.");

        this.isValid = false;
      }

      if (!this.chocolatesToReserve >= 1) {
        this.errors.push("Quantity is required and should be a number.");

        this.isValid = false;
      }

      return this.isValid;
    },
    getChocolates: function () {
      console.log({
        chocolateBoxSize: this.family.chocolateBox.chocolateBoxSize,
      });

      axios
        .get(
          `http://localhost:7071/api/listchocolates/${this.family.chocolateBox.chocolateBoxSize}`
        )
        .then((response) => {
          console.log(response.data);

          this.chocolates = response.data;

          this.selectedChocolate = this.chocolates[0]
        })
        .catch(function (error) {
          // handle error
          console.log(error.response.data);
        })
        .then(function () {
          // always executed
        });
    },
  },
  mounted() {
    this.getChocolates();
  },
};
</script>

<style scoped>
</style>