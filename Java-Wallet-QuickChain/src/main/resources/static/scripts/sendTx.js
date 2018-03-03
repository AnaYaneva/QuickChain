$(document).ready(function() {
    $('#send').click(function send() {
        let nodeUrl = $('#textBoxNode').val();
        let url = nodeUrl + '/transactions/send';
        let signedTransaction =  $('#signature').html();
        let from = $('#address').html();

        let to = $('#rec_address').html();

        let value = $('#amount').html();
        let senderPubKey = $('#senderPubKey').html();
        let senderSignature = signedTransaction.split(',');
        let dateCreated = $('#dateCreated').html();

        let signatureArray = [senderSignature[0], senderSignature[1]];

        let transaction = {
            from:from,
            to:to,
            value: Number(value),
            senderPubKey:senderPubKey,
            senderSignature: signatureArray,
            dateCreated:dateCreated
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
                $('#textareaSendTransactionresult').val("success")
            },
            error: function(err){
               // $('#textareaSendTransactionresult').val(err.responseJSON.error)
                $('#textareaSendTransactionresult').val("error")
            }
        })
        //$('#textareaSendTransactionresult').val(url+'\n'+dateCreated)
    });
});