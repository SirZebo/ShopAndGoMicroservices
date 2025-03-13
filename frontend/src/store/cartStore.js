export default {
    getCart() {
      const cart = sessionStorage.getItem("shoppingCart");
      return cart ? JSON.parse(cart) : [];
    },
    addToCart(product) {
      let cart = this.getCart();
      cart.push(product);
      sessionStorage.setItem("shoppingCart", JSON.stringify(cart));
    },
    clearCart() {
      sessionStorage.removeItem("shoppingCart");
    }
  };  