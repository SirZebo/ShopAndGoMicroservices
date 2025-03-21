<template>
  <div class="admin-page">
    <div class="dispute-section under-review">
      <h2>Under Review</h2>
      <div class="dispute" v-for="dispute in disputesUnderReview" :key="dispute.id">
        <p><strong>Review Title:</strong> {{ dispute.body }}</p>
        <button @click="acceptDispute(dispute.id)">Accept</button>
        <button @click="rejectDispute(dispute.id)">Reject</button>
      </div>
    </div>

    <div class="dispute-section accepted">
      <h2>Accepted</h2>
      <div class="dispute" v-for="dispute in disputesAccepted" :key="dispute.id">
        <p><strong>Review Title:</strong> {{ dispute.body }}</p>
      </div>
    </div>

    <div class="dispute-section rejected">
      <h2>Rejected</h2>
      <div class="dispute" v-for="dispute in disputesRejected" :key="dispute.id">
        <p><strong>Review Title:</strong> {{ dispute.body }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'AdminPage',
  data() {
    return {
      disputesUnderReview: [],
      disputesAccepted: [],
      disputesRejected: []
    };
  },
  async created() {
    await this.fetchDisputes();
  },
  methods: {
    async fetchDisputes() {
      try {
        const response = await axios.get('https://localhost:6065/reviews/customer/58c49479-ec65-4de2-86e7-033c546291aa?pageNumber=1&pageSize=5');
        this.disputesUnderReview = response.data.reviews.map(dispute => ({
          id: dispute.id,
          body: dispute.body
        }));
      } catch (error) {
        console.error('Error fetching disputes:', error);
      }
    },
    acceptDispute(id) {
      const index = this.disputesUnderReview.findIndex(d => d.id === id);
      if (index !== -1) {
        this.disputesAccepted.push(this.disputesUnderReview[index]);
        this.disputesUnderReview.splice(index, 1);
      }
    },
    rejectDispute(id) {
      const index = this.disputesUnderReview.findIndex(d => d.id === id);
      if (index !== -1) {
        this.disputesRejected.push(this.disputesUnderReview[index]);
        this.disputesUnderReview.splice(index, 1);
      }
    }
  }
};
</script>

<style scoped>
.admin-page {
  margin-top: 100px;
  padding: 0 20px;
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
