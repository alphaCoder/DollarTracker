app.directive('appBody', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/layout/appBody.html?r=' + RANDOM,
        scope: {},
        controller: 'appBodyCtrl'
    }
})
.controller('appBodyCtrl', ['$scope', 'authToken', function ($scope, authToken) {
    $scope.user = {};
   // $scope.user.isLoggedIn = authToken.isAuthenticated();
   // $scope.user.isLoggedIn = true; // time being for debugging
}])