<template>
	<main id="logged-page">
	  	<div class="container">
			<div class="content">
				<h2>Bienvenue <span style="font-weight: bold;">{{ userName }}</span> ! 🎉</h2>
		  		<p>Vous êtes connecté avec ces informations :</p>
		  		<p>Nom d'utilisateur : <span style="font-weight: bold;">{{ username }}</span></p>
		  		<p>E-mail : <span style="font-weight: bold;">{{ userEmail }}</span></p>
		  		<p>Fonction : <span style="font-weight: bold;">{{ userRole }}</span></p>
		  		<p>Entreprise : <span style="font-weight: bold;">{{ companyName }}</span></p>
		  		<p>Token d'entreprise : <span style="font-weight: bold;">{{ companyToken }}</span></p>
			</div>
			<div class="logged-button-container">
				<router-link to="/settings#Utilisateur" class="updateUserSettings-button">Modifier les informations</router-link>
				<button @click="logout" class="logout-button">Se déconnecter</button>
			</div>
	  	</div>
	</main>
</template>
  
<script>
import axios from 'axios';
export default {
	name: 'AccountLogged',
  	data() {
    	return {
			username: '',
			userName: '',
			userEmail: '',
			userRole: '',
			userToken: '',
			companyToken: '',
			companyName: '',
			company: '', // Ajoute cette ligne
			success: false,
			message: ''
		};
  	},
  	async mounted() {
		console.log("Logged.vue: ");
    	const user = JSON.parse(localStorage.getItem('user'));

		this.username = user?.nickname || 'utilisateur';
		this.userName = user?.name || 'Nom';
		this.userEmail = user?.email || 'Aucun';
		this.userRole = localStorage.getItem('userRole');
		this.userToken = localStorage.getItem('token');
		this.companyName = localStorage.getItem('companyName');
		this.companyToken = localStorage.getItem('company_token');
		console.log(`Logged.vue - User : `, user);

		/*try {
			const response = await axios.get(`${import.meta.env.VITE_NEXEPI_API_URL}/settings/company?company_token=${this.companyToken}`);
			this.companyName = response.data.data.name;
		} catch (error) {
			console.error(`Logged.vue - companyToken : ${error}`);
		}*/
  	},
  	methods: {
    	logout() {
			localStorage.clear();
      		location.href = 'https://NEXEPI.francium-digital.fr/account/';
    	}
  	}
};
</script>

<style scoped>
#logged-page {
	text-align: left;
	padding: 2%;
	margin: 0, auto;
	width: 100%;
}

.container {
	width: 100%;
	height: 500px;
	padding: 20px;
	background-color: var(--color-light);
	border-radius: 10px;
	box-shadow: 0 4px 8px #00000039;

	display: flex;
  	flex-direction: column;
  	
  	align-items: left;
}

.content {
  flex-grow: 1;
  display: flex;
  flex-direction: column;
}

.logged-button-container {
	display: flex;
	justify-content: space-between;
	margin-top: 20px;
}

.updateUserSettings-button, .logout-button {
	padding: 10px 20px;
	font-size: 1rem;
	border: none;
	border-radius: 8px;
	cursor: pointer;
	transition: color 0.3s ease;
  }
  
.updateUserSettings-button {
	background-color: var(--color-theme-primary);
	color: var(--color-dark);
}
  
.logout-button {
	background-color: var(--color-dark);
	color: var(--color-light);
}

.updateUserSettings-button:hover {
	background-color: var(--color-dark);
	color: var(--color-light);
}
  
.logout-button:hover {
	background-color: var(--color-theme-primary);
	color: var(--color-dark);
}
</style>