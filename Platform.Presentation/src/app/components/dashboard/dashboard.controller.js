(function() {

    function dashboardController(){
        var vm = this;
        vm.addFile = addFile;
        vm.file = "";
        vm.files = [{
            name:"test1",
            description:"desc1"
        },{
            name:"test2",
            description:"desc2"
        }];

        function addFile(){
            vm.files.push(vm.file);
        }
    }
    
    angular
       .module('app')
       .controller('dashboardController',dashboardController)
})();
