<template>
    <div class="profile-page">
      <h1>User Profile</h1>
      <p><strong>Name:</strong> {{ userProfile.name }}</p>
      <p><strong>Balance:</strong> ${{ userProfile.balance.toFixed(2) }}</p>
    </div>
</template>

<script>
export default {
  name: 'UserProfile',
  data() {
    return {
      userProfile: {
        name: 'User 1',
        balance: 1000.00
      }
    };
  },
  mounted() {
    this.fetchUserProfile();
  },
  methods: {
    fetchUserProfile() {
      // API call to fetch user profile
      // this.userProfile = fetchedData;
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


  