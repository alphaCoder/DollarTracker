app = angular.module('DollarTrackerApp', ['ui.router']);

app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', '$httpProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, $httpProvider) {

    $locationProvider.html5Mode(false);
   $urlRouterProvider.otherwise("/");
    //$httpProvider.interceptors.push('auth');

    $stateProvider
    .state('/', {
        url: '/',
        templateUrl: 'app/dashboard/dashboard.html?random='+RANDOM
    });

}]);

//app.run([''], function () { }); //todo: comeback