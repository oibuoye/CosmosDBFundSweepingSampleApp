$(document).ready(function () {
    var token = $("input[name=__RequestVerificationToken]").val();
    $('[data-toggle="tooltip"]').tooltip();
    var PageSize = 0;
    var Page = 0;
    var rulesArray = new Array();
    Page = 1;
    PageSize = parseInt($("#pageSize").text(), 10);
    getFirstPageData();
    manageCursors();


    function manageCursors() {
        if (Page < PageSize && PageSize > 1) {
            $("#moveright").removeClass("disablelink");
            $("#moveright").prop("href", "#");
        } else {
            $("#moveright").addClass("disablelink");
            $("#moveright").prop("href", "");
        }

        if (Page > 1) {
            $("#moveleft").removeClass("disablelink");
            $("#moveleft").prop("href", "#");
        } else {
            $("#moveleft").addClass("disablelink");
            $("#moveleft").prop("href", "");
        }
    }


    function getFirstPageData() {
        Page = 1;
        rulesArray[Page] = rulePage1;
    }



    $('[name="cursor"]').click(function (event) {
        event.preventDefault();
        var cursor = event.target.id;
        $("#tbody").empty();

        if (cursor === "moveright") {
            if (Page < PageSize) {
                Page += 1;

                //do ajax
                if (rulesArray[Page] === undefined) {
                    var url = 'getrulesmovetonextpage';
                    var requestData = { "page": Page };
                    $.get(url, requestData, function (data) {
                        if (!data.Error) {
                            buildTableFromProcess(data.rules, false);
                        } else {
                            $("#level").html("Cannot fetch more data. Please try again leter.");
                        }
                    });
                } else {
                    buildTableFromProcess(rulesArray[Page], true);
                }
            }
        } else if (cursor === "moveleft") {
            Page -= 1;
            if (rulesArray[Page] !== undefined) {
                buildTableFromProcess(rulesArray[Page], true);
            } else {
                Page += 1;
            }
        }
        manageCursors();
    });


    function buildTableFromProcess(ruleList, fromArr) {
        $('[data-toggle="tooltip"]').tooltip();
        var tbody = $("#tbody");
        if (ruleList.length >= 1) {
            //set page number
            $("#page").html(Page);
            //check if this object is a new object, if it is new add to the existing account array
            if (!fromArr) {
                rulesArray[Page] = ruleList;
            }

            for (var val in ruleList) {
                var tr = $('<tr />').appendTo(tbody);

                var td0 = $('<td>' + ruleList[val].ruleName + '</td>').appendTo(tr);

                var td1 = $('<td>' + ruleList[val].ruleCode + '</td>').appendTo(tr);

                var td2 = $('<td>' + ruleList[val].description + '</td>').appendTo(tr);

                var td3 = $('<td>' + ruleList[val].sourceAccountName + "|" + ruleList[val].sourceAccountNumber + '</td>').appendTo(tr);

                var td4 = $('<td>' + ruleList[val].settlementTime + '</td>').appendTo(tr);

                var td5 = $('<td>' + ruleList[val].settlementModeId + '</td>').appendTo(tr);

                var td6 = $('<td>' + ruleList[val].dateCreated + '</td>').appendTo(tr);

                var td7 = $('<td>' + '<a href="participants/' + ruleList[val].id + '" name = "participants" class="btn btn-sm btn-info">view participants</a></td>').appendTo(tr);
            }
            $('[data-toggle="tooltip"]').tooltip();
            manageCursors();
        }
    }
});