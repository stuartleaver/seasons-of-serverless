<template>
  <div class="container">
    <div class="row justify-content-center">
      <div class="col-md-6 create-family">
        <div id="azure-map"></div>
      </div>
      <div class="col-md-6 load-family">
        <div class="mb-3">
          <label for="selectedLatitude" class="form-label">Selected Latitude</label>
          <input type="text" class="form-control" id="selectedLatitude" placeholder="" v-model="selectedLatitude">
        </div>
        <div class="mb-3">
          <label for="selectedLongitude" class="form-label">Selected Longitude</label>
          <input type="text" class="form-control" id="selectedLongitude" placeholder="" v-model="selectedLongitude">
        </div>
        <br />
        <div v-if="country">
          <h2>Regional Center Information</h2>
          <div class="mb-3">
            <label for="countrySubdivision" class="form-label">Country Subdivision</label>
            <input type="text" class="form-control" id="countrySubdivision" placeholder="" v-model="countrySubdivision">
          </div>
          <div class="mb-3">
            <label for="municipality" class="form-label">Municipality</label>
            <input type="text" class="form-control" id="municipality" placeholder="" v-model="municipality">
          </div>
          <div class="mb-3">
            <label for="country" class="form-label">Country</label>
            <input type="text" class="form-control" id="country" placeholder="" v-model="country">
          </div>
          <div class="mb-3">
            <label for="emailAddress">Email address (to recieve your recipe)</label>
            <input type="email" class="form-control" id="emailAddress" aria-describedby="emailHelp" placeholder="Enter email" v-model="email">
            <small id="emailHelp" class="form-text text-muted">We'll never share your email address.</small>
          </div>
          <input class="btn btn-primary" type="submit" value="Send Request" @click="sendRequest">
          <br />
          <br />
          <div class="alert alert-success" role="alert" v-if="requestSent">
            <h4 class="alert-heading">Success</h4>
              <p>Your request has been sent. You should recieve an email soon with the information for your selected area.</p>
          </div>
          <div class="alert alert-danger" role="alert" v-if="errors.length">
            <h4 class="alert-heading">Opps, this is embarrassing!</h4>
            <p v-for="error in errors" v-bind:key="error">{{ error }}</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import * as atlas from "azure-maps-control";

const axios = require("axios");

export default {
  name: "HelloWorld",
  data: function () {
    return {
      map: null,
      azureMaps: null,
      point: null,
      selectedLatitude: null,
      selectedLongitude: null,
      countrySubdivision: null,
      municipality: null,
      country: null,
      email: null,
      requestSent: null,
      errors: []
    };
  },
  props: {
    msg: String,
  },
  methods: {
    async loadAzureMap () {
      await this.getAzureMaps();

      this.map = new atlas.Map("azure-map", {
        center: [18, 0],
        zoom: 2.4,
        language: "en-US",
        authOptions: {
          authType: "subscriptionKey",
          subscriptionKey: this.azureMaps,
        },
      });

      let map = this.map;

      this.map.events.add("ready", () => {
        /* Construct a zoom control*/
        var zoomControl = new atlas.control.ZoomControl();

        /* Add the zoom control to the map*/
        map.controls.add(zoomControl, {
          position: "bottom-right",
        });

        /*Create a data source and add it to the map*/
        var dataSource = new atlas.source.DataSource();
        map.sources.add(dataSource);

        /* Gets co-ordinates of clicked location*/
        map.events.add("click", (e) => {
          this.requestSent = false;

          this.countrySubdivision = null;
          this.municipality = null;
          this.country = null;

          if (!this.point) {
            var point = new atlas.Shape(new atlas.data.Point(e.position));
            //Add the symbol to the data source.
            dataSource.add([point]);

            this.point = point;
          }
          /* Update the position of the point feature to where the user clicked on the map. */
          this.point.setCoordinates(e.position);

          this.selectedLatitude = e.position[1];
          this.selectedLongitude = e.position[0];

          this.getCountryName();
        });

        //Create a symbol layer using the data source and add it to the map
        map.layers.add(new atlas.layer.SymbolLayer(dataSource, null));
      });
    },
    getCountryName: function() {
      this.errors = [];

      axios
          .get(`http://localhost:7071/api/getregionalcenter/${this.selectedLatitude}/${this.selectedLongitude}`)
          .then((response) => {
            console.log(response);
            
            this.countrySubdivision = response.data.countrySubdivision;
            this.municipality = response.data.municipality;
            this.country = response.data.country;
          })
          .catch((error) => {
            // handle error
            console.log(error);
            
            this.errors.push("Something went wrong. Please try again later.");
          })
          .then(function () {
            // always executed
          });
    },
    getAzureMaps: async function() {
      this.errors = [];

      await axios
          .get(`http://localhost:7071/api/azuremaps`)
          .then((response) => {
            this.azureMaps = response.data
          })
          .catch((error) => {
            // handle error
            console.log(error);
            
            this.errors.push("Something went wrong. Please try again later.");
          })
          .then(function () {
            // always executed
          });
    },
    sendRequest: function() {
      this.errors = [];

      axios
          .post("http://localhost:7100/api/sendrequest", {
            countrySubdivision: this.countrySubdivision,
            municipality: this.municipality,
            country: this.country,
            email: this.email
          })
          .then((response) => {
            console.log(response);
  
            this.requestSent = true;
          })
          .catch((error) => {
            // handle error
            console.log(error);

            this.errors.push("Something went wrong. Please try again later.");
          })
          .then(function () {
            // always executed
          });
    }
  },
  mounted() {
    this.loadAzureMap();
  },
};
</script>

<style scoped>
#azure-map {
  height: 50rem;
  width: 100%;
}
</style>
