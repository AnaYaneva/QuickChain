package wallet.bindingModel;

import javax.validation.constraints.NotNull;

public class TransactionBindingModel {

    @NotNull
    private String from;

    @NotNull
    private String to;

    @NotNull
    private long value;

    @NotNull
    private long fee;

    @NotNull
    private String dateCreated;

    @NotNull
    private String senderPubKey;

    @NotNull
    private String[] senderSignature;

    @NotNull
    private String transactionHash;

    @NotNull
    private String minedInBlockIndex;

    public TransactionBindingModel() {
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

    public String getDateCreated() {
        return this.dateCreated;
    }

    public void setDateCreated(String dateCreated) {
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

    public String getMinedInBlockIndex() {
        return this.minedInBlockIndex;
    }

    public void setMinedInBlockIndex(String minedInBlockIndex) {
        this.minedInBlockIndex = minedInBlockIndex;
    }
}
