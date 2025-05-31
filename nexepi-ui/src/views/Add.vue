<template>
  <main id="Add-page">
    <div class="form-container">
      <h1>Ajouter un nouvel EPI</h1>

      <form @submit.prevent="submitForm" class="form-grid">
        <div class="form-group">
          <label for="sn">SN*</label>
          <input id="sn" type="text" v-model="formData.sn" required />
        </div>

        <div class="form-group">
          <label for="type">Type*</label>
          <input id="type" type="text" v-model="formData.type" required />
        </div>

        <div class="form-group">
          <label for="manufacturer">Fabricant*</label>
          <input id="manufacturer" type="text" v-model="formData.manufacturer" required />
        </div>

        <div class="form-group">
          <label for="model">Modèle*</label>
          <input id="model" type="text" v-model="formData.model" required />
        </div>

        <div class="form-group">
          <label for="year">Année*</label>
          <input id="year" type="number" v-model="formData.year" required />
        </div>

        <div class="form-group">
          <label for="acquisition_date">Date d'acquisition</label>
          <input id="acquisition_date" type="date" v-model="formData.acquisition_date" required />
        </div>

        <div class="form-group">
          <label for="max_discard_date">Durée max (années)</label>
          <input id="max_discard_date" type="number" v-model="formData.max_discard_date" required />
        </div>

        <div class="form-group">
          <label for="discard_date">Date de rebut</label>
          <input id="discard_date" type="date" v-model="formData.discard_date" required />
        </div>

        <div class="form-group">
          <label for="avc_ref">Référence AVC</label>
          <input id="avc_ref" type="number" v-model="formData.avc_ref" required />
        </div>

        <div class="form-group">
          <label for="manual_url">URL du manuel</label>
          <input id="manual_url" type="text" v-model="formData.manual_url" required />
        </div>

        <div class="form-group">
          <label for="certification_url">URL certificat</label>
          <input id="certification_url" type="text" v-model="formData.certification_url" required />
        </div>

        <div class="form-group">
          <label for="owner">Propriétaire*</label>
          <input id="owner" type="text" v-model="formData.owner" required />
        </div>

        <div class="form-group">
          <label for="comments">Commentaires</label>
          <input id="comments" type="text" v-model="formData.comments" required />
        </div>

        <div class="form-group">
          <label for="status">Statut*</label>
          <input id="status" type="text" v-model="formData.status" required />
        </div>

        <div class="form-actions">
          <button type="submit" :disabled="isFormInvalid" class="submit-btn">Ajouter</button>
        </div>
      </form>
    </div>
  </main>
</template>


<style scoped>
#Add-page {
  display: flex;
  justify-content: center;
  padding: 2rem;
  width: 100%;
  box-sizing: border-box;
}

.form-container {
  width: 100%;
  max-width: 1100px;
  background-color: #f9f9f9;
  padding: 2rem;
  border: 1px solid #ddd;
  border-radius: 6px;
  box-sizing: border-box;
}

h1 {
  font-size: 1.8rem;
  color: var(--color-dark);
  margin-bottom: 1.5rem;
  font-weight: 600;
  text-align: center;
}

.form-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(260px, 1fr));
  gap: 1.5rem;
}

.form-group {
  display: flex;
  flex-direction: column;
}

label {
  font-size: 0.95rem;
  margin-bottom: 6px;
  color: #333;
  font-weight: 500;
}

input {
  padding: 8px 12px;
  font-size: 0.95rem;
  border: 1px solid #ccc;
  border-radius: 3px;
  background-color: #fff;
  transition: border-color 0.2s;
}

input:focus {
  border-color: var(--color-theme-primary);
  outline: none;
}

.form-actions {
  grid-column: 1 / -1;
  display: flex;
  justify-content: flex-end;
  margin-top: 1rem;
}

.submit-btn {
  background-color: var(--color-theme-primary);
  color: var(--color-theme-secondary);
  padding: 10px 20px;
  font-size: 1rem;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.2s ease;
}

.submit-btn:hover {
  background-color: var(--color-dark);
  color: var(--color-light);
}

@media (max-width: 600px) {
  .form-grid {
    grid-template-columns: 1fr;
  }

  .form-actions {
    justify-content: center;
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