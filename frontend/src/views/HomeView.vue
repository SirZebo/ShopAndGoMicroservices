<template>
  <div class="home">
    <!-- Image Carousel -->
    <vueper-slides :loop="true" :per-page="1" :navigation-enabled="true">
      <vueper-slide v-for="(slide, i) in slides" :key="i">
        <img :src="slide.img" alt="carousel-image" class="carousel-img"/>
      </vueper-slide>
    </vueper-slides>

    <!-- Short Write-up and Button -->
    <div class="intro-text">
      <p>Your one-stop destination for the latest and greatest products! Explore a curated selection of tech, fashion, home essentials, and more. With fast and free shipping on orders over $50, shopping has never been easier.</p>
      <router-link to="/products" class="shop-btn">Shop Now</router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: 'HomeView',
  data() {
    return {
      slides: [
        { img: require('@/assets/top.jpg') },
        { img: require('@/assets/top2.jpg') },
        { img: require('@/assets/shoes.jpg') },
        { img: require('@/assets/watch.jpg') }
      ]
    };
  }
}
</script>

<style scoped>
.home {
  margin-top: 100px;  /* Adjusted to ensure space between navbar and content */
  padding: 0 20px; /* Added padding for mobile responsiveness */
}

.carousel-img {
  width: 100%;
  height: 400px;
  object-fit: cover;
  border-radius: 10px;
}

.intro-text {
  text-align: center;
  max-width: 800px;
  margin-top: 50px;  /* Space between carousel and intro text */
  margin-bottom: 50px;  /* Space after intro text */
  margin-left: auto;
  margin-right: auto;
}

.intro-text p {
  font-size: 18px;
  color: #363d46;
  margin-bottom: 30px;
  line-height: 1.5;
}

.shop-btn {
  padding: 12px 20px;
  background-color: #363d46;
  color: white;
  text-decoration: none;
  border-radius: 5px;
  font-size: 18px;  
  display: inline-block;  /* Ensure it stays inline with text */
  transition: background-color 0.3s ease; /* Smooth transition for hover effect */
}

.shop-btn:hover {
  background-color: #2a333d;
}

@media screen and (max-width: 768px) {
  .carousel-img {
    height: 300px;  /* Adjust carousel image height on small screens */
  }

  .intro-text p {
    font-size: 16px;  /* Smaller text size for mobile */
  }

  .shop-btn {
    font-size: 16px;
    padding: 10px 18px;  /* Adjust padding for mobile */
  }
}
</style>