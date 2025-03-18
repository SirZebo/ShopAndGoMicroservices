<template>
  <div class="review-page">
    <h1>Review Details</h1>
    <h2>Review Status: {{ reviewStatusText }}</h2>

    <!-- Show review content -->
    <p><strong>Review Body:</strong> {{ review.body }}</p>
    <p><strong>Order Item:</strong> {{ getProductName(review.order.productId) }}</p>
    <p><strong>Order Quantity:</strong> {{ review.order.quantity }}</p>

    <!-- Review Form -->
    <div v-if="!reviewSubmitted" class="review-form">
      <textarea v-model="review.body" :disabled="reviewSubmitted" placeholder="Update your review here..." rows="5"></textarea>
      <button @click="submitReview" :disabled="!isFormValid">Update Review</button>
    </div>

    <div v-if="reviewSubmitted" class="success-message">
      <p>Your review has been updated.</p>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'ReviewView',
  data() {
    return {
      review: {
        id: '',
        body: '',
        order: {
          productId: '',
          quantity: 0
        }
      },
      products: [],
      reviewSubmitted: false
    };
  },
  computed: {
    reviewStatusText() {
      return this.review.feedbackStatus === 1 ? "Complaint" : "Satisfied";
    },
    isFormValid() {
      return this.review.body.trim().length > 0;
    }
  },
  async created() {
    const reviewId = this.$route.params.id;
    await this.fetchReview(reviewId);
    await this.fetchProducts();
  },
  methods: {
    async fetchReview(reviewId) {
      try {
        const response = await axios.get(`https://localhost:6065/reviews/${reviewId}`);
        this.review = response.data.review;
      } catch (error) {
        console.error("Error fetching review:", error);
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
    async submitReview() {
      try {
        const hardcodedReview = {
          id: "2d517904-c141-47cf-9ba2-6eb5f015570f",
          feedbackStatus: 2,
          body: "Bad review"
        };

        const response = await axios.put(`https://localhost:6065/reviews`, hardcodedReview);

        // Store response message
        this.reviewResponse = `Success: ${response.detail}`;
      } catch (error) {
        // Capture error message
        this.reviewResponse = `Error: ${error.response?.status || 'Unknown'}`;
      } finally {
        // Redirect to review-list no matter what
        this.$router.push('/review-list');
      }
    }




  }
};
</script>

<style scoped>
.review-page {
  margin-top: 100px;
  padding: 0 20px;
  font-family: Arial, sans-serif;
}

h1, h2 {
  color: #363d46;
}

h1 {
  text-align: center;
  margin-bottom: 20px;
}

.review-form {
  margin-top: 20px;
}

textarea {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  margin-bottom: 20px;
}

button {
  padding: 10px;
  background-color: #363d46;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

button:hover:not(:disabled) {
  background-color: #2a333d;
}

.success-message {
  color: #27ae60;
  text-align: center;
  margin-top: 20px;
}
</style>
