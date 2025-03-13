<template>
  <div class="product-details">
    <h2>{{ product.name }}</h2>
    <p>{{ product.description }}</p>
    <p class="price">${{ product.price }}</p>
    <p>Category: {{ product.category.join(', ') }}</p>
    <button @click="addToCart(product)">Add to Cart</button>
  </div>
</template>

<script>
import { cartStore } from '@/store/cartStore'; // Import the cart store

export default {
  name: 'ProductDetailsView',
  data() {
    return {
      product: {
        id: '4f136e9f-ff8c-4c1f-9a33-d12f689bdab8',
        name: 'Huawei Plus',
        description: 'This phone is the company\'s biggest change to its flagship smartphone in years. It includes a borderless.',
        price: 650.00,
        category: ['White Appliances'],
        imageFile: 'product-3.png',
      }
    };
  },
  methods: {
    addToCart(product) {
      cartStore.addToCart(product);  // Add product to cart
      console.log("Cart after adding:", cartStore.getCart());
    }
  }
};
</script>

<style scoped>
.product-details {
  margin-top: 100px;  /* Adjusted to ensure space between navbar and content */
  text-align: center;
  padding: 0 20px; /* Added padding for mobile responsiveness */
  margin-bottom: 50px; /* Added margin to match the home page's bottom spacing */
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
</style>
