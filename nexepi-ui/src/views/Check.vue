<template>
	<main id="Checking-page">
		<!-- Check if the user is authenticated -->
		<div v-if="!isAuthenticated" class="not-auth">
			<h2>🔒 Accès restreint</h2>
			<p>Vous devez être connecté pour accéder à cette page.</p>
			<router-link to="/account" class="cta-button">Se connecter</router-link>
		</div>

		<!-- If the user is authenticated -->
		<div v-else>
		<div v-for="(item, index) in filteredItems" :key="index" class="item">
  			<router-link :to="'/check/item/' + item.id" class="item-link">
				<div class="item-row">
					<div class="item-info-status">
						<div class="item-details">
							<p><strong>ID :</strong> {{ item.id }}</p>
							<p><strong>SN :</strong> {{ item.sn }}</p>
							<p><strong>Type :</strong> {{ item.type }}</p>
							<p><strong>Modèle :</strong> {{ item.model }}</p>
							<p><strong>Fabricant :</strong> {{ item.manufacturer }}</p>
							<p><strong>Année :</strong> {{ item.year }}</p>
							<p><strong>Propriétaire :</strong> {{ item.owner }}</p>
							<p><strong>Référence Au Vieux Campeur :</strong> {{ item.avc_ref }}</p>
							<p><strong>Commentaires :</strong> {{ item.comments }}</p>
						</div>

						<div class="item-status">
							<p><strong>Status :</strong> <span v-html="getStatusLabel(item.status)"></span></p>
						</div>
					</div>

					<div class="item-image">
						<img v-if="imageExists(item)" class="picture" :src="item.photo_url + item.avc_ref + '.jpg'" alt="-- Item Picture --"/>
					</div> 
				</div>
  			</router-link>
		</div>
	</div>
	</main>
</template>
  
  
<script>
  import axios from 'axios';

  export default {
	data() {
	  return {
		isAuthenticated: false,
		items: [], // Original data from API
		filteredItems: [], // Items after sorting and filtering
		filterBy: '', // Category to filter by (either "type" or "status")
		filterValue: 'verify',
	  };
	},
	async created() {
	  	const company_token = localStorage.getItem('company_token');
	  	try {
			const response = await axios.get(`${import.meta.env.VITE_NEXEPI_API_URL}/list?company_token=${company_token}`);
		this.items = response.data.data;
		this.filteredItems = [...this.items];
		this.filterItems();
	  } catch (error) {
		console.error('Error fetching data', error);
	  }
	},
	async mounted() {
		this.userToken = localStorage.getItem('token');
		if (this.userToken) {
			this.isAuthenticated = true;
		} else {
			this.isAuthenticated = false;
		}
  	},
	methods: {
	  // Return status as a human-readable label with color
	  getStatusLabel(status) {
		switch (status) {
		  case 'verify':
			return '<span style="color: orange;">À vérifier</span>';
		  default:
			return '<span style="color: blue;">Non défini</span>';
		}
	  },
	  // Method to filter items based on selected filter type and value
	  filterItems() {
		this.filteredItems = this.items.filter(item => item.status === this.filterValue);
	  },
	  	imageExists(item) {
      	const imageUrl = item.photo_url + item.avc_ref + '.jpg';
      	// Check if image exists locally by some means (like via a URL check or API)
      	return true; // Placeholder for actual logic (like API call to check file existence)
      }
	},
  };
</script>
  
  
  
  
  <style scoped>
  /* Ensure full width for page */
  #Checking-page {
	text-align: left;
	padding: 1%;
	margin: 0;
	width: 100%;
  }
  .not-auth {
  text-align: center;
  background-color: #a70000d2;
  color: #fff;  
  padding: 2rem;
  border-radius: 10px;
  box-shadow: 0 2px 6px #000000ae;
  height: 100%;
  width: 100%;
}
/* Add styling for filter options */
.filter-options {
  margin-bottom: 20px;
  font-size: 1.1em;
}

.filter-options label {
  font-weight: bold;
  margin-right: 10px;
}

.filter-options select {
  padding: 5px;
  font-size: 1em;
  border-radius: 5px;
  margin-right: 10px;
  background-color: var(--color-light);
  box-shadow: 0 4px 8px #00000039;
  border: none;
}
  
  /* Styling for each item container */
  .item {
	display: flex;
	justify-content: space-between;
	padding: 20px;
	margin-bottom: 20px;
	box-sizing: border-box;
	border-radius: 10px;
	background-color: var(--color-light);
	box-shadow: 0 4px 8px #00000039;
	cursor: help;
  }
  
  /* Layout for two columns */
  .item-row {
	display: grid;
	grid-template-columns: 50fr 1fr;
	gap: 0px;
  }
  
  /* Styling for left column (image) */
  .item-image {
	display: flex;
	justify-content: center;
	align-items: center;
	text-align: center;
  }
  
  .picture {
	min-width: 100px;
	max-height: 400px;
	border-radius: 10px;
	margin-right: 30px;
	object-fit: cover;
  }
  
  /* Styling for right column (info and status) */
  .item-info-status {
	display: flex;
	flex-direction: column;
  }
  
  .item-details p {
	margin: 5px 0;
	font-size: 1.1em;
  }
  
  .item-details p {
	font-weight: bold;
	padding: 5px;
  }
  
  .item-status p {
	font-weight: bold;
	padding: 5px;
  }

  @media (max-width: 767px) {
	/* Adjust item layout to a single column */
	.item-row {
		grid-template-columns: 1fr; /* Stacks the content in a single column */
	}

	/* Adjust image size for mobile */
	.picture {
		width: 100px;
		height: auto;
		max-height: 200px; /* Reduce max height */
		margin-right: 0; /* Remove right margin */
	}

	/* Adjust font sizes for smaller screens */
	.item-details p, 
	.item-status p {
		font-size: 1em;
	}

	/* Make the filter options more compact */
	.filter-options select {
		width: 100%; /* Make dropdowns full width */
		margin-bottom: 10px; /* Add some space between dropdowns */
	}
}
</style>
  