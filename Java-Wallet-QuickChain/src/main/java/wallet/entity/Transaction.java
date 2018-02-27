package wallet.entity;

import java.util.Date;

public class Transaction {
    private String from;

    private String to;

    private int value;

    private int fee;

    private Date dateCreated;

    private String senderPubKey;

    private String[] senderSignature;

    private String transactionHash;

    private Integer minedInBlockIndex;

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

    public int getValue() {
        return this.value;
    }

    public void setValue(int value) {
        this.value = value;
    }

    public int getFee() {
        return this.fee;
    }

    public void setFee(int fee) {
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

    public Integer getMinedInBlockIndex() {
        return this.minedInBlockIndex;
    }

    public void setMinedInBlockIndex(Integer minedInBlockIndex) {
        this.minedInBlockIndex = minedInBlockIndex;
    }
}
