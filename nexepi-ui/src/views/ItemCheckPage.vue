<template>
	<main id="ItemCheck-page">
	  <div class="form-container">
		<h1>Vérification de l'élément</h1>
  
		<div v-if="item" class="form">
		  <div class="item-detail" v-for="(value, key) in formattedItem" :key="key">
			<p><strong>{{ key }} :</strong> {{ value }}</p>
		  </div>

          <div class="form-group">
            <label for="status">État* :</label>
            <select id="status" v-model="selectedStatus">
                <option value="bon">Bon état</option>
                <option value="moyen">Moyen état</option>
                <option value="mauvais">Mauvais état</option>
            </select>
            </div>

		  <div class="button-container">
			<button @click="validItem" class="edit-button">Valider</button>
			<button @click="closePage" class="close-button">Fermer</button>
		  </div>
		</div>
  
		<div v-else-if="isLoading" class="form loading">
		  <p>Chargement des informations de l'élément...</p>
		</div>
  
		<div v-else class="form error">
		  <p>Erreur lors du chargement de l'élément. Veuillez réessayer plus tard.</p>
		</div>
	  </div>
	</main>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
	data() {
	  return {
		item: null,
		isLoading: true,
        selectedStatus: 'bon',
	  };
	},
	computed: {
		formattedItem() {
			if (!this.item) return {};

			return {
			'Item': this.item.id+" . "+this.item.type+" "+this.item.model+" . "+this.item.sn
			};
		}
	},
	methods: {
		async validItem() {
		const itemId = parseInt(this.item.id);
		const inspectionStatus = this.selectedStatus;
		const inspectionComments = "Inspection après validation";
		const companyToken = localStorage.getItem('company_token');

		// Vérifications préalables
		if (isNaN(itemId)) {
			console.error("ID d'item invalide.");
			alert("Élément non valide.");
			return;
		}
		if (!inspectionStatus) {
			alert("Veuillez sélectionner un statut.");
			return;
		}
		if (!companyToken) {
			alert("Token d'entreprise manquant.");
			return;
		}

		try {
			await axios.post(`${import.meta.env.VITE_NEXEPI_API_URL}/check/save`, {
				item_id: itemId,
				company_token: companyToken,
				status: inspectionStatus,
				comments: inspectionComments
			});
			this.closePage();
		} catch (error) {
			console.error('validItem() : Error fetching data : ', error?.response?.data || error.message);
			alert("Échec de l'enregistrement : " + (error?.response?.data?.message || "Erreur inconnue."));
		}
	},
	closePage() {
		this.$router.go(-1);
	}
	},
	async created() {
	  const itemId = this.$route.params.itemId;
	  if (!itemId) {
		console.error('Item ID is missing in the URL path');
		return;
	  }
  
	  const company_token = localStorage.getItem('company_token');
	  	try {
			const response = await axios.get(`${import.meta.env.VITE_NEXEPI_API_URL}/list?company_token=${company_token}`);
		this.item = response.data.data.find(i => i.id === parseInt(itemId));
		this.isLoading = false;
	  } catch (error) {
		console.error('Error fetching data:', error);
		this.isLoading = false;
	  }
	}
  };
  </script>
  
  <style>
  #Item-page {
	text-align: center;
	padding: 2rem;
	display: flex;
	justify-content: center;
	width: 100%;
  }
  
  .form-container {
	width: 100%;
	background: white;
	padding: 20px;
	border-radius: 12px;
	box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  }
  
  h1 {
	color: var(--color-dark);
	font-size: 2rem;
	margin-bottom: 1rem;
  }
  
  .form {
	display: grid;
	gap: 16px;
  }
  
  .item-detail {
	background: var(--color-light);
	padding: 12px;
	border-radius: 8px;
	box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.1);
	text-align: left;
	display: flex;
	justify-content: space-between;
	align-items: center;
  }
  
  .item-detail p {
	font-size: 1rem;
	color: var(--color-dark);
	font-weight: bold;
	margin: 0;
  }
  
  .button-container {
	display: flex;
	justify-content: space-between;
	margin-top: 20px;
  }
  
  .edit-button, .close-button {
	padding: 10px 20px;
	font-size: 1rem;
	border: none;
	border-radius: 8px;
	cursor: pointer;
	transition: color 0.1s ease;
  }
  
  .edit-button {
  background-color: var(--color-theme-primary);
  color: var(--color-theme-secondary);
  transition: color 0.3s ease;
  }

  .edit-button:hover {
  background-color: var(--color-dark);
  color: var(--color-light);
  }
  
  .close-button {
	background-color: var(--color-light);
	color: var(--color-dark);
  }

.close-button:hover{
	background-color: var(--color-dark);
  	color: var(--color-light);
}
  </style>