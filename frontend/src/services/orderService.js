import axios from 'axios';

const API_URL = 'http://localhost:5000/api/orders';  // Replace with your backend API URL

export const orderService = {
  submitOrder(orderData) {
    return axios.post(`${API_URL}/submit`, orderData); // Make sure this matches your backend route
  },

  confirmPayment(orderId) {
    return axios.post(`${API_URL}/payment-confirmed`, { orderId });
  },

  confirmDelivery(orderId) {
    return axios.post(`${API_URL}/delivery-confirmed`, { orderId });
  },
};