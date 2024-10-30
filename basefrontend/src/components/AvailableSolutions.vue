<template>
    <div class="container-fluid d-flex">
        <!-- Fixed Sidebar -->
        <aside class="sidebar">
            <h5 class="text-gray-800 w-bolder mb-4">ECU Info Selected</h5>
            <ul class="list-unstyled">
                <li class="text-gray-900 fs-6">Ecu Info: {{ ecuInfoSelected?.ecuName }}</li>
            </ul>
            <h6 class="text-gray-800 w-bolder mb-2">Tuning Tools Info</h6>
            <ul class="list-unstyled">
                <li v-for="(tool, toolName) in ecuInfoSelected?.groupedAvailableConnections" :key="toolName" class="text-gray-900 fs-6">
                    <strong>{{ tool.toolName }}</strong>
                    <ul>
                        <li v-for="(connection, index) in tool.connectionTypes" :key="index" class="text-gray-700 fs-6">
                            Mode: {{ connection }}
                        </li>
                    </ul>
                </li>
            </ul>
        </aside>

        <!-- Main Content Area -->
        <div class="main-content">
            <div v-if="solutions.length" class="mt-4">
                <h2 class="mb-5 text-center">Available Solutions</h2>
                <div class="row g-4">
                    <div class="col-md-6" v-for="solution in solutions" :key="solution.name">
                        <div class="card solution-card p-4 d-flex align-items-center" @click="emitCheckboxToggle(solution)">
                            <!-- Card content divided into 3 areas -->
                            <div class="d-flex w-100 align-items-center justify-content-between">
                                <!-- First area: Icon (25%) -->
                                <div class="icon-area">
                                    <img v-if="solution.logo" :src="solution.logo" class="solution-logo" alt="" style="max-height:4em" />
                                </div>

                                <!-- Second area: Text (50%) -->
                                <div class="text-area">
                                    <div class="text-gray-900 fw-bold fs-5">{{ solution.name }}</div>
                                    <span class="text-gray-700 d-block fs-7">
                                        {{ solution.information }}
                                        <a class="text-gray-800 text-hover-gray-600" href="javascript:void(0)" @click.prevent="emitDetails(solution)">(read more)</a>
                                    </span>
                                </div>

                                <!-- Third area: Checkbox (25%) -->
                                <div class="checkbox-area">
                                    <input type="checkbox" :id="solution.name" v-model="solution.checked" class="solution-checkbox">
                                </div>
                            </div>

                            <!-- Optional input area -->
                            <div v-if="solution.inputArea" class="input-area mt-3" style="width:100%;">
                                <br />
                                <div class="alert alert-warning d-flex align-items-center">
                                    <div class="d-flex flex-column">
                                        <span class="text-gray-900 fs-8">{{ solution.inputAreaText }}</span>
                                    </div>
                                </div>

                                <textarea style="width:90%;min-height:5em" placeholder="Enter additional info" v-model="dtcList"></textarea>
                            </div>
                        </div>
                    </div>

                    <section style="width: 97%; margin: auto; text-align: center;">
                        <div>
                            <h3>Additional infos for tuner</h3>
                            <textarea id="status" rows="5" style="resize: none; width: 100%;"  v-model="additionalInfoUpload"></textarea>
                        </div>
                        <span id="text_counter">{{additionalInfoUpload.length}}</span> characters
                    </section>
                </div>

                <!-- Upload area and send button -->
                <div class="upload-area mt-5">
                    <h5 class="text-gray-800 w-bolder mb-4 text-center">Upload Tuning Data</h5>
                    <div class="mt-4 text-center">
                        <h6 class="text-gray-800">Upload File</h6>
                        <input type="file" @change="handleFileUpload" class="form-control">
                    </div>

                    <!-- Send and Cancel buttons -->
                    <div class="button-group mt-4 text-center">
                        <button class="btn btn-primary me-2" @click="sendTuningData">Send</button>
                        <button class="btn btn-secondary" @click="cancelUpload">Return to Tuning Information</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        props: {
            solutions: Array, // Receives available solutions from the parent
            ecuInfoSelected: Array, // Receives ECU info from the parent,
            specialInfos: Array,

        },
        data() {
            return {
                uploadedFile: null, // Store the uploaded file
                dtcList: '',
                additionalInfoUpload: ''
            };
        },
        created() {
            this.additionalInfoUpload = '';
            this.dtcList = '';
        },
        methods: {
            emitCheckboxToggle(solution) {
                this.$emit('toggle-checkbox', solution);
            },
            emitDetails(solution) {
                this.$emit('show-more-details', solution);
            },
            handleFileUpload(event) {
                this.uploadedFile = event.target.files[0];
            },
            sendTuningData() {
                // Check if a file is uploaded
                if (!this.uploadedFile) {
                    alert('Please upload a file.');
                    return;
                }

                // Create FormData object and append the uploaded file
                const formData = new FormData();
                formData.append('file', this.uploadedFile); // Upload the file

                // Filter selected solutions and append them to FormData
                const checkedSolutions = this.solutions.filter(solution => solution.checked);
                checkedSolutions.forEach((solution, index) => {
                    formData.append(`solutions[${index}]`, solution.name); // Appending each checked solution by index
                });

                // Append solutionId to FormData
                formData.append('solutionid', this.specialInfos?.tuningId + '');

                // Append additional information text
                formData.append('additionalInfo', this.additionalInfoUpload || '');

                // Assuming this.dtcList is a string like 'code1,code2;code3,code4'
                // Assuming this.dtcList is a string that may be empty
                let dtcListS = this.dtcList ? this.dtcList.split(/[,;]+/) : [];
               
                // If the array is empty, append an empty array to formData
                if (dtcListS.length === 0) {
                    formData.append('dtcList[]', ''); // Option 1: Add an empty value
                    // formData.append('dtcList', []); // Option 2: Add an empty array directly if supported
                } else {
                    // Iterate over the array and append each element to formData
                    dtcListS.forEach((listS, index) => {
                        formData.append(`dtcList[${index}]`, listS);
                    });
                }


                // Get the API URL from environment variables
                const apiUrl = import.meta.env.VITE_API_BASE_URL;

                // Send POST request to upload the file and solutions
                fetch(`${apiUrl}/api/MyFiles/upload`, {
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
                        console.log('File and solutions uploaded successfully:', data);
                        alert('File and solutions uploaded successfully!');
                    })
                    .catch(error => {
                        console.error('Error:', error);
                        alert('There was an error uploading the file and solutions. Please try again.');
                    });
            },
            cancelUpload() {
                this.$emit('cancel-upload');
            }
        }
    };
</script>

<style scoped>
    .container-fluid
    {
        display: flex;
        width: 100%;
        height: 100vh; /* Full height to show fixed sidebar */
    }

    .sidebar
    {
        width: 250px; /* Fixed width for sidebar */
        padding: 15px;
        background-color: #f8f9fa; /* Background color for the sidebar */
        border-right: 1px solid #dee2e6; /* Right border for separation */
        overflow-y: auto; /* Allow scrolling if content overflows */
    }

    .main-content
    {
        flex: 1; /* Takes the remaining space */
        padding: 15px;
        overflow-y: auto; /* Allow scrolling if content overflows */
    }

    .solution-card
    {
        display: flex;
        justify-content: center;
        align-items: center;
    }

    /* Additional styles can be added as needed */
</style>
