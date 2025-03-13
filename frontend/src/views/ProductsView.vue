<template>
  <div class="products">
    <div class="product-list">
      <!-- Loop through the static product data -->
      <div v-for="product in products" :key="product.id" class="product-item">
        <img :src="`/assets/${product.imageFile}`" alt="product.name" class="product-image" />
        <div class="product-info">
          <h3>{{ product.name }}</h3>
          <p>{{ product.description }}</p>
          <p class="price">${{ product.price.toFixed(2) }}</p>
          <button @click="viewProduct(product.id)">View Details</button>
        </div>
      </div>
    </div>
    <!-- If no products, show a message -->
    <p v-if="products.length === 0">No products available at the moment.</p>
  </div>
</template>

<script>
export default {
  name: 'ProductsView',
  data() {
    return {
      // Static product data
      products: [
        {
          id: '5334c996-8457-4cf0-815c-ed2b77c4ff61',
          name: 'IPhone X',
          description: 'This phone is the company\'s biggest change to its flagship smartphone in years. It includes a borderless.',
          imageFile: 'product-1.png',
          price: 950.00,
          category: ['Smart Phone']
        },
        {
          id: 'c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914',
          name: 'Samsung 10',
          description: 'This phone is the company\'s biggest change to its flagship smartphone in years. It includes a borderless.',
          imageFile: 'product-2.png',
          price: 840.00,
          category: ['Smart Phone']
        },
        {
          id: '4f136e9f-ff8c-4c1f-9a33-d12f689bdab8',
          name: 'Huawei Plus',
          description: 'This phone is the company\'s biggest change to its flagship smartphone in years. It includes a borderless.',
          imageFile: 'product-3.png',
          price: 650.00,
          category: ['White Appliances']
        },
        {
          id: '6ec1297b-ec0a-4aa1-be25-6726e3b51a27',
          name: 'Xiaomi Mi 9',
          description: 'This phone is the company\'s biggest change to its flagship smartphone in years. It includes a borderless.',
          imageFile: 'product-4.png',
          price: 470.00,
          category: ['White Appliances']
        },
        {
          id: 'b786103d-c621-4f5a-b498-23452610f88c',
          name: 'HTC U11+ Plus',
          description: 'This phone is the company\'s biggest change to its flagship smartphone in years. It includes a borderless.',
          imageFile: 'product-5.png',
          price: 380.00,
          category: ['Smart Phone']
        },
        {
          id: 'c4bbc4a2-4555-45d8-97cc-2a99b2167bff',
          name: 'LG G7 ThinQ',
          description: 'This phone is the company\'s biggest change to its flagship smartphone in years. It includes a borderless.',
          imageFile: 'product-6.png',
          price: 240.00,
          category: ['Home Kitchen']
        },
        {
          id: '93170c85-7795-489c-8e8f-7dcf3b4f4188',
          name: 'Panasonic Lumix',
          description: 'This phone is the company\'s biggest change to its flagship smartphone in years. It includes a borderless.',
          imageFile: 'product-6.png',
          price: 240.00,
          category: ['Camera']
        }
      ]
    };
  },
  methods: {
    viewProduct(id) {
      // Navigate to the Product Details page
      this.$router.push({ name: 'ProductDetailsView', params: { id } });
    }
  }
};
</script>

<style scoped>
.products {
  margin-top: 100px;  /* Adjusted to ensure space between navbar and content */
  padding: 0 20px; /* Added padding for mobile responsiveness */
  margin-bottom: 50px; /* Added margin to match the home page's bottom spacing */
}

.product-list {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(300px, 1fr));
  gap: 20px;
}

.product-item {
  border: 1px solid #ddd;
  padding: 20px;
  text-align: center;
  background-color: #f9f9f9;
}

.product-image {
  width: 100%;
  height: 200px;
  object-fit: cover;
}

.product-info {
  margin-top: 10px;
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

p {
  text-align: center;
  color: #363d46;
  font-size: 1.2rem;
}
</style>
