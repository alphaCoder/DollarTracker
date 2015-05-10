app.controller('profileCtrl', ['$scope', 'imageUpload', 'user', '$http', 'dolt', function ($scope, imageUpload, user, $http, dolt) {
    $scope.user = user;
    imageUpload.initFileButton($('#changePicButton'), function (img, filename) {
        imageUpload.scale(img, 120, 120, function (resizedImg) {
            $http.post(dolt.getApiUrl('ProfilePic'), { "userId": user.id, "imageBase64": resizedImg.src }).then(function (result) {
                    user.forceReloadImg();
            });
        });
    });
}])