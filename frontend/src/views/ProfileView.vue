<template>
  <div class="profile-page">
    <h1>User Profile</h1>
    <p><strong>Name:</strong> {{ userProfile.name }}</p>
    <p><strong>Balance:</strong> ${{ userProfile.balance.toFixed(2) }}</p>
  </div>
</template>

<script>
import axios from 'axios';

export default {
name: 'UserProfile',
data() {
  return {
    userProfile: {
      name: 'Loading...',  // Default or loading placeholder
      balance: 0           // Default balance
    }
  };
},
mounted() {
  this.fetchUserProfile();
},
methods: {
  async fetchUserProfile() {
    try {
      const response = await axios.get('https://localhost:6061/accounts/14534836-bdbe-4dbe-af1c-80f9d5f433c2');
      this.userProfile.name = response.data.account.name;
      this.userProfile.balance = response.data.account.balance;
    } catch (error) {
      console.error('Failed to fetch user profile:', error);
      // Optionally handle errors, e.g., by showing a message or setting default values
      this.userProfile.name = 'Error';
      this.userProfile.balance = 0;
    }
  }
}
};
</script>

<style scoped>
.profile-page {
margin-top: 100px;
padding: 0 20px;
font-family: Arial, sans-serif;
text-align: center;
}
</style>