/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};
/******/
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/
/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId]) {
/******/ 			return installedModules[moduleId].exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			i: moduleId,
/******/ 			l: false,
/******/ 			exports: {}
/******/ 		};
/******/
/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/
/******/ 		// Flag the module as loaded
/******/ 		module.l = true;
/******/
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/
/******/
/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;
/******/
/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;
/******/
/******/ 	// define getter function for harmony exports
/******/ 	__webpack_require__.d = function(exports, name, getter) {
/******/ 		if(!__webpack_require__.o(exports, name)) {
/******/ 			Object.defineProperty(exports, name, { enumerable: true, get: getter });
/******/ 		}
/******/ 	};
/******/
/******/ 	// define __esModule on exports
/******/ 	__webpack_require__.r = function(exports) {
/******/ 		if(typeof Symbol !== 'undefined' && Symbol.toStringTag) {
/******/ 			Object.defineProperty(exports, Symbol.toStringTag, { value: 'Module' });
/******/ 		}
/******/ 		Object.defineProperty(exports, '__esModule', { value: true });
/******/ 	};
/******/
/******/ 	// create a fake namespace object
/******/ 	// mode & 1: value is a module id, require it
/******/ 	// mode & 2: merge all properties of value into the ns
/******/ 	// mode & 4: return value when already ns object
/******/ 	// mode & 8|1: behave like require
/******/ 	__webpack_require__.t = function(value, mode) {
/******/ 		if(mode & 1) value = __webpack_require__(value);
/******/ 		if(mode & 8) return value;
/******/ 		if((mode & 4) && typeof value === 'object' && value && value.__esModule) return value;
/******/ 		var ns = Object.create(null);
/******/ 		__webpack_require__.r(ns);
/******/ 		Object.defineProperty(ns, 'default', { enumerable: true, value: value });
/******/ 		if(mode & 2 && typeof value != 'string') for(var key in value) __webpack_require__.d(ns, key, function(key) { return value[key]; }.bind(null, key));
/******/ 		return ns;
/******/ 	};
/******/
/******/ 	// getDefaultExport function for compatibility with non-harmony modules
/******/ 	__webpack_require__.n = function(module) {
/******/ 		var getter = module && module.__esModule ?
/******/ 			function getDefault() { return module['default']; } :
/******/ 			function getModuleExports() { return module; };
/******/ 		__webpack_require__.d(getter, 'a', getter);
/******/ 		return getter;
/******/ 	};
/******/
/******/ 	// Object.prototype.hasOwnProperty.call
/******/ 	__webpack_require__.o = function(object, property) { return Object.prototype.hasOwnProperty.call(object, property); };
/******/
/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";
/******/
/******/
/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(__webpack_require__.s = 0);
/******/ })
/************************************************************************/
/******/ ({

/***/ "./Scripts/components/form-test.js":
/*!*****************************************!*\
  !*** ./Scripts/components/form-test.js ***!
  \*****************************************/
/*! exports provided: formTest */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"formTest\", function() { return formTest; });\n\r\nvar formTest = Vue.component('form-test',\r\n    {\r\n        data: function () {\r\n            return {\r\n                Value1: '2',\r\n                Value2: 'admin@mail.com'\r\n            }\r\n        },\r\n        computed: {\r\n        },\r\n        methods: {\r\n            CallGetUserById() {\r\n                axios({\r\n                    method: 'post',\r\n                    url: '/User/GetUserById?id=' + this.$data.Value1\r\n                }).then(data => {\r\n                    console.log(\"__CallGetUserById: \", data.data);\r\n                }).catch(err => {\r\n                    alert(`There was an error registering. See details: ${err}`);\r\n                });\r\n            },\r\n            CallGetUserByEmail() {\r\n                axios({\r\n                    method: 'post',\r\n                    url: '/User/GetUserByEmail?email=' + this.$data.Value2\r\n                }).then(data => {\r\n                    console.log(\"__CallGetUserById: \", data.data);\r\n                }).catch(err => {\r\n                    alert(`There was an error registering. See details: ${err}`);\r\n                });\r\n            }\r\n        },\r\n        template: `\r\n                <div>\r\n\r\n                    <label><b>CallGetUserById(must be admin)</b></label>\r\n                    <input type=\"text\" placeholder=\"Enter value\" v-model=\"Value1\">\r\n                    <button \r\n                        type=\"button\" \r\n                        class=\"success\" \r\n                        ref=\"Button1\" \r\n                        v-on:click=\"CallGetUserById\">CallGetUserById\r\n                    </button>\r\n\r\n                    <br/>\r\n\r\n                    <label><b>CallGetUserByEmail(must be authenticated)</b></label>\r\n                    <input type=\"text\" placeholder=\"Enter value\" v-model=\"Value2\">\r\n                    <button \r\n                        type=\"button\" \r\n                        class=\"success\" \r\n                        ref=\"Button2\" \r\n                        v-on:click=\"CallGetUserByEmail\">CallGetUserByEmail\r\n                    </button>\r\n\r\n                </div>`\r\n    }\r\n)\n\n//# sourceURL=webpack:///./Scripts/components/form-test.js?");

/***/ }),

