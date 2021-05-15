console.log("loaded shared.js");

$("#loginButton").click(function () {
    var email = getFieldValue("email");
    var password = getFieldValue("password");
    if (email != "" && password != "") {
        authenticate(email, password);
    }
    else {
        console.log("Input valid values.");
    }
});

$("#getUserById").click(function () {
    var userId = getFieldValue("userId");
    if (userId != "") {
        getUserById(userId);
    }
    else {
        console.log("Input valid values.");
    }
});


function getFieldValue(fieldId) {
    var field = $("#" + fieldId);
    if (field == null || field.length == 0) { return ""; }
    var value = field.val();
    if (value == undefined) { return ""; }
    else return value;
}

function getUserByEmail(email) {
    var serviceURL = '/api/User/GetUserByEmail';
    $.ajax({
        type: "GET",
        url: serviceURL + "?email=" + email,
        //data: null,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });
    function successFunc(data) {
        if (data != undefined) {
            console.log("Succesfully called.", data);
        }
        else {
            console.log("Succesfully called. No data.");
        }
    }
    function errorFunc(data) {
        if (data != undefined) {
            console.log("UNSuccesfully called.", data);
        }
        else {
            console.log("UNSuccesfully called. No data.");
        }
    }
}


function authenticate(email, password) {
    var userdata = { Email: email, Password: password }
    //var serviceURL = '/api/User/Authenticate';
    var serviceURL = '/api/User/LogIn';
    $.ajax({
        type: "POST",
        url: serviceURL,// + "?email=" + email + "&password=" + password,
        data: JSON.stringify(userdata),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });
    function successFunc(data) {
        if (data != undefined) {
            console.log("Succesfully called.", data);
        }
        else {
            console.log("Succesfully called. No data.");
        }
    }
    function errorFunc(data) {
        if (data != undefined) {
            console.log("UNSuccesfully called.", data);
        }
        else {
            console.log("UNSuccesfully called. No data.");
        }
    }
}

function getUserById(id) {
    var serviceURL = '/api/User/GetUserById';
    $.ajax({
        type: "GET",
        url: serviceURL + "?id=" + id,
        //data: null,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: successFunc,
        error: errorFunc
    });
    function successFunc(data) {
        if (data != undefined) {
            console.log("Succesfully called.", data);
        }
        else {
            console.log("Succesfully called. No data.");
        }
    }
    function errorFunc(data) {
        if (data != undefined) {
            console.log("UNSuccesfully called.", data);
        }
        else {
            console.log("UNSuccesfully called. No data.");
        }
    }
}