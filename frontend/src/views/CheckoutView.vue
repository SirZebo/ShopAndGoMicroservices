<template>
  <div class="checkout">
    <h1>Cart Summary</h1>
    <div class="cart-summary">
      <div class="cart-item" v-for="item in cartItems" :key="item.id">
        <h3>{{ item.name }}</h3>
        <p>{{ item.quantity }} x ${{ item.price.toFixed(2) }}</p>
      </div>
      <div class="total-price">
        <h3>Total Price: ${{ totalPrice.toFixed(2) }}</h3>
      </div>
    </div>

    <h2>Shipping Information</h2>
    <form @submit.prevent="submitOrder">
      <div class="form-group">
        <label for="fullName">Full Name</label>
        <input type="text" id="fullName" v-model="shippingInfo.fullName" required />
      </div>

      <div class="form-group">
        <label for="address">Address</label>
        <input type="text" id="address" v-model="shippingInfo.address" required />
      </div>

      <div class="form-group">
        <label for="city">City</label>
        <input type="text" id="city" v-model="shippingInfo.city" required />
      </div>

      <div class="form-group">
        <label for="postalCode">Postal Code</label>
        <input type="text" id="postalCode" v-model="shippingInfo.postalCode" required />
      </div>

      <h2>Payment Information</h2>
      <div class="form-group">
        <label for="creditCard">Credit Card</label>
        <input type="text" id="creditCard" v-model="paymentInfo.creditCard" required />
      </div>

      <div class="form-actions">
        <button type="submit" class="place-order-button">Place Order</button>
      </div>
    </form>
  </div>
</template>

<script>
import { cartStore } from '@/store/cartStore';
import axios from 'axios';

export default {
  name: 'CheckoutView',
  data() {
    return {
      shippingInfo: {
        fullName: '',
        address: '',
        city: '',
        postalCode: ''
      },
      paymentInfo: {
        creditCard: ''
      }
    };
  },
  computed: {
    cartItems() {
      return cartStore.getCart(); // Get the cart items
    },
    totalPrice() {
      return this.cartItems.reduce((total, item) => total + (item.price * item.quantity), 0); // Calculate the total price
    }
  },
  methods: {
    async submitOrder() {
      const orderDetails = {
        items: this.cartItems,
        shippingInfo: this.shippingInfo,
        paymentInfo: this.paymentInfo
      };

      try {
        // Make an API call to create an order and get the checkout URL
        const response = await axios.post('https://your-backend-api.com/createOrder', orderDetails);

        // Check if the checkout URL exists in the response
        if (response.data.checkout_url) {
          // Redirect user to the payment link (checkout URL)
          window.location.href = response.data.checkout_url;
        }
      } catch (error) {
        console.error('Error placing order:', error);
        alert('There was an error processing your order. Please try again.');
      }
    }
  }
};
</script>

<style scoped>
.checkout {
  margin-top: 100px;  /* Adjusted to ensure space between navbar and content */
  padding: 0 20px; /* Added padding for mobile responsiveness */
  margin-bottom: 50px; /* Added margin to match the home page's bottom spacing */
  font-family: Arial, sans-serif;
}

h1 {
  color: #363d46;
  margin-bottom: 20px;
  text-align: center;
}

h2 {
  color: #363d46;
  margin-bottom: 20px;
  text-align: left;
}

.cart-summary {
  margin-bottom: 30px;
}

.cart-item {
  margin-bottom: 10px;
}

.total-price {
  margin-top: 20px;
  font-weight: bold;
}

.form-group {
  margin-bottom: 15px;
}

.form-group label {
  display: block;
  font-size: 1rem;
  color: #363d46;
}

.form-group input {
  width: 100%;
  padding: 8px;
  font-size: 1rem;
  border: 1px solid #ddd;
  border-radius: 5px;
}

.form-actions {
  margin-top: 20px;
  text-align: left; /* Align the button to the left */
}

.place-order-button {
  padding: 12px 20px;
  background-color: #363d46;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 1rem;
}

.place-order-button:hover {
  background-color: #2a333d;
}
</style>
