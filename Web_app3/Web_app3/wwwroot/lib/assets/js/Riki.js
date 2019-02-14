// Write your JavaScript code.
$(function () {
    

    function getNotification(){
        $.ajax({
            url: "/Order/GetNotification",
            method: "GET",
            success: function(result){

                if(result!=0){
                    $("#reports").html(result);
                    $("#reports").show();
                } else {
                    $("#reports").html();
                    $("#reports").hide();                  
                }

                console.log(result);
            },
            error: function(error){
                console.log(error);
            }
        });
    }

    getNotification();

    var connection = new signalR.HubConnectionBuilder()
        .configureLogging(signalR.LogLevel.Debug)
        .withUrl("/reportsPublisher", {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets
        })
        .build();

    connection.on("displayNotification",()=>{
        getNotification();
        toastr.options.positionClass = 'toast-bottom-full-width';
        toastr.info("Provjeri narudžbe");
    });

    connection.start().catch(err => console.error(err.toString()));
});

