<template>
    <div class="uploaded-files">
        <h1>Uploaded Files</h1>
        <table class="table table-hover table-bordered">
            <thead class="table-light">
                <tr>
                    <th>UserID</th>
                    <th>Username</th>
                    <th>Email</th>
                    <th>Full Name</th>
                    <th>Address</th>
                    <th>City</th>
                    <th>Role</th>
                    <th>Country</th>
                    <th>State</th>
                    <th>Phone Number</th>
                    <th>Is Active</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="user in userList"
                    :key="user.UserID"
                    class="row-clickable"
                    @click="this.$router.push({ path: `/UserDetails/` + user.UserID });">
                    <td>{{ user.userID }}</td>
                    <td>{{ user.username }}</td>
                    <td>{{ user.email }}</td>
                    <td>{{ user.fullName }}</td>
                    <td>{{ user.address }}</td>
                    <td>{{ user.city }}</td>
                    <td>{{ user.role }}</td>
                    <td>{{ user.country }}</td>
                    <td>{{ user.state }}</td>
                    <td>{{ user.phoneNumber }}</td>
                    <td>{{ user.IsActive ? 'Yes' : 'No' }}</td>
                </tr>
            </tbody>

        </table>

        <p v-if="userList.length === 0">No user found....</p>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                userList: [], // Array to hold uploaded files
            };
        },
        coponent() {
        },
        mounted() {
            this.getUserList(); // Fetch uploaded files on component mount
        },
        methods: {
            async getUserList() {
                try {
                    const apiUrl = import.meta.env.VITE_API_BASE_URL;
                    const response = await fetch(`${apiUrl}/api/User/GetAllUser`, {
                        method: 'GET',
                        credentials: 'include' // Ensure cookies are sent with the request
                    });


                    if (!response.ok) {
                        throw new Error(`HTTP error! status: ${response.status}`); // Handle HTTP errors
                    }

                    const data = await response.json();
                    this.userList = data; // Store fetched car brands in component's data

                } catch (error) {

                }
            }
        }
    };
</script>

<style scoped>
    .uploaded-files
    {
        padding: 20px;
        max-width: 800px;
        margin: auto;
        font-family: Arial, sans-serif;
    }

    h1
    {
        color: #2c3e50;
        text-align: center;
    }

    table
    {
        width: 100%;
        border-collapse: collapse;
        margin-top: 20px;
    }

    th, td
    {
        border: 1px solid #ddd;
        padding: 8px;
        text-align: left;
    }

    th
    {
        background-color: #f2f2f2;
    }

    p
    {
        text-align: center;
        color: #999;
    }
</style>
