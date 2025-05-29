<template>
		<div v-if="!isAuthenticated" class="not-auth"></div>
    <div v-else>
	<main id="Warning-page" v-show="isVisible">
	  <table>
		<tr>
		  <td style="color: green;"><strong>Ok :</strong> {{ okCount }}</td>
		  <td style="color: orange;"><strong>A vérifier :</strong> {{ verifyCount }}</td>
		  <td style="color: red"><strong>A mettre au rebut :</strong> {{ scrapCount }}</td>
		  <td style="color: var(--color-dark);"><strong>Rebutés :</strong> {{ scrappedCount }}</td>
		  <td>
			  <button @click="$emit('close-warning')" class="close-btn">
				  <span class="material-icons">cancel</span>
			  </button>
		  </td>
		</tr>
	  </table>
	</main></div>
</template>
  
<script>
import axios from 'axios';

export default {
  data() {
    return {
      isAuthenticated: false,
      items: [],
      okCount: 0,
      verifyCount: 0,
      scrapCount: 0,
      scrappedCount: 0,
      isVisible: true, // Set this based on your needs
      userToken: '',
    };
  },
  async created() {
    this.userToken = localStorage.getItem('token');
		if (this.userToken) {
			this.isAuthenticated = true;
      isUserConnected();
		} else {
			this.isAuthenticated = false;
		}
  },
  async isUserConnected() {
    const company_token = localStorage.getItem('company_token');
	  try {
      const response = await axios.get(`${import.meta.env.VITE_NEXEPI_API_URL}/list?user_token=${this.userToken}&company_token=${company_token}`);
      this.items = response.data.data;  // Assign the fetched data to items

      // Initialize counts to 0
      this.okCount = 0;
      this.verifyCount = 0;
      this.scrapCount = 0;
      this.scrappedCount = 0;

      // Loop through the items to count the occurrences of each status
      this.items.forEach(item => {
        if (item.status === 'OK') this.okCount++;
        if (item.status === 'verify') this.verifyCount++;
        if (item.status === 'scrap') this.scrapCount++;
        if (item.status === 'scrapped') this.scrappedCount++;
      });
    } catch (error) {
      console.error('Error fetching Warning Vue data', error);
    }
  }
};

</script>
  
<style scoped>
#Warning-page {
  width: 100%;
  background-color: var(--color-light);
  padding: 1%;
  margin: 0;
}

table {
  width: 100%;
  border-collapse: collapse;
}

td {
  font-size: 1.5rem;
  text-align: center;
}

.close-btn {
  background: none;
  border: none;
  font-size: 2rem;
  color: var(--color-dark);
  cursor: pointer;
  transition: color 0.3s ease;
}

.close-btn:hover {
  color: var(--color-theme-primary);
}

/* Style pour petits écrans (téléphones) */
@media (max-width: 767px) {
  tr {
    display: block;
  }

  td {
    display: block;
    width: 100%;
    margin-bottom: 10px;
  }
}
</style>