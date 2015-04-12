app.directive('sideBar', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/layout/sidebar.html?r=' + RANDOM,
        scope: {},
        controller: 'sideBarCtrl',
        link: function (scope, el, attrs) {
            el.replaceWith(el.children());
        }
    }
})
.controller('sideBarCtrl', ['$scope', '$auth', function ($scope, $auth) {
    $scope.isAuthenticated = $auth.isAuthenticated;
}])