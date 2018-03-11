package wallet.bindingModel;


import com.fasterxml.jackson.annotation.JsonProperty;
import javax.validation.constraints.Min;
import static wallet.entity.Constants.TRANSACTION_FEE;

public class TransactionToSend {

    @JsonProperty(value = "from")
    private String from;

    @JsonProperty(value = "to")
    private String to;

    @JsonProperty(value = "value")
    @Min(0)
    private long value;

    @JsonProperty(value = "senderPublicKey")
    private String senderPublicKey;

    @JsonProperty(value = "transactionIdentifier")
    private String transactionIdentifier;

    @JsonProperty(value = "fee")
    private long fee;

    @JsonProperty(value = "signatureR")
    private String signatureR;

    @JsonProperty(value = "signatureS")
    private String signatureS;

    @JsonProperty(value = "transactionHash")
    private String transactionHash;

    public TransactionToSend() {
        this.fee=TRANSACTION_FEE;
    }

    public String getFrom() {
        return this.from;
    }

    public void setFrom(String from) {
        this.from = from;
    }

    public String getTo() {
        return this.to;
    }

    public void setTo(String to) {
        this.to = to;
    }

    public long getValue() {
        return this.value;
    }

    public void setValue(long value) {
        this.value = value;
    }

    public String getSenderPublicKey() {
        return this.senderPublicKey;
    }

    public void setSenderPublicKey(String senderPublicKey) {
        this.senderPublicKey = senderPublicKey;
    }

    public String getTransactionIdentifier() {
        return this.transactionIdentifier;
    }

    public void setTransactionIdentifier(String transactionIdentifier) {
        this.transactionIdentifier = transactionIdentifier;
    }

    public long getFee() {
        return this.fee;
    }

    public void setFee(long fee) {
        this.fee = fee;
    }

    public String getSignatureR() {
        return this.signatureR;
    }

    public void setSignatureR(String signatureR) {
        this.signatureR = signatureR;
    }

    public String getSignatureS() {
        return this.signatureS;
    }

    public void setSignatureS(String signatureS) {
        this.signatureS = signatureS;
    }

    public String getTransactionHash() {
        return this.transactionHash;
    }

    public void setTransactionHash(String transactionHash) {
        this.transactionHash = transactionHash;
    }
}
