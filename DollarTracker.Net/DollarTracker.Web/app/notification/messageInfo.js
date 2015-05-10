app.directive("messageInfo", function () {
    return {
        restrict: 'E',
        templateUrl: 'app/notification/messageInfo.html?r=' + RANDOM,
        scope: {  
        },
        controller: 'messageInfoCtrl',
        link: function (scope, el, attrs) {
            el.replaceWith(el.children());
        }
    }
})