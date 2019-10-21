import { requestLogOut } from './utils/utils-requests'

import { formUserLogin } from './components/form-user-login';
import { formUserAddOrEdit } from './components/form-user-add-or-edit';
import { formTest } from './components/form-test';
import axios from 'axios';

Startup();

function Startup() {
    console.log("Welcome!\nCompile time:", new Date().toUTCString());
}

$(document).on("click", "#button-logout", function () {
    requestLogOut();
})

new Vue(
    {
        el: '#app',
        components: {
            'form-test': formTest,
            'form-user-login': formUserLogin,
            'form-user-add-or-edit': formUserAddOrEdit
        },
        data: {},
        computed: {},
        methods: {}
    }
);