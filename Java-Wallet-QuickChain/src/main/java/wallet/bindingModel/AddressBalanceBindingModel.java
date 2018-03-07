package wallet.bindingModel;

import javax.validation.constraints.NotNull;

public class AddressBalanceBindingModel {
    @NotNull
    private String address;

    public String getAddress() {
        return this.address;
    }

    public void setAddress(String address) {
        this.address = address;
    }
}
