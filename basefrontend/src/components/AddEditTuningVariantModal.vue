<template>
    <div>
        <VueFinalModal v-model="triggerModal"
                       :fullscreen="false"
                       :clickOut="true"
                       class="miskte"
                       :style="{ marginLeft: state === 1 ? '10vw' : '5vw' }">
            <div class="modal" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content" style="min-width: 80em; max-height: 60em;">
                        <div class="modal-header">
                            <div class="mb-4">
                                <img :src="carbrand.icon" class="brand-icon" style="width: 50px; height: auto;" />
                            </div>
                            <div v-if="state === 2 || state === 3">
                                <h4 class="modal-title">{{ formData.typeName }}</h4>
                            </div>
                            <h5 v-else class="modal-title">{{ carbrand.name }}</h5>
                            <button type="button" class="close" @click="closeModal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <form @submit.prevent="submitForm">
                                <h4 class="mb-3">General Infos</h4>
                                <div class="row mb-4">
                                    <div class="col-xs-6 form-group">
                                        <label for="typeName">Model Name</label>
                                        <input type="text" id="typeName" v-model="formData.typeName" class="form-control" required />
                                    </div>
                                </div>
                                <div class="row mb-4">
                                    <div class="col-xs-6 form-group">
                                        <label for="yearStart">Year Start</label>
                                        <input type="number" id="yearStart" v-model="formData.yearStart" class="form-control" required />
                                    </div>
                                    <div class="col-xs-6 form-group">
                                        <label for="yearEnd">Year End</label>
                                        <input type="number" id="yearEnd" v-model="formData.yearEnd" class="form-control" required />
                                    </div>
                                </div>
                                <div class="row mb-4">
                                    <div class="col-xs-6 form-group">
                                        <label for="engineName">Engine Name</label>
                                        <input type="text" id="engineName" v-model="formData.engineName" class="form-control" required />
                                    </div>
                                    <div class="col-xs-6 form-group">
                                        <label for="enginePowerKw">Engine Power (kW)</label>
                                        <input type="number" id="enginePowerKw" v-model="formData.enginePowerKw" class="form-control" required />
                                    </div>
                                </div>
                                <button type="submit" class="btn btn-primary mt-4">Submit</button>
                            </form>
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
    /**
state1 => brand new
state2 => new based on model
state3 => edit on existing one
*/
    export default {
        props: {
            state: Number, // 1, 2, or 3 to control modal behavior
            model: Array,
            carbrand: Array,
            typename: String,
            ecuList: Array
        },
        data() {
            return {
                triggerModal: false,

                formData: {
                    "carBrand": {
                        "id": 0
                    },
                    yearStart: 2020,
                    yearEnd: 2020,
                    engineName: '',
                    enginePowerKw: 0,
                    fuelVariant: '',
                    typeName: '',
                    specialInfo: '',
                    selectedEcu: { id: null },
                    availableSolutions: [],
                    checkableItems: [
                        { id: 'egr', label: 'egr', checked: false, showValues: false, value1: 0, value2: 0, replacementLines: [] },
                        { id: 'adblue', label: 'adblue', checked: false, showValues: false, value1: 0, value2: 0, replacementLines: [] },
                        { id: 'stage1', label: 'stage1', checked: false, showValues: true, value1: 0, value2: 0, textValue1: 'Enter increase in kW', textValue2: 'Enter increase in NM', replacementLines: [] },
                        { id: 'stage2', label: 'stage2', checked: false, showValues: true, value1: 0, value2: 0, textValue1: 'Enter increase in kW', textValue2: 'Enter increase in NM', replacementLines: [] },
                        { id: 'dtc', label: 'dtc', checked: false, showValues: false, value1: 0, value2: 0, replacementLines: [] },
                        { id: 'flaps', label: 'flaps', checked: false, showValues: false, value1: 0, value2: 0, replacementLines: [] },
                    ]
                },
            };
        },
        components: {
            VueFinalModal
        },
        computed: {
        },
        methods: {
            setYears(startYear, endYear) {
                this.model.year = `${startYear}-${endYear}`;
            },
            closeModal() {
                this.$emit("closemodal", false);
            },
            submitForm() {
                this.$emit("submit", this.formData);
            },
            updateAvailableSolutions() {
                // Logic for updating solutions based on checkable items
            },
            addReplacementLine(item) {
                item.replacementLines.push({ searchString: "", replacementString: "", number: 0 });
            },
            removeReplacementLine(item, index) {
                item.replacementLines.splice(index, 1);
            },
            async addNew() {
                // Construct the data according to the InputNewVariant model
                const inputNewVariant = {
                    CarBrand: {
                        id: this.preselectedModelId
                    },
                    TypeName: this.formData.typeName,
                    YearStart: this.formData.yearStart,
                    YearEnd: this.formData.yearEnd,
                    EngineName: this.formData.engineName,
                    EnginePowerKw: this.formData.enginePowerKw,
                    FuelVariant: this.formData.fuelVariant,
                    SpecialInfo: this.formData.specialInfo,
                    SelectedEcu: {
                        // Assuming SelectedEcu should contain id and other properties
                        id: this.formData.selectedEcu.id,
                        // Add other properties of input_SelectedEcu as needed
                    },
                    AvailableSolutions: this.formData.availableSolutions
                };

                try {
                    const apiUrl = import.meta.env.VITE_API_BASE_URL; // Define your API URL here
                    const response = await fetch(`${apiUrl}/api/Tuning/GenerateTuningVariant`, {
                        method: 'POST', // Specify the method
                        headers: {
                            'Content-Type': 'application/json', // Set the content type to JSON
                        },
                        credentials: 'include', // Include credentials such as cookies
                        body: JSON.stringify(inputNewVariant), // Send the constructed data as JSON
                    });

                    // Handle response
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    this.close_Modal();
                    this.close_Modal_Model();
                    this.fetchTuningData(this.preselectedModelId);
                    const result = await response.json();
                    console.log('Success:', result);
                    // Handle success (e.g., show a success message, close the modal, etc.)

                } catch (error) {
                    console.error('Error:', error);
                    // Handle error (e.g., show an error message to the user)
                }

            },
            async updateExistingOne() {
                // Construct the data according to the InputNewVariant model
                const inputNewVariant = {
                    CarBrand: {
                        id: this.preselectedModelId
                    },
                    TypeName: this.formData.typeName,
                    YearStart: this.formData.yearStart,
                    YearEnd: this.formData.yearEnd,
                    EngineName: this.formData.engineName,
                    EnginePowerKw: this.formData.enginePowerKw,
                    FuelVariant: this.formData.fuelVariant,
                    SpecialInfo: this.formData.specialInfo,
                    SelectedEcu: {
                        // Assuming SelectedEcu should contain id and other properties
                        id: this.formData.selectedEcu.id,
                        // Add other properties of input_SelectedEcu as needed
                    },
                    AvailableSolutions: this.formData.availableSolutions
                };

                try {
                    const apiUrl = import.meta.env.VITE_API_BASE_URL; // Define your API URL here
                    const response = await fetch(`${apiUrl}/api/Tuning/GenerateTuningVariant`, {
                        method: 'POST', // Specify the method
                        headers: {
                            'Content-Type': 'application/json', // Set the content type to JSON
                        },
                        credentials: 'include', // Include credentials such as cookies
                        body: JSON.stringify(inputNewVariant), // Send the constructed data as JSON
                    });

                    // Handle response
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    this.close_Modal();
                    this.close_Modal_Model();
                    this.fetchTuningData(this.preselectedModelId);
                    const result = await response.json();
                    console.log('Success:', result);
                    // Handle success (e.g., show a success message, close the modal, etc.)

                } catch (error) {
                    console.error('Error:', error);
                    // Handle error (e.g., show an error message to the user)
                }

            },
        },
        mounted() {
            if (this.state === 1 || this.state === 2) {
                this.formData = {
                    ...this.formData, // Preserve checkableItems structure
                    typeName: "",
                    yearStart: null,
                    yearEnd: null,
                    engineName: "",
                    enginePowerKw: null,
                    fuelVariant: "petrol",
                    selectedEcu: { id: "" },
                    specialInfo: "",
                    availableSolutions: [],
                };

                if (this.state === 2) {
                    this.formData.typeName = this.typename;
                    //this.formData.typeName= this.model.typeName;
                }
            } else if (this.state === 3 && this.model) {
                console.log(this.model);
                this.formData = {
                    ...this.formData, // Preserve checkableItems structure
                    typeName: this.model.typeName || "",
                    yearStart: parseInt(this.model.year.split("-")[0], 10) || null,
                    yearEnd: parseInt(this.model.year.split("-")[1], 10) || null,
                    engineName: this.model.engine || "",
                    enginePowerKw: parseInt(this.model.horsepower, 10) || null,
                    fuelVariant: this.model.variant || "petrol",
                    selectedEcu: { id: "" }, // Populate this based on your requirements
                    specialInfo: "",
                    availableSolutions: [],
                };


                //get selected ecu from backend
                //get replacementscripts and solutions from backend
            }

            this.formData.carBrand = this.carbrand.id;
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