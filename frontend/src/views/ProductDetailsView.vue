<template>
    <div class="product-details">
      <div class="product-card">
        <img :src="product.image" :alt="product.name" class="product-image" />
        <div class="product-info">
          <h1>{{ product.name }}</h1>
          <p class="description">{{ product.description }}</p>
          <span class="price">${{ product.price.toFixed(2) }}</span>
  
          <!-- Wrap buttons in button-group for proper alignment -->
          <div class="button-group">
            <button @click="addToCart(product)" class="cart-btn">Add to Cart</button>
            <router-link to="/products" class="back-btn">Back to Products</router-link>
          </div>
        </div>
      </div>
    </div>
</template>
  
  
<script>
import cartStore from "@/store/cartStore";

export default {
  name: "ProductDetailsView",
  data() {
    return {
      products: [
        { id: 1, name: "Laptop", description: "High-performance laptop", price: 1200, image: "https://via.placeholder.com/300" },
        { id: 2, name: "Smartphone", description: "Latest model smartphone", price: 999, image: "https://via.placeholder.com/300" },
        { id: 3, name: "Headphones", description: "Noise-canceling headphones", price: 250, image: "https://via.placeholder.com/300" },
        { id: 4, name: "Gaming Mouse", description: "Wireless gaming mouse", price: 75, image: "https://via.placeholder.com/300" },
        { id: 5, name: "Mechanical Keyboard", description: "RGB mechanical keyboard", price: 150, image: "https://via.placeholder.com/300" }
      ],
      product: {}
    };
  },
  created() {
    const productId = parseInt(this.$route.params.id);
    this.product = this.products.find(p => p.id === productId) || {};
  },
  methods: {
    addToCart(product) {
      cartStore.addToCart(product);
      alert(`${product.name} added to cart!`);
    }
  }
};
</script>
  
<style scoped>
.product-details {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 80vh;
  padding: 20px;
}

.product-card {
  display: flex;
  flex-direction: column;
  align-items: center;
  background: #fff;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
  width: 350px;
  text-align: center;
}

.product-image {
  width: 100%;
  height: auto;
  border-radius: 10px;
}

.product-info {
  margin-top: 15px;
}

h1 {
  font-size: 24px;
  color: #333;
}

.description {
  font-size: 16px;
  color: #666;
  margin: 10px 0;
}

.price {
  font-size: 22px;
  font-weight: bold;
  color: #007bff;
  display: block;
  margin-top: 10px;
}

/* Flexbox to align buttons side by side */
.button-group {
  display: flex;
  gap: 10px; /* Space between buttons */
  margin-top: 15px;
}

.cart-btn, .back-btn {
  padding: 10px 15px;
  border-radius: 5px;
  font-size: 16px;
  text-decoration: none;
  text-align: center;
}

.cart-btn {
  background-color: #28a745;
  color: white;
  border: none;
  cursor: pointer;
}

.cart-btn:hover {
  background-color: #218838;
}

.back-btn {
  background-color: #007bff;
  color: white;
}

.back-btn:hover {
  background-color: #0056b3;
}
</style>

  