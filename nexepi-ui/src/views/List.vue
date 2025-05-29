<template>
	<main id="List-page"><!-- Check if the user is authenticated -->
		<div v-if="!isAuthenticated">
        	<NotAuth/>
		</div>

		<!-- If the user is authenticated -->
		<div v-else>
	  <!-- First dropdown to choose the filter category (Type or Status) -->
	  <div class="filter-options">
		<label for="filter-by">Filtrer par :</label>
		<select id="filter-by" v-model="filterBy" @change="filterItems">
		  <option value="">-- Sélectionner un critère --</option>
		  <option value="type">Type</option>
		  <option value="status">Statut</option>
		  <option value="model">Modèle</option>
		  <option value="year">Année</option>
		  <option value="owner">Propriétaire</option>
		</select>
  
		<!-- Second dropdown to choose the filter value -->
		<select v-if="filterBy" v-model="filterValue" @change="filterItems">
		  <option value="">-- Sélectionner --</option>
		  <option v-if="filterBy === 'type'" v-for="type in types" :key="type" :value="type">{{ type }}</option>
		  <option v-if="filterBy === 'status'" v-for="status in statuses" :key="status" :value="status">{{ status }}</option>
		  <option v-if="filterBy === 'model'" v-for="model in models" :key="model" :value="model">{{ model }}</option>
		  <option v-if="filterBy === 'year'" v-for="year in years" :key="year" :value="year">{{ year }}</option>
		  <option v-if="filterBy === 'owner'" v-for="owner in owners" :key="owner" :value="owner">{{ owner }}</option>
		</select>
	  </div>
  
	   <!-- List of items after sorting and filtering -->
		<div v-for="(item, index) in filteredItems" :key="index" class="item">
  			<router-link :to="'/list/item/' + item.NEXEPI_id" class="item-link">
				<div class="item-row">
					<div class="item-info-status">
						<div class="item-details">
							<p><strong>NEXEPI_ID :</strong> {{ item.NEXEPI_id }}</p>
							<p><strong>SN :</strong> {{ item.sn }}</p>
							<p><strong>Type :</strong> {{ item.type }}</p>
							<p><strong>Modèle :</strong> {{ item.model }}</p>
							<p><strong>Fabricant :</strong> {{ item.manufacturer }}</p>
							<p><strong>Année :</strong> {{ item.year }}</p>
							<p><strong>Propriétaire :</strong> {{ item.company_name }}</p>
							<p><strong>Référence Au Vieux Campeur :</strong> {{ item.avc_ref }}</p>
							<p><strong>Commentaires :</strong> {{ item.comments }}</p>
						</div>

						<div class="item-status">
							<p><strong>Status :</strong> <span v-html="getStatusLabel(item.status)"></span></p>
						</div>
					</div>

					<div class="item-image">
						<img v-if="imageExists(item)" class="picture" :src="'https://api.NEXEPI.francium-digital.fr/media/pictures/' + item.avc_ref + '.jpg'" alt="-- Item Picture --"/>
					</div>
				</div>
  			</router-link>
		</div>
	</div>
	</main>
</template>

<script setup>
import NotAuth from './account/NotAuth.vue';
</script>
  
<script>
import axios from 'axios';
  
export default {
	data() {
	  return {
		isAuthenticated: false,
		items: [], // Original data from API
		filteredItems: [], // Items after sorting and filtering
		filterBy: '', // Category to filter by (either "type" or "status")
		filterValue: '', // Value to filter by
		types: [], // Array of unique types for filtering
		statuses: ['OK', 'verify', 'scrap', 'scrapped'], // Possible statuses for filtering
		models: [], // Array of unique models for filtering
		years: [], // Array of unique years for filtering
		owners: [], // Array of unique owners for filtering
		userToken: '',
	  };
	},
	async created() {
		this.userToken = localStorage.getItem('token');
		if (this.userToken) {
			this.isAuthenticated = true;
		} else {
			this.isAuthenticated = false;
		}

		if(this.isAuthenticated) {
			//const company_token = localStorage.getItem('company_token');
			const company_token = "7c62114c00c536dca5ce0e0c76f8cea45a5b546cc652fb126d7e0e206624daad";
			const user = JSON.parse(localStorage.getItem('user'));
			const username = user?.nickname || 'utilisateur';
			try {
				const response = await axios.get(`${import.meta.env.VITE_NEXEPI_API_URL}/list?username=${username}&company_token=${company_token}`);
	
				// Vérification si les données existent dans la réponse
				if (response.data && response.data.success) {
					this.items = response.data.data; // Assurer que 'data' contient les items
					this.filteredItems = [...this.items]; // Initialize filtered items with all items
					this.types = [...new Set(this.items.map(item => item.type))]; // Get unique types for filtering
					this.models = [...new Set(this.items.map(item => item.model))]; // Get unique models for filtering
					this.years = [...new Set(this.items.map(item => item.year))]; // Get unique years for filtering
					this.owners = [...new Set(this.items.map(item => item.company_name))]; // Get unique company names for filtering
				} else {
					console.error('Error: Invalid response from API');
				}
			} catch (error) {
				console.error('Error fetching data', error);
			}
		}
	},
	methods: {
	  // Return status as a human-readable label with color
	  getStatusLabel(status) {
		switch (status) {
		  case 'OK':
			return '<span style="color: green;">Ok</span>';
		  case 'verify':
			return '<span style="color: orange;">À vérifier</span>';
		  case 'scrap':
			return '<span style="color: red;">À rebuter</span>';
		  case 'scrapped':
			return '<span style="color: var(--color-dark);">Rebuté</span>';
		  default:
			return '<span style="color: blue;">Non défini</span>';
		}
	  },
	  // Method to filter items based on selected filter type and value
	  filterItems() {
		if (this.filterBy === 'type' && this.filterValue) {
		  this.filteredItems = this.items.filter(item => item.type === this.filterValue);
		} else if (this.filterBy === 'status' && this.filterValue) {
		  this.filteredItems = this.items.filter(item => item.status === this.filterValue);
		} else if (this.filterBy === 'model' && this.filterValue) {
		  this.filteredItems = this.items.filter(item => item.model === this.filterValue);
		} else if (this.filterBy === 'year' && this.filterValue) {
		  this.filteredItems = this.items.filter(item => item.year === this.filterValue);
		} else if (this.filterBy === 'owner' && this.filterValue) {
		  this.filteredItems = this.items.filter(item => item.owner === this.filterValue);
		} else {
		  this.filteredItems = [...this.items]; // If no filter, show all items
		}
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
#List-page {
	text-align: left;
	padding: 1%;
	margin: 0;
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

.item-details p strong {
	font-weight: bold;
	padding: 5px;
}

.item-status p strong {
	font-weight: bold;
	padding: 5px;
}

/* Media query for small screens (mobile devices) */
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
