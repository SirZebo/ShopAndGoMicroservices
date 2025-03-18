<template>
  <div class="shipments-page">
    <div class="shipments-list">
      <h1>Shipments Overview</h1>
      <div 
        class="shipment" 
        v-for="shipment in shipments" 
        :key="shipment.id" 
        @click="selectShipment(shipment)"
      >
        <h2>Shipment {{ shipment.id }}</h2>
        <p>Status: {{ shipmentStatus(shipment.shipmentStatus) }}</p>
      </div>
    </div>

    <div class="shipment-details" v-if="selectedShipment">
      <h2>Shipment {{ selectedShipment.id }} Details</h2>
      <p>Current status: {{ shipmentStatus(selectedShipment.shipmentStatus) }}</p>
      <textarea v-model="shipmentTx" placeholder="Enter tracking transaction hash..."></textarea>
      <button @click="updateShipmentStatus">Submit</button>
      <p v-if="submissionSuccess" class="success-message">
        Shipment details have been successfully updated.
      </p>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  name: "ShipmentsPage",
  data() {
    return {
      shipments: [],
      selectedShipment: null,
      shipmentTx: "",
      submissionSuccess: false,
    };
  },
  async created() {
    await this.fetchShipments(); // Fetch shipments on component mount
  },
  methods: {
    async fetchShipments() {
      try {
        const response = await axios.get("https://localhost:6062/shipments/merchant/189dc8dc-990f-48e0-a37b-e6f2b60b9d7d"); // Replace with actual JSON URL or API
        this.shipments = response.data.shipments; // Assign data to shipments array
      } catch (error) {
        console.error("Error fetching shipments:", error);
      }
    },
    selectShipment(shipment) {
      this.selectedShipment = shipment;
      this.submissionSuccess = false;
    },
    shipmentStatus(status) {
      const statuses = ["Order Received", "Tracking Info Received", "In Transit", "Delivered", "Exception"];
      return statuses[status] || "Unknown Status";
    },
    updateShipmentStatus() {
      if (this.shipmentTx.trim()) {
        console.log("Shipment updated:", this.selectedShipment.id, "Hash:", this.shipmentTx);
        this.submissionSuccess = true;
      } else {
        alert("Please enter a tracking transaction hash.");
      }
    }
  }
};
</script>

<style scoped>
.shipments-page {
  display: flex;
  justify-content: space-between;
  align-items: start;
  margin-top: 100px;
  padding: 0 20px;
  font-family: Arial, sans-serif;
}

.shipments-list {
  width: 48%;
  padding: 20px;
  border-right: 1px solid #ccc;
}

.shipment {
  padding: 10px;
  border: 1px solid #ccc;
  margin-bottom: 10px;
  cursor: pointer;
}

.shipment-details {
  width: 48%;
  padding: 20px;
}

textarea {
  width: 100%;
  margin-bottom: 10px;
}

button {
  padding: 10px;
  background-color: #363d46;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

button:hover {
  background-color: #2a333d;
}

.success-message {
  color: #27ae60;
  margin-top: 10px;
}
</style>
