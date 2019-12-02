import { requestLogOut } from './utils/utils-requests'

import { formUserLogin } from './components/form-user-login';
import { formUserAdd } from './components/form-user-add';
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
            'form-user-add': formUserAdd,
            'form-user-edit': formUserEdit
        },
        data: {},
        computed: {},
        methods: {}
    }
);