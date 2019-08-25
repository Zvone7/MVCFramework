import { userCookieName } from './const';
import { readCookie } from './utils';
import { formLogin } from './components/form-login'

Startup();

function Startup() {
    console.log("Welcome!\nCompile time:", new Date().toUTCString());
    var userCookie = readCookie(userCookieName);
    //console.log(userCookie);
    if ((userCookie === null || userCookie === undefined)) {
        if (!window.location.toString().toLowerCase().includes("login".toLowerCase())) {
            //console.log("redirecting to login...");
            window.location.href = '/Login';
        }
    }
    else {
        console.log("Welcome", JSON.parse(userCookie).name);
        if (window.location.toString().toLowerCase().includes("login".toLowerCase())) {
            //console.log("redirecting home...");
            window.location.href = '/';
        }
    }
}

new Vue(
    {
        el: '#app',
        components: { 'login-form': formLogin },
        data: {
            UserName: '',
            Password: '',
            isDisabled: false
        },
        computed: {
            isLoginDisabled() { }
        },
        methods: {
            SubmitLoginForm() { },
            ResetForm() { }
        }
    }
);