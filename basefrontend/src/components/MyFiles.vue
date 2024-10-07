<template>
    <div class="uploaded-files">
        <h1>Uploaded Files</h1>
        <table>
            <thead>
                <tr>
                    <th>Request ID</th>
                    <th>Car Model ID</th>
                    <th>Upload Date</th>
                    <th>Last Modified Date</th>
                    <th>State</th>
                    <th>Title</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="file in uploadedFiles" :key="file.requestID">
                    <td>{{ file.requestID }}</td>
                    <td>{{ file.carModelId }}</td>
                    <td>{{ formatDate(file.uploadDate) }}</td>
                    <td>{{ formatDate(file.lastModifiedDate) }}</td>
                    <td>{{ file.state }}</td>
                    <td>{{ file.title }}</td>
                </tr>
            </tbody>
        </table>
        <p v-if="uploadedFiles.length === 0">No files uploaded yet.</p>
        <p v-if="errorMessage">{{ errorMessage }}</p>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                uploadedFiles: [], // Array to hold uploaded files
                errorMessage: '', // To store error messages if any
            };
        },
        mounted() {
            this.fetchUploadedFiles(); // Fetch uploaded files on component mount
        },
        methods: {
            async fetchUploadedFiles() {
                try {
                    const apiUrl = import.meta.env.VITE_API_BASE_URL;
                    const response = await fetch(`${apiUrl}/api/MyFiles`, {
                        method: 'GET',
                        credentials: 'include', // This ensures cookies are sent with the request
                      
                    });

                    if (!response.ok) {
                        throw new Error('Failed to fetch uploaded files');
                    }

                    const data = await response.json();
                    this.uploadedFiles = data;
                } catch (error) {
                    console.error('Error fetching uploaded files:', error);
                    this.errorMessage = 'Could not load files. Please try again later.';
                }
            },
            formatDate(dateString) {
                // Format the date to a more readable format, e.g., 'DD.MM.YYYY'
                const date = new Date(dateString);
                return `${date.getDate()}.${date.getMonth() + 1}.${date.getFullYear()}`;
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
