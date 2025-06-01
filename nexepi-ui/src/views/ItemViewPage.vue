<template>
  <main id="ItemView-page">
    <div class="form-container">
      <!-- Titre -->
      <div class="button-container">
        <h1 class="icon-title">
          <span class="material-icons">live_help</span>
          Détails de l'équipement
        </h1>
        <div style="display: flex; gap: 1rem;">
          <h1 class="icon-title">État : {{ item?.status || "Inconnu" }}</h1>
        </div>
        <div style="display: flex; gap: 1rem;">
          <button v-if="!isError" @click="editItem" class="edit-button">Éditer</button>
          <button @click="closePage" class="close-button">Fermer</button>
        </div>
      </div>

      <div v-if="isLoading">
        Chargement en cours...
      </div>

      <div v-if="isError">
        Erreur lors du chargement des données
      </div>

      <div v-else-if="!isError">
        <div class="form-grid-two-columns">
          <div class="left-column">
            <div class="form-grid">
              <!-- Description -->
              <section class="block">
                <h2 class="block-title">
                  <span class="material-icons">info</span>
                  Description
                </h2>
                <div class="item-summary">
                  <img src="../assets/logo.png" alt="Image équipement" class="item-image" />
                  <div>
                    <h3>{{ item?.type || "Type inconnu" }} - {{ item?.model || "Modèle inconnu" }}</h3>
                    <p>{{ item?.name}}</p>
                    <p>{{ item?.description}}</p>
                    <p>{{ item?.comments}}</p>
                  </div>
                </div>
              </section>

              <!-- Informations techniques -->
              <section class="block">
                <h2 class="block-title">
                  <span class="material-icons">precision_manufacturing</span>
                  Informations techniques
                </h2>
                <ul>
                  <li><strong>Numéro de série :</strong> {{ item?.sn || "Non renseigné" }}</li>
                  <li><strong>Référence nexepi :</strong> {{ item?.nexepi_id }}</li>
                  <li><strong>Fabricant :</strong> {{ item?.manufacturer || "Inconnu" }}</li>
                  <li><strong>Matériaux :</strong> {{ item?.materials || "N/A" }}</li>
                  <li><strong>Durée de vie maximale :</strong> {{ item?.lifespan || "N/A" }} ans</li>
                  <li><strong>Emplacement :</strong> {{ item?.location || "Non assigné" }}</li>
                </ul>
              </section>

              <!-- Documents -->
              <section class="block">
                <h2 class="block-title">
                  <span class="material-icons">folder</span>
                  Documents
                </h2>
                <ul>
                  <li>
                    <span class="material-icons">menu_book</span>
                    <strong>Manuel : </strong>{{ item?.manual ? "Disponible" : "Non disponible" }}
                  </li>
                  <li>
                    <span class="material-icons">verified</span>
                    <strong>Certification : </strong>{{ item?.certification ? "Disponible" : "Non disponible" }}
                  </li>
                </ul>
              </section>

              <!-- Dates -->
              <section class="block">
                <h2 class="block-title">
                  <span class="material-icons">event</span>
                  Dates importantes
                </h2>
                <ul>
                  <li><strong>Acquisition :</strong> {{ item?.acquisition_date?.split('T')[0] || "--/--/--" }}</li>
                  <li><strong>Première utilisation :</strong> {{ item?.first_use_date?.split('T')[0] || "--/--/--" }}</li>
                  <li><strong>Date de garantie :</strong> {{ item?.warranty_expiration?.split('T')[0] || "--/--/--" }}</li>
                  <li><strong>Réforme prévue :</strong> {{ item?.max_discard_date?.split('T')[0] || "--/--/--" }}</li>
                  <li><strong>Date d'enregistrement :</strong> {{ item?.record_date?.split('T')[0] || "--/--/--" }}</li>
                  <li><strong>Heure d'enregistrement :</strong> {{ item?.record_time || "--:--:--" }}</li>
                </ul>
              </section>
            </div>
          </div>

          <div class="right-column">
            <section class="block history-block">
              <h2 class="block-title">
                <span class="material-icons">history</span>
                Historique
              </h2>
              <ul class="timeline">
                <li v-if="item?.createdAt">
                  <span class="material-icons">event</span>
                  {{ item.createdAt }} — Ajout de l\'équipement
                </li>
                <li v-if="item?.initialCheckDate">
                  <span class="material-icons">check_circle</span>
                  {{ item.initialCheckDate }} — Vérification initiale
                </li>
                <li v-if="item?.lastCheckDate">
                  <span class="material-icons">fact_check</span>
                  {{ item.lastCheckDate }} — Vérification périodique
                </li>
                <li v-if="item?.nextCheckDate">
                  <span class="material-icons">schedule</span>
                  {{ item.nextCheckDate }} — Prochaine vérification
                </li>
              </ul>
            </section>
          </div>
        </div>
      </div>
    </div>
  </main>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      isLoading: true,
      isError: false,
      item: {
        id: null,
        sn: null,
        type: null,
        manufacturer: null,
        model: null,
        year: null,
        acquisition_date: null,
        first_use_date: null,
        max_discard_date: null,
        discard_date: null,
        record_date: null,
        record_time: null,
        avc_ref: null,
        company_id: null,
        comments: null,
        manual_url: null,
        certification_url: null,
        photo_url: null,
        status: null,
        special_conditions: null,
        price_purchase: null,
        price_current: null,
        materials: null,
        description: null,
        location: null,
        maintenance_schedule: null,
        warranty_expiration: null,
        supplier_name: null,
        supplier_url: null,
        nexepi_id: null,
        createdAt: null,
        initialCheckDate: null,
        lastCheckDate: null,
        nextCheckDate: null,
        lifespan: null,
      },
    };
  },
  computed: {
    timelineEvents() {
      const rawEvents = [
        {
          date: this.item.createdAt,
          label: "Ajout de l’équipement",
          icon: "event",
        },
        {
          date: this.item.initialCheckDate,
          label: "Vérification initiale",
          icon: "check_circle",
        },
        {
          date: this.item.lastCheckDate,
          label: "Vérification périodique",
          icon: "fact_check",
        },
        {
          date: this.item.nextCheckDate,
          label: "Prochaine vérification",
          icon: "schedule",
        },
      ];

      return rawEvents
        .filter(e => e.date) // garde seulement ceux avec une date
        .sort((a, b) => new Date(a.date) - new Date(b.date)); // tri croissant
    },
  },
  async created() {
    const itemId = parseInt(this.$route.query.item_id);
    try {
      const res = await axios.get('/items');
      const items = Array.isArray(res.data) ? res.data : res.data.data;
      this.item = items.find(i => i.id === itemId) || {};
      // console.log("Item chargé :", this.item);
      this.isLoading = false;
      this.isError = false;
    } catch (e) {
      console.error("Erreur lors du chargement de l'item :", e);
      this.isLoading = false;
      this.isError = true;
    }
  },
  methods: {
	  editItem() {
      this.$router.push({
        name: 'ItemEditPage',
        query: { item_id: this.item?.id }
      });
    },
	  closePage() {
		  this.$router.go(-1);
	  }
	},
};
</script>

