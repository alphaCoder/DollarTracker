app = angular.module('DollarTrackerApp', ['ui.router', 'ui.bootstrap']);

app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', '$httpProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, $httpProvider) {

    $locationProvider.html5Mode(false);
    $urlRouterProvider.otherwise("/");
    //$httpProvider.interceptors.push('auth');

}]);

app.run([]); //todo: comeback