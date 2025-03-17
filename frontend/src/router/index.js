import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ProductsView from '../views/ProductsView.vue'
import ProductDetailsView from '../views/ProductDetailsView.vue'
import CartView from '../views/CartView.vue';
import CheckoutView from '../views/CheckoutView.vue';
import OrderConfirmationView from '../views/OrderConfirmationView.vue';
import OrderStatusView from '../views/OrderStatusView.vue';
import OrderTrackingView from '../views/OrderTrackingView.vue'; 
import OrderCancellationView from '../views/OrderCancellationView.vue'; 
import ReviewView from '../views/ReviewView.vue'; 

const routes = [
  { path: '/', name: 'home', component: HomeView},
  { path: '/products', name: 'ProductsView', component: ProductsView },
  { path: '/products/:id', name: 'ProductDetailsView', component: ProductDetailsView, props: true },
  { path: '/cart', name: 'CartView', component: CartView },
  { path: '/checkout', name: 'CheckoutView', component: CheckoutView },
  { path: '/order-confirmation', name: 'OrderConfirmationView', component: OrderConfirmationView },
  { path: '/order-status', name: 'OrderStatusView', component: OrderStatusView },
  { path: '/order-tracking', name: 'OrderTrackingView', component: OrderTrackingView },
  { path: '/order-cancellation', name: 'OrderCancellationView', component: OrderCancellationView },
  { path: '/review', name: 'ReviewView', component: ReviewView },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
