(function ($) {
    'use strict';
    function ActivityLogIndex() {
        var $this = this, locationGrid, formRequestResponse;

        function initializeGrid() {
            var locationGrid = new Global.GridAjaxHelper('#grid-index', {

                "aoColumns": [
                    { "sName": "Id" },
                    { "sName": "UserId" },
                    { "sName": "ActivityName" },
                    { "sName": "ActivityDate" },
                    { "sName": "IPAddress" },
                    { "sName": "ActivityPage" },
                    { "sName": "Remark" }
                ],
                "order": [[0, "desc"]]
            }, "ActivityLog/GetActivityLogReport");
        }

        function initializeModalWithForm() {

            $("#modal-view-user-detail").on('show.bs.modal', function (event) {
                $('#modal-view-user-detail .modal-content').load($(event.relatedTarget).prop('href'));
            });

            $("#modal-view-url-detail").on('show.bs.modal', function (event) {
                $('#modal-view-url-detail .modal-content').load($(event.relatedTarget).prop('href'));
            });

            $("#modal-view-rec-detail").on('show.bs.modal', function (event) {
                $('#modal-view-rec-detail .modal-content').load($(event.relatedTarget).prop('href'));
            });


        }

        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
        };
    }
    $(function () {
        var self = new ActivityLogIndex();
        self.init();
    });

}(jQuery));