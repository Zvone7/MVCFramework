import { validateEmail } from '../../Scripts/utils/utils-general';

export var formUserRegister = Vue.component('form-user-register',
    {
        data: function () {
            return {
                Id:'0',
                Name: '',
                LastName: '',
                Email: '',
                Password: '',
                PasswordAgain: ''
            }
        },
        computed: {
            isRegisterDisabled() {
                let isDisabled = true;

                if (
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
            SubmitRegisterForm() {
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
                    console.log("__Registered: ", data.data);
                    this.$refs.RegisterButton.setAttribute("disabled", "disabled");
                    //// create cookie
                    //if (data.data.email != undefined && data.data.password != "") {
                    //    var userData = data.data;
                    //    createCookie(userCookieName, JSON.stringify(userData), new Date(new Date().getTime() + 10 * 60 * 1000), "/", null);
                    //    //todo cookies not expiring/being deleted when expired..handle it.

                    //    window.location.href = '/';
                    //}

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
        template: '<div><label><b>Name</b></label><input type="text" placeholder="Enter name" v-model="Name" required><label><b>Last Name</b></label><input type="text" placeholder="Enter last name" v-model="LastName" required><label><b>Email</b></label><input type="text" placeholder="Enter email" v-model="Email" required><label><b>Password</b></label><input type="password" placeholder="Enter password" v-model="Password" required><label><b>Password again</b></label><input type="password" placeholder="Enter password again" v-model="PasswordAgain" required><button type="button" class="success" ref="RegisterButton" v-bind:disabled="isRegisterDisabled" v-on:click="SubmitRegisterForm">Register</button></div>'
    }
)