/***/ "./Scripts/components/form-user-add-or-edit.js":
/*!*****************************************************!*\
  !*** ./Scripts/components/form-user-add-or-edit.js ***!
  \*****************************************************/
/*! exports provided: formUserAddOrEdit */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"formUserAddOrEdit\", function() { return formUserAddOrEdit; });\n/* harmony import */ var _Scripts_utils_utils_general__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../Scripts/utils/utils-general */ \"./Scripts/utils/utils-general.js\");\n/* harmony import */ var _Scripts_controls_controls_user__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../Scripts/controls/controls-user */ \"./Scripts/controls/controls-user.js\");\n\r\n\r\n\r\nvar formUserAddOrEdit = Vue.component('form-user-add-or-edit',\r\n    {\r\n        data: function () {\r\n            return {\r\n                Id: '',\r\n                Name: '',\r\n                LastName: '',\r\n                Email: '',\r\n                Password: '',\r\n                PasswordAgain: ''\r\n            }\r\n        },\r\n        created: function () {\r\n            var userData;\r\n            var userData = this.getUserData();\r\n            console.log(\"here:\", userData);\r\n            if (userData.id > 0) {\r\n                this.Id = userData.id;\r\n                this.Name = userData.name;\r\n                this.LastName = userData.lastName;\r\n                this.Email = userData.email;\r\n                this.Password = '';\r\n                this.PasswordAgain = '';\r\n            }\r\n        },\r\n        computed: {\r\n            isSubmitDisabled() {\r\n                let isDisabled = true;\r\n                if (\r\n                    this.Id !== undefined &&\r\n                    this.Id !== 0 &&\r\n                    this.Email !== '' &&\r\n                    Object(_Scripts_utils_utils_general__WEBPACK_IMPORTED_MODULE_0__[\"validateEmail\"])(this.Email) &&\r\n                    this.Name !== '' &&\r\n                    this.LastName !== '' &&\r\n                    this.PasswordAgain !== '' &&\r\n                    this.Password !== '' &&\r\n                    this.Password === this.PasswordAgain\r\n                ) {\r\n                    isDisabled = false;\r\n                }\r\n                return isDisabled;\r\n            }\r\n        },\r\n        methods: {\r\n            SubmitForm() {\r\n                console.log(\"Submitting\", this.$data.Id);\r\n                console.log(\"Submitting\", this.$data.Email);\r\n                console.log(\"Submitting\", this.$data.Name);\r\n                console.log(\"Submitting\", this.$data.LastName);\r\n                console.log(\"Submitting\", this.$data.Password);\r\n                var EndUser = {\r\n                    Email: this.$data.Email,\r\n                    Name: this.$data.Name,\r\n                    LastName: this.$data.LastName,\r\n                    Password: this.$data.Password\r\n                }\r\n\r\n                console.log(\"Submitting\", EndUser);\r\n                axios({\r\n                    method: 'post',\r\n                    url: '/User/AddOrUpdate',\r\n                    data: EndUser\r\n\r\n                }).then(data => {\r\n                    console.log(\"__AddedOrUpdated: \", data.data);\r\n                    this.$refs.SubmitButton.setAttribute(\"disabled\", \"disabled\");\r\n\r\n                }).catch(err => {\r\n                    alert(`There was an error submitting user. See details: ${err}`);\r\n                });\r\n            },\r\n            ResetForm() {\r\n                console.log(\"ResetForm called\");\r\n                this.Email = '';\r\n                this.Name = '';\r\n                this.LastName = '';\r\n                this.Password = '';\r\n            },\r\n            getUserData() {\r\n                return null;\r\n            }\r\n        },\r\n        template: `<div>\r\n\r\n                    <label><b>Name</b></label>\r\n                    <input type=\"text\" placeholder=\"Enter name\" v-model=\"Name\" required>\r\n\r\n                    <label><b>Last Name</b></label>\r\n                    <input type=\"text\" placeholder=\"Enter last name\" v-model=\"LastName\" required>\r\n\r\n                    <label><b>Email</b></label>\r\n                    <input type=\"text\" placeholder=\"Enter email\" v-model=\"Email\" required>\r\n\r\n                    <label><b>Password</b></label>\r\n                    <input type=\"password\" placeholder=\"Enter password\" v-model=\"Password\" required>\r\n\r\n                    <label><b>Password again</b></label>\r\n                    <input type=\"password\" placeholder=\"Enter password again\" v-model=\"PasswordAgain\" required>\r\n\r\n                    <button \r\n                        type=\"button\" \r\n                        class=\"success\" \r\n                        ref=\"RegisterButton\" \r\n                        v-bind:disabled=\"isSubmitDisabled\" \r\n                        v-on:click=\"SubmitForm\">Submit\r\n                    </button>\r\n\r\n                    </div>`\r\n    }\r\n)\n\n//# sourceURL=webpack:///./Scripts/components/form-user-add-or-edit.js?");

