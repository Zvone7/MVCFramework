import { userCookieName } from './utils/constants';
import { getUserCookieOrLogout } from './utils/utils-cookie';
import { JSONtryParse } from './utils/utils-general';
import { formLogin } from './components/form-login';
import { displayUserData } from './components/display-user-data';

Startup();

function Startup() {
    console.log("Welcome!\nCompile time:", new Date().toUTCString());
    var userCookie = getUserCookieOrLogout(userCookieName);
    if (userCookie) {
        console.log("Welcome", JSONtryParse(userCookie).name);
        if (window.location.toString().toLowerCase().includes("login".toLowerCase())) {
            console.log("redirecting home...");
            window.location.href = '/';
        }
    }
}

new Vue(
    {
        el: '#app',
        components: { 'form-login': formLogin, 'display-user-data': displayUserData },
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