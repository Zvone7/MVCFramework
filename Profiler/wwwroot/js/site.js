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

/***/ "./Scripts/components/display-user-data.js":
/*!*************************************************!*\
  !*** ./Scripts/components/display-user-data.js ***!
  \*************************************************/
/*! exports provided: displayUserData */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"displayUserData\", function() { return displayUserData; });\n/* harmony import */ var _utils_utils_cookie__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../utils/utils-cookie */ \"./Scripts/utils/utils-cookie.js\");\n/* harmony import */ var _utils_utils_general__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../utils/utils-general */ \"./Scripts/utils/utils-general.js\");\n/* harmony import */ var _utils_constants__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../utils/constants */ \"./Scripts/utils/constants.js\");\n﻿\r\n\r\n\r\n\r\nvar displayUserData = Vue.component('display-user-data',\r\n    {\r\n        data: function () {\r\n            return {\r\n                Name: '',\r\n                LastName: ''\r\n                //isLogoutDisabled: true\r\n            }\r\n        },\r\n        computed: {\r\n            isLogoutDisabled() {\r\n                let isDisabled = true;\r\n                var userCookie = Object(_utils_utils_cookie__WEBPACK_IMPORTED_MODULE_0__[\"getUserCookieOrLogout\"])(_utils_constants__WEBPACK_IMPORTED_MODULE_2__[\"userCookieName\"]);\r\n                var userData = Object(_utils_utils_general__WEBPACK_IMPORTED_MODULE_1__[\"JSONtryParse\"])(userCookie);\r\n                this.$data.Name = userData.name;\r\n                this.$data.LastName = userData.lastName;\r\n                if (\r\n                    this.$data.Name &&\r\n                    this.$data.LastName !== ''\r\n                ) {\r\n                    isDisabled = false;\r\n                }\r\n\r\n                return isDisabled;\r\n            }\r\n        },\r\n        methods: {\r\n            Logout() {\r\n                Object(_utils_utils_cookie__WEBPACK_IMPORTED_MODULE_0__[\"deleteCookie\"])(_utils_constants__WEBPACK_IMPORTED_MODULE_2__[\"userCookieName\"]);\r\n                this.$parent.Name = '';\r\n                this.$parent.LastName = '';\r\n                window.location.href = '/';\r\n            }\r\n        },\r\n        template: '<div><label>{{Name}}</label><br/><label>{{LastName}}</label><button type=\"button\" ref=\"LogoutButton\" v-bind:disabled=\"isLogoutDisabled\" v-on:click=\"Logout\">Logout</button></div>'\r\n    }\r\n)\n\n//# sourceURL=webpack:///./Scripts/components/display-user-data.js?");

/***/ }),

/***/ "./Scripts/components/form-login.js":
/*!******************************************!*\
  !*** ./Scripts/components/form-login.js ***!
  \******************************************/
