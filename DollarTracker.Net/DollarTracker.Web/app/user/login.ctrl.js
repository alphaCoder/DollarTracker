'use strict';

app.controller('loginCtrl', ['$scope', '$auth', 'user', function ($scope, $auth, user) {
    $scope.email = "jsrao16@gmail.com";
    $scope.password = "a";
    $scope.submit = function () {
        $auth.login({
            email: $scope.email,
            password: $scope.password
        }).then(function (res) {
            var message = 'Thanks for coming back ' + res.data.user.email + '!';
            user.setUser(res.data.user);
            if (!res.data.user.active)
                message = 'Just a reminder, please activate your account soon :)';

            console.log('success', 'Welcome', message);
        }).catch(handleError);
    };

    $scope.authenticate = function (provider) {
        $auth.authenticate(provider).then(function (res) {
            console.log("user::", JSON.stringify(res.data.user));
            user.setUser(res.data.user);

            console.log('success', 'Welcome', 'Thanks for coming back ' + res.data.user.displayName + '!');
        }, handleError);
    }

    function handleError(err) {
        console.log('warning', 'Something went wrong :(', err.message);
    }
}]);