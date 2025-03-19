import { reactive } from 'vue';

export const cartStore = reactive({
  cart: [],
  addToCart(product) {
    const existingProduct = this.cart.find(item => item.id === product.id);
    if (existingProduct) {
      existingProduct.quantity += 1;  // If the product already exists, increase the quantity
    } else {
      this.cart.push({ ...product, quantity: 1 });  // Otherwise, add it to the cart with a quantity of 1
    }
  },
  getCart() {
    return this.cart;
  },
  getCartCount() {
    return this.cart.reduce((total, item) => total + item.quantity, 0);  // Count the total items in the cart
  }
});