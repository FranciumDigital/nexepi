<template>
	<div>Redirection en cours...</div>
</template>
  
<script>
export default {
	name: 'Account',
	mounted() {
		const isLoggedIn = localStorage.getItem('isLoggedIn');
		console.log(`Account.vue: isLoggedIn = ${isLoggedIn}`);

	  	if (isLoggedIn) {
			this.$router.replace('/account/logged');
	  	} else {
			this.$router.replace('/account/login');
	  	}
	}
};
</script>