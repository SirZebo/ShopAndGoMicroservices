<template>
    <div class="admin-page">
      <div class="dispute-section under-review">
        <h2>Under Review</h2>
        <div class="dispute" v-for="dispute in disputesUnderReview" :key="dispute.id">
          <p>{{ dispute.description }}</p>
          <button @click="acceptDispute(dispute.id)">Accept</button>
          <button @click="rejectDispute(dispute.id)">Reject</button>
        </div>
      </div>
      <div class="dispute-section accepted">
        <h2>Accepted</h2>
        <div class="dispute" v-for="dispute in disputesAccepted" :key="dispute.id">
          <p>{{ dispute.description }}</p>
        </div>
      </div>
      <div class="dispute-section rejected">
        <h2>Rejected</h2>
        <div class="dispute" v-for="dispute in disputesRejected" :key="dispute.id">
          <p>{{ dispute.description }}</p>
        </div>
      </div>
    </div>
  </template>
  
<script>
  export default {
    name: 'AdminPage',
    data() {
      return {
        disputesUnderReview: [
          { id: 1, description: 'Dispute 1' },
          { id: 2, description: 'Dispute 2' }
        ],
        disputesAccepted: [],
        disputesRejected: []
      };
    },
    methods: {
      acceptDispute(id) {
        const index = this.disputesUnderReview.findIndex(d => d.id === id);
        this.disputesAccepted.push(this.disputesUnderReview[index]);
        this.disputesUnderReview.splice(index, 1);
      },
      rejectDispute(id) {
        const index = this.disputesUnderReview.findIndex(d => d.id === id);
        this.disputesRejected.push(this.disputesUnderReview[index]);
        this.disputesUnderReview.splice(index, 1);
      }
    }
  };
</script>
  
<style scoped>
  .admin-page {
    margin-top: 100px; /* Adjusted to ensure space between navbar and content */
    padding: 0 20px; /* Added padding for mobile responsiveness */
    font-family: Arial, sans-serif;
  }
  
  .dispute-section {
    border: 1px solid #ccc;
    padding: 10px;
    width: 30%;
  }
  
  .dispute {
    margin-bottom: 10px;
    padding: 10px;
    background-color: #f4f4f4;
  }
  
  button {
    margin-right: 5px;
  }
</style>
  