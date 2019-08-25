import { createCookie, deleteCookie } from '../utils';
import { userCookieName } from '../const';

export var formLogin = Vue.component('form-login',
    {
        data: function () {
            return {
                UserName: 'admin',
                Password: 'admin',
                isDisabled: false
            }
        },
        computed: {
            isLoginDisabled() {
                let isDisabled = true;

                if (
                    this.UserName !== '' &&
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
                    url: '/User/LogIn',
                    data: {
                        UserName: this.$data.UserName,
                        Password: this.$data.Password
                    }
                }).then(data => {
                    console.log("_Logged in_: ", data.data);
                    this.$refs.LoginButton.setAttribute("disabled", "disabled");
                    // create cookie
                    if (data.data.username != undefined && data.data.username != "") {
                        var userData = data.data;
                        createCookie(userCookieName, JSON.stringify(userData), null, null,null)
                        window.location.href = '/';
                    }

                }).catch(err => {
                    alert(`There was an error logging in. See details: ${err}`);
                });
            },
            ResetForm() {
                this.UserName = '';
                this.Password = '';
            }
        },
        template: '<div><label><b>Username</b></label><input type="text" placeholder="Enter UserName" v-model="UserName" required><label><b>Password</b></label><input type="password" placeholder="Enter Password" v-model="Password" required><button type="button" class="success" ref="LoginButton" v-bind:disabled="isLoginDisabled" v-on:click="SubmitLoginForm">Login</button></div>'
    }
)