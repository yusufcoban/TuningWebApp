<template>
    <div class="uploaded-files">
        <h1>Uploaded Files</h1>
        <div class="table-container">
            <table class="table table-hover table-bordered">
                <thead class="table-light">
                    <tr>
                        <th>taskId ID</th>
                        <th>createDate</th>
                        <th>tuningVariantId</th>
                        <th>dtcList</th>
                        <th>selectedVariants</th>
                        <th>additionalInfo</th>
                        <th>Download</th>
                        <th>Upload</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="file in openTasks" :key="file.id" class="row-clickable">
                        <td>{{ file.taskId }}</td>
                        <td>{{ formatDate(file.createDate) }}</td>
                        <td>{{ file.myUploadedFile.tuningVariantId }}</td>
                        <td>{{ file.myUploadedFile.dtcList }}</td>
                        <td>{{ file.myUploadedFile.selectedVariants }}</td>
                        <td>{{ file.myUploadedFile.additionalInfo }}</td>
                        <td>
                            <button @click="downloadFile(file.myUploadedFile.fileName, file.myUploadedFile.username)">
                                Download uploaded file
                            </button>
                        </td>
                        <td>
                            <button @click="openUploadModal(file)">
                                Upload tuning file
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <p v-if="openTasks.length === 0">No files uploaded yet.</p>
        <p v-if="errorMessage">{{ errorMessage }}</p>
        <UploadTuningFileModal v-if="openModalUpload" :fileUploadTask="fileUploadTask" @closemodal="closeUploadModal" />
    </div>
</template>


<script>
    import UploadTuningFileModal from './UploadTuningFileModal.vue';

    export default {
        data() {
            return {
                openTasks: [], // Array to hold uploaded files
                errorMessage: '', // To store error messages if any
                openModalUpload: false,
                fileUploadTask: {}
            };
        },
        components: {
            UploadTuningFileModal
        },
        mounted() {
            this.fetchopenTasks(); // Fetch uploaded files on component mount
        },
        methods: {
            openUploadModal: function (taskModel) {
                this.fileUploadTask = {},
                    this.openModalUpload = false;
                this.fileUploadTask = taskModel;
                this.$nextTick(() => {
                    this.openModalUpload = true;
                });
            },
            closeUploadModal: function () {
                this.$nextTick(() => {
                    this.openModalUpload = false;
                });
            },
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

