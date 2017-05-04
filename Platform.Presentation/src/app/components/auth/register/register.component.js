(function () {
    var register = {
        templateUrl: './register.template.html',
        controller: 'RegisterController'
    };

    angular
        .module('components')
        .component('register', register)
        .config(function ($stateProvider) {
            $stateProvider
                .state('auth.register', {
                    url: '/register',
                    component: 'register'
                });
        });
})();