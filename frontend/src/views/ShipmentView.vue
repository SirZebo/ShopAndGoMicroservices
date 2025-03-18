<template>
    <div class="shipments-page">
      <div class="shipments-list">
        <h1>Shipments Overview</h1>
        <div class="shipment" v-for="shipment in shipments" :key="shipment.id" @click="selectShipment(shipment)">
          <h2>Shipment {{ shipment.id }}</h2>
          <p>Status: {{ shipmentStatus(shipment.status) }}</p>
        </div>
      </div>
      <div class="shipment-details" v-if="selectedShipment">
        <h2>Shipment {{ selectedShipment.id }} Details</h2>
        <p>Current status: {{ shipmentStatus(selectedShipment.status) }}</p>
        <textarea v-model="shipmentTx" placeholder="Enter tracking transaction hash..."></textarea>
        <button @click="updateShipmentStatus">Submit</button>
        <p v-if="submissionSuccess" class="success-message">Shipment details have been successfully updated.</p>
      </div>
    </div>
</template>

<script>
export default {
  name: 'ShipmentsPage',
  data() {
    return {
      shipments: [
        { id: 1, status: 0 },
        { id: 2, status: 1 },
        { id: 3, status: 2 }
      ],
      selectedShipment: null,
      shipmentTx: '',
      submissionSuccess: false // Tracks if the submission was successful
    };
  },
  methods: {
    selectShipment(shipment) {
      this.selectedShipment = shipment;
      this.submissionSuccess = false; // Reset success message when selecting a new shipment
    },
    shipmentStatus(status) {
      const statuses = ['Order Received', 'Tracking Info Received', 'In Transit', 'Delivered', 'Exception'];
      return statuses[status] || 'Unknown Status';
    },
    updateShipmentStatus() {
      if (this.shipmentTx.trim()) {
        console.log("Shipment status updated for:", this.selectedShipment.id, "with transaction hash:", this.shipmentTx);
        this.submissionSuccess = true; // Set success message to show
      } else {
        alert("Please enter a tracking transaction hash.");
      }
    }
  }
};
</script>
  
<style scoped>
.shipments-page {
  display: flex; /* Ensures that the list and details sections are side by side */
  justify-content: space-between; /* Distributes space evenly */
  align-items: start; /* Aligns items at the start of the container */
  margin-top: 100px; /* Adjusted to ensure space between navbar and content */
  padding: 0 20px; /* Added padding for mobile responsiveness */
  font-family: Arial, sans-serif; /* Standard font for consistency */
}

.shipments-list {
  width: 48%; /* Slightly reduced width to fit side by side without squeezing */
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
  width: 48%; /* Matching width to shipments-list for symmetry */
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
  color: #27ae60; /* Green color for success messages */
  margin-top: 10px;
}
</style>
