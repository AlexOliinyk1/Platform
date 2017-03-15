
App.factory('contactService', ['$http', '$location', function ($http, $location) {
    var protocol = $location.protocol();
    var host = $location.host();
    var port = 57090;

    var baseUrl = protocol + "://" + host + ":" + port;

    var loadContacts = function (page, perPage, searchWord) {

        //Todo: implement paging {page, perPage, searchWord}
        return $http({
            method: 'get',
            url: baseUrl + '/api/Contacts',
            data: { page, perPage, searchWord },
            headers: {'Content-Type': 'application/json'}
        }).then(function (result) {
            return result.data;
        });
    }

    var uploadExcel = function (excelDocument) {
        var fd = new FormData();
        fd.append("file", excelDocument);
        return $http({
            method: 'post',
            url: baseUrl + '/api/Contacts/SetContactsFromDocument',
            data: fd,
            headers: { 'Content-Type': undefined },
            transformRequest: angular.identity
        }).then(function (result) {
            return result.data;
        });
    }

    return {
        loadContacts: loadContacts,
        uploadExcel: uploadExcel
    };
}]);
