<template>
  <div class="products">
    <h1>Products</h1>
    <p>Browse our collection of amazing products.</p>

    <div class="product-list">
      <div class="product" v-for="product in products" :key="product.id">
        <h2>{{ product.name }}</h2>
        <p>{{ product.description }}</p>
        <span class="price">${{ product.price.toFixed(2) }}</span>
        <router-link :to="'/products/' + product.id" class="details-btn">View Details</router-link>
        <button @click="addToCart(product)" class="cart-btn">Add to Cart</button>
      </div>
    </div>
  </div>
</template>

<script>
import cartStore from "@/store/cartStore";

export default {
  name: "ProductsView",
  data() {
    return {
      products: [
        { id: 1, name: "Laptop", description: "High-performance laptop", price: 1200 },
        { id: 2, name: "Smartphone", description: "Latest model smartphone", price: 999 },
        { id: 3, name: "Headphones", description: "Noise-canceling headphones", price: 250 },
        { id: 4, name: "Gaming Mouse", description: "Wireless gaming mouse", price: 75 },
        { id: 5, name: "Mechanical Keyboard", description: "RGB mechanical keyboard", price: 150 }
      ]
    };
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
.products {
  text-align: center;
  padding: 20px;
}

.product-list {
  display: flex;
  justify-content: center;
  flex-wrap: wrap;
  gap: 20px;
  margin-top: 20px;
}

.product {
  padding: 15px;
  border: 1px solid #ddd;
  border-radius: 5px;
  width: 250px;
  background: #fff;
  box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
}

.product h2 {
  margin: 10px 0;
  font-size: 18px;
}

.price {
  font-weight: bold;
  color: #007bff;
}

.details-btn {
  display: block;
  margin-top: 10px;
  padding: 8px 12px;
  background-color: #007bff;
  color: white;
  text-decoration: none;
  border-radius: 5px;
}

.details-btn:hover {
  background-color: #0056b3;
}

.cart-btn {
  display: block;
  margin-top: 10px;
  padding: 8px 12px;
  background-color: #28a745;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.cart-btn:hover {
  background-color: #218838;
}
</style>