import { userCookieName } from './utils/constants';
import { getUserCookieOrLogout } from './utils/utils-cookie';
import { JSONtryParse } from './utils/utils-general';

import { formUserLogin } from './components/form-user-login';
import { formUserRegister } from './components/form-user-register';
import { displayUserData } from './components/display-user-data';
import { formTest } from './components/form-test';

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
        components: {
            'form-test': formTest,
            'form-user-login': formUserLogin,
            'form-user-register': formUserRegister,
            'display-user-data': displayUserData
        },
        data: {},
        computed: {},
        methods: {}
    }
);