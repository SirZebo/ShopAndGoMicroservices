<template>
  <div class="review-page">
    <h1>Review Details</h1>

    <!-- Show loading state -->
    <p v-if="loading">Loading review...</p>

    <!-- Show review content when data is loaded -->
    <div v-else>
      <h2>Review Status: {{ reviewStatusText }}</h2>

      <p><strong>Review Body:</strong> {{ review.body }}</p>
      <p><strong>Order Item:</strong> {{ getProductName(review.order.productId) }}</p>
      <p><strong>Order Quantity:</strong> {{ review.order.quantity }}</p>

      <!-- Review Form -->
      <div v-if="!reviewSubmitted" class="review-form">
        <textarea v-model="review.body" :disabled="reviewSubmitted" placeholder="Update your review here..." rows="5"></textarea>
        <button @click="submitReview" :disabled="!isFormValid">Update Review</button>
      </div>

      <!-- Success message -->
      <div v-if="reviewSubmitted" class="success-message">
        <p>Your review has been updated.</p>
      </div>
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
        feedbackStatus: 0, // Default to prevent undefined errors
        order: {
          productId: '',
          quantity: 0
        }
      },
      products: [],
      loading: true, // Added loading state
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
  async beforeMount() {
    await this.fetchData();
  },
  methods: {
    async fetchData() {
      try {
        const reviewId = this.$route.params.id;
        const [reviewResponse, productsResponse] = await Promise.all([
          axios.get(`https://localhost:6065/reviews/${reviewId}`),
          axios.get("https://localhost:6060/products?pageNumber=1&pageSize=10"),
        ]);

        this.review = reviewResponse.data.review || this.review;
        this.products = productsResponse.data.products || [];
      } catch (error) {
        console.error("Error fetching data:", error);
      } finally {
        this.loading = false; // Hide loading indicator
      }
    },
    getProductName(productId) {
      const product = this.products.find(p => p.id === productId);
      return product ? product.name : "Unknown Product";
    },
    async submitReview() {
      try {
        const updatedReview = {
          id: this.review.id,
          feedbackStatus: this.review.feedbackStatus,
          body: this.review.body
        };

        await axios.put(`https://localhost:6065/reviews`, updatedReview);
        this.reviewSubmitted = true;

        setTimeout(() => {
          this.$router.push('/review-list'); // Redirect after update
        }, 1500);
      } catch (error) {
        console.error("Error updating review:", error);
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
