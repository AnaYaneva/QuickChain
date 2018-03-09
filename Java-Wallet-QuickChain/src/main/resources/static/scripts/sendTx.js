//$(document).ready(function() {
function startApp(){
    $('#send').click(function send() {
        let from = $('#address').html();
        let to = $('#rec_address').html();
        let value = $('#amount').html();
        let senderPubKey = $('#senderPubKey').html();
        let transactionIdentifier = $('#transactionIdentifier').html();
        let fee = $('#fee').html();
        let signatureR = $('#signatureR').html();
        let signatureS = $('#signatureS').html();
        let transactionHash = $('#transactionHash').html();
        console.log(transactionHash);
        let url = "http://quickchain.azurewebsites.net/api/Transactions";
        //let url = "http://quickchain.azurewebsites.net/api/Transactions/"+transactionHash+"/send";

        let transaction = {
            from:from,
            to:to,
            value: Number(value),
            senderPubKey:senderPubKey,
            transactionIdentifier:transactionIdentifier,
            fee:Number(fee),
            signatureR: signatureR,
            signatureS:signatureS,
            transactionHash:transactionHash
        };

        let jsonRequest = JSON.stringify(transaction);

        console.log(jsonRequest);

        $.ajax({
            url: url,
            method: 'POST',
            dataType: 'json',
            data: jsonRequest,
            contentType: "application/json",
            success: function(data) {
                //$('#textareaSendTransactionresult').val(data.responseJSON.transactionHash)
                $('#textareaSendTransactionresult').val(data.responseJSON.transactionHash)
            },
            error: function(err){
               // $('#textareaSendTransactionresult').val(err.responseJSON.error)
                $('#textareaSendTransactionresult').val(err.responseJSON.error)
            }
        })
        //$('#textareaSendTransactionresult').val(url+'\n'+dateCreated)
    });

    $("#buttonGetBalance").click(function(){
        let addressGetBalance = $("#addressGetBalance").val();
        console.log(addressGetBalance);
        let urlBalance="http://quickchain.azurewebsites.net/api/Address/"+addressGetBalance+"/balance";
        $.getJSON( urlBalance, function( json ) {
            console.log("JSON Data: " + json.address );
        });

    });
};