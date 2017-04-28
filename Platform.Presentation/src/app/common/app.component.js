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
                    url: '/app',
                    data: {
                        requiredAuth: false
                    },
                    component: 'app'
                })
        });
})();