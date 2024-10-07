import './assets/main.css'
import { createApp } from 'vue'
import App from './App.vue'
import router from './router' // Import the router
import store from './store' // Import the store

import FontAwesomeIcon from './fontawesome' // Import FontAwesome config

// Create the Vue app
const app = createApp(App)

// Register FontAwesomeIcon globally
app.component('font-awesome-icon', FontAwesomeIcon)

// Use the router
app.use(router)
app.use(store)
// Mount the app
app.mount('#app')
