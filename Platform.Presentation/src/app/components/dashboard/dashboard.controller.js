(function () {

    function DashboardController() {
        var self = this;
        self.addFile = addFile;
        self.file = "";
        self.files = [{
            name: "test1",
            description: "desc1"
        }, {
            name: "test2",
            description: "desc2"
        }];

        function addFile() {
            self.files.push(self.file);
        }
    }

    angular
        .module('components')
        .controller('DashboardController', DashboardController);
})();