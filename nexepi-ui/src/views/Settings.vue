<template>
  	<main id="Settings-page">
    <!-- Check if the user is authenticated -->
		<div v-if="!isAuthenticated" class="not-auth">
			<h2>🔒 Accès restreint</h2>
			<p>Vous devez être connecté pour accéder à cette page.</p>
			<router-link to="/account" class="cta-button">Se connecter</router-link>
		</div>

		<!-- If the user is authenticated -->
		<div v-else>
			<h1>Paramètres - Bienvenue, {{ userName }} - {{ userRole }}</h1>

			<!-- Tabs for different sections -->
			<div v-if="userRole === 'administrateur' || userRole === 'moderateur' || userRole === 'utilisateur'" class="tabs">
				<button v-for="tab in tabs" :key="tab" :class="{ active: activeTab === tab }" @click="activeTab = tab">
				{{ tab }}
				</button>
			</div>

			<!-- Tab Content -->
			<div class="tab-content">
				<!-- Interface Settings Tab -->
				<div v-if="activeTab === 'Interface'">
					<h2>🎨 Interface</h2>
					<p>Changer le thème de l'application :</p>
					<label>
						<input type="checkbox" v-model="darkMode" />
						Activer le mode sombre
					</label>
				</div>

				<!-- Informations Utilisateur Tab -->
				<div v-if="activeTab === 'utilisateur'">
					<h2>👤 Informations Utilisateur</h2>
					<form @submit.prevent="updateUserInfo">
						<div>
							<label>Nom d'utilisateur : <span style="font-weight: bold;">{{ user }}</span></label>
						</div>
						<div>
							<label>Nom :</label>
							<input type="text" v-model="userName" />
						</div>
						<div>
							<label>Email :</label>
							<input type="email" v-model="userEmail" />
						</div>
						<div>
							<label>Mot de passe :</label>
							<input :type="!canEditPassword ? 'text' : 'password'" v-model="password" :disabled="!canEditPassword" :style="!canEditPassword ? 'cursor: not-allowed;' : ''"/>
						</div>

						<div>
							<label>Votre role chez <span style="font-weight: bold;">{{ companyName }}</span> : <span style="font-weight: bold;">{{ userRole }}</span></label>
						</div>
						
						<!-- Le bouton Valider -->
						<button type="submit" class="cta-button">Valider les modifications</button>
					</form>
				</div>

				<!-- Notifications Settings Tab -->
				<div v-if="activeTab === 'Notifications'">
					<h2>🔔 Notifications Personnelles</h2>
					<div>
						<label>
							<input type="checkbox" v-model="userNotificationsEnabled" />
							Activer les notifications par mail
						</label>
					</div>

					<!-- Notifications Settings Tab (Admin only) -->
					<div v-if="activeTab === 'Notifications' && userRole === 'administrateur'">
						<h2>🔔 Notifications Globales</h2>
						<div>
							<label>
								<input type="checkbox" v-model="globalNotificationsEnabled" />
								Activer les notifications globales
							</label>
						</div>
					</div>

					<!-- Le bouton Valider -->
					<button type="button" @click="updateNotificationsSettings" class="cta-button">Valider les notifications</button>
				</div>

				<!-- Section ENTREPRISE -->
				<div v-if="activeTab === 'Entreprise' && userRole === 'administrateur'">
					<h2>🏢 Paramètres de l'entreprise</h2>
					<br>
					<form @submit.prevent="updateEntrepriseSettings">
						<h3><span style="font-weight: bold;">Informations de l'entreprise</span></h3>
						<div>
							<label>Nom de l'entreprise :</label>
							<input type="text" v-model="companyName" />
						</div>
						<div>
							<label>Email de contact :</label>
							<input type="email" v-model="companyEmail" />
						</div>
						<br>
						<h3><span style="font-weight: bold;">Personalisation de l'entreprise</span></h3>
						<div>
							<label>Couleur principale :</label>
							<input type="color" v-model="primarythemeColor" />
						</div>
						<div>
							<label>Couleur secondaire :</label>
							<input type="color" v-model="secondarythemeColor" />
						</div>
						<div>
							<input type="button" value="Réinitialiser les couleurs" @click="resetColors" />
						</div>
						<div>
							<label>Logo :</label>
							<input type="file" @change="handleLogoUpload" />
						</div>

						<button type="submit" class="cta-button">Valider les paramètres d’entreprise</button>
					</form>
				</div>

				<!-- Vue uniquement pour Modérateurs -->
				<div v-else-if="activeTab === 'Entreprise' && userRole === 'moderateur'">
					<h2>🏢 Informations de l'entreprise</h2>
					<p><strong>Nom :</strong> {{ companyName }}</p>
					<p><strong>Email :</strong> {{ companyEmail }}</p>
					<p><strong>Couleur primaire :</strong> <span :style="{ color: primarythemeColor }">{{ primarythemeColor }}</span></p>
					<p><strong>Couleur secondaire :</strong> <span :style="{ color: secondarythemeColor }">{{ secondarythemeColor }}</span></p>
					<!-- Logo preview si besoin -->
				</div>

				<!-- Vue uniquement pour Users -->
				<div v-else-if="activeTab === 'Entreprise' && userRole === 'utilisateur'">
					<h2>🏢 Informations de l'entreprise</h2>
					<p><strong>Nom :</strong> {{ companyName }}</p>
					<p><strong>Email :</strong> {{ companyEmail }}</p>
					<p><strong>Couleur primaire :</strong> <span :style="{ color: primarythemeColor }">{{ primarythemeColor }}</span></p>
					<p><strong>Couleur secondaire :</strong> <span :style="{ color: secondarythemeColor }">{{ secondarythemeColor }}</span></p>
					<!-- Logo preview si besoin -->
				</div>

				<!-- Section EPIs -->
				<div v-if="activeTab === 'EPI'">
					<h2>🧤 Paramètres EPI</h2>
					<form @submit.prevent="updateEPISettings">
						<button type="submit" class="cta-button">Valider les paramètres EPI</button>
					</form>
				</div>
      		</div>
    	</div>
  	</main>
