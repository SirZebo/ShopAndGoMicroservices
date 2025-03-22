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
        <label for="firstname">First Name</label>
        <input type="text" id="firstname" v-model="shippingInfo.firstName" required />
      </div>

      <div class="form-group">
        <label for="lastname">Last Name</label>
        <input type="text" id="lastname" v-model="shippingInfo.lastName" required />
      </div>

      <div class="form-group">
        <label for="emailaddress">Email Address</label>
        <input type="text" id="emailaddress" v-model="shippingInfo.emailAddress" required />
      </div>

      <div class="form-group">
        <label for="addressLine">Address</label>
        <input type="text" id="addressLine" v-model="shippingInfo.addressLine" required />
      </div>

      <div class="form-group">
        <label for="country">Country</label>
        <input type="text" id="country" v-model="shippingInfo.country" required />
      </div>

      <div class="form-group">
        <label for="state">State</label>
        <input type="text" id="state" v-model="shippingInfo.state" required />
      </div>

      <div class="form-group">
        <label for="postalCode">Postal Code</label>
        <input type="text" id="postalCode" v-model="shippingInfo.postalCode" required />
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
        firstName: '',
        lastName: '',
        addressLine: '',
        postalCode: '',
        country: '',
        state: ''
      },
      paymentInfo: {
        creditCard: ''
      },
      userName: 'Test',
      maxCompletionTime: '3.00:00:00',
      language: 'Malay',
      emailAddress: ''
    };
  },
  computed: {
    cartItems() {
      return cartStore.getCart();
    },
    totalPrice() {
      return this.cartItems.reduce((total, item) => total + item.price * item.quantity, 0);
    },
    cartItemsWithIds() {
      return this.cartItems.map(item => ({
        productId: item.id,
        price: item.price,
        quantity: item.quantity
      }));
    }
  },
  methods: {
    async submitOrder() {
      try {
        const orderData = {
          CheckoutProductDto: {
            userName: this.userName,
            CustomerId: '189dc8dc-990f-48e0-a37b-e6f2b60b9d7d',
            ProductId: '4f136e9f-ff8c-4c1f-9a33-d12f689bdab8',
            MerchantId: '189dc8dc-990f-48e0-a37b-e6f2b60b9d7d',
            Price: this.totalPrice,
            Quantity: this.cartItems.reduce((total, item) => total + item.quantity, 0),
            totalPrice: this.totalPrice,
            MaxCompletionTime: this.maxCompletionTime,
            Lanugage: this.language,
            firstName: this.shippingInfo.firstName,
            lastName: this.shippingInfo.lastName,
            emailAddress: this.shippingInfo.emailAddress,
            addressLine: this.shippingInfo.addressLine,
            country: this.shippingInfo.country,
            state: this.shippingInfo.state,
            zipCode: this.shippingInfo.postalCode
          }
        };

        const response = await axios.post('https://localhost:6060/product/checkout', orderData);
        const transactionToken = response.data.transactionToken;

        // Store transaction token and order details
        localStorage.setItem('transactionToken', transactionToken);
        localStorage.setItem('orderDetails', JSON.stringify({
          name: this.cartItems[0].name,
          quantity: this.cartItems.reduce((total, item) => total + item.quantity, 0),
          price: this.totalPrice
        }));

        // Redirect to Order Status page
        this.$router.push({ path: '/enduser/order-status', query: { transactionToken } });
      } catch (error) {
        console.error('Checkout failed:', error);
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
