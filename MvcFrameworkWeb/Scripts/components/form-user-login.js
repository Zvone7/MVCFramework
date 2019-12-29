import axios from 'axios';
import {processContentLoginLogin} from '../utils/utils-requests'

export var formUserLogin = Vue.component('form-user-login',
    {
        data: function () {
            return {
                Email: '',
                Password: ''
            }
        },
        computed: {
            isLoginDisabled() {
                let isDisabled = true;

                if (
                    this.Email !== '' &&
                    this.Password !== ''
                ) {
                    isDisabled = false;
                }

                return isDisabled;
            }
        },
        methods: {
            SubmitLoginForm() {
                axios({
                    method: 'post',
                    url: '/Login/LogIn',
                    data: {
                        Email: this.$data.Email,
                        Password: this.$data.Password
                    }
                }).then(data => {
                    processContentLoginLogin(this, data);
                }).catch(err => {
                    this.$notify({
                        type: 'error',
                        text: 'There was an error logging in.'
                    });
                    console.log(`There was an error logging in. See details: ${err}`);
                });
            },
            ToRegisterForm() {
                window.location.href = '/user/register';
            },
            ResetForm() {
                console.log("ResetForm called");
                this.Email = '';
                this.Password = '';
            }
        },
        template: `<div>

                    <form>
                    <label><b>Email</b></label>
                    <input type="text" placeholder="Enter email" v-model="Email" autocomplete="email" required>

                    <label><b>Password</b></label>
                    <input type="password" placeholder="Enter Password" v-model="Password" autocomplete="current-password" required>

                    <button 
                        type="button" 
                        class="success" 
                        ref="LoginButton" 
                        v-bind:disabled="isLoginDisabled" v-on:click="SubmitLoginForm">Login
                    </button>

                    <button 
                        type="button" 
                        class="info" 
                        ref="RegisterButton" 
                        v-on:click="ToRegisterForm">Register
                    </button>
                    </form>

                    </div>`
    }
)