/*! exports provided: formLogin */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"formLogin\", function() { return formLogin; });\n/* harmony import */ var _utils_utils_cookie__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../utils/utils-cookie */ \"./Scripts/utils/utils-cookie.js\");\n/* harmony import */ var _utils_constants__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../utils/constants */ \"./Scripts/utils/constants.js\");\n\r\n\r\n\r\nvar formLogin = Vue.component('form-login',\r\n    {\r\n        data: function () {\r\n            return {\r\n                Email: 'admin@mail.com',\r\n                Password: 'admin'\r\n            }\r\n        },\r\n        computed: {\r\n            isLoginDisabled() {\r\n                let isDisabled = true;\r\n\r\n                if (\r\n                    this.Email !== '' &&\r\n                    this.Password !== ''\r\n                ) {\r\n                    isDisabled = false;\r\n                }\r\n\r\n                return isDisabled;\r\n            }\r\n        },\r\n        methods: {\r\n            SubmitLoginForm() {\r\n                axios({\r\n                    method: 'post',\r\n                    url: '/User/LogIn',\r\n                    data: {\r\n                        Email: this.$data.Email,\r\n                        Password: this.$data.Password\r\n                    }\r\n                }).then(data => {\r\n                    console.log(\"_Logged in_: \", data.data);\r\n                    this.$refs.LoginButton.setAttribute(\"disabled\", \"disabled\");\r\n                    // create cookie\r\n                    if (data.data.email != undefined && data.data.password != \"\") {\r\n                        var userData = data.data;\r\n                        Object(_utils_utils_cookie__WEBPACK_IMPORTED_MODULE_0__[\"createCookie\"])(_utils_constants__WEBPACK_IMPORTED_MODULE_1__[\"userCookieName\"], JSON.stringify(userData), new Date(new Date().getTime() + 10 * 60 * 1000), \"/\", null);\r\n                        //todo cookies not expiring/being deleted when expired..handle it.\r\n\r\n                        window.location.href = '/';\r\n                    }\r\n\r\n                }).catch(err => {\r\n                    alert(`There was an error logging in. See details: ${err}`);\r\n                });\r\n            },\r\n            Register() {\r\n                window.location.href = '/register';\r\n            },\r\n            ResetForm() {\r\n                console.log(\"ResetForm called\");\r\n                this.Email = '';\r\n                this.Password = '';\r\n            }\r\n        },\r\n        template: '<div><label><b>Email</b></label><input type=\"text\" placeholder=\"Enter email\" v-model=\"Email\" required><label><b>Password</b></label><input type=\"password\" placeholder=\"Enter Password\" v-model=\"Password\" required><button type=\"button\" class=\"success\" ref=\"LoginButton\" v-bind:disabled=\"isLoginDisabled\" v-on:click=\"SubmitLoginForm\">Login</button><button type=\"button\" class=\"info\" ref=\"RegisterButton\" v-on:click=\"Register\">Register</button></div>'\r\n    }\r\n)\n\n//# sourceURL=webpack:///./Scripts/components/form-login.js?");

/***/ }),

/***/ "./Scripts/components/form-register.js":
/*!*********************************************!*\
  !*** ./Scripts/components/form-register.js ***!
  \*********************************************/
/*! exports provided: formRegister */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"formRegister\", function() { return formRegister; });\n/* harmony import */ var _Scripts_utils_utils_general__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../Scripts/utils/utils-general */ \"./Scripts/utils/utils-general.js\");\n\r\n\r\nvar formRegister = Vue.component('form-register',\r\n    {\r\n        data: function () {\r\n            return {\r\n                Name: '',\r\n                LastName: '',\r\n                Email: '',\r\n                Password: '',\r\n                PasswordAgain: ''\r\n            }\r\n        },\r\n        computed: {\r\n            isRegisterDisabled() {\r\n                let isDisabled = true;\r\n\r\n                if (\r\n                    this.Email !== '' &&\r\n                    Object(_Scripts_utils_utils_general__WEBPACK_IMPORTED_MODULE_0__[\"validateEmail\"])(this.Email) &&\r\n                    this.Name !== '' &&\r\n                    this.LastName !== '' &&\r\n                    this.PasswordAgain !== '' &&\r\n                    this.Password !== '' &&\r\n                    this.Password === this.PasswordAgain\r\n                ) {\r\n                    isDisabled = false;\r\n                }\r\n\r\n                return isDisabled;\r\n            }\r\n        },\r\n        methods: {\r\n            SubmitRegisterForm() {\r\n                axios({\r\n                    method: 'post',\r\n                    url: '/User/Register',\r\n                    data: {\r\n                        Email: this.$data.Email,\r\n                        Name: this.$data.Name,\r\n                        LastName: this.$data.LastName,\r\n                        Password: this.$data.Password\r\n                    }\r\n                }).then(data => {\r\n                    console.log(\"Registering_: \", data.data);\r\n                    this.$refs.RegisterButton.setAttribute(\"disabled\", \"disabled\");\r\n                    //// create cookie\r\n                    //if (data.data.email != undefined && data.data.password != \"\") {\r\n                    //    var userData = data.data;\r\n                    //    createCookie(userCookieName, JSON.stringify(userData), new Date(new Date().getTime() + 10 * 60 * 1000), \"/\", null);\r\n                    //    //todo cookies not expiring/being deleted when expired..handle it.\r\n\r\n                    //    window.location.href = '/';\r\n                    //}\r\n\r\n                }).catch(err => {\r\n                    alert(`There was an error registering. See details: ${err}`);\r\n                });\r\n            },\r\n            ResetForm() {\r\n                console.log(\"ResetForm called\");\r\n                this.Email = '';\r\n                this.Name = '';\r\n                this.LastName = '';\r\n                this.Password = '';\r\n            }\r\n        },\r\n        template: '<div><label><b>Name</b></label><input type=\"text\" placeholder=\"Enter name\" v-model=\"Name\" required><label><b>Last Name</b></label><input type=\"text\" placeholder=\"Enter last name\" v-model=\"LastName\" required><label><b>Email</b></label><input type=\"text\" placeholder=\"Enter email\" v-model=\"Email\" required><label><b>Password</b></label><input type=\"password\" placeholder=\"Enter password\" v-model=\"Password\" required><label><b>Password again</b></label><input type=\"password\" placeholder=\"Enter password again\" v-model=\"PasswordAgain\" required><button type=\"button\" class=\"success\" ref=\"RegisterButton\" v-bind:disabled=\"isRegisterDisabled\" v-on:click=\"SubmitRegisterForm\">Register</button></div>'\r\n    }\r\n)\n\n//# sourceURL=webpack:///./Scripts/components/form-register.js?");

