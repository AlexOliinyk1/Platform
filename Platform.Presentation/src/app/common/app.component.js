(function () {
    var app = {
        templateUrl: './app.template.html',
        controller: 'AppController'
    };

    angular
        .module('common')
        .component('app', app)
        .config(function ($stateProvider) {
            $stateProvider
                .state('app', {
                    redirectTo: 'dashboard',
                    url: '/dashboard',
                    data: {
                        requiredAuth: false
                    },
                    component: 'app'
                })
        });
})();