/***/ }),

/***/ "./Scripts/components/form-user-login.js":
/*!***********************************************!*\
  !*** ./Scripts/components/form-user-login.js ***!
  \***********************************************/
/*! exports provided: formUserLogin */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"formUserLogin\", function() { return formUserLogin; });\nvar formUserLogin = Vue.component('form-user-login',\r\n    {\r\n        data: function () {\r\n            return {\r\n                Email: 'admin@mail.com',\r\n                Password: 'admin'\r\n            }\r\n        },\r\n        computed: {\r\n            isLoginDisabled() {\r\n                let isDisabled = true;\r\n\r\n                if (\r\n                    this.Email !== '' &&\r\n                    this.Password !== ''\r\n                ) {\r\n                    isDisabled = false;\r\n                }\r\n\r\n                return isDisabled;\r\n            }\r\n        },\r\n        methods: {\r\n            SubmitLoginForm() {\r\n                axios({\r\n                    method: 'post',\r\n                    url: '/Login/LogIn',\r\n                    data: {\r\n                        Email: this.$data.Email,\r\n                        Password: this.$data.Password\r\n                    }\r\n                }).then(data => {\r\n                    console.log(\"__Logged in: \", data.data);\r\n                    this.$refs.LoginButton.setAttribute(\"disabled\", \"disabled\");\r\n                    // create cookie\r\n                    if (data.data.email != undefined && data.data.password != \"\") {\r\n                        window.location.href = '/';\r\n                    }\r\n                }).catch(err => {\r\n                    alert(`There was an error logging in. See details: ${err}`);\r\n                });\r\n            },\r\n            ToRegisterForm() {\r\n                window.location.href = '/user/register';\r\n            },\r\n            ResetForm() {\r\n                console.log(\"ResetForm called\");\r\n                this.Email = '';\r\n                this.Password = '';\r\n            }\r\n        },\r\n        template: `<div>\r\n\r\n                    <label><b>Email</b></label>\r\n                    <input type=\"text\" placeholder=\"Enter email\" v-model=\"Email\" required>\r\n\r\n                    <label><b>Password</b></label>\r\n                    <input type=\"password\" placeholder=\"Enter Password\" v-model=\"Password\" required>\r\n\r\n                    <button \r\n                        type=\"button\" \r\n                        class=\"success\" \r\n                        ref=\"LoginButton\" \r\n                        v-bind:disabled=\"isLoginDisabled\" v-on:click=\"SubmitLoginForm\">Login\r\n                    </button>\r\n\r\n                    <button \r\n                        type=\"button\" \r\n                        class=\"info\" \r\n                        ref=\"RegisterButton\" \r\n                        v-on:click=\"ToRegisterForm\">Register\r\n                    </button>\r\n\r\n                    </div>`\r\n    }\r\n)\n\n//# sourceURL=webpack:///./Scripts/components/form-user-login.js?");