/***/ }),

/***/ "./Scripts/site.js":
/*!*************************!*\
  !*** ./Scripts/site.js ***!
  \*************************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony import */ var _utils_constants__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ./utils/constants */ \"./Scripts/utils/constants.js\");\n/* harmony import */ var _utils_utils_cookie__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./utils/utils-cookie */ \"./Scripts/utils/utils-cookie.js\");\n/* harmony import */ var _utils_utils_general__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./utils/utils-general */ \"./Scripts/utils/utils-general.js\");\n/* harmony import */ var _components_form_login__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./components/form-login */ \"./Scripts/components/form-login.js\");\n/* harmony import */ var _components_form_register__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./components/form-register */ \"./Scripts/components/form-register.js\");\n/* harmony import */ var _components_display_user_data__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ./components/display-user-data */ \"./Scripts/components/display-user-data.js\");\n\r\n\r\n\r\n\r\n\r\n\r\n\r\n\r\nStartup();\r\n\r\nfunction Startup() {\r\n    console.log(\"Welcome!\\nCompile time:\", new Date().toUTCString());\r\n    var userCookie = Object(_utils_utils_cookie__WEBPACK_IMPORTED_MODULE_1__[\"getUserCookieOrLogout\"])(_utils_constants__WEBPACK_IMPORTED_MODULE_0__[\"userCookieName\"]);\r\n    if (userCookie) {\r\n        console.log(\"Welcome\", Object(_utils_utils_general__WEBPACK_IMPORTED_MODULE_2__[\"JSONtryParse\"])(userCookie).name);\r\n        if (window.location.href.toLowerCase().includes(\"login\") ||\r\n            window.location.href.toLowerCase().includes(\"register\")) {\r\n            console.log(\"redirecting home...\");\r\n            window.location.href = '/';\r\n        }\r\n    }\r\n}\r\n\r\nnew Vue(\r\n    {\r\n        el: '#app',\r\n        components: { 'form-login': _components_form_login__WEBPACK_IMPORTED_MODULE_3__[\"formLogin\"], 'form-register': _components_form_register__WEBPACK_IMPORTED_MODULE_4__[\"formRegister\"], 'display-user-data': _components_display_user_data__WEBPACK_IMPORTED_MODULE_5__[\"displayUserData\"] },\r\n        data: {\r\n            //Email: 'admin',\r\n            //Password: 'admin',\r\n        },\r\n        computed: {},\r\n        methods: {\r\n            SubmitLoginForm() { },\r\n            ResetForm() { },\r\n            Logout() { }\r\n        }\r\n    }\r\n);\n\n//# sourceURL=webpack:///./Scripts/site.js?");

/***/ }),

/***/ "./Scripts/utils/constants.js":
/*!************************************!*\
  !*** ./Scripts/utils/constants.js ***!
  \************************************/
/*! exports provided: userCookieName */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"userCookieName\", function() { return userCookieName; });\nconst userCookieName = \"UserCookie\";\n\n//# sourceURL=webpack:///./Scripts/utils/constants.js?");

/***/ }),

/***/ "./Scripts/utils/utils-cookie.js":
/*!***************************************!*\
  !*** ./Scripts/utils/utils-cookie.js ***!
  \***************************************/
