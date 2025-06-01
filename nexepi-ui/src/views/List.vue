<template>
  <main id="items-page">
    <h1>Liste des Équipements</h1>

    <!-- Sélecteur de colonnes sous forme de dropdown -->
    <div class="dropdown" @click.stop="toggleDropdown">
      <button class="dropdown-button">
        Colonnes à afficher ▼
      </button>
      <div v-if="dropdownOpen" class="dropdown-menu" @click.stop>
        <label
          v-for="col in columns"
          :key="col.key"
          class="dropdown-item"
        >
          <input
            type="checkbox"
            :value="col.key"
            v-model="visibleKeys"
          />
          {{ col.label }}
        </label>
      </div>
    </div>

    <div v-if="loading" class="loading">Chargement...</div>
    <div v-else>
      <div class="table-grid">
        <!-- Colonne 1 : Le tableau scrollable -->
        <div class="left-table-container">
          <table :style="{ tableLayout: 'fixed' }">
            <thead>
              <tr>
                <th
                  v-for="col in visibleColumns"
                  :key="col.key"
                  :style="{ width: col.width + 'px' }"
                  @click="sortBy(col.key)"
                >
                  {{ col.label }}
                  <span v-if="sortKey === col.key">
                    {{ sortOrders[col.key] === 1 ? '↑' : '↓' }}
                  </span>
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in sortedItems" :key="item.id">
                <td v-for="col in visibleColumns" :key="col.key">
                  {{ item[col.key] }}
                </td>
              </tr>
            </tbody>
          </table>
        </div>

        <!-- Colonne 2 : Espace réservé pour autre contenu -->
        <div class="right-table-container">
          <!-- Tu peux ajouter des filtres, infos, etc. ici -->
          <table :style="{ tableLayout: 'fixed' }">
            <thead>
              <tr>
                <th>
                  <span>Actions</span>
                </th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in sortedItems" :key="item.id">
                <td>
                  <span class="material-icons" title="Visualiser les informations détaillées" @click="goToView(item.id)">visibility</span>
                  <span class="material-icons" title="Modifier les informations" @click="goToEdit(item.id)">edit</span>
                  <span class="material-icons" title="Vérifier son état" @click="goToVerify(item.id)">check_circle</span>
                </td>
              </tr>
            </tbody>
          </table>
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
      items: [],
      loading: false,
      error: null,
      sortKey: "",
      sortOrders: {
        id: 1,
        model: 1,
        year: 1,
      },
      columns: [
        { key: "id", label: "ID", width: 50 },
        { key: "sn", label: "Numéro de série", width: 150 },
        { key: "type", label: "Type", width: 200 },
        { key: "manufacturer", label: "Fabricant", width: 200 },
        { key: "model", label: "Modèle", width: 200 },
        { key: "year", label: "Année", width: 100 },
        { key: "acquisitionDate", label: "Date d'acquisition", width: 300 },
        { key: "firstUseDate", label: "Date de première utilisation", width: 300 },
        { key: "maxDiscardDate", label: "Date de réforme max", width: 300 },
        { key: "discardDate", label: "Date de réforme", width: 300 },
        { key: "recordDate", label: "Date d'enregistrement", width: 300 },
        { key: "recordTime", label: "Heure d'enregistrement", width: 300 },
        { key: "avcRef", label: "Réf. AVC", width: 300 },
        { key: "companyId", label: "ID Société", width: 300 },
        { key: "comments", label: "Commentaires", width: 300 },
        { key: "manualUrl", label: "URL du manuel", width: 300 },
        { key: "certificationUrl", label: "URL de certification", width: 300 },
        { key: "status", label: "Statut", width: 150 },
        { key: "specialConditions", label: "Conditions spéciales", width: 300 },
        { key: "pricePurchase", label: "Prix d'achat", width: 300 },
        { key: "priceCurrent", label: "Prix actuel", width: 300 },
        { key: "materials", label: "Matériaux", width: 300 },
        { key: "description", label: "Description", width: 300 },
        { key: "location", label: "Emplacement", width: 300 }
      ],
      visibleKeys: ["id", "sn", "type", "manufacturer", "model", "year", "status"],
      dropdownOpen: false,
    };
  },
  computed: {
    visibleColumns() {
      return this.columns.filter((col) => this.visibleKeys.includes(col.key));
    },
    sortedItems() {
      if (!this.sortKey) return this.items;
      const order = this.sortOrders[this.sortKey];
      return [...this.items].sort((a, b) => {
        const aVal = a[this.sortKey];
        const bVal = b[this.sortKey];
        if (typeof aVal === "number" && typeof bVal === "number") {
          return (aVal - bVal) * order;
        }
        return aVal.toString().localeCompare(bVal.toString()) * order;
      });
    },
  },
  methods: {
    async fetchItems() {
      this.loading = true;
      this.error = null;
      try {
        const response = await axios.get("/items");
        this.items = response.data;
      } catch {
        this.error = "Erreur lors du chargement des items";
      } finally {
        this.loading = false;
      }
    },
    sortBy(key) {
      if (this.sortKey === key) {
        this.sortOrders[key] = -this.sortOrders[key];
      } else {
        this.sortKey = key;
      }s
    },
    toggleDropdown() {
      this.dropdownOpen = !this.dropdownOpen;
    },
    goToView(itemId) {
      this.$router.push({ name: 'ItemViewPage', query: { item_id: itemId } });
    },
    goToEdit(itemId) {
      this.$router.push({ name: 'ItemEditPage', query: { item_id: itemId } });
    },
    goToVerify(itemId) {
      this.$router.push({ name: 'ItemCheckPage', query: { item_id: itemId } });
    },
  },
  mounted() {
    this.fetchItems();
    // Optional: Close dropdown on outside click
    window.addEventListener("click", () => {
      this.dropdownOpen = false;
    });
  },
  beforeUnmount() {
    window.removeEventListener("click", () => {
      this.dropdownOpen = false;
    });
  },
};
</script>

