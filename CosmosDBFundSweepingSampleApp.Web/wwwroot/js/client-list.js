$(document).ready(function () {
    var token = $("input[name=__RequestVerificationToken]").val();
    $('[data-toggle="tooltip"]').tooltip();
    var PageSize = 0;
    var Page = 0;
    var clientsArray = new Array();
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
        clientsArray[Page] = clientPage1;
    }



    $('[name="cursor"]').click(function (event) {
        event.preventDefault();
        var cursor = event.target.id;
        $("#tbody").empty();

        if (cursor === "moveright") {
            if (Page < PageSize) {
                Page += 1;

                //do ajax
                if (clientsArray[Page] === undefined) {
                    var url = 'getclientsmovetonextpage';
                    var requestData = { "page": Page };
                    $.get(url, requestData, function (data) {
                        if (!data.Error) {
                            buildTableFromProcess(data.clients, false);
                        } else {
                            $("#level").html("Cannot fetch more data. Please try again leter.");
                        }
                    });
                } else {
                    buildTableFromProcess(clientsArray[Page], true);
                }
            }
        } else if (cursor === "moveleft") {
            Page -= 1;
            if (clientsArray[Page] !== undefined) {
                buildTableFromProcess(clientsArray[Page], true);
            } else {
                Page += 1;
            }
        }
        manageCursors();
    });


    function buildTableFromProcess(clientList, fromArr) {
        $('[data-toggle="tooltip"]').tooltip();

        var tbody = $("#tbody");
        if (clientList.length >= 1) {
            //set page number
            $("#page").html(Page);
            //check if this object is a new object, if it is new add to the existing account array
            if (!fromArr) {
                clientsArray[Page] = clientList;
            }

            for (var val in clientList) {
                var tr = $('<tr />').appendTo(tbody);

                var td0 = $('<td>' + clientList[val].name + '</td>').appendTo(tr);

                var td1 = $('<td>' + clientList[val].clientCode + '</td>').appendTo(tr);

                var td2 = $('<td>' + clientList[val].secretKey + '</td>').appendTo(tr);

                var td3 = $('<td>' + clientList[val].dateCreated + '</td>').appendTo(tr);
            }
            $('[data-toggle="tooltip"]').tooltip();
            manageCursors();
        }
    }
});