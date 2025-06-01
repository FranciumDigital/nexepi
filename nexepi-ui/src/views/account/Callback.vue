<template>
	<main id="callback-page">
		<p v-if="success" class="success">Connecté !</p>
		<p v-else-if="message" :class="{ success: success, error: !success }">{{ message }}</p>
		<p v-else>En attente de connexion...</p>
		<div class="spinner"></div>
	</main>
</template>

<script>
import axios from 'axios';

export default {
	data() {
		return {
			message: '',
			success: false
		};
	},
	async mounted() {
		const urlParams = new URLSearchParams(window.location.search);
		const code = urlParams.get('code');
		console.log(code);

		if (code) {
			try {
				const response = await axios.get(`${import.meta.env.VITE_NEXEPI_API_URL}/callback?code=${code}`);
				const token = response.data.token;
				const userInfo = response.data.user;

				if (token && userInfo) {
					this.message = "Connecté avec succès ✅";
					this.success = true;

					// Sauvegarde propre des infos dans le localStorage
					localStorage.setItem('token', token);
					localStorage.setItem('companyName', userInfo.company);
					localStorage.setItem('userRole', userInfo.roles);
					localStorage.setItem('user', JSON.stringify(userInfo));
					localStorage.setItem('isLoggedIn', 'true');
					this.$router.replace('/account');
				} else {
					this.message = "Token ou infos utilisateur non reçus ❌";
					this.success = false;
				}
			} catch (err) {
				console.error(`Callback.vue : ${err}`);
				this.message = "Erreur lors de l'authentification ❌";
				this.success = false;
			}
		}
	}
};
</script>

<style scoped>
#callback-page {
	text-align: left;
	padding: 2%;
	margin: 0, auto;
	width: 100%;
}

.success {
	color: green;
}

.error {
	color: red;
}

.spinner {
  border: 6px solid var(--color-theme-primary);
  border-right-color: var(--color-theme-secondary);
  border-radius: 50%;
  width: 100px;
  height: 100px;
  animation: spin 1s linear infinite;
  margin: auto;
}

@keyframes spin {
  to {
    transform: rotate(360deg);
  }
}
</style>