/***/ }),

/***/ "./Scripts/controls/controls-user.js":
/*!*******************************************!*\
  !*** ./Scripts/controls/controls-user.js ***!
  \*******************************************/
/*! exports provided: getUserData */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"getUserData\", function() { return getUserData; });\nvar getUserData = function () {\r\n    var data;\r\n    \r\n    return data;\r\n}\r\n\n\n//# sourceURL=webpack:///./Scripts/controls/controls-user.js?");

/***/ }),

/***/ "./Scripts/site.js":
/*!*************************!*\
  !*** ./Scripts/site.js ***!
  \*************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var _utils_utils_requests__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./utils/utils-requests */ \"./Scripts/utils/utils-requests.js\");\n/* harmony import */ var _components_form_user_login__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./components/form-user-login */ \"./Scripts/components/form-user-login.js\");\n/* harmony import */ var _components_form_user_add_or_edit__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./components/form-user-add-or-edit */ \"./Scripts/components/form-user-add-or-edit.js\");\n/* harmony import */ var _components_form_test__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./components/form-test */ \"./Scripts/components/form-test.js\");\n\r\n\r\n\r\n\r\n\r\n\r\nStartup();\r\n\r\nfunction Startup() {\r\n    console.log(\"Welcome!\\nCompile time:\", new Date().toUTCString());\r\n}\r\n\r\n$(document).on(\"click\", \"#button-logout\", function () {\r\n    Object(_utils_utils_requests__WEBPACK_IMPORTED_MODULE_0__[\"requestLogOut\"])();\r\n})\r\n\r\nnew Vue(\r\n    {\r\n        el: '#app',\r\n        components: {\r\n            'form-test': _components_form_test__WEBPACK_IMPORTED_MODULE_3__[\"formTest\"],\r\n            'form-user-login': _components_form_user_login__WEBPACK_IMPORTED_MODULE_1__[\"formUserLogin\"],\r\n            'form-user-add-or-edit': _components_form_user_add_or_edit__WEBPACK_IMPORTED_MODULE_2__[\"formUserAddOrEdit\"]\r\n        },\r\n        data: {},\r\n        computed: {},\r\n        methods: {}\r\n    }\r\n);\n\n//# sourceURL=webpack:///./Scripts/site.js?");

/***/ }),

/***/ "./Scripts/utils/constants.js":
/*!************************************!*\
  !*** ./Scripts/utils/constants.js ***!
  \************************************/
