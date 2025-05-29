<template>
	<main id="login-page">
		<div class="container">
			<h2>Connexion</h2>

			<!-- Bouton de connexion -->
			<button type="button" @click="login">Se connecter avec Francium Digital</button>

			<!-- Message d'état -->
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
			message: '',
			success: false,
		};
	},
	mounted() {
		// Vérifie si on est revenu du redirect avec un code
		const urlParams = new URLSearchParams(window.location.search);
		const code = urlParams.get('code');
		if (code) {
			this.handleCallback(code);
		}
	},
	methods: {
		// Redirection vers Authentik
		login() {
			const clientId = '1Wlvv7dXPBDcSRf2PeODymJGlT6mPFPAr8q37OPh';
			const redirectUri = 'https://NEXEPI.francium-digital.fr/callback';
			const scope = 'openid profile email NEXEPI-scope';

			const authUrl = `https://auth.francium-digital.fr/application/o/authorize/?client_id=${clientId}&redirect_uri=${redirectUri}&response_type=code&scope=${encodeURIComponent(scope)}`;

			window.location.href = authUrl;
		},

		// Callback : échange le code contre un token
		async handleCallback(code) {
			try {
				const response = await axios.get(`${import.meta.env.VITE_NEXEPI_API_URL}/callback?code=${code}`);
				const token = response.data.token;

				localStorage.setItem('auth_token', token);
				this.success = true;
				this.message = 'Connexion réussie ! 🎉';

				// Optionnel : rediriger vers une autre page
				// this.$router.push('/dashboard');
			} catch (error) {
				console.error(error);
				this.success = false;
				this.message = 'Erreur lors de la connexion.';
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

button {
	margin-top: 15px;
	padding: 10px 20px;
	background-color: var(--color-theme-primary);
	color: var(--color-dark);
	border: none;
	border-radius: 5px;
	width: 100%;
	cursor: pointer;
	transition: all 0.2s;
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
