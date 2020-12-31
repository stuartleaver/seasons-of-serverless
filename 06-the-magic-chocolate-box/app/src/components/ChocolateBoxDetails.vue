<template>
  <div class="container">
    <h2 class=" text-center">Family Details</h2>
    <p>Family name: {{ this.family.familyName }}</p>
    <p>Chocolate box size: {{ this.family.chocolateBox.chocolateBoxSize | convertChocolateBoxSize }}</p>
    <p>Chocolates remaining: {{ this.family.chocolateBox.remainingChocolates }}</p>
    <b-button v-b-modal.modal-1>Chocolate Box Status</b-button>
    <b-modal id="modal-1" title="Chocolate Box Status">
      <p class="my-4">List of chocolates remaining after the reservations:</p>
      <table class="table">
        <thead>
          <tr>
            <th scope="col">Chocolate name</th>
            <th scope="col">Remaining</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="chocolate in this.family.chocolateBox.chocolates" v-bind:key="chocolate.name">
            <th scope="row">{{ chocolate.name }}</th>
            <td>{{ chocolate.quantity }}</td>
          </tr>
        </tbody>
      </table>
    </b-modal>
    <br />
    <br />
    <div class="alert alert-danger" role="alert" v-if="errors.length">
        <h4 class="alert-heading">Opps, the chocolate making machines broke!</h4>
        <p>Please fix the following errors:</p>
          <hr />
        <p v-for="error in errors" v-bind:key="error">{{ error }}</p>
    </div>
    <div class="alert alert-success" role="alert" v-if="successes.length">
        <h4 class="alert-heading">Success</h4>
        <p v-for="success in successes" v-bind:key="success">{{ success }}</p>
        <hr />
        <p>Your reservation may not show below straight away. This is because Durable Entities return the commited state, and your reservation may not have been commited yet. Rest assured though, your chocolate is safe.</p>
    </div>
    <div class="row justify-content-center">
      <div class="col-md-6 create-family">
        <div>
          <h2>Reserve</h2>
          <div class="form-group">
            <label for="inputFirstName">First name</label>
            <input type="text" class="form-control" id="inputFirstName" aria-describedby="firstNameHelp" placeholder="Enter your first name" v-model="name">
          </div>
          <div class="form-group">
            <label for="chocolateSelect">Select your chocolate</label>
            <select class="form-control" id="chocolateSelect" v-model="selectedChocolate">
              <option v-for="chocolate in chocolates" v-bind:value="chocolate" v-bind:key="chocolate">
                {{ chocolate }}
              </option>
            </select>
          </div>
          <div class="form-group">
            <label for="inputQuantity">Quantity to reserve</label>
            <input type="text" class="form-control" id="inputQuantity" aria-describedby="quantityHelp" placeholder="Enter the quantity to reserve" v-model="quantity">
          </div>
          <input class="btn btn-primary" type="submit" value="Reserve chocolate" @click="onReserve">
        </div>
      </div>
      <div class="col-md-6 load-family">
        <h2 class="text-center">Reservations</h2>
        <table class="table">
          <thead>
            <tr>
              <th scope="col">Name</th>
              <th scope="col">Time</th>
              <th scope="col">Chocolate name</th>
              <th scope="col">Reserved</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="reservation in this.family.reservations" v-bind:key="reservation.reservedDateTime">
              <th scope="row">{{ reservation.name }}</th>
              <th>{{ reservation.reservedDateTime | formatDate }}</th>
              <td>{{ reservation.chocolateName }}</td>
              <td>{{ reservation.quantity }}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </div>
</template>

<script>
import moment from 'moment'

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
      successes: [],
      isValid: Boolean,
    };
  },
  props: {
    family: null
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
      this.successes = [];

      if (this.isValidReservation()) {
        axios
          .post("/api/reserve", {
            familyName: this.family.familyName,
            name: this.name,
            chocolateName: this.selectedChocolate,
            quantity: this.quantity,
          })
          .then((response) => {
            console.log(response);

            this.successes.push(response.data);

            this.refreshFamily();
          })
          .catch((error) => {
            // handle error
            console.log(error.response.data);

            this.errors.push(error.response.data);
          })
          .then(function () {
            // always executed
          });
      }
    },
    isValidReservation: function () {
      this.isValid = true;

      if (!this.name) {
        this.errors.push("Name required.");

        this.isValid = false;
      }

      if (!this.chocolatesToReserve >= 1) {
        this.errors.push("Quantity is required and should be a number greater than zero.");

        this.isValid = false;
      }

      if (this.chocolatesToReserve > 2) {
        this.errors.push("Remember to leave some for other family members. A maximum of two is allowed");

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
          `/api/listchocolates/${this.family.chocolateBox.chocolateBoxSize}`
        )
        .then((response) => {
          console.log(response.data);

          this.chocolates = response.data;

          this.selectedChocolate = this.chocolates[0];
        })
        .catch((error) => {
          // handle error
          console.log(error.response.data);

          this.errors.push(error.response.data);
        })
        .then(function () {
          // always executed
        });
    },
    refreshFamily: function () {
      console.log({
        familyName: this.family.familyName,
      });

      axios
        .post("/api/getfamily", {
          familyName: this.family.familyName,
        })
        .then((response) => {
          console.log(response.data);

          this.$emit("familyRefreshed", response.data);
        })
        .catch((error) => {
          // handle error
          console.log(error.response.data);

          this.errors.push(error.response.data);
        })
        .then(function () {
           // always executed
        });
    }
  },
  mounted() {
    this.getChocolates();
  },
  filters: {
    formatDate(value) {
      if (value) {
        return moment(String(value)).format('DD/MM/YYYY hh:mm')
      }
    },
    convertChocolateBoxSize(value) {
      if (value) {
        let stringSize = "";

        if(value == 1)
        {
          stringSize = "Small";
        } else if (value == 2) {
          stringSize = "Medium";
        } else {
          stringSize = "Large";
        }

        return stringSize;
      }
    }
  },
};
</script>