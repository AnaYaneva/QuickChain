package wallet.entity;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.Date;

public class Transaction {

    @JsonProperty("from")
    private String from;

    @JsonProperty("to")
    private String to;

    @JsonProperty("value")
    private long value;

    @JsonProperty("fee")
    private long fee;

    @JsonProperty("dateCreated")
    private Date dateCreated;

    @JsonProperty("senderPubKey")
    private String senderPubKey;

    @JsonProperty("senderSignature")
    private String[] senderSignature;

    @JsonProperty("transactionHash")
    private String transactionHash;

    @JsonProperty("from")
    private Long minedInBlockIndex;

    @JsonProperty("from")
    private boolean transferSuccessful;

    public Transaction() {
        this.senderSignature=new String[2];
        this.transferSuccessful=false;
        this.dateCreated=new Date();
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
