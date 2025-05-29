<template>
  <main id="header-page">
    <div class="greetings">
      <!-- Lien NEXEPI -->
      <a href="/" rel="noopener">NEXEPI</a>

      <!-- Bouton pour réafficher le warning -->
      <button @click="$emit('show-warning')" class="warning-btn">
        <span class="material-icons">warning</span>
      </button>

      <!-- Lien vers le compte -->
      <router-link to="/account" class="button">
        <span class="material-icons">account_circle</span>
        <b>{{ user.firstname }} {{ user.lastname }}</b>
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
      required: false,
      default: () => ({
        firstname: 'Robin',
        lastname: 'Letrange'
      })
    }
  }
}
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

b {
  margin-left: 10px;
  margin-right: 0px;
  font-weight: 500;
  font-size: 1.3rem;
  position: relative;
  text-align: center;
  font-weight: bold;
  color: var(--color-theme-secondary);
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

.warning-btn {
  display: flex;
  border: none;
  align-items: center;
  cursor: pointer;
  
  font-size: 3rem;
  border-radius: 5px;
  padding: 5px;
  background-color: var(--color-theme-secondary);
  color: var(--color-theme-primary);
}

.warning-btn:hover {
  background-color: var(--color-theme-primary);
  color: var(--color-theme-secondary);
  border-radius: 5px;
  transition: color, background-color 0.3s ease-in-out;
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

  .warning-btn {
    font-size: 2rem;
    padding: 2px;
  }
}
</style>