/*! exports provided: userDataCookieName, userLoginCookieName */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"userDataCookieName\", function() { return userDataCookieName; });\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"userLoginCookieName\", function() { return userLoginCookieName; });\nconst userDataCookieName = \"UserDataCookie\";\r\nconst userLoginCookieName = \"UserLoginCookie\";\n\n//# sourceURL=webpack:///./Scripts/utils/constants.js?");

/***/ }),

/***/ "./Scripts/utils/utils-cookie.js":
/*!***************************************!*\
  !*** ./Scripts/utils/utils-cookie.js ***!
  \***************************************/
/*! exports provided: getCookie, deleteCookie, createCookie, getUserDataCookieOrLogout */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"getCookie\", function() { return getCookie; });\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"deleteCookie\", function() { return deleteCookie; });\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"createCookie\", function() { return createCookie; });\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"getUserDataCookieOrLogout\", function() { return getUserDataCookieOrLogout; });\n/* harmony import */ var _Scripts_utils_constants__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../Scripts/utils/constants */ \"./Scripts/utils/constants.js\");\n\r\n\r\n//readCookie/deleteCookie/createCookie thanks to\r\n//https://www.sitepoint.com/how-to-deal-with-cookies-in-java\r\n\r\nconst getCookie = function (name) {\r\n    var nameEQ = name + \"=\";\r\n    var ca = document.cookie.split(';');\r\n    for (var i = 0; i < ca.length; i++) {\r\n        var c = ca[i];\r\n        while (c.charAt(0) == ' ') c = c.substring(1, c.length);\r\n        if (c.indexOf(nameEQ) == 0) {\r\n            var cookieData = c.substring(nameEQ.length, c.length).trim();\r\n            return cookieData;\r\n        }\r\n    }\r\n    return null;\r\n}\r\n\r\nconst deleteCookie = function (name, path, domain) {\r\n    // If the cookie exists\r\n    if (getCookie(name))\r\n        createCookie(name, \"\", -1, path, domain);\r\n}\r\n\r\nconst createCookie = function (name, value, expires, path, domain) {\r\n    var cookie = name + \"=\" + (value) + \";\";\r\n\r\n    if (expires) {\r\n        // If it's a date\r\n        if (expires instanceof Date) {\r\n            // If it isn't a valid date\r\n            if (isNaN(expires.getTime()))\r\n                expires = new Date();\r\n        }\r\n        else\r\n            expires = new Date(new Date().getTime() + 1 * 60 * 1000);\r\n\r\n        cookie += \"expires=\" + expires.toString() + \";\";\r\n    }\r\n\r\n    if (path)\r\n        cookie += \"path=\" + path + \";\";\r\n    if (domain)\r\n        cookie += \"domain=\" + domain + \";\";\r\n\r\n    document.cookie = cookie;\r\n}\r\n\r\nconst getUserDataCookieOrLogout = function () {\r\n    var aspNetCookie = getCookie(_Scripts_utils_constants__WEBPACK_IMPORTED_MODULE_0__[\"userLoginCookieName\"]);\r\n    //console.log(userLoginCookieName,\":\", aspNetCookie);\r\n    if (aspNetCookie === null || aspNetCookie === undefined || aspNetCookie.trim() === \"\") {\r\n        console.log(\"Unable to get user login cookie.\");\r\n        if (!window.location.href.toLowerCase().includes(\"login\") &&\r\n            !window.location.href.toLowerCase().includes(\"register\")) {\r\n            console.log(\"redirecting to login..\");\r\n            window.location.href = '/login';\r\n        }\r\n    } else {\r\n        var cookie = getCookie(_Scripts_utils_constants__WEBPACK_IMPORTED_MODULE_0__[\"userDataCookieName\"]);\r\n        if (cookie === null || cookie === undefined || cookie.trim() === \"\") {\r\n            deleteCookie(_Scripts_utils_constants__WEBPACK_IMPORTED_MODULE_0__[\"userDataCookieName\"]);\r\n            if (!window.location.href.toLowerCase().includes(\"login\") &&\r\n                !window.location.href.toLowerCase().includes(\"register\")) {\r\n                console.log(\"redirecting to login..\");\r\n                window.location.href = '/login';\r\n            }\r\n        }\r\n        else {\r\n            return cookie;\r\n        }\r\n    }\r\n}\n\n//# sourceURL=webpack:///./Scripts/utils/utils-cookie.js?");

