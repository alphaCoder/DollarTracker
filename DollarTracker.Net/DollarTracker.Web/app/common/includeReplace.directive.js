/*  For more reference follow the below link    
 * http://stackoverflow.com/questions/16496647/replace-ng-include-node-with-template
 * 
 */

app.directive('includeReplace', function () {
    return {
        require: 'ngInclude',
        restrict: 'A', /* optional */
        link: function (scope, el, attrs) {
            el.replaceWith(el.children());
        }
    };
});