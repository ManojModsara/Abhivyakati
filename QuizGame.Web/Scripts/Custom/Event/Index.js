(function ($) {
    'use strict';
    function Events() {
        var $this = this, locationGrid, formAddEditLocation;


        function initializeGrid() {

            var locationGrid = new Global.GridAjaxHelper('#grid-index', {
                "aoColumns": [
                    { "sName": "Id" },
                    { "sName": "Title" },
                    { "sName": "EventDate" },
                    { "sName": "Description" },
                    { "sName": "Image" },
                    { "sName": "AddedDate" },
                    { "sName": "AddedBy" },
                    { "sName": "UpdatedDate" },
                    { "sName": "UpdatedBy" },
                    {}
                ],
                "order": [[0, "desc"]]
            }, "Event/GetEvents"
            );
        }

        function initializeModalWithForm() {
            $("#modal-delete-event").on('show.bs.modal', function (event) {
                $('#modal-delete-event .modal-content').load($(event.relatedTarget).prop('href'));
            });
        }

        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
        };
    }
    $(function () {
        var self = new Events();
        self.init();
    });

}(jQuery));