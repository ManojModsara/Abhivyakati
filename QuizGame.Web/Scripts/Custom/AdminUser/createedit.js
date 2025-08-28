
(function ($) {
    'use strict';
    function CreateEdit() {
        var $this = this, locationGrid, formAddEditLocation, uploadObj;

        function initModalControlsWithEvents() {
            // set select2
            $('select#RoleId').select2();


            $('select#RoleId').change(function () {

                var value = $(this).val();
                if (value == '' || value == null) {
                    value = "0";
                }
                else {
                    $(this).closest('div').find('div.select2-container').removeClass("error").addClass("valid");
                    $(this).closest('div').find('label.error').hide();
                    $(this).removeClass("error").addClass("valid");
                    //$.get(Global.DomainName + 'user/UserRoleList', { id: this.value }, function (result) {
                    //    if (!result) {
                    //        $(switchElement).bootstrapSwitch('toggleState', true);
                    //        alertify.error('Error occurred.');
                    //    }
                    //    else {
                    //        $.each(result, function () {
                    //            alertify.success('Status Updated.');
                    //            alert(JSON.stringify(result));
                    //            $("#Parentid").append($("<option></option>").val(JSON.stringify(result.Error)).html(JSON.stringify(result.Message)));
                    //        })  
                    //    }
                    //});
                }
            });
            $('select#Parentid').select2();


            $('select#Parentid').change(function () {

                var value = $(this).val();
                if (value == '' || value == null) {
                    value = "0";
                }
                else {
                    $(this).closest('div').find('div.select2-container').removeClass("error").addClass("valid");
                    $(this).closest('div').find('label.error').hide();
                    $(this).removeClass("error").addClass("valid");
                }
            });
        }

        $this.init = function () {
            initModalControlsWithEvents();
            formAddEditLocation = new Global.FormHelper($("#model-createedit-adminuser"), { updateTargetId: "validation-summary", validateSettings: { ignore: '' } });
        };
    }
    $(function () {
        var self = new CreateEdit();
        self.init();
    });

}(jQuery));