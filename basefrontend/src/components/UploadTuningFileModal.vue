<template>
    <div>
        <VueFinalModal v-model="triggerModal"
                       :fullscreen="false"
                       :clickOut="true"
                       :style="{ marginLeft:  '10vw'  }">
            <div class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="min-width: 50em; max-height: 40em;">
                        <div class="modal-header">

                            <h5 class="modal-title">Upload File</h5>
                            <button type="button" class="close" @click="closeModal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div>
                                <h2>Infos</h2>
                                <p><strong>File Name:</strong> {{ fileUploadTask.myUploadedFile.fileName }}</p>
                                <p><strong>Added infos:</strong> {{ fileUploadTask.myUploadedFile.additionalInfo }}</p>
                                <p><strong>Selected variants:</strong> {{ fileUploadTask.myUploadedFile.selectedVariants }}</p>
                                <p><strong>DTCList:</strong> {{ fileUploadTask.myUploadedFile.dtcList? fileUploadTask.myUploadedFile.dtcList:'No dtc list added to this request'}}</p>
                            </div>


                            <div class="upload-area">
                                <h6>Select a File to Upload</h6>
                                <input type="file" @change="handleFileUpload" />
                            </div>
                            <div class="upload-info">
                                <p v-if="selectedFile">Selected File: {{ selectedFile.name }}</p>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" @click="closeModal">Close</button>
                            <button type="button" class="btn btn-primary" @click="uploadFile" :disabled="!selectedFile">Upload</button>
                        </div>
                    </div>
                </div>
            </div>
        </VueFinalModal>
    </div>
</template>

<script>
    import { VueFinalModal } from 'vue-final-modal'
    import 'vue-final-modal/style.css'

    export default {
        props: {
            fileUploadTask: Object
        },
        data() {
            return {
                triggerModal: false,
                selectedFile: null,
            };
        },
        components: {
            VueFinalModal
        },
        computed: {
        },
        methods: {
            closeModal() {
                this.$emit("closemodal", false);
            },
            handleFileUpload(event) {
                const file = event.target.files[0];
                if (file) {
                    this.selectedFile = file; // Store the selected file
                }
            },
            async uploadFile() {
                if (!this.selectedFile) {
                    alert("Please select a file first.");
                    return;
                }
                const formData = new FormData();
                formData.append("file", this.selectedFile); // Append file to FormData

                try {
                    // Get the API URL from environment variables
                    const apiUrl = import.meta.env.VITE_API_BASE_URL;

                    // Send POST request to upload the file and solutions
                    fetch(`${apiUrl}/api/MyFiles/UploadTuningFile`, {
                        method: 'POST',
                        credentials: 'include',
                        body: formData,
                    })
                        .then(response => {
                            if (!response.ok) {
                                throw new Error('Failed to upload data');
                            }
                            return response.json(); // assuming the response is JSON
                        })
                        .then(data => {
                            alert('Solutions uploaded successfully!');
                        })
                        .catch(error => {
                            alert('There was an error uploading solutions. Please try again.');
                        });



                } catch (error) {
                    alert(`Error: ${error.message}`);
                }
            },
        },
        mounted() {
            this.triggerModal = true;
        }
    };
</script>

<style scoped>
    .modal-body
    {
        overflow-y: scroll;
        height: 1050px
    }

    .modal-dialog
    {
        max-width: 100%;
        position: relative;
        width: auto;
        margin: .5rem;
        pointer-events: none;
    }

    @media (min-width: 576px)
    {
        .modal-dialog
        {
            margin: 1.75rem auto;
        }
    }

    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        z-index: 1050;
        width: 100%;
        display: flex;
        height: 100%;
        overflow: hidden;
        outline: 0;
    }

    .auto-data
    {
        text-align: center;
        width: 100%;
    }

    .tuning-info
    {
        margin-top: 20px; /* Optional margin for overall tuning info */
    }

    .tuning-row
    {
        display: flex;
        flex-wrap: wrap; /* Allow cards to wrap into the next line */
        justify-content: space-between; /* Space out the cards evenly */
    }

    .tuning-card
    {
        flex: 0 1 calc(50% - 20px); /* Two cards per row with space between */
        margin-bottom: 20px; /* Space between rows */
    }

    .card
    {
        /* Add any additional styles for the card here */
    }

    .underline-header
    {
        text-decoration: underline; /* Underline the typeName */
    }

    .card
    {
        margin: 20px auto;
        max-width: 100em;
    }

    .items-grid
    {
        display: flex;
        flex-wrap: wrap;
        justify-content: center;
    }

    .item
    {
        transition: transform 0.3s;
    }

        .item:hover
        {
            transform: scale(1.05);
        }

    .brand-icon, .model-icon
    {
        width: 120px; /* Adjust size as needed */
        height: auto;
    }

    .tuning-section
    {
        margin-top: 20px;
    }

        .tuning-section h6
        {
            margin-bottom: 10px;
        }

    .petrol-diesel
    {
        margin-top: 10px;
    }

        .petrol-diesel h7
        {
            font-weight: bold;
        }

    .upload-area
    {
        margin-top: 20px;
        text-align: center;
    }
</style>