<style scoped>
#items-page {
  padding: 2rem;
}

.dropdown {
  position: relative;
  display: inline-block;
  margin-bottom: 1rem;
}

.dropdown-button {
  padding: 6px 12px;
  cursor: pointer;
  user-select: none;
  border: 1px solid #ccc;
  background: white;
  border-radius: 4px;
}

.dropdown-menu {
  position: absolute;
  background: white;
  border: 1px solid #ccc;
  margin-top: 2px;
  z-index: 10;
  max-height: 500px;
  overflow-y: auto;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.15);
  width: 300px;
  border-radius: 4px;
}

.dropdown-item {
  display: flex;
  align-items: center;
  padding: 6px 12px;
  cursor: pointer;
  user-select: none;
}

.dropdown-item input {
  margin-right: 8px;
}

.loading {
  font-style: italic;
  color: gray;
}

table {
  width: 100%;
  border-collapse: collapse;
  table-layout: fixed;
}

th,
td {
  padding: 12px;
  border: 1px solid #ccc;
  text-align: left;
  overflow: hidden;
  white-space: nowrap;
  text-overflow: ellipsis;
  user-select: none;
}

thead {
  background-color: #f0f0f0;
}

th {
  cursor: pointer;
  position: relative;
}

.table-grid {
  display: grid;
  grid-template-columns: 1fr 120px;
  overflow: hidden;
  max-width: 100%;
}

.left-table-container {
  padding: 0;
  overflow-x: auto;
  background: var(--color-theme-secondary);
}

.right-table-container {
  padding: 0;
  overflow-x: auto;
  background: var(--color-theme-secondary);
}

.material-icons {
  cursor: pointer;
  font-size: 10px;
  margin-left: 10px;
  margin-right: 10px;
  transform: scale(2.4);
  transition: transform 0.2s ease;
}

.material-icons:hover {
  transform: scale(3);
  color: var(--color-theme-primary);
}
</style>
