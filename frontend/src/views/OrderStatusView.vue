<template>
  <div class="order-status">
    <h2>Order Status</h2>

    <!-- Display the status with corresponding colors (above order details) -->
    <div class="order-status-message" :class="order.status.toLowerCase()">
      <p><strong>Status:</strong> {{ order.status }}</p>
    </div>

    <!-- Display order details -->
    <div class="order-summary">
      <h3>Order Details</h3>
      <p><strong>Product:</strong> {{ order.name }}</p>
      <p><strong>Quantity:</strong> {{ order.quantity }}</p>
      <p><strong>Total Price:</strong> ${{ order.price.toFixed(2) }}</p>
    </div>

    <!-- Pay button fetches purchase URL -->
    <button v-if="order.status === 'Pending'" @click="getPurchaseUrl" class="pay-button">
      Get Purchase URL
    </button>

    <!-- Display purchase URL as a hyperlink if available -->
    <div v-if="purchaseUrl" class="purchase-link">
      <p>Click the link below to complete your purchase:</p>
      <a :href="purchaseUrl" target="_blank">{{ purchaseUrl }}</a>
    </div>

    <button v-if="order.status === 'Success'" class="pay-button" disabled>Payment Successful</button>
    <button v-if="order.status === 'Failed'" class="pay-button" disabled>Payment Failed</button>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'OrderStatus',
  data() {
    return {
      order: {
        name: "",
        price: 0,
        quantity: 0,
        status: "Pending", // Default status
      },
      purchaseUrl: "",
      transactionToken: ""
    };
  },
  created() {
    // Retrieve transactionToken from query params or localStorage
    this.transactionToken = this.$route.query.transactionToken || localStorage.getItem('transactionToken');

    // Retrieve order details from localStorage
    const storedOrder = localStorage.getItem('orderDetails');
    if (storedOrder) {
      this.order = { ...this.order, ...JSON.parse(storedOrder) };
    }
  },
  methods: {
    async getPurchaseUrl() {
      if (!this.transactionToken) {
        alert("Transaction token is missing.");
        return;
      }

      try {
        const response = await axios.get(
          `http://127.0.0.1:6006/getPurchaseUrl?transactionToken=${this.transactionToken}`
        );

        if (response.data.purchaseUrl) {
          this.purchaseUrl = response.data.purchaseUrl; // Store the URL
        } else {
          alert("Purchase URL not found.");
        }
      } catch (error) {
        console.error("Error retrieving purchase URL:", error);
        alert("There was an error retrieving the purchase URL.");
      }
    }
  }
};
</script>


<style scoped>
.order-status {
  margin-top: 100px;
  padding: 0 20px;
  margin-bottom: 50px;
  text-align: center;
}

.order-status-message {
  margin-bottom: 20px;
  font-weight: bold;
  font-size: 1.2rem;
}

.order-status-message.pending {
  color: orange;
}

.order-status-message.success {
  color: green;
}

.order-status-message.failed {
  color: red;
}

.order-summary {
  margin-bottom: 20px;
}

.pay-button {
  padding: 10px 20px;
  background-color: #363d46;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
}

.pay-button:hover {
  background-color: #2a333d;
}

.pay-button:disabled {
  background-color: #aaa;
  cursor: not-allowed;
}

/* Styling for purchase link */
.purchase-link {
  margin-top: 20px;
}

.purchase-link a {
  color: #007bff;
  text-decoration: none;
  font-weight: bold;
}

.purchase-link a:hover {
  text-decoration: underline;
}
</style>
