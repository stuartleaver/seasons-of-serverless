<template>
  <div class="container">
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
        <p>Your family has been created, please use the right hand side to load your record. Your family may not show below straight away. This is because Durable Entities return the commited state, and your reservation may not have been commited yet. Rest assured though, your chocolate is safe.</p>
    </div>
    <div class="row justify-content-center">
      <div class="col-md-6 create-family">
        <h2 class=" text-center">Create Family</h2>
        <div class="form-group">
          <label for="inputSurnameCreate">Surname</label>
          <input type="text" class="form-control" id="inputSurnameCreate" aria-describedby="surnameHelp" placeholder="Enter your surname" v-model="createFamilyName">
        </div>
        <br />
        <b-button v-b-modal.modal-1>View Chockolate Box Sizes</b-button>
        <b-modal id="modal-1" title="Chocolate Box Details">
          <p class="my-4">Details of the contents of each chocolate box size:</p>
          <b-card no-body>
            <b-tabs pills card vertical>
              <b-tab :title="chocolateBox.chocolateBoxSize | convertChocolateBoxSize" v-for="chocolateBox in chocolateBoxes" v-bind:key="chocolateBox.chocolateBoxSize">
                <b-card-text>
                  <table class="table">
                    <thead>
                      <tr>
                        <th scope="col">Chocolate name</th>
                        <th scope="col">Quantity</th>
                      </tr>
                    </thead>
                    <tbody>
                      <tr v-for="chocolate in chocolateBox.chocolates" v-bind:key="chocolate.name">
                        <th scope="row">{{ chocolate.name }}</th>
                        <td>{{ chocolate.quantity }}</td>
                      </tr>
                    </tbody>
                  </table>
                </b-card-text>
              </b-tab>
            </b-tabs>
          </b-card>
        </b-modal>
        <br />
        <br />
        <div class="form-group">
          <label for="chocolateBoxSizeSelect">Select your families chocolate box size</label>
          <select class="form-control" id="chocolateBoxSizeSelect" v-model="chocolateBoxSize">
            <option value="1">Small</option>
            <option value="2">Medium</option>
            <option value="3">Large</option>
          </select>
        </div>
        <div class="field has-text-right">
          <input class="btn btn-primary" type="submit" value="Create Family" @click="onCreateFamily">
        </div>
      </div>
      <div class="col-md-6 load-family">
        <h2 class="text-center">Load Family</h2>
        <div class="form-group">
          <label for="inputSurnameLoad">Surname</label>
          <input type="text" class="form-control" id="inputSurnameLoad" aria-describedby="surnameHelp" placeholder="Enter your surname" v-model="loadFamilyName">
        </div>
        <div class="field has-text-right">
          <input class="btn btn-primary" type="submit" value="Load Family" @click="onLoadFamily">
        </div>
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
      chocolateBoxSize: "1",
      info: null,
      errors: [],
      successes: [],
      chocolateBoxes: [],
      foo: "foo"
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
          .post("https://magicchocolatebox.azurewebsites.net/api/createfamily", {
            familyName: this.createFamilyName,
            chocolateBoxSize: this.chocolateBoxSize,
          })
          .then((response) => {
            console.log(response);

            this.successes.push(response.data);
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
    onLoadFamily: function () {
      console.log({
        familyName: this.loadFamilyName,
      });

      this.errors = [];

      if (!this.loadFamilyName) {
        this.errors.push("Name required.");
      } else {
        axios
          .post("https://magicchocolatebox.azurewebsites.net/api/getfamily", {
            familyName: this.loadFamilyName,
          })
          .then((response) => {
            console.log(response.data);
            this.$emit("familyLoaded", response.data);
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
    getChocolateBoxes: function () {
      console.log("List chocolate box sizes")
      axios
        .get("https://magicchocolatebox.azurewebsites.net/api/listchocolateboxes")
        .then((response) => {
          console.log(response.data);

          this.chocolateBoxes = response.data;
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
  },
  mounted() {
    this.getChocolateBoxes();
  },
  filters: {
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
  }
};
</script>