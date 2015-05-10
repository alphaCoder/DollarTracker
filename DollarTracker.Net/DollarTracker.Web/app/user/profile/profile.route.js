app.config(['$stateProvider', function ($stateProvider) {
    $stateProvider
      .state('profile', {
          url: '/profile',
          templateUrl: 'app/user/profile/profile.html?random=' + RANDOM,
          controller: 'profileCtrl'
      })
}]);