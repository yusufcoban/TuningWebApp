<template>
    <div class="uploaded-files">
        <h1>Uploaded Files</h1>
        <div class="table-container">
            <table class="table table-hover table-bordered">
                <thead class="table-light">
                    <tr>
                        <th>Request ID</th>
                        <th>Car Model ID</th>
                        <th>Upload Date</th>
                        <th>Last Modified Date</th>
                        <th>State</th>
                        <th>Title</th>
                        <th>Infos</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="file in uploadedFiles" :key="file.id" class="row-clickable" @click="this.$router.push({ path: `/MyFilesViewer/`+file.id });">
                        <td>{{ file.id }}</td>
                        <td>{{ file.carmodelId }}</td>
                        <td>{{ formatDate(file.uploadDate) }}</td>
                        <td>{{ formatDate(file.modifyDate) }}</td>
                        <td>{{ file.state }}</td>
                        <td>{{ file.fileName }}</td>
                        <td>{{ file.additionalInfo }}</td>
                    </tr>
                </tbody>
            </table>
        </div>
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
        coponent() {
        },
        mounted() {
            this.fetchUploadedFiles(); // Fetch uploaded files on component mount
        },
        methods: {
            async downloadFile(fileName) {
                try {
                    const apiUrl = import.meta.env.VITE_API_BASE_URL;
                    const response = await fetch(`${apiUrl}/api/MyFiles/download/${fileName}`, {
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
        max-width: 100em;
        margin: auto;
        font-family: Arial, sans-serif;
    }

    h1
    {
        color: #2c3e50;
        text-align: center;
    }

    .table-container
    {
        overflow-x: auto; /* Enable horizontal scrolling */
        max-width: 100%; /* Restrict width to parent container */
        margin: 0 auto; /* Center the container */
    }

    table
    {
        width: 100%; /* Make table take up the full width of the container */
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

    .row-clickable
    {
        cursor: pointer;
    }

        .row-clickable:hover
        {
            background-color: #f9f9f9; /* Highlight row on hover */
        }

    p
    {
        text-align: center;
        color: #999;
    }
</style>

