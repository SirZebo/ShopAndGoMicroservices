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
import ProfileView from '../views/ProfileView.vue';
import OrderView from '../views/OrderView.vue';

const routes = [
  // For end user
  { path: '/', name: 'home', component: HomeView},
  { path: '/enduser/products', name: 'ProductsView', component: ProductsView },
  { path: '/enduser/products/:id', name: 'ProductDetailsView', component: ProductDetailsView, props: true },
  { path: '/enduser/cart', name: 'CartView', component: CartView },
  { path: '/enduser/checkout', name: 'CheckoutView', component: CheckoutView },
  { path: '/enduser/order-confirmation', name: 'OrderConfirmationView', component: OrderConfirmationView },
  { path: '/enduser/order-status', name: 'OrderStatusView', component: OrderStatusView },
  { path: '/enduser/order-tracking', name: 'OrderTrackingView', component: OrderTrackingView },
  { path: '/enduser/order-cancellation', name: 'OrderCancellationView', component: OrderCancellationView },
  { path: '/enduser/review/:id', name: 'ReviewView', component: ReviewView, props: true },
  { path: '/enduser/review-list', name: 'ReviewListView', component: ReviewListView },
  { path: '/enduser/profile', name: 'ProfileView', component: ProfileView },
  { path: '/enduser/order', name: 'OrderView', component: OrderView },

  // For admin
  { path: '/admin/dispute', name: 'AdminView', component: AdminView },

  // For SME
  { path: '/sme/shipment', name: 'ShipmentView', component: ShipmentView }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
