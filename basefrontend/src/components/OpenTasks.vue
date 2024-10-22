<template>
    <div class="uploaded-files">
        <h1>Uploaded Files</h1>
        <table class="table table-hover table-bordered">
            <thead class="table-light">
                <tr>
                    <th>taskId ID</th>
                    <th>createDate</th>
                    <th>tuningVariantId</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="file in openTasks" :key="file.id" class="row-clickable"   >
                    <td>{{ file.taskId }}</td>
                    <td>{{ formatDate(file.createDate) }}</td>
                    <td>{{ file.myUploadedFile.tuningVariantId }}</td>
                    <td>
                    <button @click="downloadFile(file.myUploadedFile.fileName,file.myUploadedFile.username)">Download uploaded file
                    </button></td>

                </tr>
            </tbody>
        </table>
        <p v-if="openTasks.length === 0">No files uploaded yet.</p>
        <p v-if="errorMessage">{{ errorMessage }}</p>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                openTasks: [], // Array to hold uploaded files
                errorMessage: '', // To store error messages if any
            };
        },
        coponent() {
        },
        mounted() {
            this.fetchopenTasks(); // Fetch uploaded files on component mount
        },
        methods: {
            async downloadFile(fileName, userName) {
                try {
                    const apiUrl = import.meta.env.VITE_API_BASE_URL;

                    // Modify the fetch URL to include the userName as a query parameter
                    const response = await fetch(`${apiUrl}/api/MyFiles/downloadAdmin/${fileName}?userName=${encodeURIComponent(userName)}`, {
                        method: 'GET',
                        credentials: 'include' // Ensure cookies are sent with the request
                    });

                    if (!response.ok) {
                        throw new Error('Failed to download the file');
                    }

                    // Create a Blob from the response data
                    const blob = await response.blob();
                    const url = window.URL.createObjectURL(blob);

                    // Create a link element to initiate the download
                    const a = document.createElement('a');
                    a.href = url;
                    a.download = fileName; // Specify the file name for download
                    document.body.appendChild(a); // Append to the DOM
                    a.click(); // Trigger the download
                    a.remove(); // Clean up

                    // Release the URL object
                    window.URL.revokeObjectURL(url);
                } catch (error) {
                    console.error('Error downloading the file:', error);
                    this.errorMessage = 'Could not download the file. Please try again later.';
                }
            },
            async fetchopenTasks() {
                try {
                    const apiUrl = import.meta.env.VITE_API_BASE_URL;
                    const response = await fetch(`${apiUrl}/api/MyFiles/GetAllTasks`, {
                        method: 'GET',
                        credentials: 'include', // This ensures cookies are sent with the request

                    });

                    if (!response.ok) {
                        throw new Error('Failed to fetch uploaded files');
                    }

                    const data = await response.json();
                    this.openTasks = data;
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
