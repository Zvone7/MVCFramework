
export var formTest = Vue.component('form-test',
    {
        data: function () {
            return {
                Value1: '2',
                Value2: 'admin@mail.com'
            }
        },
        computed: {
        },
        methods: {
            CallGetUserById() {
                axios({
                    method: 'post',
                    url: '/User/GetUserById?id=' + this.$data.Value1
                }).then(data => {
                    console.log("__CallGetUserById: ", data.data);
                }).catch(err => {
                    alert(`There was an error registering. See details: ${err}`);
                });
            },
            CallGetUserByEmail() {
                axios({
                    method: 'post',
                    url: '/User/GetUserByEmail?email=' + this.$data.Value2
                }).then(data => {
                    console.log("__CallGetUserById: ", data.data);
                }).catch(err => {
                    alert(`There was an error registering. See details: ${err}`);
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

                    <label><b>CallGetUserByEmail(must be authenticated)</b></label>
                    <input type="text" placeholder="Enter value" v-model="Value2">
                    <button 
                        type="button" 
                        class="success" 
                        ref="Button2" 
                        v-on:click="CallGetUserByEmail">CallGetUserByEmail
                    </button>

                </div>`
    }
)