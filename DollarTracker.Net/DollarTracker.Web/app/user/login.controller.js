'use strict';

app.controller('loginCtrl', ['$scope','auth', '$auth', function ($scope, auth, $auth) {
    $scope.submit = function () {
        $auth.login({
            email: $scope.email,
            password: $scope.password
        }).then(function (res) {
            var message = 'Thanks for coming back ' + res.data.user.email + '!';

            if (!res.data.user.active)
                message = 'Just a reminder, please activate your account soon :)';

            console.log('success', 'Welcome', message);
        }).catch(handleError);
    };

    $scope.authenticate = function (provider) {
        $auth.authenticate(provider).then(function (res) {
            console.log('success', 'Welcome', 'Thanks for coming back ' + res.data.user.displayName + '!');
        }, handleError);
    }

    function handleError(err) {
        console.log('warning', 'Something went wrong :(', err.message);
    }
}]);