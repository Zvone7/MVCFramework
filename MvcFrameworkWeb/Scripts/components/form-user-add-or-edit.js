import { validateEmail } from '../../Scripts/utils/utils-general';
import axios from 'axios';

export var formUserAddOrEdit = Vue.component('form-user-add-or-edit',
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
        created: function () {
            var userData;
            var userData = this.getUserData();
            console.log("here:", userData);
            if (userData.id > 0) {
                this.Id = userData.id;
                this.Name = userData.name;
                this.LastName = userData.lastName;
                this.Email = userData.email;
                this.Password = '';
                this.PasswordAgain = '';
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
                console.log("Submitting", this.$data.Id);
                console.log("Submitting", this.$data.Email);
                console.log("Submitting", this.$data.Name);
                console.log("Submitting", this.$data.LastName);
                console.log("Submitting", this.$data.Password);
                var EndUser = {
                    Email: this.$data.Email,
                    Name: this.$data.Name,
                    LastName: this.$data.LastName,
                    Password: this.$data.Password
                }

                console.log("Submitting", EndUser);
                axios({
                    method: 'post',
                    url: '/User/AddOrUpdate',
                    data: EndUser

                }).then(data => {
                    console.log("__AddedOrUpdated: ", data.data);
                    this.$refs.SubmitButton.setAttribute("disabled", "disabled");

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
            },
            getUserData() {
                return null;
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