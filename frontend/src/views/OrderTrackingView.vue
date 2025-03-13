<template>
  <div class="order-tracking">
    <h2>Track Your Order</h2>

    <!-- Display the order tracking number -->
    <div class="tracking-info">
      <h3>Tracking Number: {{ trackingNumber }}</h3>
      <p>Status: {{ orderStatus }}</p>
    </div>

    <!-- Display the status timeline -->
    <div class="status-timeline">
      <div v-for="(status, index) in statusUpdates" :key="index" class="status-item">
        <div :class="{'completed': status.completed}">
          <div class="status-step">{{ status.step }}</div>
          <div class="status-date">{{ status.date }}</div>
        </div>
      </div>
    </div>

    <!-- Cancel Order Button -->
    <div class="cancel-order-container">
      <button class="cancel-order-button" @click="cancelOrder">Cancel Order</button>
      <!-- Add a Review Button -->
      <button class="review-order-button" @click="goToReview">Go to Review</button>
    </div>
  </div>
</template>

<script>
export default {
  name: "OrderTracking",
  data() {
    return {
      trackingNumber: "91466c01-9730-455d-a24d-3e9f812b9357", // Example tracking number
      orderStatus: "Shipped", // Example order status
      statusUpdates: [
        { step: "Order Received", date: "2025-03-12", completed: true },
        { step: "Shipped", date: "2025-03-13", completed: true },
        { step: "Out for Delivery", date: "2025-03-14", completed: false },
        { step: "Delivered", date: "2025-03-15", completed: false },
      ],
    };
  },
  methods: {
    cancelOrder() {
      // Navigate to the Order Cancellation page
      this.$router.push({ name: 'OrderCancellationView' });
    },
    goToReview() {
      // Navigate to the Review page
      this.$router.push({ name: 'ReviewView' });
    }
  },
};
</script>

<style scoped>
.order-tracking {
  margin-top: 100px; /* Adjusted to ensure space between navbar and content */
  padding: 0 20px; /* Added padding for mobile responsiveness */
  margin-bottom: 50px; /* Added margin to match the home page's bottom spacing */
}

h2 {
  font-size: 2rem;
  color: #363d46;
  text-align: center;
  margin-bottom: 20px;
}

.tracking-info {
  text-align: center;
  margin-bottom: 30px;
}

.tracking-info h3 {
  font-size: 1.5rem;
  color: #363d46;
  font-weight: bold;
}

.status-timeline {
  margin-bottom: 30px;
}

.status-item {
  display: flex;
  justify-content: space-between;
  padding: 15px;
  border: 1px solid #ddd;
  background-color: #f9f9f9;
  margin-bottom: 10px;
}

.status-item .status-step {
  font-size: 1.2rem;
  color: #2c3e50;
  font-weight: bold;
}

.status-item .status-date {
  font-size: 1rem;
  color: #2c3e50;
}

.status-item.completed .status-step {
  color: #27ae60; /* Green for completed steps */
}

.status-item:not(.completed) .status-step {
  color: #e74c3c; /* Red for pending steps */
}

.cancel-order-container {
  text-align: center;
  margin-top: 20px;
}

.cancel-order-button,
.review-order-button {
  padding: 10px 20px;
  background-color: #363d46;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  margin-right: 10px;
}

.cancel-order-button:hover,
.review-order-button:hover {
  background-color: #2a333d;
}

.review-order-button {
  background-color: #27ae60; /* Green button for review */
}

.review-order-button:hover {
  background-color: #2ecc71;
}
</style>