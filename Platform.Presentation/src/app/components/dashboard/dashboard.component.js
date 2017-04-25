(function () {

    var dashboard = {
        templateUrl: './dashboard.template.html',
        controller: 'DashboardController'
    };

    angular
        .module('components.dashboard')
        .component('dashboard', dashboard)
        .config(function ($stateProvider) {
            $stateProvider
                .state('dashboard', {
                    url: '/dashboard',
                    component: 'dashboard'
                });
        });

})();
