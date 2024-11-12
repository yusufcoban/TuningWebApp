<template >
    <div class="login-container">
        <div class="login-box">
            <h2>Login</h2>
            <form @submit.prevent="handleLogin">
                <div class="input-group">
                    <font-awesome-icon icon="user" />
                    <input type="text"
                           v-model="username"
                           placeholder="Username"
                           required />
                </div>
                <div class="input-group">
                    <font-awesome-icon icon="lock" />
                    <input type="password"
                           v-model="password"
                           placeholder="Password"
                           required />
                </div>
                <button type="submit">
                    <font-awesome-icon icon="sign-in-alt" /> Login
                </button>
                <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
            </form>
        </div>
    </div>
</template>

<script>
    import { mapMutations, mapGetters } from 'vuex';
    export default {
        data() {
            return {
                username: '',
                password: '',
                errorMessage: null,
            };
        },
        computed: {
            ...mapGetters(['isAuthenticated']), // Map isAuthenticated getter to this component
        },
        mounted() {
            if (this.isAuthenticated) {
                this.$router.push('/home'); // Redirect to home if already logged in
            } else {
                this.$router.push('/login'); 
            }
        },
        methods: {
            ...mapMutations(['login']),
            async handleLogin() {
                const apiUrl = import.meta.env.VITE_API_BASE_URL;

                try {
                    const response = await fetch(`${apiUrl}/api/login`, {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({
                            username: this.username,
                            password: this.password,
                        }),
                        credentials: 'include', // Include cookies in the request
                    });

                    if (!response.ok) {
                        const data = await response.json();
                        this.errorMessage = data.message || 'Login failed';
                    } else {
                        const userData = await response.json();
                        this.login({ name: this.username, role: userData.role }); // Commit the role to the store
                        this.$router.push('/home'); // Redirect to home or dashboard after login
                    }
                } catch (error) {
                    console.error(error);
                    this.errorMessage = 'An error occurred. Please try again.';
                }
            },
        },
    };
</script>

<style scoped>
    /* Your existing styles */
</style>

<style scoped>
    .login-container
    {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        background-color: #f0f2f5;
    }

    .login-box
    {
        background-color: white;
        padding: 2rem;
        border-radius: 8px;
        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
        width: 100%;
        max-width: 400px;
        text-align: center;
    }

    h2
    {
        margin-bottom: 2rem;
        color: #333;
    }

    .input-group
    {
        display: flex;
        align-items: center;
        margin-bottom: 1.5rem;
        padding: 0.5rem;
        border: 1px solid #ccc;
        border-radius: 4px;
    }

        .input-group input
        {
            flex: 1;
            padding: 0.5rem;
            border: none;
            outline: none;
            font-size: 1rem;
        }

            .input-group input::placeholder
            {
                color: #aaa;
            }

    input:focus
    {
        border-color: #007bff;
    }

    button
    {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 100%;
        padding: 0.75rem;
        background-color: #007bff;
        color: white;
        font-size: 1rem;
        font-weight: bold;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

        button:hover
        {
            background-color: #0056b3;
        }

        button:focus
        {
            outline: none;
        }

    .error
    {
        margin-top: 1rem;
        color: red;
    }
</style>