/*! exports provided: getCookie, deleteCookie, createCookie, getUserCookieOrLogout */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
eval("__webpack_require__.r(__webpack_exports__);\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"getCookie\", function() { return getCookie; });\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"deleteCookie\", function() { return deleteCookie; });\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"createCookie\", function() { return createCookie; });\n/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, \"getUserCookieOrLogout\", function() { return getUserCookieOrLogout; });\n/* harmony import */ var _Scripts_utils_constants__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../../Scripts/utils/constants */ \"./Scripts/utils/constants.js\");\n\r\n\r\n//readCookie/deleteCookie/createCookie thanks to\r\n//https://www.sitepoint.com/how-to-deal-with-cookies-in-java\r\n\r\nconst getCookie = function (name) {\r\n    var nameEQ = name + \"=\";\r\n    var ca = document.cookie.split(';');\r\n    for (var i = 0; i < ca.length; i++) {\r\n        var c = ca[i];\r\n        while (c.charAt(0) == ' ') c = c.substring(1, c.length);\r\n        if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length).trim();\r\n    }\r\n    return null;\r\n}\r\n\r\nconst deleteCookie = function (name, path, domain) {\r\n    // If the cookie exists\r\n    if (getCookie(name))\r\n        createCookie(name, \"\", -1, path, domain);\r\n}\r\n\r\nconst createCookie = function (name, value, expires, path, domain) {\r\n    var cookie = name + \"=\" + (value) + \";\";\r\n\r\n    if (expires) {\r\n        // If it's a date\r\n        if (expires instanceof Date) {\r\n            // If it isn't a valid date\r\n            if (isNaN(expires.getTime()))\r\n                expires = new Date();\r\n        }\r\n        else\r\n            expires = new Date(new Date().getTime() + 1 * 60 * 1000);\r\n\r\n        cookie += \"expires=\" + expires.toString() + \";\";\r\n    }\r\n\r\n    if (path)\r\n        cookie += \"path=\" + path + \";\";\r\n    if (domain)\r\n        cookie += \"domain=\" + domain + \";\";\r\n\r\n    document.cookie = cookie;\r\n}\r\n\r\nconst getUserCookieOrLogout = function () {\r\n    var cookie = getCookie(_Scripts_utils_constants__WEBPACK_IMPORTED_MODULE_0__[\"userCookieName\"]);\r\n    if (cookie === null || cookie === undefined || cookie.trim() === \"\") {\r\n        deleteCookie(_Scripts_utils_constants__WEBPACK_IMPORTED_MODULE_0__[\"userCookieName\"]);\r\n        if (!window.location.href.toLowerCase().includes(\"login\") && !window.location.href.toLowerCase().includes(\"register\"))\r\n            //console.log(\"redirecting to login..\");\r\n            window.location.href = '/login';\r\n    }\r\n    else {\r\n        return cookie;\r\n    }\r\n}\n\n//# sourceURL=webpack:///./Scripts/utils/utils-cookie.js?");

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

/***/ 0:
/*!************************************************************************************************************************************************************************************************************************************************!*\
  !*** multi ./Scripts/site.js ./Scripts/components/display-user-data.js ./Scripts/components/form-login.js ./Scripts/components/form-register.js ./Scripts/utils/constants.js ./Scripts/utils/utils-cookie.js ./Scripts/utils/utils-general.js ***!
  \************************************************************************************************************************************************************************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

eval("__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\site.js */\"./Scripts/site.js\");\n__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\components\\display-user-data.js */\"./Scripts/components/display-user-data.js\");\n__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\components\\form-login.js */\"./Scripts/components/form-login.js\");\n__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\components\\form-register.js */\"./Scripts/components/form-register.js\");\n__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\utils\\constants.js */\"./Scripts/utils/constants.js\");\n__webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\utils\\utils-cookie.js */\"./Scripts/utils/utils-cookie.js\");\nmodule.exports = __webpack_require__(/*! C:\\Repos\\Profiler\\Profiler\\Scripts\\utils\\utils-general.js */\"./Scripts/utils/utils-general.js\");\n\n\n//# sourceURL=webpack:///multi_./Scripts/site.js_./Scripts/components/display-user-data.js_./Scripts/components/form-login.js_./Scripts/components/form-register.js_./Scripts/utils/constants.js_./Scripts/utils/utils-cookie.js_./Scripts/utils/utils-general.js?");

/***/ })

/******/ });