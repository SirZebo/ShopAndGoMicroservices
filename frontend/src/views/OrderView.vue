<template>
    <div class="orders-page">
      <h1>Your Orders</h1>
      <div class="order" v-for="order in orders" :key="order.id">
        <h2>Order {{ order.id }}</h2>
        <p><strong>Status:</strong> {{ orderStatus(order.status) }}</p>
        <p><strong>Details:</strong> {{ order.details }}</p>
      </div>
    </div>
</template>

<script>
export default {
  name: 'UserOrders',
  data() {
    return {
      orders: [
        { id: 1, status: 1, details: '2x T-shirt, 1x Jeans' },
        { id: 2, status: 3, details: '1x Jacket' }
      ]
    };
  },
  methods: {
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
  margin-top: 100px;
  padding: 0 20px;
  font-family: Arial, sans-serif;
}

.order {
  margin-bottom: 20px;
  padding: 10px;
  border: 1px solid #ccc;
}
</style>