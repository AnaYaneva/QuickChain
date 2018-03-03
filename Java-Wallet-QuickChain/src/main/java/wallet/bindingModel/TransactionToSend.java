package wallet.bindingModel;

import com.fasterxml.jackson.annotation.JsonFormat;
import com.fasterxml.jackson.annotation.JsonProperty;

import javax.validation.constraints.Min;
import java.util.Date;

public class TransactionToSend {

    @JsonProperty(value = "from")
    private String from;

    @JsonProperty(value = "to")
    private String to;

    @JsonProperty(value = "senderPubKey")
    private String senderPubKey;

    @JsonProperty(value = "value")
    @Min(0)
    private long value;

    @JsonProperty(value = "fee")
    private long fee;

    @JsonProperty(value = "dateCreated")
    @JsonFormat(shape = JsonFormat.Shape.STRING, pattern = "yyyy-MM-dd@HH:mm:ss.SSSZ")
    private Date dateCreated;

    @JsonProperty(value = "senderSignature")
    private String[] senderSignature;


    public TransactionToSend() {
        this.senderSignature=new String[2];
        this.dateCreated=new Date();
        this.fee=20L;
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

    public long getFee() {
        return this.fee;
    }

    public void setFee(long fee) {
        this.fee = fee;
    }

    public Date getDateCreated() {
        return this.dateCreated;
    }

    public void setDateCreated(Date dateCreated) {
        this.dateCreated = dateCreated;
    }

    public String getSenderPubKey() {
        return this.senderPubKey;
    }

    public void setSenderPubKey(String senderPubKey) {
        this.senderPubKey = senderPubKey;
    }

    public String[] getSenderSignature() {
        return this.senderSignature;
    }

    public void setSenderSignature(String[] senderSignature) {
        this.senderSignature = senderSignature;
    }

}
