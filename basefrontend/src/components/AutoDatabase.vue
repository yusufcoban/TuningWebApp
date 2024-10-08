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

        <div v-if="tuningInfo && !isUploading && selectedModel == null" class="tuning-info mt-5" id="selectModelArea">
            <h5 class="text-gray-800 w-bolder mb-4">Tuning Information</h5>
            <div class="tuning-row">
                <div v-for="(group, typeName) in groupedTuningInfo" :key="typeName" class="tuning-card">
                    <!-- Card for each tuning group -->
                    <div class="card mb-4">
                        <div class="card-header text-left">
                            <h6 class="text-gray-800 w-bolder underline-header">{{ typeName }}</h6>
                        </div>
                        <div class="card-body">
                            <h7 class="text-gray-800">Petrol Models</h7>
                            <ul class="list-unstyled">
                                <li v-for="(petrol, index) in group.petrol" :key="index" class="text-gray-900 fs-6">
                                    <a href="#" @click.prevent="showDetails(petrol)">{{ petrol.engine }} - {{ petrol.year }} - {{ petrol.horsepower }} {{ petrol.ecutype }}</a>
                                </li>
                            </ul>
                            <h7 class="text-gray-800">Diesel Models</h7>
                            <ul class="list-unstyled">
                                <li v-for="(diesel, index) in group.diesel" :key="index" class="text-gray-900 fs-6">
                                    <a href="#" @click.prevent="showDetails(diesel)">{{ diesel.engine }} - {{ diesel.year }} - {{ diesel.horsepower }} {{ diesel.ecutype }}</a>
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
    </div>
</template>


<script>
    import { carBrands, tuningDataBaseInfo, tuningSpecialInfo, imagelogosolutions } from './carbrands'; // Import the car brands and tuning data
    import AvailableSolutions from './AvailableSolutions.vue';

    export default {
        data() {
            return {
                searchTerm: "",
                selectedMake: null,
                tuningInfo: null,
                selectedModel: null,
                availableSolutions: [],
                uploadedFile: null,
                carBrands: carBrands,
                imagelogosolutions: imagelogosolutions,
                tuningSpecialInfo: tuningSpecialInfo,
                tuningDataBaseInfo: tuningDataBaseInfo,
                groupedTuningInfo: {},
                specialInfo: [],
                isUploading: false, // Flag to check if uploading is in progress
            };
        },
        components: {
            AvailableSolutions, // Register the component
        },
        computed: {
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
            },
            returnToMakes() {
                this.selectedMake = null;
                this.tuningInfo = null;
                this.selectedModel = null;
                this.groupedTuningInfo = {};
            },
            fetchTuningData(modelId) {
                const brandInfo = this.tuningDataBaseInfo.find(item => item.id === modelId);
                if (brandInfo) {
                    this.groupedTuningInfo = brandInfo.variants.reduce((acc, variant) => {
                        const key = variant.typeName;
                        if (!acc[key]) {
                            acc[key] = { petrol: [], diesel: [] };
                        }
                        if (variant.variant === 'petrol') {
                            acc[key].petrol.push(variant);
                        } else if (variant.variant === 'diesel') {
                            acc[key].diesel.push(variant);
                        }
                        return acc;
                    }, {});
                    this.tuningInfo = brandInfo;
                } else {
                    this.tuningInfo = null;
                    this.groupedTuningInfo = {};
                }

                this.$nextTick(() => {
                    const modelArea = document.getElementById('selectModelArea');
                    if (modelArea) {
                        modelArea.scrollIntoView({ behavior: 'smooth' }); // Smooth scroll to the area
                    }
                });

            },
            showDetails(item) {
                this.availableSolutions = [];
                this.specialInfo = this.tuningSpecialInfo.find(info => info.id === item.tuning_id);
                if (this.specialInfo) {
                    this.availableSolutions = this.specialInfo.availAbleSolution;
                    this.selectedModel = item;
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
    };
</script>


<style scoped>
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
