import { validateEmail } from '../utils/utils-general';
import { processContentUserRegister } from '../utils/utils-requests'
import axios from 'axios';

export var formUserAdd = Vue.component('form-user-add',
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
        computed: {
            isSubmitDisabled() {
                let isDisabled = true;
                if (
                    this.Id !== undefined &&
                    this.Id !== 0 &&
                    this.Email !== '' &&
                    validateEmail(this.Email) &&
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
                    alert(`There was an error submitting user. See details: ${err}`);
                });
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
                        <input type="password" placeholder="Enter password" v-model="Password" autocomplete="new-password" required>

                        <label><b>Password again</b></label>
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