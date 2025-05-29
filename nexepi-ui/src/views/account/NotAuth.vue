<template>
	<main id="NotAuth-page">
		<div class="not-auth">
			<h2>🔒 Accès restreint</h2>
			<p>Vous devez être connecté pour accéder à cette page.</p>
			<router-link to="/account" class="cta-button">Se connecter</router-link>
		</div>
	</main>
</template>
  
<style scoped>
#NotAuth-page {
	text-align: left;
	padding: 0;
	margin: 0;
	width: 100%;
	height: 100%;
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
</style>