</template>

<script>
import axios from 'axios';
export default {
  name: 'SettingsPage',
  data() {
    return {
      	isAuthenticated: false,
      	tabs: ['Interface', 'Utilisateur', 'Notifications', 'Entreprise', 'EPI'],
      	activeTab: '',
		// User
		user: '',
		userName: '',
		userEmail: '',
      	password: 'Modification impossible',
		canEditPassword: false,
		userRole: 'Aucun Role',
      	darkMode: false,
      	globalNotificationsEnabled: false,
      	userNotificationsEnabled: false,
	  	// Entreprise
    	companyName: '',
    	companyEmail: '',
		primarythemeColor: '',
		secondarythemeColor: '',
    	// EPI
    	epiInfo: '',
    	customField: '',
    };
  },
  async created() {
  },
  async mounted() {
		const hash = window.location.hash.replace('#', '');
		if (hash) {
			console.log(hash);
			this.activeTab = decodeURIComponent(hash);
		} else {
			console.log('Hash is missing in the URL path');
			this.activeTab = 'Interface';
		}

    	const user = JSON.parse(localStorage.getItem('user'));
		this.userToken = localStorage.getItem('token');
		if (this.userToken) {
			this.isAuthenticated = true;
		} else {
			this.isAuthenticated = false;
		}

		if (this.isAuthenticated == true && user) {

			this.user = user?.username;
			this.userName = user?.name;
			this.userEmail = user?.email;
			this.userRole = localStorage.getItem('userRole');
			this.companyName = user.company;

			const style = getComputedStyle(document.documentElement);

			const company_token = localStorage.getItem('company_token');
			try {
				const response = await axios.get(`${import.meta.env.VITE_NEXEPI_API_URL}/settings/company?company_token=${company_token}`);
				this.companyName = response.data.data.name;
				this.companyEmail = response.data.data.email;
				this.primarythemeColor = response.data.data.theme_primary_color;
				this.secondarythemeColor = response.data.data.theme_secondary_color;
			} catch (error) {
				console.error('Error fetching data', error);
				this.primarythemeColor = style.getPropertyValue('--color-theme-primary').trim();
				this.secondarythemeColor = style.getPropertyValue('--color-theme-secondary').trim();
			}
		}
  	},
  	methods: {
		// Update User Info (for example, change name, email, and password)
		async updateUserInfo() {
			const user = JSON.parse(localStorage.getItem('user'));
			const userToken = user?.user_token;

			if (!userToken) {
				alert("❌ Token utilisateur introuvable. Veuillez vous reconnecter.");
				return;
			}

			try {
				await axios.post(`${import.meta.env.VITE_NEXEPI_API_URL}/settings/user/update`, {
					user_token: userToken,
					name: this.userName,
					email: this.userEmail
				});
				alert(`✅ Informations mises à jour pour ${this.userName}`);
			} catch (error) {
				console.error('updateUserInfo() : Erreur lors de la mise à jour : ', error);
				alert(`❌ Erreur de mise à jour : ${error.response?.data?.message || error.message}`);
			}
		},
		// Update Notifications Settings
		updateNotificationsSettings() {
			alert(`Notifications mises à jour`);
			// Call an API to update notification settings
		},
		// Handle changing theme color (dark mode)
		toggleDarkMode() {
		this.darkMode = !this.darkMode;
		},
		// Update Company Info (for example, company name)
		updateCompanyInfo() {
		alert(`Informations de l'entreprise mises à jour: ${this.companyName}`);
		// Call an API to update company info,
		},
		async updateEntrepriseSettings() {
			const companyToken = localStorage.getItem('company_token');
			try {
				await axios.post(`${import.meta.env.VITE_NEXEPI_API_URL}/settings/companies/update`,
				{
					company_token: companyToken,
					name: this.companyName,
					email: this.companyEmail,
					theme_primary_color: this.primarythemeColor,
					theme_secondary_color: this.secondarythemeColor
				});
				
				document.documentElement.style.setProperty('--color-theme-primary', this.primarythemeColor);
				document.documentElement.style.setProperty('--color-theme-secondary', this.secondarythemeColor);
				alert(`✅ Mise à jour réussie pour : ${this.companyName}`);
			} catch (error) {
				console.error('updateEntrepriseSettings() : Error fetching data : ', error);
				alert(`❌ Erreur lors de la mise à jour de ${this.companyName} : ${error.message}`);
			}
		},
		resetColors() {
			this.primarythemeColor = getComputedStyle(document.documentElement).getPropertyValue('--vt-c-theme-primary-color').trim();
			this.secondarythemeColor = getComputedStyle(document.documentElement).getPropertyValue('--vt-c-theme-secondary-color').trim();
		}
  	}
};
</script>

