<template>
    <div class="cart">
      <h1>Your Shopping Cart</h1>
  
      <div v-if="cart.length === 0">
        <p>Your cart is empty. 😞</p>
        <router-link to="/products" class="shop-btn">Browse Products</router-link>
      </div>
  
      <div v-else class="cart-list">
        <div class="cart-item" v-for="(item, index) in cart" :key="index">
          <img :src="item.image" :alt="item.name" class="cart-image" />
          <div class="cart-details">
            <h2>{{ item.name }}</h2>
            <p>{{ item.description }}</p>
            <span class="price">${{ item.price.toFixed(2) }}</span>
          </div>
          <button @click="removeFromCart(index)" class="remove-btn">❌ Remove</button>
        </div>
  
        <button @click="clearCart" class="clear-btn">🛒 Clear Cart</button>
  
        <!-- Complete Checkout button -->
        <router-link to="/checkout" class="checkout-btn">Complete Checkout</router-link>
      </div>
    </div>
</template>
  
<script>
import cartStore from "@/store/cartStore";

export default {
  name: "CartView",
  data() {
    return {
      cart: cartStore.getCart()
    };
  },
  methods: {
    removeFromCart(index) {
      this.cart.splice(index, 1);
      sessionStorage.setItem("shoppingCart", JSON.stringify(this.cart));
      this.updateCartCount(); // Update cart count immediately
    },
    clearCart() {
      cartStore.clearCart();
      this.cart = [];
      this.updateCartCount();
    },
    updateCartCount() {
      this.$forceUpdate(); // Force Vue to re-render the cart component
    }
  }
};
</script>
  
<style scoped>
.cart {
  text-align: center;
  padding: 20px;
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

.remove-btn {
  background-color: #ff4d4d;
  color: white;
  padding: 8px 12px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-left: 15px;
}

.remove-btn:hover {
  background-color: #cc0000;
}

.clear-btn {
  background-color: #ff5733;
  color: white;
  padding: 10px 15px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-top: 20px;
}

.clear-btn:hover {
  background-color: #c70039;
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

/* Styling for the Checkout button */
.checkout-btn {
  background-color: #28a745;
  color: white;
  padding: 10px 20px;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-top: 20px;
}

.checkout-btn:hover {
  background-color: #218838;
}
</style>
