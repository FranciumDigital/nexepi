import { createRouter, createWebHistory } from 'vue-router'
import Home from '../views/Home.vue'
import List_Items from '../views/List.vue'
import Add from '../views/Add.vue'
import About from '../views/About.vue'
import Settings from '../views/Settings.vue'
import Account from '../views/account/Account.vue'
import Account_Login from '../views/account/Login.vue'
import Account_Logged from '../views/account/Logged.vue'
import Callback from '../views/account/Callback.vue'

// Importer la nouvelle vue ItemPage
import ItemViewPage from '../views/ItemViewPage.vue'
import ItemEditPage from '../views/ItemEditPage.vue'
import ItemCheckPage from '../views/ItemCheckPage.vue'

import UsersPage from '../views/Users.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', component: Home },
    { path: '/items/list', component: List_Items, name: 'ListPage' },
    { path: '/items/add', component: Add },
    { path: '/about', component: About },
    { path: '/settings', component: Settings },
    { path: '/account', component: Account },
    { path: '/account/login', component: Account_Login },
    { path: '/account/logged', component: Account_Logged },
    { path: '/callback', component: Callback },

    // Route dynamique pour la page de détails de l'élément
    { path: '/items/list/item_view', component: ItemViewPage, name: 'ItemViewPage' },
    { path: '/items/list/item_edit', component: ItemEditPage, name: 'ItemEditPage' },
    { path: '/items/list/item_verify', component: ItemCheckPage, name: 'ItemCheckPage' },

    { path: '/users', component: UsersPage },
  ],
})

export default router
