export const validateEmail = function (context, email) {
    const re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    if (!re.test(String(email).toLowerCase())) {
        context.$notify({
            type: 'error',
            text: "Email format not valid."
        });
    }
    else return true;
};

export const JSONtryParse = function (obj) {
    try {
        return JSON.parse(obj);
    }
    catch (e) {
        console.log("Exception parsing JSON:", obj)
        return {};
    }
}

export const validatePasswordComplexity = function (context, password) {
    var hasUpperCase = /[A-Z]/.test(password);
    var hasLowerCase = /[a-z]/.test(password);
    var hasNumbers = /\d/.test(password);

    if (password.length < 8)
        context.$notify({
            type: 'error',
            text: "Password should contain at least 8 characters."
        });
    else if (!hasUpperCase)
        context.$notify({
            type: 'error',
            text: "Password should contain at least one upper-case letter."
        });
    else if (!hasLowerCase)
        context.$notify({
            type: 'error',
            text: "Password should contain at least one lower-case letter."
        });
    else if (!hasNumbers)
        context.$notify({
            type: 'error',
            text: "Password should contain at least one number."
        });
    else
        return true;
    return false;
}