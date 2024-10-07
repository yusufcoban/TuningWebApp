// src/store/index.js
import { createStore } from 'vuex';

const store = createStore({
    state() {
        return {
            user: JSON.parse(localStorage.getItem('user')) || null, // Initialize user from local storage
        };
    },
    mutations: {
        login(state, user) {
            state.user = { name: user.name, role: user.role }; // Set user data upon successful login
            localStorage.setItem('user', JSON.stringify(user)); // Store user data in local storage
        },
        logout(state) {
            state.user = null; // Clear user data on logout
            localStorage.removeItem('user'); // Remove user data from local storage
        },
    },
    actions: {
        async login({ commit }, userCredentials) {
            const response = await fetch(`${import.meta.env.VITE_API_BASE_URL}/api/login`, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(userCredentials),
                credentials: 'include', // Include credentials for cookie-based auth
            });

            if (response.ok) {
                const userData = await response.json();
                commit('setUser', userData); // Commit the user data
                return userData;
            } else {
                throw new Error('Login failed');
            }
        },
        logout({ commit }) {
            commit('logout'); // Clear user data on logout
        },
    },
    getters: {
        isAuthenticated(state) {
            return state.user !== null; // Check for authentication
        },
        role(state) {
            return state.user; // Return the user's role
        },
    },
});

export default store;
