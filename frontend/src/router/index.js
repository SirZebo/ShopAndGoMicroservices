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
import ReviewListView from '../views/ReviewListView.vue'; 
import AdminView from '../views/AdminView.vue';
import ShipmentView from '../views/ShipmentView.vue';

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
  { path: '/review-list', name: 'ReviewListView', component: ReviewListView },
  { path: '/admin', name: 'AdminView', component: AdminView },
  { path: '/shipment', name: 'ShipmentView', component: ShipmentView },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
