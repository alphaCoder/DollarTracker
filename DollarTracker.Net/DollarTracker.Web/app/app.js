app = angular.module('DollarTrackerApp', ['ui.router', 'ui.bootstrap', 'ng-bootstrap-datepicker', 'satellizer']);
app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', '$httpProvider', '$authProvider', 'API_URL',
function ($stateProvider, $urlRouterProvider, $locationProvider, $httpProvider, $authProvider, API_URL) {

    $locationProvider.html5Mode(false);
    $urlRouterProvider.otherwise("/");

    $stateProvider
    .state('/', {
        url: '/',
        templateUrl: 'app/dashboard/dashboard.html?random=' + RANDOM,
        controller: 'dashboardCtrl',
        resolve: {
            expenseStories: ['dashboard', function (dashboard) {
                return dashboard.getExpenseStories().then(function (results) {
                    console.log("resolve");
                    console.log(results);
                    return results.data.data;
                });
            }]
        }
    })
    .state('login', {
        url: '/login',
        templateUrl: 'app/user/login.html?random=' + RANDOM,
        controller: 'loginCtrl'
    })
    .state('register', {
        url: '/register',
        templateUrl: 'app/user/register.html?random=' + RANDOM,
        controller: 'registerCtrl'
    })
    .state('logout', {
        url: '/logout',
        controller: 'logoutCtrl'
    })
    .state('err', {
        "abstract": true,
        url: '/err',
        template: '<div ui-view></div>'
    })
    .state('err.401', {
        url: "/401",
        template: "<h3>401</h3> you are not authorized",
    })
    .state('err.404', {
        url: "/404",
        template: "<h3>404</h3> error was encountered",
    })
    .state('err.oops', { // Any errors not otherwise specified
        url: "/oops",
        template: "Unknown error",
    });

    $authProvider.apiServer = API_URL;
    $authProvider.loginUrl = API_URL + 'login';
    $authProvider.signupUrl = API_URL + 'register';
    $authProvider.loginRedirect = '/';
    $authProvider.logoutRedirect = 'login';
    $authProvider.httpInterceptor = true;
    $authProvider.signupRedirect = '/login';
    $authProvider.loginOnSignup = false;

    $authProvider.google({
        clientId: '603422408309-rinan2timml0ufbbp0qi9jmnjf6n9bkl.apps.googleusercontent.com',
        url: API_URL + 'auth/google'
    })

    $httpProvider.interceptors.push('authInterceptor');
}])
.constant('API_URL', 'http://localhost:3000/')
.controller('appCtrl', ['$scope', '$auth', 'user', function ($scope, $auth, user) {
    $scope.isAuthenticated = $auth.isAuthenticated;
}])
.run(['$window', '$rootScope', '$auth', '$state', 'user', function ($window, $rootScope, $auth, $state, user) {

    $rootScope.user = JSON.parse(user.getUser());

    $rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
        if ($auth.isAuthenticated() && (toState.name === 'login' || toState.name === 'register')) {
            console.log(toState.name);
            event.preventDefault();
            $state.go('/');
        }
    });
    var params = $window.location.search.substring(1);

    if (params && $window.opener && $window.opener.location.origin === $window.location.origin) {
        var pair = params.split('=');
        var code = decodeURIComponent(pair[1]);

        $window.opener.postMessage(code, $window.location.origin);
    }
}]);