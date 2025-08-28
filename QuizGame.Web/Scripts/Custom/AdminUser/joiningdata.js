(function ($) {
    'use strict';
    function JoiningData() {
        var $this = this, locationGrid, formAddEditLocation;

        function initializeGrid() {
            var filterdata = GetFilterData();
            var locationGrid = new Global.GridAjaxHelper('#grid-index', {
                "aoColumns": [
                    { "sName": "Id" },
                    { "sName": "Name" },
                    { "sName": "GuardianName" },
                    { "sName": "MobileNumber" },
                    { "sName": "AlternateMobileNumber" },
                    { "sName": "DOB" },
                    { "sName": "DistrictName" },
                    { "sName": "BlockName" },
                    { "sName": "GramPanchayatName" },
                    { "sName": "Village" },
                    { "sName": "PinCode" },
                    { "sName": "Education" },
                    { "sName": "FullAddress" },
                    { "sName": "AadharNumber" },
                    { "sName": "PanNumber" },
                    { "sName": "UniqueId" },
                    { "sName": "JoiningTime" },
                    {}
                ],
                "order": [[0, "desc"]]
            }, "user/GetJoiningData", filterdata);
        }

        function initializeModalWithForm() {
            $('#btnSearch').on('click', function () {
            setTimeout(function () {
                var table = $('#grid-index').DataTable({
                    paging: false
                });
                table.destroy();
                initializeGrid();
            }, 0);
        });
        }

        function GetFilterData() {
            var filterdata = {};
            var txtName = $('#txtName').val();
            var txtUniqueId = $('#txtUniqueId').val();

            if (txtName != '' && txtName != null) filterdata.Name = txtName;
            if (txtUniqueId != '' && txtUniqueId != null) filterdata.RefId = txtUniqueId;

            return filterdata;
        }

        $this.init = function () {
            initializeGrid();
            initializeModalWithForm();
        };
    }
    $(function () {
        var self = new JoiningData();
        self.init();
    });

}(jQuery));