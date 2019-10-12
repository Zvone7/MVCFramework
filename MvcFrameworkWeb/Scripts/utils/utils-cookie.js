import { userDataCookieName, userLoginCookieName } from '../../Scripts/utils/constants'

//readCookie/deleteCookie/createCookie thanks to
//https://www.sitepoint.com/how-to-deal-with-cookies-in-java

export const getCookie = function (name) {
    var nameEQ = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEQ) == 0) {
            var cookieData = c.substring(nameEQ.length, c.length).trim();
            return cookieData;
        }
    }
    return null;
}

export const deleteCookie = function (name, path, domain) {
    // If the cookie exists
    if (getCookie(name))
        createCookie(name, "", -1, path, domain);
}

export const createCookie = function (name, value, expires, path, domain) {
    var cookie = name + "=" + (value) + ";";

    if (expires) {
        // If it's a date
        if (expires instanceof Date) {
            // If it isn't a valid date
            if (isNaN(expires.getTime()))
                expires = new Date();
        }
        else
            expires = new Date(new Date().getTime() + 1 * 60 * 1000);

        cookie += "expires=" + expires.toString() + ";";
    }

    if (path)
        cookie += "path=" + path + ";";
    if (domain)
        cookie += "domain=" + domain + ";";

    document.cookie = cookie;
}

export const getUserDataCookieOrLogout = function () {
    var aspNetCookie = getCookie(userLoginCookieName);
    //console.log(userLoginCookieName,":", aspNetCookie);
    if (aspNetCookie === null || aspNetCookie === undefined || aspNetCookie.trim() === "") {
        console.log("Unable to get user login cookie.");
        if (!window.location.href.toLowerCase().includes("login") &&
            !window.location.href.toLowerCase().includes("register")) {
            console.log("redirecting to login..");
            window.location.href = '/login';
        }
    } else {
        var cookie = getCookie(userDataCookieName);
        if (cookie === null || cookie === undefined || cookie.trim() === "") {
            deleteCookie(userDataCookieName);
            if (!window.location.href.toLowerCase().includes("login") &&
                !window.location.href.toLowerCase().includes("register")) {
                console.log("redirecting to login..");
                window.location.href = '/login';
            }
        }
        else {
            return cookie;
        }
    }
}