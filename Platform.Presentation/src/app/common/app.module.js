(function () {
    run.$inject = ['$transitions', 'cfpLoadingBar'];

    angular
        .module('common', [
            'ui.router',
            'angular-loading-bar'
        ])
        .run(run);

    function run($transitions, cfpLoadingBar) {
        //to use spinner when loading
        $transitions.onStart({}, cfpLoadingBar.start);
        $transitions.onSuccess({}, cfpLoadingBar.complete);
    }
})();