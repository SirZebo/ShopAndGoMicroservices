<template>
  <div class="reviews-container">
    <h1>Your Reviews</h1>

    <!-- Show loading message while fetching data -->
    <p v-if="loading">Loading reviews...</p>

    <!-- Display reviews only when data is loaded -->
    <div v-else>
      <div class="review" v-for="review in reviews" :key="review.id">
        <h3>{{ review.body }}</h3>
        <p><strong>Order Item:</strong> {{ getProductName(review.order.productId) }}</p>
        <p><strong>Order Quantity:</strong> {{ review.order.quantity }}</p>
        
        <!-- Show feedback status -->
        <p>
          <strong>Feedback Status:</strong> 
          <span :class="getFeedbackClass(review.feedbackStatus)">
            {{ getFeedbackStatus(review.feedbackStatus) }}
          </span>
        </p>

        <!-- Disable button if feedbackStatus is already set to Complain or Satisfied -->
        <button 
          @click="viewReview(review.id)" 
          :disabled="review.feedbackStatus !== 1"
        >
          {{ review.feedbackStatus === 1 ? "View Review" : "Already Reviewed" }}
        </button>
      </div>

      <!-- If no reviews are available -->
      <p v-if="!loading && reviews.length === 0">No reviews available.</p>
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
      products: [],
      loading: true, // Added loading state
    };
  },
  async beforeMount() {
    await this.fetchData();
  },
  methods: {
    async fetchData() {
      try {
        const [reviewsResponse, productsResponse] = await Promise.all([
          axios.get("https://localhost:6065/reviews/customer/189dc8dc-990f-48e0-a37b-e6f2b60b9d7d?pageNumber=1&pageSize=5"),
          axios.get("https://localhost:6060/products?pageNumber=1&pageSize=10"),
        ]);

        this.reviews = reviewsResponse.data.reviews || [];
        this.products = productsResponse.data.products || [];
        console.log(this.reviews);
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
    getFeedbackStatus(status) {
      if (status === 1) return "Pending Review";
      if (status === 2) return "Complain";
      if (status === 3) return "Satisfied";
      return "Unknown";
    },
    getFeedbackClass(status) {
      return {
        'pending': status === 1,
        'complain': status === 2,
        'satisfied': status === 3
      };
    },
    viewReview(id) {
      this.$router.push(`/enduser/review/${id}`);
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

button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

button:hover:not(:disabled) {
  background-color: #2a333d;
}

/* Feedback Status Styling */
.pending {
  color: orange;
  font-weight: bold;
}

.complain {
  color: red;
  font-weight: bold;
}

.satisfied {
  color: green;
  font-weight: bold;
}
</style>
