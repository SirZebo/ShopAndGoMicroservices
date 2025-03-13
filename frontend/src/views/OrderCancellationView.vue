<template>
  <div class="order-cancellation">
    <h2>Cancel Your Order</h2>

    <!-- Display order summary -->
    <div class="order-summary">
      <h3>Order Summary</h3>
      <p><strong>Product:</strong> {{ order.name }}</p>
      <p><strong>Price:</strong> ${{ order.price.toFixed(2) }}</p>
      <p><strong>Status:</strong> {{ order.status }}</p>
    </div>

    <!-- Confirmation message -->
    <p class="warning-message">Are you sure you want to cancel this order?</p>

    <!-- Cancel button -->
    <button @click="cancelOrder" class="cancel-button">Cancel Order</button>

    <!-- Success message (shown after cancellation) -->
    <div v-if="cancelled" class="success-message">
      <h3>Your order has been successfully cancelled.</h3>
    </div>
  </div>
</template>

<script>
export default {
  name: 'OrderCancellation',
  data() {
    return {
      cancelled: false,
      order: {
        name: "Huawei Plus",
        price: 650.00,
        status: "Shipped",  // Dynamic status can be based on order data
      }
    };
  },
  methods: {
    cancelOrder() {
      // Call backend API to handle order cancellation
      // For now, simulate order cancellation by setting 'cancelled' to true
      this.cancelled = true;
    }
  }
};
</script>

<style scoped>
.order-cancellation {
  margin-top: 100px;  /* Adjusted to ensure space between navbar and content */
  padding: 0 20px; /* Added padding for mobile responsiveness */
  margin-bottom: 50px; /* Added margin to match the home page's bottom spacing */
  text-align: center;
}

.order-summary {
  margin-bottom: 20px;
}

.warning-message {
  color: #e74c3c;
  font-weight: bold;
  margin-bottom: 20px;
}

.cancel-button {
  padding: 10px 20px;
  background-color: #363d46;  /* Main button color */
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
}

.cancel-button:hover {
  background-color: #2a333d; /* Darker color for hover effect */
}

.success-message {
  margin-top: 20px;
  color: #27ae60;
  font-size: 18px;
  font-weight: bold;
}
</style>
