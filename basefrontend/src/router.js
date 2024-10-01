import { createRouter, createWebHistory } from 'vue-router';
import App from './App.vue'; // Main layout
import AutoDatabase from './components/AutoDatabase.vue'; // Dashboard component
import Donate from './components/Donate.vue'; // New component for Donate page

const routes = [
    {
        path: '/',
        name: 'Dashboard',
        component: AutoDatabase, // Render AutoDatabase on the Dashboard
    },
    {
        path: '/donate',
        name: 'Donate',
        component: { template: '<div><h1>Donate Page</h1><p>Your donation helps us to...</p></div>' } // Placeholder for Donate page
    },
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

export default router;