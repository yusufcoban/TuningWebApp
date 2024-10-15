import { createStore } from 'vuex';
import 'vue-neat-modal/dist/vue-neat-modal.css'
const store = createStore({
    state() {
        return {
            user: JSON.parse(localStorage.getItem('user')) || null, // Initialize user from local storage
            isAdmin: false, // Add an isAdmin state
        };
    },
    mutations: {
        login(state, user) {
            state.user = { name: user.name, role: user.role }; // Set user data upon successful login
            state.isAdmin = user.role === 'Admin'; // Set isAdmin based on role
            localStorage.setItem('user', JSON.stringify(state.user)); // Store user data in local storage
        },
        logout(state) {
            state.user = null; // Clear user data on logout
            state.isAdmin = false; // Reset isAdmin
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
                commit('login', { name: userCredentials.username, role: userData.role }); // Commit the user data
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
            return state.user ? state.user.role : null; // Return the user's role
        },
        isAdmin(state) {
            return state.isAdmin; // Return the isAdmin state
        },
    },
});

export default store;
