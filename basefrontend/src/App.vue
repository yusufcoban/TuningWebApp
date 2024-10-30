<template>
    <div id="app" class="d-flex">
        <!-- Sidebar -->
        <div id="sidebar" class="bg-light border-right shadow-sm" v-if="isAuthenticated">
            <div class="logo mb-4">
                <a href="/">
                    <img src="./assets/logo.png" alt="Logo" class="sidebar-logo" />
                </a>
            </div>


            <!-- User Section -->
            <div class="user-section d-flex align-items-center p-3">
                <span class="material-icons user-icon">account_circle</span>
                <span class="user-name">{{ user ? user.name : 'No user found...' }}</span> <!-- Display current user's name -->
                <button @click="handleLogout" class="logout-button">
                    <span class="material-icons" style="color: red;">logout</span> <!-- Logout icon -->
                </button>
            </div>


            <ul class="menu list-unstyled">
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
            </ul>
        </div>

        <!-- Main Content -->
        <div class="flex-grow-1">
            <header class="header-bar colorLogo text-white d-flex align-items-center p-3 shadow">
                <h1 class="m-0" style="">SgTuners</h1>
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

<style scoped>
    #app
    {
        height: 100vh; /* Make sure the app takes full height */
    }

    .colorLogo
    {
        background-color: #3476ba; /* Color for the header */
    }

    .flex-grow-1
    {
        margin-left: 250px; /* Adjust main content margin to sidebar width */
        display: flex; /* Enable flexbox for header and content */
        flex-direction: column; /* Stack header above content */
        height: 100%; /* Full height to allow flex-grow */
    }

    .header-bar
    {
        height: 60px; /* Set a fixed height for the header */
        border-bottom: 1px solid rgba(255, 255, 255, 0.2); /* Light border for separation */
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

    #sidebar
    {
        width: 250px; /* Fixed width for the sidebar */
        height: 100vh; /* Make sidebar full height */
        position: fixed; /* Fix position to the left */
        top: 0; /* Align to the top */
        left: 0; /* Align to the left */
        overflow-y: auto; /* Enable scrolling if content overflows */
        transition: width 0.2s; /* Smooth transition for width change */
        box-shadow: 2px 0 5px rgba(0, 0, 0, 0.1); /* Subtle shadow for depth */
    }

    .colorLogo
    {
        background-color: #3476ba
    }

    .flex-grow-1
    {
        margin-left: 250px; /* Adjust main content margin to sidebar width */
        display: flex; /* Enable flexbox for header and content */
        flex-direction: column; /* Stack header above content */
        height: 100%; /* Full height to allow flex-grow */
    }

    .header-bar
    {
        /* Styling for the header bar */
        height: 60px; /* Set a fixed height for the header */
        border-bottom: 1px solid rgba(255, 255, 255, 0.2); /* Light border for separation */
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

    .menu
    {
        margin-top: 20px; /* Optional: Add space above menu */
    }

    .sidebar-item
    {
        transition: background-color 0.2s, color 0.2s; /* Smooth transitions on hover */
    }

        .sidebar-item:hover
        {
            background-color: rgba(0, 123, 255, 0.1); /* Light blue background on hover */
        }

    .sidebar-link
    {
        display: flex; /* Align items in the sidebar */
        align-items: center; /* Center icons and text vertically */
        padding: 10px 15px; /* Padding around links */
        color: #333; /* Dark text color */
        text-decoration: none; /* Remove underline */
        border-radius: 4px; /* Rounded corners */
        transition: color 0.2s; /* Smooth transition for text color */
    }

        .sidebar-link i
        {
            margin-right: 10px; /* Space between icon and text */
        }

        .sidebar-link:hover
        {
            color: #007bff; /* Change text color on hover */
        }

    /* Ensure logo is fixed in place */
    .logo
    {
        display: flex;
        justify-content: center; /* Center horizontally */
        align-items: center; /* Center vertically */
        height: 100px; /* Fixed height for the logo area */
    }

    .sidebar-logo
    {
        max-width: 100%; /* Prevent the logo from exceeding the sidebar width */
        max-height: 100%; /* Prevent the logo from exceeding the fixed height */
        object-fit: contain; /* Ensure the logo retains its aspect ratio */
    }

    #app
    {
        height: 100vh; /* Make sure the app takes full height */
    }

    .colorLogo
    {
        background-color: #3476ba; /* Color for the header */
    }

    .flex-grow-1
    {
        margin-left: 250px; /* Adjust main content margin to sidebar width */
        display: flex; /* Enable flexbox for header and content */
        flex-direction: column; /* Stack header above content */
        height: 100%; /* Full height to allow flex-grow */
    }

    .header-bar
    {
        height: 60px; /* Set a fixed height for the header */
        border-bottom: 1px solid rgba(255, 255, 255, 0.2); /* Light border for separation */
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

    .user-section
    {
        display: flex; /* Use flexbox for alignment */
        align-items: center; /* Center items vertically */
        background-color: #f1f1f1; /* Light background for user section */
        border-radius: 4px; /* Rounded corners */
        margin-bottom: 20px; /* Space below user section */
        padding: 10px; /* Padding for user section */
    }

    .user-icon
    {
        font-size: 30px; /* Size for user icon */
    }

    .user-name
    {
        margin-left: 10px; /* Space between icon and name */
        font-weight: bold; /* Bold text for user name */
    }

    .logout-button
    {
        background: none; /* No background for button */
        border: none; /* Remove border */
        cursor: pointer; /* Pointer cursor on hover */
        margin-left: auto; /* Push logout button to the right */
        color: red; /* Red color for logout */
        font-size: 24px; /* Size for logout icon */
    }

    .menu
    {
        margin-top: 20px; /* Optional: Add space above menu */
    }

    .sidebar-item
    {
        transition: background-color 0.2s, color 0.2s; /* Smooth transitions on hover */
    }

        .sidebar-item:hover
        {
            background-color: rgba(0, 123, 255, 0.1); /* Light blue background on hover */
        }

    .sidebar-link
    {
        display: flex; /* Align items in the sidebar */
        align-items: center; /* Center icons and text vertically */
        padding: 10px 15px; /* Padding around links */
        color: #333; /* Dark text color */
        text-decoration: none; /* Remove underline */
        border-radius: 4px; /* Rounded corners */
        transition: color 0.2s; /* Smooth transition for text color */
    }

        .sidebar-link i
        {
            margin-right: 10px; /* Space between icon and text */
        }

        .sidebar-link:hover
        {
            color: #007bff; /* Change text color on hover */
        }

    /* Ensure logo is fixed in place */
    .logo
    {
        display: flex;
        justify-content: center; /* Center horizontally */
        align-items: center; /* Center vertically */
        height: 100px; /* Fixed height for the logo area */
    }

    .sidebar-logo
    {
        max-width: 100%; /* Prevent the logo from exceeding the sidebar width */
        max-height: 100%; /* Prevent the logo from exceeding the fixed height */
        object-fit: contain; /* Ensure the logo retains its aspect ratio */
    }
</style>
