'use strict';

app.controller('logoutCtrl',['$auth','$state', 'user', function ($auth, $state, user) {
    $auth.logout();
    user.removeUser();
    $state.go('login');
}]);