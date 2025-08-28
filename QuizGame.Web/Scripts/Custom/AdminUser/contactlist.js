(function ($) {
    'use strict';
    function contactList() {
        var $this = this, locationGrid, formAddEditLocation;

        function initializeGrid() {

            var locationGrid = new Global.GridAjaxHelper('#grid-index', {
                "aoColumns": [
                    { "sName": "Id" },
                    { "sName": "Name" },
                    { "sName": "MobileNumber" },
                    { "sName": "Message" },
                    { "sName": "Date" }
                ],
                "order": [[0, "desc"]]
            }, "user/GetContactList", function () {
            });
        }

        $this.init = function () {
            initializeGrid();
        };
    }
    $(function () {
        var self = new contactList();
        self.init();
    });

}(jQuery));