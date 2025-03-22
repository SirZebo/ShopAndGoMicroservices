<template>
  <div class="order-confirmation">
    <h1>Thank You for Your Purchase!</h1>
    <p>Your order has been successfully placed.</p>

    <!-- Order Details -->
    <div class="order-details">
      <h3>Order Summary</h3>
      <div v-for="item in orderItems" :key="item.id" class="order-item">
        <div class="order-item-info">
          <h4>{{ item.name }}</h4>
          <p>Quantity: {{ item.quantity }}</p>
          <p class="price">${{ (item.price * item.quantity).toFixed(2) }}</p>
        </div>
      </div>
    </div>

    <!-- Total Price -->
    <div class="order-total">
      <h3>Total Price: ${{ totalPrice.toFixed(2) }}</h3>
    </div>

    <!-- Shipping Details -->
    <div class="shipping-info">
      <h3>Shipping Information</h3>
      <div class="shipping-detail">
        <p><strong>Full Name:</strong> {{ shippingDetails.fullName }}</p>
        <p><strong>Address:</strong> {{ shippingDetails.address }}</p>
        <p><strong>City:</strong> {{ shippingDetails.city }}</p>
        <p><strong>Postal Code:</strong> {{ shippingDetails.postalCode }}</p>
      </div>
    </div>

    <!-- Track Order Button -->
    <router-link to="/order-tracking">
      <button class="track-button">Track Order</button>
    </router-link>
  </div>
</template>

<script>
export default {
  name: 'OrderConfirmationView',
  data() {
    return {
      orderItems: [
        {
          id: '5334c996-8457-4cf0-815c-ed2b77c4ff61',
          name: 'Huawei Plus',
          description: 'This phone is the company\'s biggest change to its flagship smartphone in years. It includes a borderless.',
          imageFile: 'product-3.png',
          price: 650.00,
          quantity: 1
        }
      ],
      shippingDetails: {
        fullName: 'Cindy',
        address: '123 Main Street',
        city: 'Singapore',
        postalCode: '123456'
      }
    };
  },
  computed: {
    totalPrice() {
      return this.orderItems.reduce((total, item) => total + (item.price * item.quantity), 0);  // Calculate total price
    }
  }
};
</script>

<style scoped>
.order-confirmation {
  margin-top: 100px;  /* Adjusted to ensure space between navbar and content */
  padding: 0 20px; /* Added padding for mobile responsiveness */
  margin-bottom: 50px; /* Added margin to match the home page's bottom spacing */
  text-align: left;
}

h1 {
  font-size: 2rem;
  color: #363d46;
  text-align: center;
  margin-bottom: 20px;
}

p {
  color: #363d46
}

.order-details, .order-total, .shipping-info {
  margin-top: 30px;
  margin-bottom: 20px;
}

.order-item {
  display: flex;
  margin-bottom: 15px;
  border-bottom: 1px solid #ddd;
  padding-bottom: 10px;
}

.order-item-info {
  flex-grow: 1;
}

.order-item-image {
  width: 80px;
  height: 80px;
  object-fit: cover;
  margin-right: 15px;
}

h3, h4 {
  font-size: 1.5rem;
  color: #363d46;
}

.price {
  font-weight: bold;
  color: #363d46;
}

.shipping-info .shipping-detail {
  margin-left: 20px;
}

.shipping-info p {
  font-size: 1.1rem;
  color: #363d46;
}

.track-button {
  padding: 12px 20px;
  background-color: #363d46;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-top: 30px;
  display: inline-block;
}

.track-button:hover {
  background-color: #2a333d;
}
</style>
