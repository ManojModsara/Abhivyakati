(function ($) {
    'use strict';
    function AdminRoleIndex() {
        var $this = this, locationGrid, formAddEditRole;

        function initGridControlsWithEvents() {
            $('.switchBox').each(function (index, element) {
                if ($(element).data('bootstrapSwitch')) {
                    $(element).off('switch-change');
                    $(element).bootstrapSwitch('destroy');
                }

                $(element).bootstrapSwitch()
                .on('switch-change', function () {
                    var switchElement = this;
                    $.get(Global.DomainName + 'adminrole/active', { id: this.value }, function (result) {
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
                    { "sName": "RoleName" },
                    {
                    }
                ],
                "order": [[0, "asc"]]
            }, "role/getroles");
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
            initGridControlsWithEvents();
        };
    }
    $(function () {
        var self = new AdminRoleIndex();
        self.init();
    });

}(jQuery));