package wallet.bindingModel;

import com.fasterxml.jackson.annotation.JsonProperty;

import javax.validation.constraints.Min;

import static wallet.entity.Constants.TRANSACTION_FEE;

public class TransactionUnsigned {
    @JsonProperty(value = "from")
    private String from;

    @JsonProperty(value = "to")
    private String to;

    @JsonProperty(value = "value")
    @Min(0)
    private long value;

    @JsonProperty(value = "senderPublicKey")
    private String senderPublicKey;

    @JsonProperty(value = "fee")
    private long fee;

    public TransactionUnsigned() {
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

    public long getFee() {
        return this.fee;
    }

    public void setFee(long fee) {
        this.fee = fee;
    }
}
