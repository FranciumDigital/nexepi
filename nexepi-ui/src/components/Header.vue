<template>
  <main id="header-page">
    <div class="greetings">
      <!-- Lien NEXEPI -->
      <a href="/" rel="noopener">NEXEPI<!--<img src="../../src/assets/logo.png" alt="Logo NEXEPI" class="logo-img" />--></a>

      <!-- Bouton pour réafficher le warning -->
      <!--<button @click="$emit('show-warning')" class="warning-btn">
        <span class="material-icons">warning</span>
      </button> -->

      <!-- Date/heure -->
      <router-link class="button">
        <span class="material-icons">schedule</span>
        <b>{{ date }} {{ heure }}</b>
      </router-link>

      <!-- Lien vers la société -->
      <router-link to="/companie" class="button">
        <span class="material-icons">apartment</span>
        <c>{{ company }}</c>
      </router-link>

      <!-- Lien vers l'utilisateur -->
      <router-link to="/account" class="button">
        <span class="material-icons">account_circle</span>
        <c>{{ user.firstname }} {{ user.lastname }}</c>
      </router-link>
    </div>
  </main>
</template>

<script>
export default {
  name: 'HeaderPage',
  props: {
    user: {
      type: Object,
      default: () => ({
        firstname: 'John',
        lastname: 'Doe'
      })
    },
    company: {
      type: String,
      default: 'Francium'
    }
  },
  data() {
    return {
      date: '',
      heure: ''
    };
  },
  mounted() {
    this.updateDateTime();
    setInterval(this.updateDateTime, 1000); // met à jour chaque seconde
  },
  methods: {
    updateDateTime() {
      const now = new Date();
      this.date = now.toLocaleDateString();
      this.heure = now.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    }
  }
};
</script>

<style scoped>
#header-page {
	text-align: left;
	padding: 0;
	margin: 0;
	width: 100%;
}

.greetings {
  width: 100%;
  background-color: var(--color-theme-primary);
  color: var(--color-theme-secondary);
  display: flex;
  align-items: center;
  justify-content: space-between;
}

a {
  margin-left: 10px;
  margin-right: 0px;
  font-weight: 500;
  font-size: 2.6rem;
  position: relative;
  text-align: center;
  font-weight: bold;
  color: var(--color-theme-secondary);
}

b,c {
  margin-left: 10px;
  margin-right: 0px;
  font-weight: 500;
  font-size: 1.3rem;
  position: relative;
  text-align: center;
  font-weight: bold;
  color: var(--color-theme-secondary);
}

c:hover {
  text-decoration: underline;
}

.button {
  display: flex;
  align-items: center;
  text-decoration: none;
  padding: 0.5rem 1rem;
}

.material-icons {
  font-size: 3rem;
  border-radius: 5px;
}

@media (max-width: 400px) {
  a {
    margin-left: 5px;
    font-size: 2rem;
  }

  .material-icons {
    font-size: 2rem;
    border-radius: 5px;
  }
}
</style>