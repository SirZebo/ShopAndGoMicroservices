<template>
  <div class="products">
    <div class="product-list">
      <!-- Loop through the product data -->
      <div v-for="product in products" :key="product.id" class="product-item">
        <img :src="`/assets/${product.imageFile}`" :alt="product.name" class="product-image" />
        <div class="product-info">
          <h3>{{ product.name }}</h3>
          <p>{{ product.description }}</p>
          <p class="price">${{ product.price.toFixed(2) }}</p>
          <button @click="viewProduct(product.id)">View Details</button>
        </div>
      </div>
    </div>
    <!-- If no products, show a message -->
    <p v-if="products.length === 0">No products available at the moment.</p>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'ProductsView',
  data() {
    return {
      products: [] // Initialize as an empty array
    };
  },
  created() {
    this.fetchProducts();
  },
  methods: {
    async fetchProducts() {
      try {
        const response = await axios.get('https://localhost:6060/products?pageNumber=1&pageSize=10');
        console.log('Fetched products:', response.data); // Debugging log
        this.products = response.data.products || []; // Ensure it's an array
      } catch (error) {
        console.error('Error fetching products:', error);
      }
    },
    viewProduct(id) {
      this.$router.push({ name: 'ProductDetailsView', params: { id } });
    }
  }
};

</script>

<style scoped>
.products {
  margin-top: 100px;  /* Adjusted to ensure space between navbar and content */
  padding: 0 20px; /* Added padding for mobile responsiveness */
  margin-bottom: 50px; /* Added margin to match the home page's bottom spacing */
}

.product-list {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 20px;
}

.product-item {
  border: 1px solid #ddd;
  padding: 20px;
  text-align: center;
  background-color: #f9f9f9;
}

.product-image {
  width: 100%;
  height: 200px;
  object-fit: cover;
}

.product-info {
  margin-top: 10px;
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

p {
  text-align: center;
  color: #363d46;
  font-size: 1.2rem;
}
</style>