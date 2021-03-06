package wallet.entity;

import com.fasterxml.jackson.annotation.JsonFormat;
import com.fasterxml.jackson.annotation.JsonProperty;

import javax.validation.constraints.Min;
import java.util.Date;

import static wallet.entity.Constants.TRANSACTION_FEE;

public class Transaction {

    @JsonProperty(value = "from")
    private String from;

    @JsonProperty(value = "to")
    private String to;

    @JsonProperty(value = "value")
    @Min(0)
    private long value;

    @JsonProperty(value = "fee")
    private long fee;

    @JsonProperty(value = "dateCreated")
    @JsonFormat(shape = JsonFormat.Shape.STRING, pattern = "yyyy-MM-dd@HH:mm:ss.SSSZ")
    private Date dateCreated;

    @JsonProperty(value = "senderPubKey")
    private String senderPubKey;

    @JsonProperty(value = "senderSignature")
    private String[] senderSignature;


    private String transactionHash;


    private Long minedInBlockIndex;


    private boolean transferSuccessful;

    public Transaction() {
        this.senderSignature=new String[2];
        this.transferSuccessful=false;
        this.dateCreated=new Date();
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

    public String getTransactionHash() {
        return this.transactionHash;
    }

    public void setTransactionHash(String transactionHash) {
        this.transactionHash = transactionHash;
    }

    public Long getMinedInBlockIndex() {
        return this.minedInBlockIndex;
    }

    public void setMinedInBlockIndex(Long minedInBlockIndex) {
        this.minedInBlockIndex = minedInBlockIndex;
    }
}
