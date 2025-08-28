(function ($) {
    'use strict';
    function AdminUserIndex() {
        var $this = this, locationGrid, formAddEditLocation;

        function initializeModalWithForm() {
            var date = new Date();

            var today = new Date(date.getFullYear(), date.getMonth(), date.getDate());
            var end = new Date(date.getFullYear(), date.getMonth(), date.getDate());

            $("#DOB").datepicker({
                numberOfMonths: 1,
                maxDate: end,
                onSelect: function (selected) {
                    var dt = new Date(selected);
                    dt.setDate(dt.getDate());
                    $("#DOB").datepicker("option", "minDate", dt);
                }
            });

            $('#DOB').datepicker('setDate', today);

            $('#DistrictId').change(function () {
                var districtId = $(this).val();
                var getBlocksUrl = $('#get-blocks-url').data('url'); 
                if (districtId) {
                    $.ajax({
                        url: getBlocksUrl,
                        type: 'GET',
                        data: { districtId: districtId },
                        success: function (data) {
                            var blockDropdown = $('#BlockId');
                            blockDropdown.empty();
                            blockDropdown.append($('<option>').val('').text('Select a Block'));

                            $.each(data, function (i, block) {
                                blockDropdown.append($('<option>').val(block.Id).text(block.Name));
                            });

                            // Optionally reset GramPanchayat dropdown as well
                            $('#GramPanchayatId').empty().append($('<option>').val('').text('Select a GramPanchayat'));
                        },
                        error: function () {
                            alert('Error retrieving blocks.');
                        }
                    });
                } else {
                    $('#BlockId').empty().append($('<option>').val('').text('Select a Block'));
                    $('#GramPanchayatId').empty().append($('<option>').val('').text('Select a GramPanchayat'));
                }
            });

            $('#BlockId').change(function () {
                var blockId = $(this).val();
                var getgramsUrl = $('#get-grams-url').data('url');

                if (blockId) {
                    $.ajax({
                        url: getgramsUrl,
                        type: 'GET',
                        data: { blockId: blockId },
                        success: function (data) {
                            var gramDropdown = $('#GramPanchayatId');
                            gramDropdown.empty();
                            gramDropdown.append($('<option>').val('').text('Select a GramPanchayat'));

                            $.each(data, function (i, gp) {
                                gramDropdown.append($('<option>').val(gp.Id).text(gp.Name));
                            });
                        },
                        error: function () {
                            alert('Error retrieving Gram Panchayats.');
                        }
                    });
                } else {
                    $('#GramPanchayatId').empty().append($('<option>').val('').text('Select a GramPanchayat'));
                }
            });

        }

        $this.init = function () {
            initializeModalWithForm();
        };
    }
    $(function () {
        var self = new AdminUserIndex();
        self.init();
    });
}(jQuery));