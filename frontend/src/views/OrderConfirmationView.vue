<template>
    <div class="order-confirmation">
      <h1>Thank You for Your Order!</h1>
      <p>Your order has been successfully placed. 🎉</p>
      <p><strong>Order Summary:</strong></p>
      <ul>
        <li v-for="(item, index) in cart" :key="index">
          <strong>{{ item.name }}</strong> - ${{ item.price.toFixed(2) }}
        </li>
      </ul>
      <div class="total">
        <h3>Total Price: ${{ totalPrice.toFixed(2) }}</h3>
      </div>
      <router-link to="/products" class="shop-btn">Continue Shopping</router-link>
    </div>
</template>
  
  <script>
  import cartStore from "@/store/cartStore";
  
  export default {
    name: "OrderConfirmationView",
    data() {
      return {
        cart: cartStore.getCart()
      };
    },
    computed: {
      totalPrice() {
        return this.cart.reduce((total, item) => total + item.price, 0);
      }
    }
  };
  </script>
  
  <style scoped>
  .order-confirmation {
    text-align: center;
    padding: 40px;
  }
  
  h1 {
    font-size: 36px;
    margin-bottom: 20px;
  }
  
  ul {
    list-style-type: none;
    padding: 0;
  }
  
  .total {
    margin-top: 20px;
    font-size: 18px;
    font-weight: bold;
  }
  
  .shop-btn {
    display: inline-block;
    margin-top: 20px;
    padding: 10px 15px;
    background-color: #007bff;
    color: white;
    text-decoration: none;
    border-radius: 5px;
  }
  
  .shop-btn:hover {
    background-color: #0056b3;
  }
  </style>  