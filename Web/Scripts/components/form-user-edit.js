import axios from 'axios';
import { validateEmail, validatePasswordComplexity } from '../utils/utils-general';
import {
    processContentUserMe,
    processContentUserUpdateUser,
    processContentUserUpdateUserEmail,
    processContentUserUpdateUserPassword
} from '../utils/utils-requests'

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
                Name: '',
                LastName: '',
                Email: '',
                EmailAgain: '',
                Password: '',
                PasswordAgain: ''
            }
        },
        created: function () { },
        computed: {
            isUpdateUserButtonDisabled() {
                (this.getUserData()).then(userData => {
                    let isDisabled = true;
                    if (this.Name !== '' &&
                        this.Name !== userData.name &&
                        this.LastName !== '' &&
                        this.LastName !== userData.lastName) {
                        isDisabled = false;
                    }
                    return isDisabled;
                });
            },
            isUpdateEmailButtonDisabled() {
                let isDisabled = true;
                if (
                    this.Email !== '' &&
                    this.EmailAgain !== '' &&
                    this.Email === this.EmailAgain 
                ) {
                    isDisabled = false;
                }
                return isDisabled;
            },
            isUpdatePasswordButtonDisabled() {
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
            // update user ---------------------------
            UpdateUser() {
                axiosInstance({
                    method: 'post',
                    url: '/User/UpdateUser',
                    data: {
                        LastName: this.$data.LastName,
                        Name: this.$data.Name
                    }
                }).then(data => {
                    processContentUserUpdateUser(this, data);
                }).catch(err => {
                    this.$notify({
                        type: 'error',
                        text: "Fail." + err
                    });
                });
                this.$refs.UpdateUserButton.setAttribute("disabled", "disabled");
            },
            // update email ------------------------------------------------------
            UpdateEmail() {
                if (validateEmail(this, this.$data.Email)) {
                    axiosInstance({
                        method: 'post',
                        url: '/User/UpdateUserEmail',
                        data: { Email: this.$data.Email }
                    }).then(data => {
                        processContentUserUpdateUserEmail(this, data);
                    }).catch(err => {
                        this.$notify({
                            type: 'error',
                            text: "Fail." + err
                        });
                    });
                    this.$refs.UpdateEmailButton.setAttribute("disabled", "disabled");
                }
            },
            // update password ---------------------------------------------------------------------------------
            UpdatePassword() {
                if (validatePasswordComplexity(this, this.$data.Password)) {
                    axiosInstance({
                        method: 'post',
                        url: '/User/UpdateUserPassword',
                        data: { Password: this.$data.Password }
                    }).then(data => {
                        processContentUserUpdateUserPassword(this, data);
                    }).catch(err => {
                        this.$notify({
                            type: 'error',
                            text: "Fail." + err
                        });
                    });
                    this.$refs.UpdatePasswordButton.setAttribute("disabled", "disabled");
                }
            },
            // ------------------------------------------------------------------------------------------------------------
            async getUserData() {
                axiosInstance({
                    method: 'post',
                    url: '/User/Me'
                }).then(data => {
                    processContentUserMe(this, data);
                }).catch(err => {
                    console.log("error on getUserData()", err);
                });
            }
        },
        template: `<div>

                    <form>

                        <label><b>Change user data</b></label>
                        <br>

                        <input type="text" placeholder="Enter name" v-model="Name" required>
                        <input type="text" placeholder="Enter last name" v-model="LastName" required>

                        <button 
                            type="button" 
                            class="success" 
                            ref="SubmitLastNameButton" 
                            v-bind:disabled="isUpdateUserButtonDisabled" 
                            v-on:click="UpdateUser">Update
                        </button>

                        <hr />

                        <label><b>Change email</b></label>
                        <br>

                        <input type="text" placeholder="Enter email" v-model="Email" autocomplete="email" required>
                        <input type="text" placeholder="Enter email again" v-model="EmailAgain" autocomplete="new-email" required>

                        <button 
                            type="button" 
                            class="success" 
                            ref="UpdateEmailButton" 
                            v-bind:disabled="isUpdateEmailButtonDisabled" 
                            v-on:click="UpdateEmail">Update
                        </button>

                        <hr />

                        <label><b>Change password</b></label>
                        <br>
                        <i>
                            <label>password should contain</label>
                            <ul>
                              <li>at least 8 characters</li>
                              <li>at least one lower-case letter</li>
                              <li>at least one upper-case letter</li>
                              <li>at least one number</li>
                            </ul>
                        </i>
                        <input type="password" placeholder="Enter password" v-model="Password" autocomplete="new-password" required>
                        <input type="password" placeholder="Enter password again" v-model="PasswordAgain" autocomplete="new-password" required>

                        <button 
                            type="button" 
                            class="success" 
                            ref="UpdatePasswordButton" 
                            v-bind:disabled="isUpdatePasswordButtonDisabled" 
                            v-on:click="UpdatePassword">Update
                        </button>

                        <hr />

                    </form>

                    </div>`
    }
)