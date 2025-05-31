<template>
  <main id="ItemView-page">
    <div class="form-container">
      <!-- Boutons -->
      <div class="button-container">
        <h1>Détails de l'équipement</h1>
        <button disabled class="close-button">Fermer</button>
      </div>

      <div class="form-grid">
        <!-- Bloc 1 : Description générale -->
        <section class="block description-block">
          <h2>Description</h2>
          <div class="item-summary">
            <img src="../assets/logo.png" alt="Image équipement" class="item-image" />
            <div>
              <h3>Type - Modèle</h3>
              <p><strong>Nom :</strong> Nom de l'équipement</p>
              <p>Description de l'équipement.</p>
            </div>
          </div>
        </section>

        <!-- Bloc 2 : Infos avancées -->
        <section class="block advanced-info-block">
          <h2>Informations avancées</h2>
          <ul>
            <li><strong>Numéro de série :</strong> 123456</li>
            <li><strong>Fabricant :</strong> Fabricant X</li>
            <li><strong>Référence AVC :</strong> N/A</li>
            <li><strong>Matériaux :</strong> N/A</li>
            <li><strong>Emplacement :</strong> N/A</li>
          </ul>
        </section>

        <!-- Bloc 3 : Documents -->
        <section class="block docs-block">
          <h2>Documents</h2>
          <ul>
            <li>Manuel non disponible</li>
            <li>Certification non disponible</li>
          </ul>
        </section>

        <!-- Bloc 4 : Dates importantes -->
        <section class="block dates-block">
          <h2>Dates importantes</h2>
          <ul>
            <li><strong>Date d'acquisition :</strong> --/--/----</li>
            <li><strong>Date de première utilisation :</strong> --/--/----</li>
            <li><strong>Date limite de réforme :</strong> --/--/----</li>
            <li><strong>Date de réforme :</strong> --/--/----</li>
            <li><strong>Date d'enregistrement :</strong> --/--/----</li>
          </ul>
        </section>

        <!-- Bloc 5 : Statut et conditions -->
        <section class="block status-block">
          <h2>Statut & Conditions</h2>
          <p><strong>Statut :</strong> OK</p>
          <p><strong>Conditions spéciales :</strong> Aucune</p>
        </section>

        <!-- Bloc 6 : Modifier statut -->
        <section class="block edit-status-block">
          <label for="status">État* :</label>
          <select id="status" disabled>
            <option>Bon état</option>
            <option>Moyen état</option>
            <option>Mauvais état</option>
          </select>
        </section>
      </div>
    </div>
  </main>
</template>



<style scoped>
#ItemView-page {
  padding: 1rem;
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

.button-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}

h1 {
  font-size: 1.6rem;
  margin: 0;
  color: var(--color-dark);
}

.form-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 1rem;
  justify-content: space-between;
}

.block {
  flex: 1 1 30%;
  min-width: 280px;
  max-width: 32%;
  background: var(--color-light);
  padding: 16px;
  border-radius: 8px;
  box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.1);
  box-sizing: border-box;
}

h2 {
  font-size: 1.2rem;
  color: var(--color-theme-primary);
  margin-bottom: 0.5rem;
  border-bottom: 2px solid var(--color-theme-primary);
  padding-bottom: 4px;
}

.item-summary {
  display: flex;
  gap: 10px;
  align-items: center;
  flex-wrap: wrap;
}

.item-image {
  width: 80px;
  height: 80px;
  object-fit: contain;
  border-radius: 6px;
  background: #eee;
  border: 1px solid #ccc;
}

ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

ul li {
  font-weight: 600;
  margin-bottom: 6px;
}

select {
  width: 100%;
  padding: 6px 10px;
  border-radius: 6px;
  border: 1px solid #ccc;
  font-size: 1rem;
}

.edit-button, .close-button {
  padding: 10px 20px;
  font-size: 1rem;
  border: none;
  border-radius: 8px;
  cursor: not-allowed;
  background-color: var(--color-dark);
  color: var(--color-light);
}

.close-button:hover {
  background-color: var(--color-theme-primary);
  color: var(--color-theme-secondary);
}
</style>




<script>
import axios from 'axios';

export default {
  data() {
    return {
      item: null,
      isLoading: true,
    };
  },
  async created() {
    const itemId = parseInt(this.$route.query.item_id);
    if (!itemId) {
      console.error('ID de l\'item manquant ou invalide');
      this.isLoading = false;
      return;
    }
    try {
      const response = await axios.get('/items');
      const itemsArray = Array.isArray(response.data) ? response.data : response.data.data;
      this.item = itemsArray.find(i => i.id === itemId);

      if (!this.item) {
        console.warn(`Aucun item trouvé avec l'id ${itemId}`);
      }
      this.isLoading = false;
    } catch (error) {
      console.error('Erreur chargement item:', error);
      this.isLoading = false;
    }
  }
};
</script>