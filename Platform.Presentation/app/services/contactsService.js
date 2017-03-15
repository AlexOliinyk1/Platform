
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

    return {
        loadContacts: loadContacts
    };
}]);
