(function () {

    var about = {
        templateUrl: './about.template.html',
        controller: 'AboutController'
    };

    angular
        .module('components')
        .component('about', about)
        .config(function ($stateProvider) {
            $stateProvider
                .state('about', {
                    parent:'app',
                    url: '/about',
                    component: 'about'
                });
        });

})();
