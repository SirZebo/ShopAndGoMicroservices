<template>
  <div class="shipments-page">
    <div class="shipments-list">
      <h1>Shipments Overview</h1>

      <!-- Show loading state -->
      <p v-if="loading">Loading shipments...</p>

      <!-- Display shipments -->
      <div 
        class="shipment" 
        v-for="shipment in shipments" 
        :key="shipment.id" 
        @click="selectShipment(shipment)"
      >
        <h2>Shipment {{ shipment.id }}</h2>
        <p>Status: {{ shipmentStatus(shipment.shipmentStatus) }}</p>
      </div>

      <!-- Show error message if fetching fails -->
      <p v-if="fetchError" class="error-message">Failed to load shipments. Please try again.</p>
    </div>

    <!-- Shipment Details Section -->
    <div class="shipment-details" v-if="selectedShipment">
      <h2>Shipment {{ selectedShipment.id }} Details</h2>
      <p>Current status: {{ shipmentStatus(selectedShipment.shipmentStatus) }}</p>

      <textarea 
        v-model="shipmentTx" 
        placeholder="Enter tracking transaction hash..."
      ></textarea>

      <button @click="updateShipmentStatus" :disabled="!isFormValid">Submit</button>

      <!-- Success Message -->
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
      loading: true, // Added loading state
      fetchError: false, // Track fetch errors
    };
  },
  computed: {
    isFormValid() {
      return this.shipmentTx.trim().length > 0;
    }
  },
  async beforeMount() {
    await this.fetchShipments();
  },
  methods: {
    async fetchShipments() {
      try {
        const response = await axios.get("https://localhost:6062/shipments/merchant/189dc8dc-990f-48e0-a37b-e6f2b60b9d7d");
        this.shipments = response.data.shipments || [];
      } catch (error) {
        console.error("Error fetching shipments:", error);
        this.fetchError = true;
      } finally {
        this.loading = false; // Hide loading indicator
      }
    },
    selectShipment(shipment) {
      this.selectedShipment = shipment;
      this.shipmentTx = ""; // Reset input field
      this.submissionSuccess = false;
    },
    shipmentStatus(status) {
      const statuses = ["Order Received", "Tracking Info Received", "In Transit", "Delivered", "Exception"];
      return statuses[status] || "Unknown Status";
    },
    async updateShipmentStatus() {
      if (!this.isFormValid) {
        alert("Please enter a tracking transaction hash.");
        return;
      }

      try {
        await axios.put(`https://localhost:6062/shipments/${this.selectedShipment.id}`, {
          trackingTransactionHash: this.shipmentTx,
        });

        this.submissionSuccess = true;
        this.shipmentTx = ""; // Clear input field after successful submission
      } catch (error) {
        console.error("Error updating shipment:", error);
        alert("Failed to update shipment. Please try again.");
      }
    }
  }
};
</script>

<style scoped>
.shipments-page {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
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
  transition: background-color 0.3s ease;
}

.shipment:hover {
  background-color: #f5f5f5;
}

.shipment-details {
  width: 48%;
  padding: 20px;
}

textarea {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
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

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

button:hover:not(:disabled) {
  background-color: #2a333d;
}

.success-message {
  color: #27ae60;
  margin-top: 10px;
}

.error-message {
  color: #d63031;
  margin-top: 10px;
}
</style>
