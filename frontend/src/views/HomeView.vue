<template>
  <div class="home">
    <h1>Welcome to Shop & Go</h1>
    <p>Your one-stop shop for all your needs.</p>

    <router-link to="/products" class="btn">View Products</router-link>
  </div>
</template>

<style scoped>
.home {
  text-align: center;
  padding: 20px;
}

.btn {
  display: inline-block;
  padding: 10px 20px;
  margin-top: 20px;
  background-color: #007bff;
  color: white;
  text-decoration: none;
  border-radius: 5px;
}

.btn:hover {
  background-color: #0056b3;
}
</style>