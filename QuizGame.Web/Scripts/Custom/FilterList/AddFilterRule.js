$(document).ready(function () {
    initGridControlsWithEvents();
})
function initGridControlsWithEvents() {

    $("#btnAdd").on("click", function () {
        var Id = $("#Id").val();
        var RuleId = $("#RuleId :Selected").val();
        var space = $("#ddspace :Selected").val();
        var Duplicated = $("#ddDuplicated :Selected").val();
        var fixlenth = $("#txtfixlenth").val();
        var startwith = $("#txtstart").val();
        var endtwith = $("#txtend").val();
        debugger
        if (Id == '' || Id == undefined || Id == '0') {
            Id = 0;
        }
        if (RuleId == '' || RuleId == undefined) {

            alertify.error("Select Rule ");
        }
        //else if (space == '' || space == undefined) {

        //    alertify.error("Select space");
        //}
        //else if (Duplicated == '' || Duplicated == undefined) {

        //    alertify.error("Select Duplicated");
        //}
        //else if (fixlenth == '' || fixlenth == undefined) {

        //    alertify.error("fill fixlenth");
        //}
        //else if (startwith == '' || startwith == undefined) {

        //    alertify.error("fill startwith!!");
        //}
        //else if (endtwith == '' || endtwith == undefined) {

        //    alertify.error("fill endtwith!!");
        //}
        else {
            var cObj = {};
            cObj.Id = Id;
            cObj.RuleId = RuleId;
            cObj.IsAllowSpace = space;
            cObj.IsAllowDuplicated = Duplicated;
            cObj.FixLength = fixlenth;
            cObj.StartWith = startwith;
            cObj.EndWith = endtwith;
            $.post(Global.DomainName + 'FilterList/AddFilterRule', { model: cObj }, function (result) {
                if (result == "1") {

                    alertify.success("Data Saved Successfully.");
                    window.location.href = "/FilterList/Index";
                }
                else if (result == "2") {
                    alertify.success("Success! Data has been Updated.");
                    window.location.href = "/FilterList/Index";
                }
                else {

                    alertify.error(result);
                }

            });

        }


    });

    $('.switchBox').each(function (index, element) {
        if ($(element).data('bootstrapSwitch')) {
            $(element).off('switch-change');
            $(element).bootstrapSwitch('destroy');
        }

        $(element).bootstrapSwitch()
            .on('switch-change', function () {
                var switchElement = this;

                $.get(Global.DomainName + 'Package/Active', { id: this.value }, function (result) {
                    if (result == "1") {
                        $(switchElement).bootstrapSwitch('toggleState', true);
                        alertify.error('Error occurred.');
                    }
                    if (result == "2") {
                        alertify.success('Status Updated.');
                    }
                    else {
                        $(switchElement).bootstrapSwitch('toggleState', true);
                        alertify.error(result);

                    }
                });
            });
    });

}


