$(document).ready(function () {
    var token = $("input[name=__RequestVerificationToken]").val();
    $('[data-toggle="tooltip"]').tooltip();
    var PageSize = 0;
    var Page = 0;
    var accountsArray = new Array();
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
        accountsArray[Page] = accountPage1;
    }



    $('[name="cursor"]').click(function (event) {
        event.preventDefault();
        var cursor = event.target.id;
        $("#tbody").empty();

        if (cursor === "moveright") {
            if (Page < PageSize) {
                Page += 1;

                //do ajax
                if (accountsArray[Page] === undefined) {
                    var url = 'getaccountsmovetonextpage';
                    var requestData = { "page": Page };
                    $.get(url, requestData, function (data) {
                        if (!data.Error) {
                            buildTableFromProcess(data.accounts, false);
                        } else {
                            $("#level").html("Cannot fetch more data. Please try again leter.");
                        }
                    });
                } else {
                    buildTableFromProcess(accountsArray[Page], true);
                }
            }
        } else if (cursor === "moveleft") {
            Page -= 1;
            if (accountsArray[Page] !== undefined) {
                buildTableFromProcess(accountsArray[Page], true);
            } else {
                Page += 1;
            }
        }
        manageCursors();
    });


    function buildTableFromProcess(accountList, fromArr) {
        $('[data-toggle="tooltip"]').tooltip();

        var tbody = $("#tbody");
        if (accountList.length >= 1) {
            //set page number
            $("#page").html(Page);
            //check if this object is a new object, if it is new add to the existing account array
            if (!fromArr) {
                accountsArray[Page] = accountList;
            }

            for (var val in accountList) {
                var tr = $('<tr />').appendTo(tbody);

                var td0 = $('<td>' + accountList[val].accountName + '</td>').appendTo(tr);

                var td1 = $('<td>' + accountList[val].accountNumber + '</td>').appendTo(tr);

                var td2 = $('<td>' + accountList[val].bankName + '</td>').appendTo(tr);

                var td3 = $('<td>' + accountList[val].dateCreated + '</td>').appendTo(tr);
            }
            $('[data-toggle="tooltip"]').tooltip();
            manageCursors();
        }
    }
});