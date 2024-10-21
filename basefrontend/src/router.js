// src/router/index.js
import { createRouter, createWebHistory } from 'vue-router';
import Login from '@/components/Login.vue'; // Login component
import Home from '@/components/Home.vue'; // Home component
import { useStore } from 'vuex'; // Vuex store
import App from './App.vue'; // Main layout
import MyFiles from '@/components/MyFiles.vue'; // Main layout
import MyFilesViewer from '@/components/MyFilesViewer.vue';
import AutoDatabase from './components/AutoDatabase.vue'; // Dashboard component

// Define your routes
const routes = [
    {
        path: '/login',
        component: Login,
        meta: { requiresAuth: false }, // This route does not require authentication
    },
    {
        path: '/UploadFile',
        component: AutoDatabase,
        meta: { requiresAuth: true }, // This route requires authentication
    },
    {
        path: '/myfiles',
        meta: { requiresAuth: true }, // This route requires authentication
        component: MyFiles
    },
    {
        path: '/',
        redirect: '/login', // Redirect to login by default
    },
    {
        path: '/home',
        meta: { requiresAuth: true }, // Ensure this route requires authentication,
        component: Home
    },
    {
        path: '/MyFilesViewer/:id',
        meta: { requiresAuth: true }, // Ensure this route requires authentication,
        component: MyFilesViewer
    },
    
];

// Create the router instance
const router = createRouter({
    history: createWebHistory(),
    routes,
});

// Navigation Guard
router.beforeEach((to, from, next) => {
    const store = useStore();
    const isAuthenticated = store.state.user !== null; // Check if user is authenticated

    console.log(`Navigating to: ${to.path}, Authenticated: ${isAuthenticated}`);
    
    if (to.meta.requiresAuth && !isAuthenticated) {
        next({ path: '/login' }); // Redirect to login if not authenticated
    } else {
        next(); // Allow the navigation
    }
});

// Export the router
export default router;
