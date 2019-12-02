import axios from 'axios';

var storedUserData = "none";
let axiosInstance = axios.create({
    headers: {
        "Content-Type": "application/json",
        "Accept": "application/json"
    }
})

export var formUserEdit = Vue.component('form-user-edit',
    {
        data: function () {
            return {
                Id: '',
                Name: '',
                LastName: '',
                Email: '',
                Password: '',
                PasswordAgain: ''
            }
        },
        created: function () { },
        computed: {
            isSubmitNameButtonDisabled() {
                (this.getUserDataTest()).then(userData => {
                    let isDisabled = true;
                    if (this.Name !== '' &&
                        this.Name !== userData.name) {
                        isDisabled = false;
                    }
                    return isDisabled;
                });
            },
            isSubmitLastNameButtonDisabled() {
                (this.getUserDataTest()).then(userData => {
                    let isDisabled = true;
                    if (this.LastName !== '' &&
                        this.LastName !== userData.lastName) {
                        isDisabled = false;
                    }
                    return isDisabled;
                });
            },
            isSubmitEmailButtonDisabled() {
                (this.getUserDataTest()).then(userData => {
                    let isDisabled = true;
                    if (this.Email !== '' &&
                        this.Email !== userData.email) {
                        isDisabled = false;
                    }
                    return isDisabled;
                });
            },
            isSubmitPasswordButtonDisabled() {
                let isDisabled = true;
                if (
                    this.Password !== '' &&
                    this.PasswordAgain !== '' &&
                    this.Password === this.PasswordAgain
                ) {
                    isDisabled = false;
                }
                return isDisabled;
            },
        },
        methods: {
            SubmitName() {
                SubmitValue(this, "Name", this.$data.Name);
                this.$refs.SubmitNameButton.setAttribute("disabled", "disabled");
            },
            SubmitLastName() {
                SubmitValue(this,"Name", this.$data.LastName);
                this.$refs.SubmitLastNameButton.setAttribute("disabled", "disabled");
            },
            SubmitEmail() {
                SubmitValue(this,"Name", this.$data.Email);
                this.$refs.SubmitEmailButton.setAttribute("disabled", "disabled");
            },
            SubmitPassword() {
                SubmitValue(this,"Password", this.$data.Password);
                this.$refs.SubmitPasswordButton.setAttribute("disabled", "disabled");
            },
            async getUserDataTest() {
                if (storedUserData === "none") {
                    console.log("storedUserData is none, fetching data..");
                }
                else {
                    return storedUserData;
                }
                axiosInstance({
                    method: 'post',
                    url: '/User/Me'
                }).then(data => {
                    if (data.data != undefined && data.data.name !== undefined) {
                        var userData = data.data;

                        this.Name = userData.name;
                        this.LastName = userData.lastName;
                        this.Email = userData.email;
                        this.Password = '';
                        this.PasswordAgain = '';

                        var userData = {
                            name: data.data.name,
                            lastName: data.data.lastName,
                            email: data.data.email
                        }
                        storedUserData = userData;
                        return userData;
                    }
                    else {
                        this.$notify({
                            type: 'error',
                            text: 'Invalid user info.'
                        });
                    }

                }).catch(err => {
                    this.$notify({
                        type: 'error',
                        text: 'Unable to fetch user info.'
                    });
                });
            }
        },
        template: `<div>

                    <form>

                        <label><b>Change name</b></label>
                        <input type="text" placeholder="Enter name" v-model="Name" required>

                        <button 
                            type="button" 
                            class="success" 
                            ref="SubmitNameButton" 
                            v-bind:disabled="isSubmitNameButtonDisabled" 
                            v-on:click="SubmitName">Submit
                        </button>

                        <hr />

                        <label><b>Change last name</b></label>
                        <input type="text" placeholder="Enter last name" v-model="LastName" required>

                        <button 
                            type="button" 
                            class="success" 
                            ref="SubmitLastNameButton" 
                            v-bind:disabled="isSubmitLastNameButtonDisabled" 
                            v-on:click="SubmitLastName">Submit
                        </button>

                        <hr />

                        <label><b>Change email</b></label>
                        <input type="text" placeholder="Enter email" v-model="Email" autocomplete="email" required>

                        <button 
                            type="button" 
                            class="success" 
                            ref="SubmitEmailButton" 
                            v-bind:disabled="isSubmitEmailButtonDisabled" 
                            v-on:click="SubmitEmail">Submit
                        </button>

                        <hr />

                        <label><b>Change password</b></label>

                        <input type="password" placeholder="Enter password" v-model="Password" autocomplete="new-password" required>

                        <label><b>Password again</b></label>
                        <input type="password" placeholder="Enter password again" v-model="PasswordAgain" autocomplete="new-password" required>

                        <button 
                            type="button" 
                            class="success" 
                            ref="SubmitPasswordButton" 
                            v-bind:disabled="isSubmitPasswordButtonDisabled" 
                            v-on:click="SubmitPassword">Submit
                        </button>

                        <hr />

                    </form>

                    </div>`
    }
)
let SubmitValue = function (formUserEditContext, nameOfValue, valueOfValue) {
    axiosInstance({
        method: 'post',
        url: '/User/Change' + nameOfValue,
        data: { Data: valueOfValue }

    }).then(data => {
        console.log("__User_Change " + nameOfValue, data.data);
        if (data.data === true) {
            formUserEditContext.$notify({
                type: 'success',
                text: "Changed " + nameOfValue + " successfully."
            });
        }
        else {
            formUserEditContext.$notify({
                type: 'error',
                text: "Unable to update" + nameOfValue + "."
            });
        }
    }).catch(err => {
        formUserEditContext.$notify({
            type: 'error',
            text: "Fail." + err
        });
    });

}