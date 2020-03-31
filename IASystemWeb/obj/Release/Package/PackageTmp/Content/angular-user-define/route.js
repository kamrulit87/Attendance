
angular.module('app', ['ngTouch', 'ngRoute', 'ngResource', 'ngCookies', 'smart-table', 'ngDialog', 'ngAnimate', 'ngSanitize', 'treeGrid', 'angucomplete-alt', 'ngIdle'],
function ($routeProvider, $compileProvider) {

    $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|file|ftp|blob):|data:image\//);    
    //$routeProvider.when('/', {
    //    templateUrl: '/Home/Index'
    //}).when('/LogIn', {
    //    templateUrl: '/Account/Login#!/'
    //}).when('/LogOut', {
    //    templateUrl: '/Account/LogOff#!/'
    //}).otherwise('/', {
    //    templateUrl: '/Account/Login#!/'
    //});

    toastr.options = {
        "closeButton": true,
        "debug": true,
        "newestOnTop": true,
        "progressBar": true,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": true,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }


}).config(['KeepaliveProvider', 'IdleProvider', function (KeepaliveProvider, IdleProvider) {
    IdleProvider.idle(300);
    IdleProvider.timeout(5);
    KeepaliveProvider.interval(305);
}]);

// Following code use for ng-idle logout option
var myApp = angular.module('app');
myApp.run(['Idle', function (Idle) {
    Idle.watch();
}]);

