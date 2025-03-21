<template>
  <div class="cart">
    <div v-if="cartItems.length === 0">
      <p>Shopping cart is empty.</p>
    </div>
    
    <div v-else>
      <div class="cart-item" v-for="item in cartItems" :key="item.id">
        <div class="cart-item-info">
          <img :src="`/assets/${item.imageFile}`" alt="product name" class="cart-item-image" />
          <div>
            <h3>{{ item.name }}</h3>
            <p>{{ item.description }}</p>
            <p class="price">${{ item.price.toFixed(2) }}</p>
            <div class="quantity">
              <button @click="decreaseQuantity(item)">-</button>
              <span>{{ item.quantity }}</span>
              <button @click="increaseQuantity(item)">+</button>
            </div>
          </div>
        </div>
        <button @click="removeFromCart(item)" class="remove-button">
          <i class="fa fa-trash"></i> <!-- Bin icon -->
        </button>
      </div>

      <div class="cart-total">
        <h3>Total: ${{ totalPrice.toFixed(2) }}</h3>
        <router-link to="/enduser/checkout">
          <button>Checkout</button>
        </router-link>
      </div>
    </div>
  </div>
</template>
  
<script>
import { cartStore } from '@/store/cartStore';

export default {
  name: 'CartView',
  computed: {
    cartItems() {
      return cartStore.getCart();  // Get the cart items from the store
    },
    totalPrice() {
      return this.cartItems.reduce((total, item) => total + (item.price * item.quantity), 0);  // Calculate the total price
    }
  },
  methods: {
    removeFromCart(item) {
      const index = this.cartItems.findIndex(cartItem => cartItem.id === item.id);
      if (index !== -1) {
        this.cartItems.splice(index, 1);  // Remove the item from the cart
      }
    },
    increaseQuantity(item) {
      item.quantity += 1;  // Increase the item quantity by 1
    },
    decreaseQuantity(item) {
      if (item.quantity > 1) {
        item.quantity -= 1;  // Decrease the item quantity by 1, but not below 1
      }
    }
  }
};
</script>

<style scoped>
.cart {
  margin-top: 100px;  /* Adjusted to ensure space between navbar and content */
  padding: 0 20px; /* Added padding for mobile responsiveness */
  margin-bottom: 50px; /* Added margin to match the home page's bottom spacing */
}

.cart-item {
  display: flex;
  justify-content: space-between;
  margin-bottom: 20px;
  padding: 15px;
  border: 1px solid #ddd;
  background-color: #f9f9f9;
}

.cart-item-info {
  display: flex;
}

.cart-item-image {
  width: 100px;
  height: 100px;
  object-fit: cover;
  margin-right: 15px;
}

.cart-item-info div {
  flex-grow: 1;
}

.price {
  font-size: 1.2rem;
  font-weight: bold;
  color: #363d46;
}

.quantity {
  display: flex;
  align-items: center;
  gap: 10px;
}

button {
  padding: 10px;
  background-color: #363d46;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.remove-button {
  background-color: transparent;
  border: none;
  cursor: pointer;
  color: #363d46; 
  font-size: 18px;
}

.cart-total {
  text-align: right;
  margin-top: 20px;
}

.cart-total h3 {
  font-size: 1.5rem;
  color: #363d46;
  font-weight: bold;
}

.cart-total button {
  padding: 10px 20px;
  background-color: #363d46;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.cart-total button:hover {
  background-color: #2a333d;
}
</style>