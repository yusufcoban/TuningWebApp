// @ts-ignore
//import '@/assets/main.css' 
import { createApp } from 'vue'
import { createVfm } from 'vue-final-modal'

import App from './App.vue'
//@ts-ignore
import router from '@/router.js' // Import the router
//@ts-ignore
import store from '@/store/index.js' // Import the store
//@ts-ignore
import FontAwesomeIcon from '@/fontawesome.js' // Import FontAwesome config

// Create the Vue app
const app = createApp(App)
const vfm = createVfm()
// Register FontAwesomeIcon globally
app.component('font-awesome-icon', FontAwesomeIcon)
// Use the router
app.use(router)
app.use(store)
app.use(vfm)
// Mount the app
app.mount('#app')
