(function () {
    angular
        .module('app')
        .config(function ($stateProvider) {
            var dashboardState = {
                name: 'dashboard',
                url: '/dashboard',
                component: 'dashboard'
            }

            $stateProvider.state(dashboardState);
        });
})();
