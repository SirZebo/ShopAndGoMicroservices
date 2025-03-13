<template>
  <div class="checkout">
    <h1>Checkout</h1>
    <div v-if="cart.length === 0">
      <p>Your cart is empty. 😞</p>
      <router-link to="/products" class="shop-btn">Browse Products</router-link>
    </div>

    <div v-else>
      <div class="cart-list">
        <div class="cart-item" v-for="(item, index) in cart" :key="index">
          <img :src="item.image" :alt="item.name" class="cart-image" />
          <div class="cart-details">
            <h2>{{ item.name }}</h2>
            <p>{{ item.description }}</p>
            <span class="price">${{ item.price.toFixed(2) }}</span>
          </div>
        </div>
      </div>

      <div class="total">
        <h3>Total Price: ${{ totalPrice.toFixed(2) }}</h3>
      </div>

      <form @submit.prevent="submitOrder">
        <h3>Billing & Shipping Information</h3>

        <div class="form-group">
          <label for="fullName">Full Name</label>
          <input
            type="text"
            id="fullName"
            v-model="form.fullName"
            required
            placeholder="Enter your full name"
            :class="{'error': formErrors.fullName}"
          />
          <span v-if="formErrors.fullName" class="error-message">{{ formErrors.fullName }}</span>
        </div>

        <div class="form-group">
          <label for="email">Email Address</label>
          <input
            type="email"
            id="email"
            v-model="form.email"
            required
            placeholder="Enter your email"
            :class="{'error': formErrors.email}"
          />
          <span v-if="formErrors.email" class="error-message">{{ formErrors.email }}</span>
        </div>

        <div class="form-group">
          <label for="phone">Phone Number</label>
          <input
            type="text"
            id="phone"
            v-model="form.phone"
            required
            placeholder="Enter your phone number"
            :class="{'error': formErrors.phone}"
          />
          <span v-if="formErrors.phone" class="error-message">{{ formErrors.phone }}</span>
        </div>

        <div class="form-group">
          <label for="shippingAddress">Shipping Address</label>
          <input
            type="text"
            id="shippingAddress"
            v-model="form.shippingAddress"
            required
            placeholder="Enter your shipping address"
            :class="{'error': formErrors.shippingAddress}"
          />
          <span v-if="formErrors.shippingAddress" class="error-message">{{ formErrors.shippingAddress }}</span>
        </div>

        <div class="form-group">
          <label for="billingAddress">Billing Address</label>
          <input
            type="text"
            id="billingAddress"
            v-model="form.billingAddress"
            required
            placeholder="Enter your billing address"
            :class="{'error': formErrors.billingAddress}"
          />
          <span v-if="formErrors.billingAddress" class="error-message">{{ formErrors.billingAddress }}</span>
        </div>

        <button type="submit" class="checkout-btn" :disabled="isFormInvalid">Complete Checkout</button>
      </form>
    </div>
  </div>
</template>
  
<script>
import cartStore from "@/store/cartStore";

export default {
  name: "CheckoutView",
  data() {
    return {
      cart: cartStore.getCart(),
      form: {
        fullName: "",
        email: "",
        phone: "",
        shippingAddress: "",
        billingAddress: ""
      },
      formErrors: {
        fullName: "",
        email: "",
        phone: "",
        shippingAddress: "",
        billingAddress: ""
      }
    };
  },
  computed: {
    totalPrice() {
      return this.cart.reduce((total, item) => total + item.price, 0);
    },
    isFormInvalid() {
      // Check if any field has errors or is empty
      return Object.values(this.formErrors).some(error => error !== "") || Object.values(this.form).some(value => value === "");
    }
  },
  methods: {
    validateForm() {
      this.formErrors = {}; // Reset errors
      let isValid = true;

      // Validate Full Name
      if (!this.form.fullName) {
        this.formErrors.fullName = "Full name is required.";
        isValid = false;
      }

      // Validate Email
      const emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
      if (!this.form.email || !emailPattern.test(this.form.email)) {
        this.formErrors.email = "Valid email is required.";
        isValid = false;
      }

      // Validate Phone Number (just digits for now)
      const phonePattern = /^[0-9]+$/;
      if (!this.form.phone || !phonePattern.test(this.form.phone)) {
        this.formErrors.phone = "Phone number should contain only digits.";
        isValid = false;
      }

      // Validate Shipping Address
      if (!this.form.shippingAddress) {
        this.formErrors.shippingAddress = "Shipping address is required.";
        isValid = false;
      }

      // Validate Billing Address
      if (!this.form.billingAddress) {
        this.formErrors.billingAddress = "Billing address is required.";
        isValid = false;
      }

      return isValid;
    },

    submitOrder() {
      if (this.validateForm()) {
        console.log("Order Submitted", this.form);
        // You can now submit this form data to the backend for processing
        alert("Order submitted!");
        this.$router.push("/order-confirmation");
      } else {
        console.log("Form validation failed");
      }
    }
  }
};
</script>

<style scoped>
.checkout {
  text-align: center;
  padding: 40px;
}

.cart-list {
  display: flex;
  flex-direction: column;
  gap: 20px;
  margin-top: 20px;
}

.cart-item {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 15px;
  border: 1px solid #ddd;
  border-radius: 5px;
  background: #fff;
  box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
  text-align: left;
}

.cart-image {
  width: 80px;
  height: 80px;
  border-radius: 5px;
  object-fit: cover;
  margin-right: 15px;
}

.cart-details {
  flex-grow: 1;
}

.price {
  font-weight: bold;
  color: #007bff;
}

.total {
  margin-top: 20px;
  font-size: 18px;
  font-weight: bold;
}

.checkout-btn {
  background-color: #28a745;
  color: white;
  padding: 15px 30px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-top: 20px;
  width: 100%;
  font-size: 18px;
}

.checkout-btn:hover {
  background-color: #218838;
}

.form-group {
  text-align: left;
  margin-top: 15px;
  display: flex;
  flex-direction: column;
}

.form-group label {
  font-weight: bold;
  font-size: 16px;
}

.form-group input {
  padding: 12px;
  border: 1px solid #ddd;
  border-radius: 5px;
  font-size: 16px;
  margin-top: 5px;
  width: 100%;
  box-sizing: border-box;
}

.form-group input:focus {
  outline: none;
  border-color: #007bff;
}

.error {
  border-color: red;
}

.error-message {
  color: red;
  font-size: 12px;
  margin-top: 5px;
}

.shop-btn {
  display: inline-block;
  margin-top: 20px;
  padding: 10px 15px;
  background-color: #007bff;
  color: white;
  text-decoration: none;
  border-radius: 5px;
}

.shop-btn:hover {
  background-color: #0056b3;
}
</style>