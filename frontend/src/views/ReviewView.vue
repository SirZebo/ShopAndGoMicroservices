<template>
  <div class="review-page">
    <h1>Review Details</h1>
    <h2>Review Status: {{ reviewStatusText }}</h2>

    <!-- Radio buttons for review status -->
    <div v-if="!reviewSubmitted" class="review-status">
      <input type="radio" id="uncompleted" value="1" v-model="review.status">
      <label for="uncompleted">Uncompleted</label>
      <input type="radio" id="complaint" value="2" v-model="review.status">
      <label for="complaint">Complaint</label>
      <input type="radio" id="satisfied" value="3" v-model="review.status">
      <label for="satisfied">Satisfied</label>
    </div>

    <!-- Description form -->
    <div class="review-form">
      <textarea v-model="review.description" :disabled="reviewSubmitted" placeholder="Please provide details here..." rows="5" required></textarea>
      <div class="button-group">
        <button v-if="review.status === '1' && !reviewSubmitted" @click="editReview" class="edit-button">Edit Review</button>
        <button v-if="!reviewSubmitted" @click="submitReview" class="submit-button">Submit Review</button>
      </div>
    </div>

    <!-- Success Message -->
    <div v-if="reviewSubmitted" class="success-message">
      <p>Your review has been submitted and can no longer be edited.</p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ReviewView',
  data() {
    return {
      review: {
        status: '1', // Default to Uncompleted
        description: '' // User feedback
      },
      reviewSubmitted: false // To track if the review is submitted
    };
  },
  computed: {
    reviewStatusText() {
      switch (this.review.status) {
        case '1': return 'Uncompleted';
        case '2': return 'Complaint';
        case '3': return 'Satisfied';
        default: return 'Unknown'; // Fallback if status is undefined
      }
    }
  },
  methods: {
    editReview() {
      // Logic to enable editing the review
      alert("You can now edit your review.");
    },
    submitReview() {
      if (!this.review.description.trim()) {
        alert("The description is required. Please fill it in before submitting.");
        return;
      }
      this.reviewSubmitted = true;
      // Implement API call here to submit the review to the backend
    }
  }
};
</script>

<style scoped>
.review-page {
  margin-top: 100px; /* Adjusted to ensure space between navbar and content */
  padding: 0 20px; /* Added padding for mobile responsiveness */
  font-family: Arial, sans-serif;
}

h1, h2 {
  color: #363d46;
}

h1 {
  text-align: center;
  margin-bottom: 20px;
}

.review-status {
  margin-bottom: 20px;
}

.review-status label {
  margin-right: 20px;
  font-size: 1rem;
}

textarea {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  margin-bottom: 20px;
}

.button-group {
  display: flex;
  justify-content: space-between; /* Space between buttons */
  margin-top: 10px;
}

.edit-button,
.submit-button {
  flex: 1; /* Each button takes equal space */
  padding: 10px;
  background-color: #363d46;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  margin-right: 10px; /* Space between buttons */
}

.edit-button:last-child,
.submit-button:last-child {
  margin-right: 0; /* Remove margin for the last button */
}

.edit-button:hover,
.submit-button:hover {
  background-color: #2a333d;
}

.success-message {
  color: #27ae60;
  text-align: center;
  margin-top: 20px;
}
</style>
