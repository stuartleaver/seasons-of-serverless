<template>
  <div class="container">
    <h1>{{ msg }}</h1>
    <div class="row justify-content-center">
      <div class="col-md-6 create-family">
        <div id="myMap"></div>
      </div>
      <div class="col-md-6 load-family">
        <div class="mb-3">
          <label for="selectedLatitude" class="form-label">Selected Latitude</label>
          <input type="text" class="form-control" id="selectedLatitude" placeholder="" v-model="selectedLatitude">
        </div>
        <div class="mb-3">
          <label for="selectedLongitude" class="form-label">Selected Longitude</label>
          <input type="text" class="form-control" id="selectedLongitude" placeholder="" v-model="selectedLongitude">
          <!-- <input class="btn btn-primary" type="submit" value="Find Regional Center" @click="onFindRegionalCenter"> -->
        </div>
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
          <input class="btn btn-primary" type="submit" value="Send Request" @click="sendRequest">
          <br />
          <br />
          <div class="alert alert-success" role="alert" v-if="requestSent">
            <h4 class="alert-heading">Success</h4>
              <p>Your request has been sent. You should recieve an email soon with the information for your selected area.</p>
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
      point: null,
      selectedLatitude: null,
      selectedLongitude: null,
      countrySubdivision: null,
      municipality: null,
      country: null,
      requestSent: null
    };
  },
  props: {
    msg: String,
  },
  methods: {
    loadAzureMap: function () {
      this.map = new atlas.Map("myMap", {
        center: [18, 0],
        zoom: 2.4,
        language: "en-US",
        authOptions: {
          authType: "subscriptionKey",
          subscriptionKey: process.env.VUE_APP_AZURE_MAP_PRIMARY_KEY,
        },
      });

      let tempMap = this.map;

      this.map.events.add("ready", () => {
        /* Construct a zoom control*/
        var zoomControl = new atlas.control.ZoomControl();

        /* Add the zoom control to the map*/
        tempMap.controls.add(zoomControl, {
          position: "bottom-right",
        });

        /*Create a data source and add it to the map*/
        var dataSource = new atlas.source.DataSource();
        tempMap.sources.add(dataSource);

        /* Gets co-ordinates of clicked location*/
        tempMap.events.add("click", (e) => {
          this.requestSent = false;
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
        tempMap.layers.add(new atlas.layer.SymbolLayer(dataSource, null));
      });
    },
    getCountryName: function() {
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
            console.log(error.response.data);
            this.errors.push(error.response.data);
          })
          .then(function () {
            // always executed
          });
    },
    sendRequest: function() {
      axios
          .post("http://localhost:7071/api/sendrequest", {
            countrySubdivision: this.countrySubdivision,
            municipality: this.municipality,
            country: this.country,
            email: "stuartleaveruk@gmail.com"
          })
          .then((response) => {
            console.log(response);
  
            this.requestSent = true;
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
    this.loadAzureMap();
  },
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
#myMap {
  height: 50rem;
  width: 100%;
}
</style>
