import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'
import ProductsView from '../views/ProductsView.vue'
import ProductDetailsView from '../views/ProductDetailsView.vue'
import CartView from '../views/CartView.vue';
import CheckoutView from '../views/CheckoutView.vue';
import OrderConfirmationView from '../views/OrderConfirmationView.vue';

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/about',
    name: 'about',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
  },
  { path: '/products', name: 'ProductsView', component: ProductsView },
  { path: '/products/:id', name: 'ProductDetailsView', component: ProductDetailsView, props: true },
  { path: '/cart', name: 'CartView', component: CartView },
  { path: '/checkout', name: 'CheckoutView', component: CheckoutView },
  { path: '/order-confirmation', name: 'OrderConfirmationView', component: OrderConfirmationView },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
