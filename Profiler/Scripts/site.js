import { validateEmail } from './utils';
import { formLogin } from './components/form-login'

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