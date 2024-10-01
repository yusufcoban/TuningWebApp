<template>
    <div id="app" class="d-flex">
        <!-- Sidebar -->
        <div id="sidebar" class="bg-light border-right shadow-sm">
            <div class="logo mb-4">
                <a href="/">
                    <img src="./assets/logo.png" alt="Logo" class="sidebar-logo" />
                </a>
            </div>
            <ul class="menu list-unstyled">
                <li class="sidebar-item active">
                    <router-link to="/UploadFile" class="sidebar-link">
                        <i class="bi bi-grid-fill"></i>
                        Upload File
                    </router-link>
                </li>
                <li class="sidebar-item">
                    <router-link to="/donate" class="sidebar-link">
                        <i class="bi bi-cash"></i>
                        My Files
                    </router-link>
                </li>
            </ul>
        </div>

        <!-- Main Content -->
        <div class="flex-grow-1">
            <!-- Header Bar -->
            <header class="header-bar colorLogo text-white d-flex align-items-center p-3 shadow">
                <h1 class="m-0">Dashboard</h1>
            </header>

            <!-- Router View for Dynamic Content -->
            <div class="p-4 content-area">
                <router-view />
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        // No need for components here as we're using router
    };
</script>

<style scoped>
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
</style>
