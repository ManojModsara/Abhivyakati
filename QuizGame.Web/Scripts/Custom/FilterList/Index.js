(function ($) {
    'use strict';
    function AdminUserIndex() {
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
                        $.get(Global.DomainName + 'FilterList/Active', { id: this.value }, function (result) {
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
                    { "sName": "Action" },
                    { "sName": "RuleList.RuleName" },
                    { "sName": "FixLength" },
                    { "sName": "StartWith" },
                    { "sName": "EndWith" },
                    { "sName": "Space" },
                    { "sName": "Duplicated" },
                    {
                        "sName": "IsActive", "mRender": function (data, type, row) {
                            if (type === 'display') {
                                if (data) {
                                    return '<input type="checkbox" checked="checked" class="switchBox switch-small simple" value="' + row[0] + '" ' + (row[8] ? "" : " ") + '/>';
                                }
                                else {
                                    return '<input type="checkbox" class="switchBox switch-small simple"  value="' + row[0] + '" ' + (row[8] ? "" : " ") + '/>';
                                }
                            }
                            return data;
                        }
                    },

                ],
                "aoColumnDefs": [
                    { 'bSortable': false, 'aTargets': [0] },

                ],
                "order": [[0, "desc"]]
            }, "FilterList/GetFilterlist", function () {
                initGridControlsWithEvents();
            });
        }

        function initializeModalWithForm() {

            $("#modal-add-edit-adminrole").on('show.bs.modal', function (event) {
                $('#modal-add-edit-adminrole .modal-content').load($(event.relatedTarget).prop('href'));
            });

            $("#modal-delete-adminrole").on('show.bs.modal', function (event) {
                $('#modal-delete-adminrole .modal-content').load($(event.relatedTarget).prop('href'));
            });


        }

        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
        };
    }
    $(function () {
        var self = new AdminUserIndex();
        self.init();
    });

}(jQuery));