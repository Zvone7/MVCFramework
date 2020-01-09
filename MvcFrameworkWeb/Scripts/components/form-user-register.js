import { validateEmail, validatePasswordComplexity } from '../utils/utils-general';
import { processContentUserRegister } from '../utils/utils-requests'
import axios from 'axios';

export var formUserRegister = Vue.component('form-user-register',
    {
        data: function () {
            return {
                Name: '',
                LastName: '',
                Email: '',
                Password: '',
                PasswordAgain: ''
            }
        },
        computed: {
            isSubmitDisabled() {
                let isDisabled = true;
                if (
                    this.Email !== '' &&
                    this.Name !== '' &&
                    this.LastName !== '' &&
                    this.PasswordAgain !== '' &&
                    this.Password !== '' &&
                    this.Password === this.PasswordAgain
                ) {
                    isDisabled = false;
                }
                return isDisabled;
            }
        },
        methods: {
            SubmitForm() {
                if (validateEmail(this, this.$data.Email) &&
                    validatePasswordComplexity(this, this.$data.Password)) {

                    var EndUser = {
                        Email: this.$data.Email,
                        Name: this.$data.Name,
                        LastName: this.$data.LastName,
                        Password: this.$data.Password
                    }

                    axios({
                        method: 'post',
                        url: '/User/RegisterSelf',
                        data: EndUser

                    }).then(data => {
                        processContentUserRegister(this, data);

                    }).catch(err => {
                        this.$notify({
                            type: 'error',
                            text: "Fail." + err
                        });
                        console.log(`There was an error registering user. See details: ${err}`);
                    });
                }
            },
            ResetForm() {
                console.log("ResetForm called");
                this.Email = '';
                this.Name = '';
                this.LastName = '';
                this.Password = '';
            }
        },
        template: `<div>

                    <form>

                        <label><b>Name</b></label>
                        <input type="text" placeholder="Enter name" v-model="Name" required>

                        <label><b>Last Name</b></label>
                        <input type="text" placeholder="Enter last name" v-model="LastName" required>

                        <label><b>Email</b></label>
                        <input type="text" placeholder="Enter email" v-model="Email" autocomplete="email" required>

                        <label><b>Password</b></label>
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
                            ref="RegisterButton" 
                            v-bind:disabled="isSubmitDisabled" 
                            v-on:click="SubmitForm">Submit
                        </button>

                    </form>

                    </div>`
    }
)