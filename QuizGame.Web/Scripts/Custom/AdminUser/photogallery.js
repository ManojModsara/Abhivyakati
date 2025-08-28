(function ($) {
    'use strict';
    function PhotoGalleryList() {
        var $this = this, locationGrid, formAddEditLocation;

        function initializeGrid() {

            var locationGrid = new Global.GridAjaxHelper('#grid-index', {
                "aoColumns": [
                    { "sName": "Id" },
                    { "sName": "Image" },
                    { "sName": "UploadedDate" },
                    {}
                ],
                "aoColumnDefs": [
                    { 'visible': false, 'aTargets': [0] }

                ],
                "order": [[0, "desc"]]
            }, "user/GetPhotoGalleryList", function () {
            });
        }

        function initializeModalWithForm() {
            debugger;
            //$("#modal-delete-photogalleryimage").on('hidden.bs.modal', function () {
            //    $(this).removeData('bs.modal');
            //});
            $("#modal-delete-photogalleryimage").on('show.bs.modal', function (event) {
                $('#modal-delete-photogalleryimage .modal-content').load($(event.relatedTarget).prop('href'));
            });
        }

        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
        };
    }
    $(function () {
        var self = new PhotoGalleryList();
        self.init();
    });
}(jQuery));