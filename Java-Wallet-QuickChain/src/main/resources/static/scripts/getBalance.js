

$("button").click(function(){
    $.get("demo_test.asp", function(data, status){
        console.log("Data: " + data + "\nStatus: " + status);
    });
});