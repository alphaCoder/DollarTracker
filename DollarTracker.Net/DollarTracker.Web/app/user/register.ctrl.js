app.controller('registerCtrl', ['$scope', '$auth', function ($scope, $auth) {

    $scope.submit = function () {
        console.log('--signup hit--');
        $auth.signup({
            email: $scope.email,
            password: $scope.password,
            displayName: $scope.displayName
        })
        .then(function (res) {
            console.log("account successfully created", JSON.stringify(res));
        })
        .catch(function (err) {
            console.log("unable to create account:", JSON.stringify(err));
        })
    }

}]);