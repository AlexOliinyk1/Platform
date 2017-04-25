(function () {
    angular
        .module('common', [
            'ui.router',
            'angular-loading-bar'
        ])
        .run(function ($transitions, cfpLoadingBar) {
            //to use spinner when loading
            $transitions.onStart({}, cfpLoadingBar.start);
            $transitions.onSuccess({}, cfpLoadingBar.complete);
        });
})();