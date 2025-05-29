<template>
	<main id="Home-page">
	  <h1>Bienvenue <span style="font-weight: bold;">{{ name }}</span> sur NEXEPI, la plateforme de gestion des EPI</h1>
	  <p>
		Gérez et suivez facilement vos Équipements de Protection Individuelle (EPI) grâce à notre interface intuitive. 
		Accédez à des fonctionnalités dédiées pour consulter, ajouter ou mettre à jour vos équipements, 
		et assurez la sécurité de vos équipes au quotidien.
	  </p>
	  <p>
		Consultez la <strong>liste complète</strong> de votre parc d'EPI, suivez l'état de chaque équipement, 
		et assurez une conformité constante aux normes de sécurité.
	  </p>
	  <br><br>

	  <!-- Check if the user is authenticated -->
	    <div v-if="!isAuthenticated">
        	<NotAuth/>
		</div>

		<!-- If the user is authenticated -->
		<div v-else>
			<a href="/list" class="cta-button">
				Accéder à la liste des EPI
			</a>
			<a href="/add" class="cta-button">
				Ajouter un EPI
			</a>
			<a href="/check" class="cta-button">
				Vérifier un EPI
			</a>
		</div>
	</main>
  </template>

<script setup>
import NotAuth from './account/NotAuth.vue';
</script>

<script>
export default {
	name: 'Home',
  	data() {
    	return {
			isAuthenticated: false,
      		username: '',
			name: ''
    	};
  	},
	async mounted() {
		this.userToken = localStorage.getItem('token');
		if (this.userToken) {
			this.isAuthenticated = true;
		} else {
			this.isAuthenticated = false;
		}

		const user = JSON.parse(localStorage.getItem('user'));
    	this.username = user?.username || 'utilisateur';
    	this.name = user?.name || '';
  	},
};
</script>

  <style>
	#Home-page {
		text-align: center;
	  	padding: 1%;
	  	margin: 0;
	  	width: 100%;
	}
  
	h1 {
	  font-size: 2.5rem;
	  margin-bottom: 20px;
	}
  
	p {
	  font-size: 1.2rem;
	  line-height: 1.6;
	  margin-top: 20px;
	}
  
	.cta-button {
	  display: inline-block;
	  padding: 10px 20px;
	  margin-top: 30px;
  	background-color: var(--color-theme-primary);
  	color: var(--color-theme-secondary);
	  border-radius: 5px;
	  text-decoration: none;
	  margin: 15px;
	}
  
	.cta-button:hover {
		background-color: var(--color-dark);
		color: var(--color-light);
		transition: color,background-color 0.3s ease-in-out;
	}
  </style>
  