(function () {
    var authForm = {
        bindings: {
            user: '<',
            button: '@',
            message: '@',
            onSubmit: '&'
        },
        templateUrl: './auth-form.template.html',
        controller: 'AuthFormController'
    };

    angular
        .module('components')
        .component('authForm', authForm);
})();