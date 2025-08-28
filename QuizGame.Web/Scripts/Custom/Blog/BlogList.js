(function ($) {
    'use strict';
    function BlogList() {
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
                        
                        $.get(Global.DomainName + 'Blog/Active', { id: this.value }, function (result) {
                            
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
        $this.init = function () {

            initGridControlsWithEvents();

        };
    }

    $(function () {

        var self = new BlogList();
        self.init();


    });


}(jQuery));
$(document).ready($('.test55').css("display", "none"));