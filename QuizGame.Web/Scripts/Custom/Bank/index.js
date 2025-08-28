(function ($) {
    'use strict';
    function BankIndex() {
        var $this = this, locationGrid, formAddEditLocation;

        function initGridControlsWithEvents() {
            $('.switchBox').each(function (index, element) {
                if ($(element).data('bootstrapSwitch')) {
                    $(element).off('switch-change');
                    $(element).bootstrapSwitch('destroy');
                }

                $(element).bootstrapSwitch()
                    .on('switch-change', function () {
                        var switchElement = this;
                        $.get(Global.DomainName + 'BankAccount/active', { id: this.value }, function (result) {
                            if (!result) {
                                $(switchElement).bootstrapSwitch('toggleState', true);
                                alertify.error('Error occurred.');
                            }
                            else {
                                alertify.success('Status Updated.');
                            }
                        });
                    });
            });
        }

        function initializeGrid() {

            var locationGrid = new Global.GridAjaxHelper('#grid-index', {
                "aoColumns": [
                    { "sName": "Id" },
                    { "sName": "BankName" },
                    { "sName": "AccountNo" },
                    { "sName": "IFSCCode" },
                    { "sName": "HolderName" },
                    { "sName": "UpiAdress" },
                    { "sName": "BranchName" },
                    { "sName": "BranchAddress" },
                    { "sName": "Remark" },
                    { "sName": "AccountType" },
                    {
                        "sName": "IsActive", "mRender": function (data, type, row) {
                            if (type === 'display') {
                                if (data) {
                                    return '<input type="checkbox" checked="checked" class="switchBox switch-small simple" value="' + row[0] + '" ' + (row[10] ? "" : " ") + '/>';
                                }
                                else {
                                    return '<input type="checkbox" class="switchBox switch-small simple"  value="' + row[0] + '" ' + (row[10] ? "" : " ") + '/>';
                                }
                            }
                            return data;
                        }
                    },
                    { "sName": "AddedDate" },
                    { "sName": "UpdatedDate" },
                    {}
                ],
                //"aoColumnDefs": [
                //    { 'bSortable': false, 'aTargets': [3, 4] },
                //],
                "order": [[0, "desc"]]
            }, "BankAccount/GetCompanyBanks", function () {
                initGridControlsWithEvents();
            });
        }

        function initializeModalWithForm() {
            $("#modal-delete-bank").on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal');
            });
        }

        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
        };
    }
    $(function () {
        var self = new BankIndex();
        self.init();
    });

}(jQuery));