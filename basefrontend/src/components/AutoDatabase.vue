<template>
    <div class="auto-data">
        <div class="card" v-if="selectedModel == null">
            <div class="card-header border-0 pt-10 px-6 px-lg-10 px-xxl-15">
                <div class="card-title d-block w-100 m-0">
                    <h4 class="fs-1 text-gray-800 w-bolder mb-6">Upload file</h4>
                    <div class="d-flex align-items-center position-relative my-1 mb-4">
                        <span class="svg-icon svg-icon-1 position-absolute ms-6">
                            <i class="bi bi-search fs-3 vcentered"></i>
                        </span>
                        <input type="text" v-model="searchTerm" class="form-control form-control-solid w-100 ps-15 py-5 fs-4" placeholder="Search by make">
                    </div>
                </div>
            </div>
            <div class="card-body px-6 px-lg-10 px-xxl-15 pb-15 pt-5">
                <div v-if="!selectedMake && !isUploading">
                    <div class="items-grid makes in-modal">
                        <a v-for="brand in filteredBrands" :href="'#'" @click.prevent="selectMake(brand)" :key="brand.slug" class="rounded item hoverable d-flex flex-column align-items-center p-4">
                            <div class="mb-4">
                                <img :src="brand.icon" :alt="brand.name" class="brand-icon">
                            </div>
                            <div class="text-center text-uppercase fw-bolder text-gray-900 fs-8">{{ brand.name }}</div>
                        </a>
                    </div>
                    <div v-if="filteredBrands.length === 0" class="text-center text-gray-800">
                        <p>No results found for "{{ searchTerm }}"</p>
                    </div>
                </div>

                <div v-else-if="!isUploading && selectedModel == null">
                    <h5 class="text-gray-800 w-bolder mb-4">Models for {{ selectedMake.name }}</h5>
                    <div v-if="isLoading" class="loading-indicator">
                        <p>Loading tuning data...</p> <!-- You can replace this with a spinner or any loading animation -->
                    </div>
                    <div class="items-grid models">
                        <a v-for="model in selectedMake.models" :href="'#'" @click.prevent="fetchTuningData(model.id)" :key="model.id" class="rounded item hoverable d-flex flex-column align-items-center p-4">
                            <div class="mb-4">
                                <img :src="model.icon" :alt="model.name" class="model-icon">
                            </div>
                            <div class="text-center text-uppercase fw-bolder text-gray-900 fs-8">{{ model.name }}</div>
                        </a>
                    </div>
                    <button class="btn btn-primary mt-4" @click="returnToMakes">Return</button>
                </div>
            </div>
        </div>

        <span v-if="isAdmin && preselectedModelId!=null && !selectedModel" class="plus-icon" title="Add New Model">
            <span @click="openModal(1,null,null)" class="plus-icon" title="Add New Model" data-bs-toggle="modal" data-bs-target="#addModelModal">
                <button class="btn btn-primary mt-4">
                    <font-awesome-icon icon="plus" /> New Variant
                </button>
            </span>
        </span>

        <div v-if="tuningInfo && !isUploading && selectedModel == null" class="tuning-info mt-5" id="selectModelArea">
            <h5 class="text-gray-800 w-bolder mb-4">Tuning Information</h5>

            <div class="tuning-row">

                <div v-for="(group, typeName) in groupedTuningInfo" :key="typeName" class="tuning-card">
                    <!-- Card for each tuning group -->
                    <div class="card mb-4">
                        <div class="card-header text-left">
                            <h6 class="text-gray-800 w-bolder underline-header">{{ typeName }}</h6>
                            <span v-if="1==1" class="plus-icon" title="Add New Model">
                                <span @click="openModal(2,typeName,null)" class="plus-icon" title="Add New Model" data-bs-toggle="modal" data-bs-target="#addModelModal">
                                    <button v-if="isAdmin" class="btn btn-primary btn-sm">
                                        <font-awesome-icon icon="plus" /> New variant
                                    </button>

                                </span>

                            </span>
                        </div>
                        <div class="card-body">
                            <h7 class="text-gray-800">Petrol Models</h7>
                            <ul class="list-unstyled">
                                <li v-for="(petrol, index) in group.petrol" :key="index" class="text-gray-900 fs-6">
                                    <a href="#" @click.prevent="showDetails(petrol)">{{ petrol.engine }} - {{ petrol.year }} - {{ petrol.horsepower }} {{ petrol.ecuType }}</a>
                                    <span v-if="isAdmin" @click="openDeleteDialog(petrol.tuningId)" class="plus-icon" title="Add New Model" data-bs-toggle="modal" data-bs-target="#addModelModal">
                                        <button class="btn btn-primary btn-sm">
                                            <font-awesome-icon icon="trash-can" />  Delete
                                        </button>
                                    </span>
                                    <span v-if="isAdmin" @click="openModal(3,typeName,petrol)" class="plus-icon" title="Add New Model" data-bs-toggle="modal" data-bs-target="#addModelModal">
                                        <button class="btn btn-primary btn-sm">
                                            <font-awesome-icon icon="trash-can" />  Edit
                                        </button>
                                    </span>
                                </li>
                            </ul>
                            <h7 class="text-gray-800">Diesel Models</h7>
                            <ul class="list-unstyled">
                                <li v-for="(diesel, index) in group.diesel" :key="index" class="text-gray-900 fs-6">
                                    <a href="#" @click.prevent="showDetails(diesel)">{{ diesel.engine }} - {{ diesel.year }} - {{ diesel.horsepower }} {{ diesel.ecuType }}</a>
                                    <span v-if="isAdmin" @click="openDeleteDialog(diesel.tuningId)" class="plus-icon" title="Add New Model" data-bs-toggle="modal" data-bs-target="#addModelModal">
                                        <button class="btn btn-primary btn-sm">
                                            <font-awesome-icon icon="trash-can" />  Delete
                                        </button>

                                    </span>

                                    <span v-if="isAdmin" @click="openModal(3,typeName,diesel)" class="plus-icon" title="Add New Model" data-bs-toggle="modal" data-bs-target="#addModelModal">
                                        <button class="btn btn-primary btn-sm">
                                            <font-awesome-icon icon="trash-can" />  Edit
                                        </button>
                                    </span>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <!-- Available Solutions Component -->
        <AvailableSolutions v-if="selectedModel" :solutions="availableSolutionsWithLogos"
                            :additionalInformation="additionalInformation"
                            :ecuInfoSelected="ecuInfoSelected"
                            :specialInfos="selectedModel"
                            @toggle-checkbox="toggleCheckbox"
                            @show-more-details="showMoreDetails"
                            @cancel-upload="cancelUpload" />



        <VueFinalModal v-model="isDeleteModalOpen" :fullscreen="false" :clickOut="true" style="margin-left: 5vw;">
            <div class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="min-width: 30em; max-height: 20em;">
                        <!-- Match the first modal's min-width -->
                        <div class="modal-header">
                            <button type="button" class="close" @click="closeDeleteDialog" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            Do you want to delete this solution?
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" @click="closeDeleteDialog">Cancel</button>
                            <button type="button" class="btn btn-danger" @click="confirmDelete">Delete</button>
                        </div>
                    </div>
                </div>
            </div>
        </VueFinalModal>

        <div>
            <!-- Modal Component -->
            <AddEditTuningVariantModal v-if="isModalOpen" 
                                       :state="modalState"
                                       :typename="selectedTypeName"
                                       :carbrand="selectedMake"
                                       :model="selectedModelModal"
                                       :ecuList="ecuList"
                                       @closemodal="closeModal"
                                       @submit="handleSubmit" />
        </div>
    </div>
