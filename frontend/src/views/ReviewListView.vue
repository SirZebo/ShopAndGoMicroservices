<template>
    <div class="reviews-container">
      <h1>Your Reviews</h1>
      <div class="review" v-for="review in reviews" :key="review.id">
        <h3>{{ review.title }}</h3>
        <p><strong>Order Item:</strong> {{ review.orderItem }}</p>
        <p><strong>Order Quantity:</strong> {{ review.orderQuantity }}</p>
        <!-- Placeholder for link -->
        <button @click="editReview(review.id)">View/Edit Review</button>
      </div>
    </div>
</template>
  
<script>
  export default {
    name: 'ReviewList',
    data() {
      return {
        reviews: [  // Sample data for reviews
          {
            id: 1,
            title: "Review 1",
            orderItem: "Laptop",
            orderQuantity: 1
          },
          {
            id: 2,
            title: "Review 2",
            orderItem: "Headphones",
            orderQuantity: 2
          },
          {
            id: 3,
            title: "Review 3",
            orderItem: "Mouse",
            orderQuantity: 1
          }
        ]
      };
    },
    methods: {
      editReview(id) {
        alert(`View Review ${id}`); // Placeholder function for navigation
      }
    }
  };
</script>
  
<style scoped>
.reviews-container {
  margin-top: 100px; /* Adjusted to ensure space between navbar and content */
  padding: 0 20px; /* Added padding for mobile responsiveness */
  font-family: Arial, sans-serif; /* Standard font for consistency */
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
  margin-bottom: 5px; /* Smaller bottom margin for headers within reviews */
}

.review p {
  margin: 0;
  font-size: 1rem;
  color: #363d46;
}
</style>