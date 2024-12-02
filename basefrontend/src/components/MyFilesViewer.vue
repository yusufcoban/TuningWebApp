<template>
    <div class="my-files-page">
        <h1>My Uploaded Files</h1>

        <!-- Error message display -->
        <div v-if="errorMessage" class="error-message">{{ errorMessage }}</div>

        <div class="cards-container">
            <!-- Loop through each uploaded file and display it in a card -->
            <div v-if="dataFromBackend && dataFromBackend.length" v-for="file in dataFromBackend" :key="file.id" class="file-card">
                <h2>{{ file.title }}</h2>
                <p><strong>File Name:</strong> {{ file.fileName }} <button type="button" @click="downloadFile(file.fileName)">Download</button></p>
                <p><strong>Uploaded on:</strong> {{ formatDate(file.uploadDate) }}</p>
                <p><strong>DTC List:</strong> {{ file.dtcList }}</p>
                <p><strong>Selected Variants:</strong> {{ file.selectedVariants }}</p>
                <p><strong>Car Model ID:</strong> {{ file.carmodelId }}</p>
                <p><strong>Tuning Variant ID:</strong> {{ file.tuningVariantId }}</p>
                <div v-if="file.tasks && file.tasks.length">
                    <h3>Uploaded Data:</h3>
                    <ul>
                        <li v-for="task in file.tasks" :key="task.id">
                            <p><strong>Created On:</strong> {{ formatDate(task.createDate) }}</p>
                            <p>
                                <strong>New File Name: {{ task.newFileName }}</strong>
                                <button type="button" @click="downloadFile(task.newFileName)">Download tuning file</button>
                            </p>
                        </li>
                    </ul>
                </div>
            </div>

            <!-- Display a message if no files are found -->
            <div v-else>
                <p>No files found for this ID.</p>
            </div>
            <!-- Static information card -->
            <div class="static-info-card">
                <h2>Static Information</h2>
                <p><strong>Username:</strong> FakeUser123</p>
                <p><strong>Email:</strong> fakeuser@example.com</p>
                <p><strong>Subscription:</strong> Premium Member</p>
                <p><strong>Membership Since:</strong> Jan 2020</p>
            </div>
        </div>

       
    </div>
</template>

<script>
    export default {
        data() {
            return {
                fileId: null, // Set this value to the ID you want to fetch
                dataFromBackend: [], // To hold the uploaded files fetched from the server
                errorMessage: '', // To hold error messages
            };
        },
        created() {
            this.fileId = this.$route.params.id;
            this.fetchUploadedFiles(); // Fetch the files when the component is created
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
                    const response = await fetch(`${apiUrl}/api/MyFiles/GetUploadedFilesByID?id=${this.fileId}`, {
                        method: 'GET',
                        credentials: 'include', // This ensures cookies are sent with the request
                    });
                    if (!response.ok) {
                        throw new Error('Failed to fetch uploaded files');
                    }

                    const data = await response.json();
                    this.dataFromBackend = data;
                } catch (error) {
                    console.error('Error fetching uploaded files:', error);
                    this.errorMessage = 'Could not load files. Please try again later.';
                }
            },
            formatDate(dateString) {
                const options = { year: 'numeric', month: 'long', day: 'numeric' };
                return new Date(dateString).toLocaleDateString(undefined, options);
            },
        },
    };
</script>

<style scoped>
    .my-files-page
    {
        padding: 20px;
    }

    .error-message
    {
        color: red;
        margin-bottom: 20px;
    }

    .cards-container
    {
        display: flex;
        flex-wrap: wrap;
        gap: 20px;
    }

    .file-card,
    .static-info-card
    {
        background-color: #f9f9f9;
        border: 1px solid #ddd;
        border-radius: 8px;
        padding: 20px;
        box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    }

        .file-card h2,
        .static-info-card h2
        {
            margin-top: 0;
            color: #333;
        }

    p
    {
        margin: 8px 0;
    }

    .static-info-card
    {
        background-color: #e0f7fa;
    }
</style>
