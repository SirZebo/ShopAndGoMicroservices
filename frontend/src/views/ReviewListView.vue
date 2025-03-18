<template>
  <div class="reviews-container">
    <h1>Your Reviews</h1>
    <div class="review" v-for="review in reviews" :key="review.id">
      <h3>{{ review.body }}</h3>
      <p><strong>Order Item:</strong> {{ getProductName(review.order.productId) }}</p>
      <p><strong>Order Quantity:</strong> {{ review.order.quantity }}</p>
      <button @click="viewReview(review.id)">View Review</button>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'ReviewList',
  data() {
    return {
      reviews: [],
      products: []
    };
  },
  async created() {
    await this.fetchReviews();
    await this.fetchProducts();
  },
  methods: {
    async fetchReviews() {
      try {
        const response = await axios.get("https://localhost:6065/reviews/customer/58c49479-ec65-4de2-86e7-033c546291aa?pageNumber=1&pageSize=5");
        this.reviews = response.data.reviews;
      } catch (error) {
        console.error("Error fetching reviews:", error);
      }
    },
    async fetchProducts() {
      try {
        const response = await axios.get("https://localhost:6060/products?pageNumber=1&pageSize=10");
        this.products = response.data.products;
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    },
    getProductName(productId) {
      const product = this.products.find(p => p.id === productId);
      return product ? product.name : "Unknown Product";
    },
    viewReview(id) {
      this.$router.push(`/review/${id}`);
    }
  }
};
</script>

<style scoped>
.reviews-container {
  margin-top: 100px;
  padding: 0 20px;
  font-family: Arial, sans-serif;
}

h1 {
  color: #363d46;
  margin-bottom: 20px;
  text-align: center;
}

.review {
  border-bottom: 1px solid #ccc;
  padding-bottom: 10px;
  margin-bottom: 10px;
}

.review h3 {
  margin: 0;
  color: #363d46;
  margin-bottom: 5px;
}

.review p {
  margin: 0;
  font-size: 1rem;
  color: #363d46;
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
