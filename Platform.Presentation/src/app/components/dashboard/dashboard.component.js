(function () {

    var dashboard = {
        templateUrl: './dashboard.template.html',
        controller: 'DashboardController'
    };

    angular
        .module('components')
        .component('dashboard', dashboard)
        .config(function ($stateProvider) {
            $stateProvider
                .state('dashboard', {
                    parent:'app',
                    url: '/dashboard',
                    component: 'dashboard'
                });
        });

})();