/***/ }),

/***/ "./Scripts/utils/utils-general.js":
/*!****************************************!*\
  !*** ./Scripts/utils/utils-general.js ***!
  \****************************************/
/*! exports provided: validateEmail, JSONtryParse */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"validateEmail\", function() { return validateEmail; });\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"JSONtryParse\", function() { return JSONtryParse; });\nconst validateEmail = email => {\r\n    const re = /^(([^<>()\\[\\]\\\\.,;:\\s@\"]+(\\.[^<>()\\[\\]\\\\.,;:\\s@\"]+)*)|(\".+\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$/;\r\n    return re.test(String(email).toLowerCase());\r\n};\r\n\r\nconst JSONtryParse = function (obj) {\r\n    try {\r\n        return JSON.parse(obj);\r\n    }\r\n    catch (e) {\r\n        console.log(\"Exception parsing JSON:\", obj)\r\n        return {};\r\n    }\r\n}\n\n//# sourceURL=webpack:///./Scripts/utils/utils-general.js?");

/***/ }),

/***/ "./Scripts/utils/utils-requests.js":
/*!*****************************************!*\
  !*** ./Scripts/utils/utils-requests.js ***!
  \*****************************************/
/*! exports provided: requestLogOut */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"requestLogOut\", function() { return requestLogOut; });\n﻿var requestLogOut = function () {\r\n    axios({\r\n        method: 'get',\r\n        url: '/Login/LogOut'\r\n    }).then(data => {\r\n        console.log(\"__Logged out: \", data.data);\r\n        window.location.href = '/';\r\n    }).catch(err => {\r\n        alert(`There was an error logging out. See details: ${err}`);\r\n    });\r\n}\n\n//# sourceURL=webpack:///./Scripts/utils/utils-requests.js?");

/***/ }),

/***/ 0:
/*!***************************************************************************************************************************************************************************************************************************************************************************************************************************!*\
  !*** multi ./Scripts/site.js ./Scripts/components/form-test.js ./Scripts/components/form-user-add-or-edit.js ./Scripts/components/form-user-login.js ./Scripts/controls/controls-user.js ./Scripts/utils/constants.js ./Scripts/utils/utils-cookie.js ./Scripts/utils/utils-general.js ./Scripts/utils/utils-requests.js ***!
  \***************************************************************************************************************************************************************************************************************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

eval("__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\site.js */\"./Scripts/site.js\");\n__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\components\\form-test.js */\"./Scripts/components/form-test.js\");\n__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\components\\form-user-add-or-edit.js */\"./Scripts/components/form-user-add-or-edit.js\");\n__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\components\\form-user-login.js */\"./Scripts/components/form-user-login.js\");\n__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\controls\\controls-user.js */\"./Scripts/controls/controls-user.js\");\n__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\utils\\constants.js */\"./Scripts/utils/constants.js\");\n__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\utils\\utils-cookie.js */\"./Scripts/utils/utils-cookie.js\");\n__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\utils\\utils-general.js */\"./Scripts/utils/utils-general.js\");\nmodule.exports = __webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\utils\\utils-requests.js */\"./Scripts/utils/utils-requests.js\");\n\n\n//# sourceURL=webpack:///multi_./Scripts/site.js_./Scripts/components/form-test.js_./Scripts/components/form-user-add-or-edit.js_./Scripts/components/form-user-login.js_./Scripts/controls/controls-user.js_./Scripts/utils/constants.js_./Scripts/utils/utils-cookie.js_./Scripts/utils/utils-general.js_./Scripts/utils/utils-requests.js?");

/***/ })

/******/ });