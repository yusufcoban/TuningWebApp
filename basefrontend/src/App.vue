<template>
    <div id="app" class="d-flex">
        <!-- Sidebar -->
        <div :class="['sidebar', { 'sidebar-collapsed': !isSidebarOpen }]" class="bg-light border-right shadow-sm d-flex flex-column align-items-stretch" v-if="isAuthenticated">
            <div class="container">
                <div class="row">
                    <div class="col-sm" href="/">
                        <a href="/">
                            <font-awesome-icon :icon="['fas', 'house']" />
                        </a>
                    </div>
                    <div class="col-sm" @click="handleLogout">
                        <font-awesome-icon :icon="['fas', 'right-from-bracket']" style="color:red" />
                    </div>
                </div>
                <div class="row">
                    <div class="">
                        <span class="material-icons user-icon">account_circle</span>
                        <span class="user-name">{{ user ? user.name : 'No user found...' }}</span> <!-- Display current user's name -->
                        <ul class="">
                            <li class="sidebar-item">
                                <router-link to="/UploadFile" class="sidebar-link">
                                    <i class="bi bi-grid-fill"></i>
                                    Upload File
                                </router-link>
                            </li>
                            <li class="sidebar-item">
                                <router-link to="/myfiles" class="sidebar-link">
                                    <i class="bi bi-cash"></i>
                                    My Files
                                </router-link>
                            </li>
                            <li class="sidebar-item" v-if="isAdmin">
                                <router-link to="/openTasks" class="sidebar-link">
                                    <i class="bi bi-cash"></i>
                                    Open Tasks
                                </router-link>
                            </li>

                            <li class="sidebar-item" v-if="isAdmin">
                                <router-link to="/userList" class="sidebar-link">
                                    <i class="bi bi-cash"></i>
                                    Users
                                </router-link>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <!-- Main Content -->
        <div class="">
            <header class="header-bar colorLogo text-white d-flex align-items-center p-3 shadow">
                <button @click="toggleSidebar" class="hamburger-icon">
                    <span class="material-icons">menu</span>
                </button>
                <h1 class="m-0">SgTuners</h1>
            </header>
            <div class="p-4 content-area">
                <router-view />
            </div>
        </div>
    </div>
</template>

<script>
    import { mapState } from 'vuex'; // Import mapState for accessing Vuex state

    export default {
        data() {
            return {
                isSidebarOpen: true, // Sidebar visibility state
            };
        },
        computed: {
            ...mapState(['user']), // Map state properties
            isAuthenticated() {
                return this.user !== null; // Check if the user is authenticated
            },
            isAdmin() {
                return this.user && this.user.role === 'Admin'; // Adjust this as per your logic for admins
            }
        },
        methods: {
            toggleSidebar() {
                this.isSidebarOpen = !this.isSidebarOpen; // Toggle sidebar visibility
            },
            handleLogout() {
                // Logic to handle logout
                // You might want to call your API to log the user out
                // Clear the user data from Vuex and redirect to login
                this.$store.commit('logout'); // Assuming you have a logout mutation in your Vuex store
                this.$router.push('/login'); // Redirect to login page
            },
        },
        mounted() {
            // Fetch the ECU list when the component is mounted
            this.$store.dispatch('fetchEcuList')
                .catch(error => {
                    console.error('Error fetching ECU list:', error);
                });
        },
    };
</script>

<style>
</style>


<style scoped>
    #app
    {
        height: 100vh;
    }

    .sidebar
    {
        width: 250px;
        transition: transform 0.3s ease;
    }

    .sidebar-collapsed
    {
        transform: translateX(-100%);
    }

    .hamburger-icon
    {
        background: none;
        border: none;
        color: white;
        font-size: 24px;
        cursor: pointer;
    }

    .flex-grow-1
    {
        flex-grow: 1;
    }

    .colorLogo
    {
        background-color: #3476ba; /* Color for the header */
    }

    .content-area
    {
        background-color: #f9f9f9; /* Light background for content area */
        border-radius: 8px; /* Rounded corners */
        box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1); /* Subtle shadow for depth */
        margin-top: 20px; /* Space between header and content */
        padding: 20px; /* Padding for content */
        transition: background-color 0.3s; /* Smooth background transition */
    }

    .login-container
    {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        background-color: #f0f2f5; /* Background color for the login view */
    }

    #app
    {
        height: 100vh; /* Make sure the app takes full height */
    }



    .colorLogo
    {
        background-color: #3476ba
    }


    .menu
    {
        margin-top: 20px; /* Optional: Add space above menu */
    }



    .colorLogo
    {
        background-color: #3476ba; /* Color for the header */
    }
</style>
