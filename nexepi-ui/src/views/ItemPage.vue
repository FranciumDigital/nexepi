<template>
	<main id="Item-page">
	  <div class="form-container">
		<h1>Détails de l'élément</h1>
  
		<div v-if="item" class="form">
		  <div class="item-detail" v-for="(value, key) in formattedItem" :key="key">
			<p><strong>{{ key }} :</strong> {{ value }}</p>
		  </div>
		</div>
  
		<div v-else-if="isLoading" class="form loading">
		  <p>Chargement des informations de l'élément...</p>
		</div>
  
		<div v-else class="form error">
		  <p>Erreur lors du chargement de l'élément. Veuillez réessayer plus tard.</p>
		</div>

		<div class="button-container">
			<button v-if="item" button @click="editItem" class="edit-button">Éditer</button>
			<button @click="closePage" class="close-button">Fermer</button>
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
	  };
	},
	computed: {
		formattedItem() {
			if (!this.item) return {};

			return {
			'ID': this.item.NEXEPI_id || 'Non spécifié',
			'Numéro de Série': this.item.sn || 'Non spécifié',
			'Type': this.item.type || 'Non spécifié',
			'Modèle': this.item.model || 'Non spécifié',
			'Fabricant': this.item.manufacturer || 'Non spécifié',
			'Année': this.item.year || 'Non spécifiée',
			'Date d’Acquisition': this.item.acquisition_date || 'Non spécifiée',
			'Première Utilisation': this.item.first_use_date || 'Non spécifiée',
			'Date de Mise au Rebut (Prévue)': this.item.discard_due_date || 'Non spécifiée',
			'Date de Mise au Rebut (Effective)': this.item.discard_date || 'Non spécifiée',
			'Date d’Enregistrement': this.item.record_date || 'Non spécifiée',
			'Heure d’Enregistrement': this.item.record_time || 'Non spécifiée',
			'Propriétaire': this.item.owner || 'Non spécifié',
			'Référence Au Vieux Campeur': this.item.avc_ref || 'Non spécifiée',
			'Commentaires': this.item.comments || 'Aucun commentaire',
			'Statut': this.item.status || 'Non spécifié',
			'Conditions Spéciales': this.item.special_conditions || 'Non spécifié',
			'Prix Achat': (this.item.price?.purchase ?? 'Non spécifié') + ' €',
			'Prix Actuel': (this.item.price?.current ?? 'Non spécifié') + ' €',
			'Matériaux': this.item.materials || 'Non spécifié',
			'Description': this.item.description || 'Non spécifiée',
			'Localisation': this.item.location || 'Non spécifiée',
			'Planning Maintenance': this.item.maintenance_schedule || 'Non spécifié',
			'Expiration Garantie': this.item.warranty_expiration || 'Non spécifiée',
			'Fournisseur': this.item.supplier?.name || 'Non spécifié',
			'Lien Fournisseur': this.item.supplier?.url || 'Non spécifié',
			'Lien Manuel': this.item.manual_url || 'Non spécifié',
			'Lien Certification': this.item.certification_url || 'Non spécifié',
			'Photo': this.item.photo_url && this.item.avc_ref 
				? `${this.item.photo_url}${this.item.avc_ref}.jpg`
				: 'Aucune photo disponible',
			'Dernière Inspection': this.item.inspections?.[0]?.date || 'Non spécifiée',
			'Heure de la Dernière Inspection': this.item.inspections?.[0]?.time || 'Non spécifiée',
			'Statut de la Dernière Inspection': this.item.inspections?.[0]?.status || 'Non spécifié',
			'Commentaires de la Dernière Inspection': this.item.inspections?.[0]?.comments || 'Aucun commentaire'
			};
		}
	},
	methods: {
	  editItem() {
		this.$router.push({ name: 'EditItem', params: { NEXEPI_id: this.item.NEXEPI_id } });
	  },
	  closePage() {
		this.$router.go(-1);
	  }
	},
	async created() {
		const user = JSON.parse(localStorage.getItem('user'));
		this.userToken = user?.user_token;
		if (this.userToken) {
			this.isAuthenticated = true;
		} else {
			this.isAuthenticated = false;
		}

		if(this.isAuthenticated) {
			const NEXEPI_id = this.$route.params.NEXEPI_id;
			if (!NEXEPI_id) {
				console.error('Item ID is missing in the URL path');
				return;
			}
		
			const company_token = localStorage.getItem('company_token');
			try {
				const response = await axios.get(`${import.meta.env.VITE_NEXEPI_API_URL}/list?user_token=${this.userToken}&company_token=${company_token}`);
				this.item = response.data.data.find(i => i.NEXEPI_id === parseInt(NEXEPI_id));
				this.isLoading = false;
			} catch (error) {
				console.error('Error fetching data:', error);
				this.isLoading = false;
			}
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
  }
  
  .edit-button {
	background-color: var(--color-theme-primary);
	color: var(--color-dark);
  }
  
  .close-button {
	background-color: var(--color-dark);
	color: var(--color-light);
  }


  /* Media query for small screens (mobile devices) */
@media (max-width: 370px) {
	.item-detail {
		max-width: 85%;
	}
}

@media (max-width: 340px) {
	.item-detail {
		max-width: 75%;
	}
}

@media (max-width: 320px) {
	.item-detail {
		max-width: 65%;
	}
}
</style>