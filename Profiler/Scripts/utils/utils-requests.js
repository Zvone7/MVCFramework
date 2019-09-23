export var requestLogOut = function () {
    axios({
        method: 'get',
        url: '/Login/LogOut'
    }).then(data => {
        console.log("__Logged out: ", data.data);
        window.location.href = '/';
    }).catch(err => {
        alert(`There was an error logging out. See details: ${err}`);
    });
}