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
  
      <!-- Pay button based on status -->
      <button v-if="order.status === 'Pending'" @click="payOrder" class="pay-button">Pay</button>
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
        cancelled: false,
        order: {
          name: "Huawei Plus",
          price: 650.00,
          quantity: 1,
          status: "Pending",  // Dynamic status based on order data
        }
      };
    },
    methods: {
    async payOrder() {
        try {
            console.log("Pay button clicked! Sending request...");

            const orderDetails = {
                CheckoutProductDto: {
                    userName: "swn",
                    CustomerId: "db7a48da-79b0-4a29-ba03-00deb540baec",
                    ProductId: "5334c996-8457-4cf0-815c-ed2b77c4ff61",
                    Price: 950.00,
                    Quantity: 1,
                    totalPrice: 950.00,
                    Lanugage: "Malay",
                    firstName: "swn",
                    lastName: "swn",
                    emailAddress: "test@test.com",
                    addressLine: "34 Charles Street",
                    country: "USA",
                    state: "Michigan",
                    zipCode: "48198"
                }
            };

            const response = await axios.post(
                "https://localhost:6060/product/checkout",
                orderDetails,
                { headers: { "Content-Type": "application/json" } }
            );

            console.log("Response received:", response.data);

            // Extract and display transactionToken
            const transactionToken = response.data.transactionToken;
            alert(`Transaction Token: ${transactionToken}`);
        } catch (error) {
            console.error("Error processing payment:", error);
            alert("There was an error processing your payment. Please try again.");
        }
    }
}

  };
</script>
  
<style scoped>
  .order-status {
    margin-top: 100px;  /* Adjusted to ensure space between navbar and content */
    padding: 0 20px; /* Added padding for mobile responsiveness */
    margin-bottom: 50px; /* Added margin to match the home page's bottom spacing */
    text-align: center;
  }
  
  .order-status-message {
    margin-bottom: 20px;  /* Adjusted space for status above order details */
    font-weight: bold;
    font-size: 1.2rem;
  }
  
  .order-status-message.pending {
    color: orange; /* Pending status - orange */
  }
  
  .order-status-message.success {
    color: green; /* Success status - green */
  }
  
  .order-status-message.failed {
    color: red; /* Failed status - red */
  }
  
  .order-summary {
    margin-bottom: 20px;
  }
  
  .pay-button {
    padding: 10px 20px;
    background-color: #363d46; /* Main button color */
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    font-size: 16px;
  }
  
  .pay-button:hover {
    background-color: #2a333d; /* Darker color for hover effect */
  }
  
  .pay-button:disabled {
    background-color: #aaa; /* Disabled button color */
    cursor: not-allowed;
  }
</style>