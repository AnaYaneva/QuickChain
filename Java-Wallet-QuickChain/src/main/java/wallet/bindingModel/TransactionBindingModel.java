package wallet.bindingModel;

import javax.validation.constraints.Min;
import javax.validation.constraints.NotNull;

public class TransactionBindingModel {

    @NotNull
    private String privateKey;

    @NotNull
    private String from;

    @NotNull
    private String to;

    @NotNull
    @Min(0)
    private long value;


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

    public String getPrivateKey() {
        return this.privateKey;
    }

    public void setPrivateKey(String privateKey) {
        this.privateKey = privateKey;
    }
}
