<template>
  <nav>
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link> |
    <router-link to="/products">Products</router-link> |
    <router-link to="/cart" class="cart-link">
      🛒 Cart (<span>{{ cartCount }}</span>)
    </router-link>
  </nav>
  <router-view />
</template>

<script>
import cartStore from "@/store/cartStore";

export default {
  name: "App",
  computed: {
    cartCount() {
      return cartStore.getCart().length;  // Dynamically calculate cart count
    }
  },
  mounted() {
    // Listen for cart updates
    window.addEventListener("cart-updated", this.updateCartCount);
  },
  beforeUnmount() {
    window.removeEventListener("cart-updated", this.updateCartCount);
  },
  methods: {
    updateCartCount() {
      // This method is not needed anymore as computed property handles it
    }
  }
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

nav {
  padding: 20px;
}

nav a {
  font-weight: bold;
  color: #2c3e50;
  margin: 0 10px;
}

nav a.router-link-exact-active {
  color: #42b983;
}

.cart-link {
  font-weight: bold;
  color: #007bff;
}

.cart-link span {
  font-weight: bold;
  color: #ff5733;
}
</style>
