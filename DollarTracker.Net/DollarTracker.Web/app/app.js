app = angular.module('DollarTrackerApp', ['ui.router','ui.bootstrap']);

app.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', '$httpProvider', 
    function ($stateProvider, $urlRouterProvider, $locationProvider, $httpProvider) {

    $locationProvider.html5Mode(false);
   $urlRouterProvider.otherwise("/");
    //$httpProvider.interceptors.push('auth');

    $stateProvider
    .state('/', {
        url: '/',
        templateUrl: 'app/dashboard/dashboard.html?random=' + RANDOM,
        controller: 'dashboardCtrl',
        resolve: {
            expenseStories: ['dashboard',  function (dashboard) {
                return dashboard.getExpenseStories().then(function (results) {
                    console.log("resolve");
                    console.log(results);
                    return results.data.data;
                });
            }]
        }
    });

}]);

//app.run([''], function () { }); //todo: comeback