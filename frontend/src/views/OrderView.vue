<template>
  <div class="orders-page">
    <h1>Your Orders</h1>
    <div class="order" v-for="order in orders" :key="order.id">
      <h2>{{ order.orderName }}</h2>
      <p class="status"><strong>Status:</strong> {{ orderStatus(order.status) }}</p>
      <div class="address">
        <h3>Shipping Details:</h3>
        <p>Name: {{ order.shippingAddress.firstName }} {{ order.shippingAddress.lastName }}</p>
        <p>Address: {{ order.shippingAddress.addressLine }}</p>
        <p>City, State, Zip: {{ order.shippingAddress.city }}, {{ order.shippingAddress.state }}, {{ order.shippingAddress.zipCode }}</p>
        <p>Country: {{ order.shippingAddress.country }}</p>
        <p>Email: {{ order.shippingAddress.emailAddress }}</p>
      </div>
      <div class="items">
        <h3>Order Items:</h3>
        <ul>
          <li v-for="item in order.orderItems" :key="item.productId" class="item">
            <p>Quantity: {{ item.quantity }}</p>
            <p>Product Name: {{ item.productName }}</p>
            <p>Price: ${{ item.price.toFixed(2) }}</p>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'UserOrders',
  data() {
    return {
      orders: []
    };
  },
  mounted() {
    this.fetchOrders();
  },
  methods: {
    fetchOrders() {
      axios.get('https://localhost:6063/orders/customer/58c49479-ec65-4de2-86e7-033c546291aa')
        .then(response => {
          this.orders = response.data.orders;
          return Promise.all(this.orders.map(order => 
            Promise.all(order.orderItems.map(item =>
              this.fetchProductDetails(item.productId).then(productName => {
                item.productName = productName; // Only setting product name
              })
            ))
          ));
        })
        .catch(error => {
          console.error('Error fetching orders:', error);
        });
    },
    fetchProductDetails(productId) {
      return axios.get(`https://localhost:6060/products/${productId}`)
        .then(response => response.data.product.name) // Directly return the product name
        .catch(error => {
          console.error('Error fetching product details:', error);
          return 'Unknown Product'; // Fallback product name
        });
    },
    orderStatus(status) {
      const statusMap = {
        1: 'Awaiting Payment',
        2: 'Shipping',
        3: 'In Transit',
        4: 'Delivered',
        5: 'Cancelled',
        6: 'Completed'
      };
      return statusMap[status] || 'Unknown';
    }
  }
};
</script>

<style scoped>
.orders-page {
  max-width: 800px;
  margin: 20px auto;
  padding: 20px;
  font-family: Arial, sans-serif;
}

.order {
  margin-bottom: 20px;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  background-color: #f9f9f9;
}

.status, .address, .items {
  margin-top: 10px;
}

h3 {
  color: #333;
  margin-bottom: 5px;
}

ul {
  padding-left: 0; /* Remove default padding */
  list-style-type: none; /* Remove bullet points */
}

.item {
  padding: 10px;
  background-color: #e9e9e9;
  border: 1px solid #d3d3d3;
  border-radius: 4px;
}

li {
  margin-bottom: 5px;
}
</style>

