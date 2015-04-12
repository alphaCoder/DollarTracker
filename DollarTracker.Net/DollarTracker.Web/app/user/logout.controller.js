'use strict';

app.controller('logoutCtrl', function ($auth, $state) {
    $auth.logout();
    $state.go('login');
});