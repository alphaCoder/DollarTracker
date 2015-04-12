app.directive('appBody', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/layout/appBody.html?r=' + RANDOM,
        scope: {},
        controller: 'appBodyCtrl'
    }
})
.controller('appBodyCtrl', ['$scope', '$auth', function ($scope, $auth) {
   $scope.user = {};
   //$scope.user.isLoggedIn = $auth.isAuthenticated;

   $scope.isAuthenticated = $auth.isAuthenticated;
    console.log('isLoggedIn:appbody--->', $auth.isAuthenticated());
   // $scope.user.isLoggedIn = true; // time being for debugging
}])