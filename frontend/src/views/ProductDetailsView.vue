<template>
  <div v-if="product" class="product-details">
    <h2>{{ product.name }}</h2>
    <p>{{ product.description }}</p>
    <p class="price">${{ product.price ? product.price.toFixed(2) : '0.00' }}</p>
    <p>Category: {{ product.category?.join(', ') }}</p>
    <button @click="addToCart">Add to Cart</button>
  </div>
  <p v-else>Loading product details...</p>
</template>

<script>
import axios from 'axios';
import { cartStore } from '@/store/cartStore'; // Import the cart store

export default {
  name: 'ProductDetailsView',
  data() {
    return {
      product: null, // Initialize as null
    };
  },
  created() {
    this.fetchProduct();
  },
  methods: {
    async fetchProduct() {
      const productId = this.$route.params.id;
      try {
        const response = await axios.get(`https://localhost:6060/products/${productId}`);
        this.product = response.data.product || {};
      } catch (error) {
        console.error('Error fetching product:', error);
      }
    },
    addToCart() {
      if (this.product) {
        cartStore.addToCart(this.product);
        console.log("Cart after adding:", cartStore.getCart());
      }
    }
  }
};
</script>

<style scoped>
.product-details {
  margin-top: 100px;
  text-align: center;
  padding: 0 20px;
  margin-bottom: 50px;
}

.price {
  font-size: 1.2rem;
  color: #363d46;
  font-weight: bold;
}

button {
  padding: 10px;
  background-color: #363d46;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

button:hover {
  background-color: #2a333d;
}
</style>
