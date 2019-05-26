console.log("loaded shared.js");

$("#loginButton").click(function () {
    var username = getFieldValue("username");
    var password = getFieldValue("password");
    if (username != "" && password != "") {
        getUserByUsername(username);
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

function getUserByUsername(username) {
    var serviceURL = '/api/User/GetUserByUsername';
    $.ajax({
        type: "GET",
        url: serviceURL + "?username="+username,
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