<style scoped>
#ItemView-page {
  padding: 2rem;
  display: flex;
  justify-content: center;
  width: 100%;
}

.form-container {
  width: 100%;
  background: var(--color-background);
  padding: 2rem;
  border-radius: 16px;
  box-shadow: 0 6px 18px rgba(0, 0, 0, 0.05);
}

.button-container {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 2rem;
}

h1.icon-title {
  display: flex;
  align-items: center;
  font-size: 2rem;
  font-weight: 700;
  gap: 0.5rem;
}

.form-grid {
  display: flex;
  flex-wrap: wrap;
  gap: 1.5rem;
  justify-content: space-between;
}

.block {
  flex: 1 1 30%;
  min-width: 300px;
  background: var(--vt-c-white-soft);
  padding: 1.2rem 1.5rem;
  border-radius: 12px;
  box-shadow: inset 0 1px 4px rgba(0, 0, 0, 0.05);
}

.block-title {
  display: flex;
  align-items: center;
  font-size: 1.25rem;
  color: var(--color-theme-primary);
  font-weight: 600;
  margin-bottom: 1rem;
  gap: 0.5rem;
}

ul {
  list-style: none;
  padding: 0;
  margin: 0;
}

ul li {
  font-weight: 600;
  margin-bottom: 8px;
  display: flex;
  align-items: center;
  gap: 6px;
}

.item-summary {
  display: flex;
  gap: 12px;
  align-items: flex-start;
  flex-wrap: wrap;
}

.item-image {
  max-width: 200px;
  max-height: 200px;
  object-fit: contain;
  border-radius: 8px;
  border: 1px solid #ccc;
  background: #eee;
}

.material-icons {
  font-size: 1.8rem;
}

.form-grid-two-columns {
  display: flex;
  gap: 1.5rem;
  align-items: stretch;
}

.left-column {
  flex: 3;
  display: flex;
  flex-wrap: wrap;
  gap: 1.5rem;
}

.left-column .block {
  flex: 1 1 45%;
  min-width: 280px;
}

.right-column {
  flex: 1;
  min-width: 300px;
}

.history-block {
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
}

.history-section {
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
}

.history-section h4 {
  margin-bottom: 0.5rem;
  font-size: 1.05rem;
  color: #2c3e50;
}

.timeline {
  display: flex;
  flex-direction: column;
  gap: 1rem;
  padding-left: 0;
  list-style: none;
  margin-top: 1rem;
}

.timeline li {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  background: #f1f1f1;
  padding: 0.75rem 1rem;
  border-radius: 8px;
  font-weight: 500;
  box-shadow: 0 1px 3px rgba(0,0,0,0.05);
}

</style>