import axios from 'axios';

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


export var processContentLoginLogin = function (context, data) {
    console.log("processContentLoginLogin data:", data);
    if (data == undefined || data.data == undefined) {
        context.$notify({
            type: 'error',
            text: 'Network error.'
        });
    }
    else {
        if (data.data.hasError) {
            var lastMessage = "";
            for (var i = 0; i < data.data.errors.length; i++) {
                lastMessage = data.data.errors[i].description;
            }
            context.$notify({
                type: 'error',
                text: lastMessage
            });
        }
        else {
            context.$notify({
                type: 'success',
                text: 'Logged in sucessfully. Redirecting...'
            });
            setTimeout(function () {
                window.location.href = '/';
            }, 1000);
        }
    }
}

export var processContentUserRegister = function (context, data) {
    console.log("processContentUserRegister data:", data);
    if (data == undefined || data.data == undefined) {
        context.$notify({
            type: 'error',
            text: 'Network error.'
        });
    }
    else {
        if (data.data.hasError) {
            var lastMessage = "";
            for (var i = 0; i < data.data.errors.length; i++) {
                lastMessage = data.data.errors[i].description;
            }
            context.$notify({
                type: 'error',
                text: lastMessage
            });
        }
        else {
            context.$notify({
                type: 'success',
                text: 'Registered sucesfully. Redirecting...'
            });
            setTimeout(function () {
                window.location.href = '/';
            }, 1000);
        }
    }
}

export var processContentUserMe = function (context, data) {
    console.log("processContentUserMe data:", data);
    if (data == undefined || data.data == undefined) {
        context.$notify({
            type: 'error',
            text: 'Network error.'
        });
    }
    else {
        if (data.data.hasErrors) {
            var lastMessage = "";
            for (var i = 0; i < data.data.errors.length; i++) {
                lastMessage = data.data.errors[i].description;
            }
            context.$notify({
                type: 'error',
                text: lastMessage
            });
        }
        else {
            context.Name = data.data.data.name;
            context.LastName = data.data.data.lastName;
            context.Email = '';
            context.EmailAgain = '';
            context.Password = '';
            context.PasswordAgain = '';
        }
    }
}

export var processContentUserUpdateUser = function (context, data) {
    console.log("processContentUserUpdateUser data:", data);
    if (data == undefined || data.data == undefined) {
        context.$notify({
            type: 'error',
            text: 'Network error.'
        });
    }
    else {
        if (data.data.hasError) {
            var lastMessage = "";
            for (var i = 0; i < data.data.errors.length; i++) {
                lastMessage = data.data.errors[i].description;
            }
            context.$notify({
                type: 'error',
                text: lastMessage
            });
        }
        else {
            context.$notify({
                type: 'success',
                text: 'Updated sucessfully'
            });
            context.Name = data.data.data.name;
            context.LastName = data.data.data.lastName;
            context.Email = '';
            context.EmailAgain = '';
            context.Password = '';
            context.PasswordAgain = '';
        }
    }
}

export var processContentUserUpdateUserEmail = function (context, data) {
    console.log("processContentUserUpdateEmail data:", data);
    if (data == undefined || data.data == undefined) {
        context.$notify({
            type: 'error',
            text: 'Network error.'
        });
    }
    else {
        if (data.data.hasError) {
            var lastMessage = "";
            for (var i = 0; i < data.data.errors.length; i++) {
                lastMessage = data.data.errors[i].description;
            }
            context.$notify({
                type: 'error',
                text: lastMessage
            });
        }
        else {
            context.$notify({
                type: 'success',
                text: 'Updated sucessfully'
            });
            context.Email = '';
            context.EmailAgain = '';
        }
    }
}

export var processContentUserUpdateUserPassword = function (context, data) {
    console.log("processContentUserUpdatePassword data:", data);
    if (data == undefined || data.data == undefined) {
        context.$notify({
            type: 'error',
            text: 'Network error.'
        });
    }
    else {
        if (data.data.hasError) {
            var lastMessage = "";
            for (var i = 0; i < data.data.errors.length; i++) {
                lastMessage = data.data.errors[i].description;
            }
            context.$notify({
                type: 'error',
                text: lastMessage
            });
        }
        else {
            context.$notify({
                type: 'success',
                text: 'Updated sucessfully'
            });
            context.Password = '';
            context.PasswordAgain = '';
        }
    }
}