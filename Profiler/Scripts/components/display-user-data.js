import { getUserDataCookieOrLogout, deleteCookie } from '../utils/utils-cookie';
import { JSONtryParse } from '../utils/utils-general'
import { userDataCookieName } from '../utils/constants';

export var displayUserData = Vue.component('display-user-data',
    {
        data: function () {
            return {
                Name: '',
                LastName: ''
                //isLogoutDisabled: true
            }
        },
        computed: {
            isLogoutDisabled() {
                let isDisabled = true;
                var userCookie = getUserDataCookieOrLogout(userDataCookieName);
                var userData = JSONtryParse(userCookie);
                this.$data.Name = userData.name;
                this.$data.LastName = userData.lastName;
                if (
                    this.$data.Name &&
                    this.$data.LastName !== ''
                ) {
                    isDisabled = false;
                }

                return isDisabled;
            }
        },
        methods: {
            Logout() {
                this.$parent.Name = '';
                this.$parent.LastName = '';
                deleteCookie(userDataCookieName);
                window.location.href = '/';

                axios({
                    method: 'get',
                    url: '/Login/LogOut'
                }).then(data => {
                    console.log("__Logged out: ", data.data);
                    this.$refs.LogoutButton.setAttribute("disabled", "disabled");
                }).catch(err => {
                    alert(`There was an error logging out. See details: ${err}`);
                });

            }
        },
        template: `<div>

                    <label>{{Name}}</label>

                    <br/>

                    <label>{{LastName}}</label>

                    <button 
                        type="button" 
                        ref="LogoutButton" 
                        v-bind:disabled="isLogoutDisabled" 
                        v-on:click="Logout">Logout
                    </button>

                    </div>`
    }
)