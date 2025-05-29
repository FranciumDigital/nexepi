<template>
	<main id="Account-page">
	  <h1>Compte Utilisateur</h1>
	  <p v-if="!userLoggedIn">Veuillez vous connecter pour accéder à votre profil.</p>
	  <p v-if="userLoggedIn">Bienvenue {{ currentuser }}.</p>
  
	  <div>
		<form @submit.prevent="loginForm">
		  <div>
			<label for="username">Nom d'utilisateur</label>
			<input type="text" v-model="loginFormData.username" id="username" required />
		  </div>
		  <div>
			<label for="password">Mot de passe</label>
			<input type="text" v-model="loginFormData.password" id="password" required />
		  </div>
		  <button type="submit">Se connecter</button>
		</form>
	  </div>
	</main>
</template>
  
<script>
  import axios from 'axios';

  export default {
	data() {
	  return {
		loginFormData: {
			username: '',
			password: ''
        },
		currentuser: '',
		userLoggedIn: false
	  };
	},
	methods: {
		async loginForm() {
        try {
          const response = await axios.post('https://api.NEXEPI.francium-digital.fr/account/login', this.loginFormData);
          if (response.status === 200) {
            this.userLoggedIn = true;
			this.currentuser = this.loginFormData.username;
          	alert('Connecté avec succès!');
          }
        } catch (error) {
          console.error('Erreur :', error);
          alert('Erreur');
        }
		},
		async logoutForm() {
        try {
          if (response.status === 200) {
            this.userLoggedIn = false;
			this.currentuser = '';
          	alert('Déconnecté avec succès!');
          }
        } catch (error) {
          console.error('Erreur :', error);
          alert('Erreur');
        }
      },
	}
  };
</script>
  
<style scoped>
  #Account-page {
	text-align: left;
	padding: 2%;
	margin: 0;
	width: 100%;
  }
  
  form {
	max-width: 400px;
	margin: 0 auto;
  }
  
  form div {
	margin-bottom: 15px;
  }
  
  input {
	width: 100%;
	padding: 10px;
	margin-top: 5px;
  }
  
  button {
	padding: 10px 20px;
	background-color: var(--color-theme-primary);
	color: var(--color-dark);
	transition: ease-in-out 0.1s;
	border: none;
	cursor: pointer;
  }
  
  button:hover {
	background-color: var(--color-dark);
	color: var(--color-light);
  }
</style>