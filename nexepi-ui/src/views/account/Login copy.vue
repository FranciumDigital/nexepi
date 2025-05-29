<template>
	<main id="login-page">
		<div class="container">
			<h2>Connexion</h2>
			<form @submit.prevent="login">
				<div>
					<label for="username">Nom d'utilisateur</label>
					<input v-model="username" type="text" id="username" required />
				</div>
				<div>
					<label for="password">Mot de passe</label>
					<input v-model="password" type="password" id="password" required />
				</div>
				<button type="submit">Se connecter</button>
			</form>

			<!-- Affichage des étapes de l'animation -->
			<div v-if="isLoading" class="wait-container">
				<div class="spinner"></div>
				<div class="loading-text">{{ currentStep }}</div>
			</div>

			<!-- Message de retour après connexion -->
			<div v-if="message" :class="{ success: success, error: !success }">
				{{ message }}
			</div>
		</div>
	</main>
</template>

  
<script>
import axios from 'axios';
export default {
	data() {
	  return {
		username: '',
		password: '',
		message: '',
		success: false
	  };
	},
	methods: {
		async login() {
			try {
				const response = await axios.post(`${import.meta.env.VITE_NEXEPI_API_URL}/login`, {
					username: this.username,
					password: this.password
				});

				const data = response.data;

				if (data.success) {
					console.log('✅ Connexion réussie');
					this.message = data.message;
					this.success = true;

					// Stocker le token utilisateur et l'utilisateur lui-même
					localStorage.setItem('token', 'valid-token'); // <- à remplacer par un vrai token un jour ?
					localStorage.setItem('user', JSON.stringify(data.user));
					localStorage.setItem('company_token', data.company_token);

					// Redirection vers l'espace connecté
					location.href = 'https://NEXEPI.francium-digital.fr/account/';
				} else {
					this.message = data.message;
					this.success = false;
				}
			} catch (error) {
				console.error('Erreur lors de la connexion :', error);
				this.message = 'Erreur lors de la connexion.';
				this.success = false;
			}
		}
	}
  };
</script>
  
<style scoped>
#login-page {
	text-align: left;
	padding: 2%;
	margin: auto;
	width: 40%;
}

.container {
	width: 100%;
	height: 500px;
	padding: 20px;
	background-color: var(--color-light);
	border-radius: 10px;
	box-shadow: 0 4px 8px #00000039;
}

input {
	display: block;
	margin-bottom: 15px;
	width: 100%;
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

button {
	margin-top: 15px;
	padding: 10px 20px;
	background-color: var(--color-theme-primary);
	color: var(--color-dark);
	transition: ease-in-out 0.1s;
	border: none;
	cursor: pointer;
	border: none;
	border-radius: 5px;
	width: 100%;
}
  
button:hover {
	background-color: var(--color-dark);
	color: var(--color-light);
}

.success {
	color: green;
}

.error {
	color: red;
}
</style>