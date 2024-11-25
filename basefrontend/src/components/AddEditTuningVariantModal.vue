<template>
    <Modal v-if="isOpen" :fullscreen="false" :clickOut="true" :style="{ marginLeft: state === 1 ? '10vw' : '5vw' }">
        <div class="modal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content" style="min-width: 80em; max-height: 60em;">
                    <div class="modal-header">
                        <div class="mb-4">
                            <img :src="getModelByGivenId(preselectedModelId).icon" class="brand-icon" style="width: 50px; height: auto;">
                        </div>
                        <div v-if="state === 2 || state === 3">
                            <h4 class="modal-title">{{ model }}</h4>
                            <h6 v-if="state === 2 || state === 3" class="modal-title">  </h6>
                        </div>
                        <h5 v-else class="modal-title">{{ carbrand }}</h5>
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
                                    <input type="text" id="typeName" v-model="formData.typeName" class="form-control" required>
                                </div>
                            </div>
                            <div class="row mb-4">
                                <div class="col-xs-6 form-group">
                                    <label for="yearStart">Year Start</label>
                                    <input type="number" id="yearStart" v-model="formData.yearStart" class="form-control" required>
                                </div>
                                <div class="col-xs-6 form-group">
                                    <label for="yearEnd">Year End</label>
                                    <input type="number" id="yearEnd" v-model="formData.yearEnd" class="form-control" required>
                                </div>
                            </div>
                            <div class="row mb-4">
                                <div class="col-xs-6 form-group">
                                    <label for="engineName">Engine Name</label>
                                    <input type="text" id="engineName" v-model="formData.engineName" class="form-control" required>
                                </div>
                                <div class="col-xs-6 form-group">
                                    <label for="enginePowerKw">Engine Power (kW)</label>
                                    <input type="number" id="enginePowerKw" v-model="formData.enginePowerKw" class="form-control" required>
                                </div>
                            </div>
                            <div class="row mb-8">
                                <div class="col-xs-4 form-group">
                                    <label for="fuelVariant">Fuel Variant</label>
                                    <select id="fuelVariant" v-model="formData.fuelVariant" class="form-control" required>
                                        <option value="petrol">Petrol</option>
                                        <option value="diesel">Diesel</option>
                                    </select>
                                </div>
                                <div class="col-xs-4 form-group">
                                    <label for="ecuSelect">Select ECU</label>
                                    <select id="ecuSelect" v-model="formData.selectedEcu.id" class="form-control" required>
                                        <option value="" disabled>Select ECU</option>
                                        <option v-for="ecu in ecuList" :key="ecu.id" :value="ecu.id">
                                            {{ ecu.ecuName }}
                                        </option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="specialInfo">Special Info</label>
                                <textarea id="specialInfo" v-model="formData.specialInfo" class="form-control" rows="5" placeholder="Enter any additional details here..."></textarea>
                            </div>

                            <h4 class="mt-4">Options</h4>
                            <div v-for="(item, index) in checkableItems" :key="index" class="form-check mb-2">
                                <input type="checkbox" :id="item.id" class="form-check-input" v-model="item.checked" @change="updateAvailableSolutions()">
                                <label :for="item.id" class="form-check-label">{{ item.label }}</label>
                                <!-- Dynamic content for options -->
                                <div v-if="item.checked">
                                    <div v-if="state === 3" class="mt-2 ml-4">
                                        <div v-for="(line, lineIndex) in item.replacementLines" :key="lineIndex" class="replacement-line mb-2">
                                            <label>Search String:</label>
                                            <input type="text" v-model="line.searchString" class="form-control mb-1" placeholder="Enter search string" />

                                            <label>Replacement String:</label>
                                            <input type="text" v-model="line.replacementString" class="form-control mb-1" placeholder="Enter replacement string" />

                                            <label>Threshold:</label>
                                            <input type="number" v-model="line.number" class="form-control mb-1" placeholder="Enter threshold (0-100)" />

                                            <!-- Remove line button -->
                                            <button type="button" class="btn btn-danger" @click="removeReplacementLine(item, lineIndex)">Remove Line</button>
                                        </div>

                                        <button type="button" class="btn btn-secondary mb-2" @click="addReplacementLine(item)">Add Replacement Line</button>
                                    </div>
                                    <div v-else-if="item.showValues" class="mt-2">
                                        <label>{{ item.textValue1 }}</label>
                                        <input type="number" v-model="item.value1" class="form-control" placeholder="Value 1" />
                                        <label class="mt-2">{{ item.textValue2 }}</label>
                                        <input type="number" v-model="item.value2" class="form-control" placeholder="Value 2" />
                                    </div>
                                </div>
                            </div>
                            <button type="submit" class="btn btn-primary mt-4">Submit</button>
                        </form>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" @click="closeModal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    </Modal>
</template>

<script>
    import { Modal } from 'vue-neat-modal'
    /**
state1 => brand new
state2 => new based on model
state3 => edit on existing one
*/
    export default {
        props: {
            isOpen: Boolean,
            state: Number, // 1, 2, or 3 to control modal behavior
            model: Array,
            carbrand: Array,
            typename: String,
            ecuList: Array,
            checkableItems: Array,
        },
        data() {
            return {
                formData: {
                    typeName: "",
                    yearStart: null,
                    yearEnd: null,
                    engineName: "",
                    enginePowerKw: null,
                    fuelVariant: "petrol",
                    selectedEcu: { id: "" },
                    specialInfo: "",
                    formData: {
                        "carBrand": {
                            "id": this.preselectedModelId
                        },
                        yearStart: 2020,
                        yearEnd: 2020,
                        engineName: '',
                        enginePowerKw: 0,
                        fuelVariant: '',
                        typeName: '',
                        specialInfo: '',
                        selectedEcu: { id: null },
                        availableSolutions: []
                    },
                },
            };
        },
        components: {
            Modal
        },
        methods: {
            closeModal() {
                this.$emit("update:isOpen", false);
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
    };
</script>