<style scoped>
#Settings-page {
	text-align: left;
	padding: 2%;
	width: 100%;
}

.not-auth {
  text-align: center;
  background-color: #a70000d2;
  color: #fff;  
  padding: 2rem;
  border-radius: 10px;
  box-shadow: 0 2px 6px #000000ae;
  height: 100%;
  width: 100%;
}

.cta-button {
	padding: 10px 20px;
  background-color: var(--color-theme-primary);
  color: var(--color-theme-secondary);
  transition: color 0.3s ease;
	border: none;
	cursor: pointer;
	border: none;
	border-radius: 5px;
}

.cta-button:hover {
	background-color: var(--color-dark);
	color: var(--color-light);
}

.tabs {
  display: flex;
  gap: 1rem;
  margin-bottom: 1.5rem;
}

.tabs button {
	padding: 10px 20px;
  background-color: var(--color-theme-primary);
  color: var(--color-theme-secondary);
  transition: color 0.3s ease;
	border: none;
	cursor: pointer;
	border: none;
	border-radius: 5px;
}

.tabs button.active {
	background-color: var(--color-dark);
	color: var(--color-light);
}

.tab-content {
  background-color: var(--color-light);
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 2px 6px #00000025;
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

input[type="color"] {
	padding: 0px;
}
</style>
