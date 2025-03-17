<template>
  <nav>
    <!-- Shop & Go Logo on the left -->
    <div class="logo">
      <img src="@/assets/logo.png" alt="Shop & Go Logo" class="logo-img" />
    </div>

    <!-- Navigation links on the right -->
    <div class="nav-links">
      <router-link to="/" class="nav-link">Home</router-link>
      <router-link to="/products" class="nav-link">Products</router-link>
      <router-link to="/cart" class="cart-link">
        Cart <span class="cart-count">{{ cartCount }}</span>
      </router-link>
    </div>
  </nav>
  <router-view />
</template>

<script>
import { cartStore } from '@/store/cartStore';

export default {
  name: 'NavBar',
  computed: {
    cartCount() {
      return cartStore.getCartCount(); // Get cart count dynamically
    }
  }
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

nav {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 5px 20px;
  width: 100%;
  position: fixed;
  top: 0;
  left: 0;
  background-color: white;
  z-index: 1000;
  box-sizing: border-box;
}

.logo {
  display: flex;
  justify-content: flex-start;
  align-items: center;
  margin-right: 3px;
}

.logo-img {
  max-height: 90px;
  width: auto;
}

.nav-links {
  display: flex;
  gap: 10px;
  justify-content: flex-end;
  flex-grow: 1;
  align-items: center;
}

.nav-link, .cart-link {
  font-weight: bold;
  color: #363d46;
  text-decoration: none;
  position: relative;
  padding: 5px 8px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.cart-link {
  display: flex;
  align-items: center;
  justify-content: center;
}

.cart-link span {
  display: flex;
  justify-content: center;
  align-items: center;
  font-weight: bold;
  color: white;
  background-color: #363d46;
  border-radius: 50%;
  width: 30px;
  height: 30px;
  font-size: 14px;
  margin-left: 5px;
  text-align: center;
}

.nav-link::after, .cart-link::after {
  content: '';
  position: absolute;
  width: 100%;
  height: 2px;
  bottom: -5px;
  left: 0;
  background-color: #363d46;
  opacity: 0;
  transition: opacity 0.3s ease;
}

.nav-link:hover::after, .cart-link:hover::after {
  opacity: 1;
}
</style>