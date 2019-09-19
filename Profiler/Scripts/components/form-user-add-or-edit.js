import { validateEmail, JSONtryParse } from '../../Scripts/utils/utils-general';
import { getUserDataCookieOrLogout } from '../../Scripts/utils/utils-cookie';

export var formUserEdit = Vue.component('form-user-add-or-edit',
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
            isUpdateDisabled() {
                let isDisabled = true;
                var userCookie = getUserDataCookieOrLogout();
                var userData = JSONtryParse(userCookie);
                this.Id = userData.id;
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
            SubmitEditForm() {
                axios({
                    method: 'post',
                    url: '/User/AddOrUpdate',
                    data: {
                        Id: this.$data.Id,
                        Email: this.$data.Email,
                        Name: this.$data.Name,
                        LastName: this.$data.LastName,
                        Password: this.$data.Password
                    }
                }).then(data => {
                    console.log("__Updated: ", data.data);
                    this.$refs.RegisterButton.setAttribute("disabled", "disabled");

                }).catch(err => {
                    alert(`There was an error registering. See details: ${err}`);
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

                    <label><b>Name</b></label>
                    <input type="text" placeholder="Enter name" v-model="Name" required>

                    <label><b>Last Name</b></label>
                    <input type="text" placeholder="Enter last name" v-model="LastName" required>

                    <label><b>Email</b></label>
                    <input type="text" placeholder="Enter email" v-model="Email" required>

                    <label><b>Password</b></label>
                    <input type="password" placeholder="Enter password" v-model="Password" required>

                    <label><b>Password again</b></label>
                    <input type="password" placeholder="Enter password again" v-model="PasswordAgain" required>

                    <button 
                        type="button" 
                        class="success" 
                        ref="RegisterButton" 
                        v-bind:disabled="isRegisterDisabled" 
                        v-on:click="SubmitRegisterForm">Register
                    </button>

                    </div>`
    }
)