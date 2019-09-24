export var formUserLogin = Vue.component('form-user-login',
    {
        data: function () {
            return {
                Email: 'admin@mail.com',
                Password: 'admin'
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
                    console.log("__Logged in: ", data.data);
                    this.$refs.LoginButton.setAttribute("disabled", "disabled");
                    // create cookie
                    if (data.data.email != undefined && data.data.password != "") {
                        window.location.href = '/';
                    }
                }).catch(err => {
                    alert(`There was an error logging in. See details: ${err}`);
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

                    <label><b>Email</b></label>
                    <input type="text" placeholder="Enter email" v-model="Email" required>

                    <label><b>Password</b></label>
                    <input type="password" placeholder="Enter Password" v-model="Password" required>

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

                    </div>`
    }
)