// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7022/rgb", { withCredentials: false })
    .build();

connection
    .start()
    .then(
        function () {
            console.log("SignalR connection was established.");
        })
    .catch(
        function (err) {
            return console.error(err.toString());
        });

connection.on("RgbMessage", (rgbColor) =>
{
    let color = rgbToHex(rgbColor);

    $("#rgbColorId").css("background-color", color);
    $("#initialTitle").hide();

    $("#rgbColorId").show();


    //Just in case I will want to send data into Controller
    //$.ajax({
    //    url: "/Home/Index", // Replace with the actual URL of your MVC controller action
    //    type: "POST", // Use "GET" or other HTTP methods based on your needs
    //    dataType: "html", // Specify the expected data type of the response
    //    data: rgbColor, // Optional data to send to the server
    //    success: function () {
    //        // Handle the response data here
    //        console.log("Ajax call was Successssssssssssssssssssssssss");
    //    },
    //    error: function (xhr, status, error) {
    //        // Handle any errors that occurred during the request
    //        debugger;
    //        console.error(error);
    //    }
    //});
});

function rgbToHex(rgb) {
    var b = rgb.blue;
    var g = rgb.green;
    var r = rgb.red;
    var hex = ((r << 16) | (g << 8) | b).toString(16);
    return "#" + ("000000" + hex).slice(-6);
}




  

