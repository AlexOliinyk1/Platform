
App.controller('ContactsCtrl', ['$scope', '$localStorage', '$window', 'contactService',
    function ($scope, $localStorage, $window, contactService, uploadExcel) {
        $scope.contactTypes = ['ALL', 'CUSTOMER', 'SUPPLIER', 'EMPLOYEE', 'OTHER'];

        $scope.contact = {};

        $scope.pageModel = {
            byPage: 10,
            currentPage: 0,
            searchWord: '',
            contactType: $scope.contactTypes[0]
        };

        $scope.SaveContact = SaveContact;
        $scope.SelectContactType = SelectContactType;
        $scope.SendExcel = sendExcel;
        //  replaced by link on UI
        $scope.DownloadExel = downloadExel;

        function downloadExel() {
            contactService.downloadExcel().then(function (file) {
                var anchor = angular.element('<a/>');
                anchor.attr({
                    href: 'data:attachment/xlsx;charset=utf-8,' + encodeURI(file),
                    target: '_blank',
                    download: 'contacts.xlsx'
                })[0].click();
            });
        }

        function sendExcel(file) {
            contactService.uploadExcel(file).then(function () {
                $("#excelFileExport").val('');
                loadContacts();
            });
        }

        function SaveContact(model) {
            if ($('#createContactForm').valid()) {
                contactService.saveContact(model)
                    .then(function (result) {
                        if (result == 'success') {
                            $('#create-contact').modal('hide');
                            resetContactModel();
                            loadContacts();
                        }
                    });
            }
        }

        function SelectContactType(type) {
            $scope.pageModel.contactType = type;
            loadContacts();
        }

        // Init full DataTable, for more examples you can check out https://www.datatables.net/
        var initDataTableFull = function () {
            jQuery('.js-dataTable-full').dataTable({
                columnDefs: [{ orderable: false, targets: [2] }],
                columns: [
                    { data: 'name', title: 'Name' },
                    { data: 'address', title: 'Address' },
                    { data: 'zipCode', title: 'Zip Code' }
                ],
                pageLength: 10,
                lengthMenu: [[5, 10, 15, 20], [5, 10, 15, 20]],
                //dom: 'l<"toolbar"> frtip',
                initComplete: function () {
                    //$("div.toolbar")
                    //   .html('');
                }
            });
        };

        // DataTables Bootstrap integration
        var bsDataTables = function () {
            var dataTable = jQuery.fn.dataTable;

            // Set the defaults for DataTables init
            jQuery.extend(true, dataTable.defaults, {
                dom:
                    "<'row'<'col-sm-6'l><'col-sm-6'f>>" +
                    "<'row'<'col-sm-12'tr>>" +
                    "<'row'<'col-sm-6'i><'col-sm-6'p>>",
                renderer: 'bootstrap',
                oLanguage: {
                    sLengthMenu: "_MENU_",
                    sInfo: "Showing <strong>_START_</strong>-<strong>_END_</strong> of <strong>_TOTAL_</strong>",
                    oPaginate: {
                        sPrevious: '<i class="fa fa-angle-left"></i>',
                        sNext: '<i class="fa fa-angle-right"></i>'
                    }
                }
            });

            // Default class modification
            jQuery.extend(dataTable.ext.classes, {
                sWrapper: "dataTables_wrapper form-inline dt-bootstrap",
                sFilterInput: "form-control",
                sLengthSelect: "form-control"
            });

            // Bootstrap paging button renderer
            dataTable.ext.renderer.pageButton.bootstrap = function (settings, host, idx, buttons, page, pages) {
                var api = new dataTable.Api(settings);
                var classes = settings.oClasses;
                var lang = settings.oLanguage.oPaginate;
                var btnDisplay, btnClass;

                var attach = function (container, buttons) {
                    var i, ien, node, button;
                    var clickHandler = function (e) {
                        e.preventDefault();
                        if (!jQuery(e.currentTarget).hasClass('disabled')) {
                            api.page(e.data.action).draw(false);
                        }
                    };

                    for (i = 0, ien = buttons.length; i < ien; i++) {
                        button = buttons[i];

                        if (jQuery.isArray(button)) {
                            attach(container, button);
                        }
                        else {
                            btnDisplay = '';
                            btnClass = '';

                            switch (button) {
                                case 'ellipsis':
                                    btnDisplay = '&hellip;';
                                    btnClass = 'disabled';
                                    break;

                                case 'first':
                                    btnDisplay = lang.sFirst;
                                    btnClass = button + (page > 0 ? '' : ' disabled');
                                    break;

                                case 'previous':
                                    btnDisplay = lang.sPrevious;
                                    btnClass = button + (page > 0 ? '' : ' disabled');
                                    break;

                                case 'next':
                                    btnDisplay = lang.sNext;
                                    btnClass = button + (page < pages - 1 ? '' : ' disabled');
                                    break;

                                case 'last':
                                    btnDisplay = lang.sLast;
                                    btnClass = button + (page < pages - 1 ? '' : ' disabled');
                                    break;

                                default:
                                    btnDisplay = button + 1;
                                    btnClass = page === button ?
                                            'active' : '';
                                    break;
                            }

                            if (btnDisplay) {
                                node = jQuery('<li>', {
                                    'class': classes.sPageButton + ' ' + btnClass,
                                    'aria-controls': settings.sTableId,
                                    'tabindex': settings.iTabIndex,
                                    'id': idx === 0 && typeof button === 'string' ?
                                            settings.sTableId + '_' + button :
                                            null
                                })
                                .append(jQuery('<a>', {
                                    'href': '#'
                                })
                                    .html(btnDisplay)
                                )
                                .appendTo(container);

                                settings.oApi._fnBindAction(
                                    node, { action: button }, clickHandler
                                );
                            }
                        }
                    }
                };

                attach(
                    jQuery(host).empty().html('<ul class="pagination"/>').children('ul'),
                    buttons
                );
            };

            // TableTools Bootstrap compatibility - Required TableTools 2.1+
            if (dataTable.TableTools) {
                // Set the classes that TableTools uses to something suitable for Bootstrap
                jQuery.extend(true, dataTable.TableTools.classes, {
                    "container": "DTTT btn-group",
                    "buttons": {
                        "normal": "btn btn-default",
                        "disabled": "disabled"
                    },
                    "collection": {
                        "container": "DTTT_dropdown dropdown-menu",
                        "buttons": {
                            "normal": "",
                            "disabled": "disabled"
                        }
                    },
                    "print": {
                        "info": "DTTT_print_info"
                    },
                    "select": {
                        "row": "active"
                    }
                });

                // Have the collection use a bootstrap compatible drop down
                jQuery.extend(true, dataTable.TableTools.DEFAULTS.oTags, {
                    "collection": {
                        "container": "ul",
                        "button": "li",
                        "liner": "a"
                    }
                });
            }
        };

        var loadContacts = function () {
            contactService.loadContacts($scope.pageModel.currentPage, $scope.pageModel.byPage, $scope.pageModel.searchWord, $scope.pageModel.contactType).then(function (result) {
                var datatable = jQuery('.js-dataTable-full').dataTable().api();

                datatable.clear();
                datatable.rows.add(result);
                datatable.draw();

                console.log(result);
            });
        };

        var initValidationBootstrap = function () {
            jQuery('.js-validation-bootstrap').validate({
                ignore: [],
                errorClass: 'help-block animated fadeInDown',
                errorElement: 'div',
                errorPlacement: function (error, e) {
                    jQuery(e).parents('.form-group > div').append(error);
                },
                highlight: function (e) {
                    var elem = jQuery(e);

                    elem.closest('.form-group').removeClass('has-error').addClass('has-error');
                    elem.closest('.help-block').remove();
                },
                success: function (e) {
                    var elem = jQuery(e);

                    elem.closest('.form-group').removeClass('has-error');
                    elem.closest('.help-block').remove();
                },
                rules: {
                    'val-contact-type': {
                        required: true
                    },
                    'val-username': {
                        required: true,
                        minlength: 3
                    },
                    'val-email': {
                        required: true,
                        email: true
                    },
                    'val-vat': {
                        number: true
                    },
                    'val-phoneus': {
                        phoneUS: true
                    },
                },
                messages: {
                    'val-username': {
                        required: 'Please enter a username',
                        minlength: 'Your username must consist of at least 3 characters'
                    },
                    'val-contact-type': 'Please select a contact type!',
                    'val-email': 'Please enter a valid email address',
                    'val-vat': 'Please enter a number!',
                    'val-phoneus': 'Please enter a US phone!'

                }
            });
        };

        var resetContactModel = function () {
            $scope.contact.Name = '';
            $scope.contact.Title = '';
            $scope.contact.IsCompany = false;
            $scope.contact.ContactType = '';
            $scope.contact.Email = '';
            $scope.contact.VatNumber = '';
            $scope.contact.PhoneNumber = '';
            $scope.contact.CustomerType = '';
            $scope.contact.Zip = '';
            $scope.contact.Street = '';
            $scope.contact.City = '';
            $scope.contact.Country = '';
        }

        // Init Bootstrap Forms Validation
        initValidationBootstrap();

        //Apply table styles
        bsDataTables();
        initDataTableFull();

        loadContacts();
        resetContactModel();
    }
]);
