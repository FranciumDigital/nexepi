<template>
	<main id="Add-page"><!-- Check if the user is authenticated -->
		<div v-if="!isAuthenticated" class="not-auth">
			<h2>🔒 Accès restreint</h2>
			<p>Vous devez être connecté pour accéder à cette page.</p>
			<router-link to="/account" class="cta-button">Se connecter</router-link>
		</div>

		<!-- If the user is authenticated -->
		<div v-else>
		<div class="form-container">
			<h1>Ajouter un nouvel EPI</h1>
			
			<form @submit.prevent="submitForm" class="form">
				<div class="form-group">
					<label for="sn">SN* :</label>
					<input id="sn" type="text" v-model="formData.sn" required />
				</div>
		
				<div class="form-group">
					<label for="type">Type* :</label>
					<input id="type" type="text" v-model="formData.type" required />
				</div>
		
				<div class="form-group">
					<label for="manufacturer">Fabricant* :</label>
					<input id="manufacturer" type="text" v-model="formData.manufacturer" required />
				</div>
		
				<div class="form-group">
					<label for="model">Modèle* :</label>
					<input id="model" type="text" v-model="formData.model" required />
				</div>
		
				<div class="form-group">
					<label for="year">Année* :</label>
					<input id="year" type="number" v-model="formData.year" required />
				</div>

				<div class="form-group">
					<label for="acquisition_date">Date d'acquisition* :</label>
					<input id="acquisition_date" type="date" v-model="formData.acquisition_date" required />
				</div>

				<div class="form-group">
					<label for="max_discard_date">Durée d'utilisation maximale (années)* :</label>
					<input id="max_discard_date" type="number" v-model="formData.max_discard_date" required />
				</div>

				<div class="form-group">
					<label for="discard_date">Date de mise au rebut* :</label>
					<input id="discard_date" type="date" v-model="formData.discard_date" required />
				</div>

				<div class="form-group">
					<label for="avc_ref">Réference Au Vieux Campeur* :</label>
					<input id="avc_ref" type="number" v-model="formData.avc_ref" required />
				</div>

				<div class="form-group">
					<label for="manual_url">URL du manuel* :</label>
					<input id="manual_url" type="text" v-model="formData.manual_url" required />
				</div>

				<div class="form-group">
					<label for="certification_url">URL du certificat* :</label>
					<input id="certification_url" type="text" v-model="formData.certification_url" required />
				</div>

				<div class="form-group">
					<label for="owner">Propriétaire* :</label>
					<input id="owner" type="text" v-model="formData.owner" required />
				</div>

				<div class="form-group">
					<label for="comments">Commentaires* :</label>
					<input id="comments" type="text" v-model="formData.comments" required />
				</div>

				<div class="form-group">
					<label for="status">Statut* :</label>
					<input id="status" type="text" v-model="formData.status" required />
				</div>
				
				<button type="submit" :disabled="isFormInvalid" class="submit-btn">Ajouter</button>
			</form>
		</div></div>
	</main>
</template>

<style scoped>
	#Add-page {
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
	.form-container {
	  width: 100%;
	  margin: 0 auto;
	  padding: 20px;
	  background-color: var(--color-light);
	  border-radius: 10px;
	  box-shadow: 0 4px 8px #00000039;
	}
  
	h1 {
	  color: var(--color-dark);
	  font-size: 2rem;
	  margin-bottom: 20px;
	}
  
	.form {
	  display: grid;
	  gap: 20px;
	}
  
	.form-group {
	  display: flex;
	  flex-direction: column;
	}
  
	label {
	  font-size: 16px;
	  margin-bottom: 8px;
	  color: var(--color-dark);
	}
  
	input {
	  padding: 10px;
	  border: 1px solid var(--color-dark);
	  border-radius: 5px;
	  font-size: 16px;
	  outline: none;
	  transition: border 0.3s ease;
	}
  
	input:focus {
	  border-color: var(--color-theme-primary);
	}
  
	.submit-btn {
  background-color: var(--color-theme-primary);
  color: var(--color-theme-secondary);
	  font-size: 16px;
	  padding: 10px;
	  border: none;
	  border-radius: 5px;
	  cursor: pointer;
	  transition: color 0.3s ease;
	  width: 100%;
	}
  
	.submit-btn:hover {
	  color: var(--color-light);
	  background-color: var(--color-dark);
	}
  
	@media (max-width: 600px) {
	  .form-container {
		padding: 15px;
	  }
  
	  h2 {
		font-size: 20px;
	  }
  
	  .form {
		gap: 15px;
	  }
	}
  </style>

<script>
  import axios from 'axios';

  export default {
    data() {
      return {
		isAuthenticated: false,
        formData: {
          sn: '',
          type: '',
          manufacturer: '',
          model: '',
          year: null,
          acquisition_date: '',
          max_discard_date: null,
          discard_date: '',
          avc_ref: null,
          manual_url: '',
          certification_url: '',
          owner: '',
          comments: '',
          status: ''
        }
      };
    },
	async mounted() {
		this.userToken = localStorage.getItem('token');
		if (this.userToken) {
			this.isAuthenticated = true;
		} else {
			this.isAuthenticated = false;
		}
  	},
    computed: {
      isFormInvalid() {
        return !Object.values(this.formData).every(value => value && value !== '');
      },
      discardDate() {
        // Calculate discard_date if acquisition_date and max_discard_date are provided
        if (this.formData.acquisition_date && this.formData.max_discard_date) {
          const acquisitionDate = new Date(this.formData.acquisition_date);
          const discardDate = new Date(acquisitionDate);
          discardDate.setFullYear(discardDate.getFullYear() + this.formData.max_discard_date);
          return discardDate.toISOString().split('T')[0]; // Return in 'YYYY-MM-DD' format
        }
        return ''; // Return empty string if data is missing
      }
    },
    watch: {
      // Watch for changes in acquisition_date or max_discard_date to update discard_date
      'formData.acquisition_date': function() {
        this.updateDiscardDate();
      },
      'formData.max_discard_date': function() {
        this.updateDiscardDate();
      }
    },
    methods: {
      async submitForm() {
        try {
          const response = await axios.post('https://api.NEXEPI.francium-digital.fr/save/add', this.formData);
          alert('Données ajoutées avec succès!');
          this.resetForm();
        } catch (error) {
          console.error('Erreur lors de l\'ajout de l\'élément:', error);
          alert('Erreur lors de l\'ajout');
        }
      },
      resetForm() {
        this.formData = {
          sn: '',
          type: '',
          manufacturer: '',
          model: '',
          year: null,
          acquisition_date: '',
          max_discard_date: null,
          discard_date: '',
          avc_ref: null,
          manual_url: '',
          certification_url: '',
          owner: '',
          comments: '',
          status: ''
        };
      },
      // Optional: update discard_date directly
      updateDiscardDate() {
        if (this.formData.acquisition_date && this.formData.max_discard_date) {
          const acquisitionDate = new Date(this.formData.acquisition_date);
          const discardDate = new Date(acquisitionDate);
          discardDate.setFullYear(discardDate.getFullYear() + this.formData.max_discard_date);
          this.formData.discard_date = discardDate.toISOString().split('T')[0]; // Set it in 'YYYY-MM-DD'
        }
      }
    }
  };
</script>