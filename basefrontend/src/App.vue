<template>
    <div id="app" class="d-flex">
        <!-- Sidebar -->
        <div class="sidebar" v-if="isAuthenticated">
            <div class="sidebar-header d-flex justify-content-between align-items-center">
                <a href="/" class="logo-link">
                    <font-awesome-icon :icon="['fas', 'house']" class="logo-icon" />
                </a>
                <span @click="handleLogout" class="logout-icon">
                    <font-awesome-icon :icon="['fas', 'right-from-bracket']" style="color:red" />
                </span>
            </div>
            <div class="user-info text-center">
                <span class="user-icon">
                    <font-awesome-icon :icon="['fas', 'user-circle']" size="2x" style="color:red" />
                </span>
                <div class="user-name">{{ user ? 'Welcome ' + user.name : 'No user found...' }}</div>
            </div>
            <ul class="sidebar-menu">
                <li class="sidebar-item">
                    <router-link to="/UploadFile" class="sidebar-link">
                        <i class="bi bi-cloud-upload"></i>
                        Upload File
                    </router-link>
                </li>
                <li class="sidebar-item">
                    <router-link to="/myfiles" class="sidebar-link">
                        <i class="bi bi-file-earmark"></i>
                        My Files
                    </router-link>
                </li>
                <li class="sidebar-item" v-if="isAdmin">
                    <router-link to="/openTasks" class="sidebar-link">
                        <i class="bi bi-list-task"></i>
                        Open Tasks
                    </router-link>
                </li>
                <li class="sidebar-item" v-if="isAdmin">
                    <router-link to="/userList" class="sidebar-link">
                        <i class="bi bi-people"></i>
                        Users
                    </router-link>
                </li>
            </ul>
        </div>

        <!-- Main Content -->
        <div class="main-content">
            <header class="header-bar colorLogo text-white d-flex align-items-center p-3 shadow">
                <button v-if="1==2" class="hamburger-icon">
                    <span class="material-icons">menu</span>
                </button>
                <h1 class="m-0">SgTuners</h1>
            </header>
            <div>
                <router-view />
            </div>
        </div>
    </div>
</template>
<script>
    import { mapState } from 'vuex';

    export default {
        data() {
            return {
                isSidebarOpen: true, // Sidebar visibility state
            };
        },
        computed: {
            ...mapState(['user']), // Map Vuex state properties
            isAuthenticated() {
                return this.user !== null; // Check if the user is authenticated
            },
            isAdmin() {
                return this.user && this.user.role === 'Admin'; // Check if the user is an admin
            }
        },
        methods: {
            handleLogout() {
                // Handle user logout
                this.$store.commit('logout'); // Clear user data in Vuex
                this.$router.push('/login'); // Redirect to login page
            }
        },
        mounted() {
            // Fetch data when the component is mounted
            this.$store.dispatch('fetchEcuList')
                .catch(error => {
                    console.error('Error fetching ECU list:', error);
                });
        }
    };
</script>


<style scoped>
    /* Overall App Layout */
    #app
    {
        height: 100vh;
        display: flex;
        overflow: hidden;
    }

    /* Sidebar */
    .sidebar
    {
        width: 250px;
        background-color: #2c3e50;
        color: white;
        display: flex;
        flex-direction: column;
        transition: transform 0.3s ease, width 0.3s ease;
    }

    .sidebar-collapsed
    {
        transform: translateX(-100%);
    }

    /* Sidebar Header */
    .sidebar-header
    {
        padding: 1rem;
        border-bottom: 1px solid #34495e;
    }

    .logo-link
    {
        color: white;
        font-size: 24px;
        text-decoration: none;
    }

    .logout-icon
    {
        cursor: pointer;
    }

    /* User Info */
    .user-info
    {
        margin: 1rem 0;
    }

    .user-icon
    {
        color: white;
    }

    .user-name
    {
        color: white;
        font-size: 16px;
        margin-top: 0.5rem;
    }

    /* Sidebar Menu */
    .sidebar-menu
    {
        list-style: none;
        padding: 0;
        margin: 0;
    }

    .sidebar-item
    {
        margin: 0.5rem 0;
    }

    .sidebar-link
    {
        display: flex;
        align-items: center;
        text-decoration: none;
        color: white;
        padding: 0.75rem 1rem;
        border-radius: 4px;
        transition: background-color 0.3s ease;
    }

        .sidebar-link i
        {
            margin-right: 0.75rem;
            font-size: 20px;
        }

        .sidebar-link:hover
        {
            background-color: #1abc9c;
            color: white;
        }

    /* Main Content */
    .main-content
    {
        flex-grow: 1;
        background-color: #f4f6f9;
        overflow: scroll;
        transition: width 0.3s ease;
    }

        .main-content.full-width
        {
            width: 100%;
        }

    /* Header Bar */
    .header-bar
    {
        background-color: #34495e;
        color: white;
        display: flex;
        align-items: center;
        justify-content: space-between;
    }

    .hamburger-icon
    {
        background: none;
        border: none;
        color: white;
        font-size: 24px;
        cursor: pointer;
    }
</style>

