import { requestLogOut } from './utils/utils-requests'

import { formUserLogin } from './components/form-user-login';
import { formUserRegister } from './components/form-user-register';
import { formUserEdit } from './components/form-user-edit';
import { formTest } from './components/form-test';
import Notification from 'vue-notification';

Startup();

function Startup() {
}

$(document).on("click", "#button-logout", function () {
    requestLogOut();
})

Vue.use(Notification);
new Vue(
    {
        el: '#app',
        components: {
            'form-test': formTest,
            'form-user-login': formUserLogin,
            'form-user-register': formUserRegister,
            'form-user-edit': formUserEdit
        },
        data: {},
        computed: {},
        methods: {}
    }
);