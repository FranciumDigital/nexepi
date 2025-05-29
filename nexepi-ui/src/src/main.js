import './assets/main.css'
import { createApp, reactive } from 'vue'
import App from './App.vue'
import router from './router'
import functions from './functions'

const app = createApp(App)

const globalState = reactive({
	isLoggedIn: false,
	user: {
	  name: '',
	  first_name: '',
	  last_name: '',
	  email: '',
	  roles: [],
      group: [],
	}
});

globalState.isLoggedIn = JSON.parse(localStorage.getItem('isLoggedIn'));

const user = JSON.parse(localStorage.getItem('user'));
globalState.user.name = user?.name;
globalState.user.first_name = user?.name;
globalState.user.last_name = user?.name;
globalState.user.email = user?.name;
globalState.user.roles = user?.name;
globalState.user.group = user?.name;

// Injecter les fonctions et le state dans l'app
app.provide('globalState', globalState)
app.config.globalProperties.$fn = functions

// Appeler la fonction correctement depuis functions
functions.applyThemeColors();

app.use(router).mount('#app');