</template>


<script>
    import { imagelogosolutions } from './carbrands'; // Import the car brands and tuning data
    import AvailableSolutions from './AvailableSolutions.vue';
    import { mapState } from 'vuex'; // Import mapState for accessing Vuex state
    import { VueFinalModal } from 'vue-final-modal'
    import AddEditTuningVariantModal from "./AddEditTuningVariantModal.vue"; // Import the modal component    


    export default {
        data() {
            return {
                searchTerm: "",
                selectedModelModal: {},
                selectedMake: null,
                selectedTypeName: '',
                tuningInfo: null,
                selectedModel: null,
                availableSolutions: [],
                uploadedFile: null,
                carBrands: [],
                imagelogosolutions: imagelogosolutions,
                tuningSpecialInfo: [],
                tuningDataBaseInfo: [],
                groupedTuningInfo: {},
                preselectedModelId: null,
                preselectedtypeName: '',
                specialInfo: [],
                isLoading: false,
                isOpenFromOutside: false,
                isOpenFromOutsideModel: false,
                isUploading: false,
                isModalOpen:false,
                ecuList: [
                    { Id: 1, EcuName: "MED17.7", ConnectionInfoId: 12 },
                    { Id: 2, EcuName: "MED17.8", ConnectionInfoId: 13 },
                    // Add more ECUs as needed
                ],
                isDeleteModalOpen: false, // Modal visibility
                deleteTargetId: null
            };
        },
        components: {
            AvailableSolutions,
            VueFinalModal,
            AddEditTuningVariantModal
        }, mounted() {
            this.fetchCarBrands(); // Call fetch method on mount
            this.getEcuListBackend();
        },
        computed: {
            ...mapState(['user']),
            isAdmin() {
                return this.user && this.user.role === 'Admin'; // Adjust this as per your logic for admins
            },
            filteredBrands() {
                return this.carBrands.filter(brand =>
                    brand.name.toLowerCase().includes(this.searchTerm.toLowerCase())
                );
            },
            availableSolutionsWithLogos() {
                return this.availableSolutions.map(solution => {
                    const logoInfo = this.imagelogosolutions.find(logo => logo.name === solution.name);

                    return {
                        ...solution,
                        logo: logoInfo ? logoInfo.logo : null,
                        checked: solution.checked || false, // Initialize checked state,
                        inputArea: logoInfo ? logoInfo.inputArea : false,
                        inputAreaText: logoInfo ? logoInfo.inputAreaText : '',
                    };
                });
            },
            additionalInformation() {
                return this.specialInfo?.additionalInformation
            },
            ecuInfoSelected() {
                return this.specialInfo?.ecuInfo
            }
        },
        methods: {
            // Open delete confirmation dialog
            openDeleteDialog(id) {
                this.deleteTargetId = id; // Store the ID of the item to delete
                this.isDeleteModalOpen = true; // Show the modal
            },
            // Close the delete dialog
            closeDeleteDialog() {
                this.isDeleteModalOpen = false; // Hide the modal
                this.deleteTargetId = null;     // Clear the target ID
            },
            // Perform delete operation
            async confirmDelete() {
                if (!this.deleteTargetId) return;

                const apiUrl = import.meta.env.VITE_API_BASE_URL; // API base URL
                try {
                    const response = await fetch(`${apiUrl}/api/Tuning/DeleteTuningVariant`, {
                        method: 'POST', // Use POST or DELETE based on your API
                        headers: {
                            'Content-Type': 'application/json', // Set the content type to JSON
                        },
                        credentials: 'include', // Include cookies or tokens
                        body: JSON.stringify({
                            "input": this.deleteTargetId
                        }),
                    });

                    if (!response.ok) {
                        throw new Error(`Error! HTTP status: ${response.status}`);
                    }

                    // Refresh data or update the UI after successful deletion
                    this.fetchTuningData(this.preselectedModelId);
                } catch (error) {
                    console.error('Error deleting item:', error);
                } finally {
                    this.closeDeleteDialog(); // Close the modal
                }
            },
            async getEcuListBackend() {
                this.ecuList = [];
                const apiUrl = import.meta.env.VITE_API_BASE_URL; // Get base URL from environment variables
                this.isLoading = true; // Set loading to true while fetching data

                try {
                    const response = await fetch(`${apiUrl}/api/Base/GetEcuList`, {
                        method: 'GET', // Specify the method
                        credentials: 'include', // Include credentials such as cookies
                    });

                    if (!response.ok) {
                        throw new Error(`HTTP error! status: ${response.status}`); // Handle HTTP errors
                    }

                    const data = await response.json();
                    this.ecuList = data; // Store fetched tuning info
                    // Group tuning info by type and include tuningId in each variant


                } catch (error) {
                    console.error('Error fetching tuning data:', error); // Log the error for debugging
                    // You can also set an error state here to inform the user
                } finally {
                    this.isLoading = false; // Set loading to false after the request
                }
            },
            getModelByGivenId: function (id) {
                if (this.selectedMake != null) {
                    if (this.selectedMake.models != null) {
                        return this.selectedMake.models.find((ele) => ele.id == id);
                    }
                }
            },
            openModal(state, typename, model) {
                // Open the modal with the given state
                this.selectedModelModal = model;
                this.selectedTypeName = typename;
                this.modalState = state;
                this.isModalOpen = true;
            },
            closeModal() {
                // Close the modal
                this.isModalOpen = false;
            },
            async fetchCarBrands() {
                this.isLoading = true; // Set loading to true
                const apiUrl = import.meta.env.VITE_API_BASE_URL; // Get base URL from environment variables

                try {
                    const response = await fetch(`${apiUrl}/api/Tuning/carbrands`, {
                        method: 'GET', // Specify the method
                        credentials: 'include', // Include credentials such as cookies
                    });

                    if (!response.ok) {
                        throw new Error(`HTTP error! status: ${response.status}`); // Handle HTTP errors
                    }

                    const data = await response.json();
                    this.carBrands = data; // Store fetched car brands in component's data
                } catch (error) {
                    console.error('Error fetching car brands:', error); // Log the error for debugging
                    // You can also set an error state here to inform the user
                } finally {
                    this.isLoading = false; // Set loading to false after the request
                }
            },
            toggleCheckbox(solution) {
                // Handle the checkbox toggle event
                this.availableSolutions.filter(x => x.name === solution.name)[0].checked = !solution.checked;
            },
            showMoreDetails(solution) {
                // Handle the show more details event
                console.log('Show more details:', solution);
            },
            cancelUpload() {
                // Clear the available solutions and return to the previous view
                this.availableSolutions = [];
                this.selectedModel = null;
            },

            selectMake(brand) {
                this.selectedMake = brand;
                this.preselectedtypeName = '';
            },
            returnToMakes() {
                this.selectedMake = null;
                this.tuningInfo = null;
                this.selectedModel = null;
                this.preselectedModelId = null;
                this.preselectedtypeName = '';
                this.groupedTuningInfo = {};
            },
            async fetchTuningData(modelId) {
                this.preselectedModelId = modelId;
                const apiUrl = import.meta.env.VITE_API_BASE_URL; // Get base URL from environment variables
                this.isLoading = true; // Set loading to true while fetching data

                try {
                    const response = await fetch(`${apiUrl}/api/Tuning/tuningdatabase/${modelId}`, {
                        method: 'GET', // Specify the method
                        credentials: 'include', // Include credentials such as cookies
                    });

                    if (!response.ok) {
                        throw new Error(`HTTP error! status: ${response.status}`); // Handle HTTP errors
                    }

                    const data = await response.json();

                    this.tuningInfo = data[0]; // Store fetched tuning info
                    // Group tuning info by type and include tuningId in each variant
                    this.groupedTuningInfo = data[0].variants.reduce((acc, variant) => {
                        const key = variant.typeName; // Use the typeName as the key

                        // Check if the accumulator for the key already exists; if not, create it
                        if (!acc[key]) {
                            acc[key] = { petrol: [], diesel: [] }; // Initialize arrays for petrol and diesel
                        }

                        // Create a new object that includes the tuningId and other variant data
                        const variantInfo = {
                            tuningId: variant.tuningId, // Save the tuningId
                            ...variant // Spread the rest of the variant properties
                        };

                        // Debugging step to log the variantInfo object
                        console.log('Processing variant:', variant);
                        console.log('Variant Info:', variantInfo);

                        // Normalize the case of the variant type for comparison
                        const variantType = variant.variant.toLowerCase(); // Convert to lowercase

                        // Push to the appropriate array based on the normalized variant type
                        if (variantType === 'petrol') {
                            acc[key].petrol.push(variantInfo);
                        } else if (variantType === 'diesel') {
                            acc[key].diesel.push(variantInfo);
                        }

                        return acc;
                    }, {});

                    // Debugging step to check the final grouped tuning info
                    console.log('Grouped Tuning Info:', this.groupedTuningInfo);

                    // Smooth scroll to the tuning information area
                    this.$nextTick(() => {
                        const modelArea = document.getElementById('selectModelArea');
                        if (modelArea) {
                            modelArea.scrollIntoView({ behavior: 'smooth' });
                        }
                    });

                } catch (error) {
                    console.error('Error fetching tuning data:', error); // Log the error for debugging
                    // You can also set an error state here to inform the user
                } finally {
                    this.isLoading = false; // Set loading to false after the request
                }
            },
            async showDetails(item) {
                this.availableSolutions = []; // Clear previous solutions
                this.selectedModel = item; // Set the currently selected model
                this.specialInfo = {}; // Reset specialInfo before fetching new data

                try {
                    // Fetch special tuning info based on tuning_id
                    const apiUrl = import.meta.env.VITE_API_BASE_URL; // Get base URL from environment variables
                    const response = await fetch(`${apiUrl}/api/Tuning/tuningspecial/${item.tuningId}`, {
                        method: 'GET', // Specify the method
                        credentials: 'include', // Include credentials such as cookies
                    });
                    if (!response.ok) {
                        throw new Error(`HTTP error! status: ${response.status}`); // Handle HTTP errors
                    }

                    const data = await response.json(); // Parse the JSON response
                    this.specialInfo = data[0]; // Store the special tuning info in the component's data
                    // Assuming specialInfo has available solutions, set them here
                    this.availableSolutions = this.specialInfo.availableSolutions || []; // Set available solutions
                } catch (error) {
                    console.error('Error fetching tuning special data:', error); // Log the error for debugging
                    // Handle any additional error state management here
                }
            },
            handleFileUpload(event) {
                const file = event.target.files[0];
                if (file) {
                    this.uploadedFile = file;
                    // Perform upload logic
                }
            },
            sendTuningData() {
                // Logic to send tuning data
            },
            cancelUpload() {
                this.availableSolutions = [];
                this.uploadedFile = null;
                this.selectedModel = null;;
            },
        },
        watch: {
          
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
