$(document).ready(function () {
    var token = $("input[name=__RequestVerificationToken]").val();
    $('[data-toggle="tooltip"]').tooltip();
    var PageSize = 0;
    var Page = 0;
    var participantsArray = new Array();
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
        participantsArray[Page] = participantPage1;
    }



    $('[name="cursor"]').click(function (event) {
        event.preventDefault();
        var cursor = event.target.id;
        $("#tbody").empty();

        if (cursor === "moveright") {
            if (Page < PageSize) {
                Page += 1;

                //do ajax
                if (participantsArray[Page] === undefined) {
                    var url = 'getparticipantsmovetonextpage';
                    var requestData = { "page": Page };
                    $.get(url, requestData, function (data) {
                        if (!data.Error) {
                            buildTableFromProcess(data.participants, false);
                        } else {
                            $("#level").html("Cannot fetch more data. Please try again leter.");
                        }
                    });
                } else {
                    buildTableFromProcess(participantsArray[Page], true);
                }
            }
        } else if (cursor === "moveleft") {
            Page -= 1;
            if (participantsArray[Page] !== undefined) {
                buildTableFromProcess(participantsArray[Page], true);
            } else {
                Page += 1;
            }
        }
        manageCursors();
    });


    function buildTableFromProcess(participantList, fromArr) {
        $('[data-toggle="tooltip"]').tooltip();
        var tbody = $("#tbody");
        if (participantList.length >= 1) {
            //set page number
            $("#page").html(Page);
            //check if this object is a new object, if it is new add to the existing account array
            if (!fromArr) {
                participantsArray[Page] = participantList;
            }

            for (var val in participantList) {
                var tr = $('<tr />').appendTo(tbody);

                var td0 = $('<td>' + participantList[val].name + '</td>').appendTo(tr);

                var td1 = $('<td>' + participantList[val].description + '</td>').appendTo(tr);

                var td2 = $('<td>' + participantList[val].accountNumber + '</td>').appendTo(tr);

                var td3 = $('<td>' + participantList[val].accountName + '</td>').appendTo(tr);

                var td4 = $('<td>' + participantList[val].dateCreated + '</td>').appendTo(tr);
            }
            $('[data-toggle="tooltip"]').tooltip();
            manageCursors();
        }
    }
});