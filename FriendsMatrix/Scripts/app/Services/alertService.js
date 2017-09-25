'use strict';

angular.module('app').factory('alertService', function ($mdToast) {
    //TODO: . . . 

    var show = function (text, cssClass, hideDelay) {
        $mdToast.show(
             $mdToast.simple()
                 .toastClass(cssClass)
                 .textContent(text)
                 .position('top right')
                 .hideDelay(hideDelay)
         );
    };

    return {
        showError: function (response) {
            var text = "Error: " + response.status + " " + response.statusText;
            console.log(response.data.MessageDetail);
            console.log(response.data.ExceptionMessage);
            console.log(response.data.Message);
            console.log(response.data.StackTrace);

            return show(text, 'md-toast-error', 2000);
        },

        showSuccess: function (text) {
            return show(text, 'md-toast-done', 500);
        }
    }
});
