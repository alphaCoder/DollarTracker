'use strict';

app.controller('headerCtrl', ['$scope', '$auth', function ($scope, $auth) {
    $scope.isAuthenticated = $auth.isAuthenticated;
}])