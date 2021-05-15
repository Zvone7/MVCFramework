import axios from 'axios';

export var formTest = Vue.component('form-test',
    {
        data: function () {
            return {
                Value1: '2',
                Value2: 'admin@mail.com'
            }
        },
        computed: {},
        methods: {
            CallGetUserById() {
                var context = this;
                axios({
                    method: 'post',
                    url: '/User/GetUserById?id=' + this.$data.Value1
                }).then(data => {
                    this.$notify('response:' + data.data);

                }).catch(err => {
                    alert(`There was an error registering. See details: ${err}`);
                });
            },
            CallAuthorizedEndpoint() {
                console.log("AuthorizedEndpoint");


                axios({
                    method: 'Get',
                    url: '/Test/AuthorizedEndpoint',
                    data: "test"

                }).then(data => {
                    console.log("tested: ", data.data);

                }).catch(err => {
                    console.log(`There was an error. See details: ${err}`);
                });
            },
            CallUnuthorizedEndpoint() {
                console.log("UnuthorizedEndpoint");


                axios({
                    method: 'Get',
                    url: '/Test/UnuthorizedEndpoint',
                    data: "test123"

                }).then(data => {
                    console.log("tested: ", data.data);

                }).catch(err => {
                    console.log(`There was an error. See details: ${err}`);
                });
            }
            , CallSetAdminRole() {
                console.log("SetAdminRole");


                axios({
                    method: 'Get',
                    url: '/Test/SetAdminRole'

                }).then(data => {
                    console.log("tested: ", data.data);

                }).catch(err => {
                    console.log(`There was an error. See details: ${err}`);
                });
            }
        },
        template: `
                <div>
                    <label><b>CallGetUserById(must be admin)</b></label>
                    <input type="text" placeholder="Enter value" v-model="Value1">
                    <button 
                        type="button" 
                        class="success" 
                        ref="Button1" 
                        v-on:click="CallGetUserById">CallGetUserById
                    </button>

                    <br/>
     
                    <button 
                        type="button" 
                        class="success" 
                        ref="ButtonUnathorizedEndpoint" 
                        v-on:click="CallUnuthorizedEndpoint">UnuthorizedEndpoint
                    </button>

                    <br/>

                    <button 
                        type="button" 
                        class="success" 
                        ref="ButtonSetAdminRole" 
                        v-on:click="CallSetAdminRole">SetAdminRole
                    </button>

                    <br/>

                    <button 
                        type="button" 
                        class="success" 
                        ref="ButtonAuthorizedEndpoint" 
                        v-on:click="CallAuthorizedEndpoint">AuthorizedEndpoint
                    </button>

                </div>`
    }
)