<template>
  <div class="review-page">
    <h2>Review Your Order</h2>

    <!-- Display Order Summary -->
    <div class="order-summary">
      <h3>Order Summary</h3>
      <div class="order-details">
        <img :src="`/assets/${productImage}`" alt="product name" class="order-image" />
        <div>
          <h4>{{ productName }}</h4>
          <p>{{ productDescription }}</p>
          <p class="price">${{ productPrice }}</p>
        </div>
      </div>
    </div>

    <!-- Review Form -->
    <div class="review-form">
      <h3>Leave Your Review</h3>
      <label for="rating">Rating:</label>
      <select v-model="rating" id="rating">
        <option>1 Star</option>
        <option>2 Stars</option>
        <option>3 Stars</option>
        <option>4 Stars</option>
        <option>5 Stars</option>
      </select>
      <textarea v-model="feedback" placeholder="Share your thoughts..." rows="5"></textarea>
      <button @click="submitReview" class="submit-button">Submit Review</button>

      <!-- Success Message -->
      <div v-if="reviewSubmitted" class="success-message">
        <p>Review submitted!</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "ReviewView",
  data() {
    return {
      productName: "Huawei Plus",
      productDescription: "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless design.",
      productPrice: 650.00,
      productImage: "product-3.png",
      rating: "5 Stars", // Default rating
      feedback: "", // Default feedback is empty
      reviewSubmitted: false, // To track if the review is submitted
    };
  },
  methods: {
    submitReview() {
      // Handle review submission
      this.reviewSubmitted = true;  // Show success message after submission
      // You can make an API call here to submit the review to the backend if necessary
    }
  }
};
</script>

<style scoped>
.review-page {
  margin-top: 100px;  /* Adjusted to ensure space between navbar and content */
  padding: 0 20px;
  margin-bottom: 50px; /* Added margin to match the home page's bottom spacing */
}

h2 {
  font-size: 2rem;
  color: #363d46;
  text-align: center;
  margin-bottom: 20px;
}

.order-summary {
  margin-bottom: 40px;
}

.order-summary h3 {
  font-size: 1.5rem;
  color: #363d46;
}

.order-details {
  display: flex;
  align-items: center;
  margin-top: 20px;
}

.order-details .order-image {
  width: 120px;
  height: 120px;
  object-fit: cover;
  margin-right: 20px;
}

.review-form {
  max-width: 600px;
  margin-left: 0; /* Align form to the left */
}

.review-form h3 {
  font-size: 1.5rem;
  color: #363d46;
  margin-bottom: 20px;
}

.review-form label {
  font-weight: bold;
  margin-bottom: 10px;
  display: block;
}

.review-form select,
.review-form textarea {
  width: 100%;
  padding: 10px;
  margin-bottom: 20px;
  border: 1px solid #ddd;
  border-radius: 5px;
  font-size: 1rem;
}

.review-form .submit-button {
  padding: 10px 20px;
  background-color: #363d46;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
}

.review-form .submit-button:hover {
  background-color: #2a333d;
}

.success-message {
  margin-top: 20px;
  color: #363d46;
  font-size: 1.2rem;
  text-align: left;
}
</style>
