app.directive('appBody', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/layout/appBody.html?r=' + RANDOM,
        scope: {},
        controller: 'appBodyCtrl'
    }
})
.controller('appBodyCtrl', ['$scope', 'authTokenService', function ($scope, authTokenService) {
    $scope.user = {};
    $scope.user.isLoggedIn = authTokenService.isAuthenticated();
   // $scope.user.isLoggedIn = true; // time being for debugging
}])