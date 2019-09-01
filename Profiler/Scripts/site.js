import { userCookieName } from './utils/constants';
import { getUserCookieOrLogout } from './utils/utils-cookie';
import { JSONtryParse } from './utils/utils-general';

import { formLogin } from './components/form-login';
import { formRegister } from './components/form-register';
import { displayUserData } from './components/display-user-data';

Startup();

function Startup() {
    console.log("Welcome!\nCompile time:", new Date().toUTCString());
    var userCookie = getUserCookieOrLogout(userCookieName);
    if (userCookie) {
        console.log("Welcome", JSONtryParse(userCookie).name);
        if (window.location.href.toLowerCase().includes("login") ||
            window.location.href.toLowerCase().includes("register")) {
            console.log("redirecting home...");
            window.location.href = '/';
        }
    }
}

new Vue(
    {
        el: '#app',
        components: { 'form-login': formLogin, 'form-register': formRegister, 'display-user-data': displayUserData },
        data: {
            //Email: 'admin',
            //Password: 'admin',
        },
        computed: {},
        methods: {
            SubmitLoginForm() { },
            ResetForm() { },
            Logout() { }
        }
    }
);