import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import List_Items from '../views/List.vue'
import Add_Item from '../views/Add_Item.vue'
import Check_Item from '../views/Check.vue'
import About from '../views/About.vue'
import Settings from '../views/Settings.vue'
import Account from '../views/account/Account.vue'
import Account_Login from '../views/account/Login.vue'
import Account_Logged from '../views/account/Logged.vue'
import Callback from '../views/account/Callback.vue'

// Importer la nouvelle vue ItemPage
import ItemPage from '../views/ItemPage.vue'
import ItemCheckPage from '../views/ItemCheckPage.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', component: Home },
    { path: '/list', component: List_Items },
    { path: '/add', component: Add_Item },
    { path: '/check', component: Check_Item },
    { path: '/about', component: About },
    { path: '/settings', component: Settings },
    { path: '/account', component: Account },
    { path: '/account/login', component: Account_Login },
    { path: '/account/logged', component: Account_Logged },
    { path: '/callback', component: Callback },

    // Route dynamique pour la page de détails de l'élément
    { path: '/list/item/:NEXEPI_id', component: ItemPage, name: 'ItemPage' },
    { path: '/check/item/:NEXEPI_id', component: ItemCheckPage, name: 'ItemCheckPage' },
  ],
})

export default router
