import { getUserCookieOrLogout, deleteCookie } from '../utils/utils-cookie';
import { JSONtryParse } from '../utils/utils-general'
import { userCookieName } from '../utils/constants';

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
                var userCookie = getUserCookieOrLogout(userCookieName);
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
                deleteCookie(userCookieName);
                this.$parent.Name = '';
                this.$parent.LastName = '';
                window.location.href = '/';
            }
        },
        template: '<div><label>{{Name}}</label><br/><label>{{LastName}}</label><button type="button" ref="LogoutButton" v-bind:disabled="isLogoutDisabled" v-on:click="Logout">Logout</button></div>'
    }
)