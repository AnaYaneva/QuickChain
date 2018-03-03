package wallet.bindingModel;

import javax.validation.constraints.NotNull;

public class AddressBindingModel {

    @NotNull
    private String mnemonic;

    public String getMnemonic() {
        return this.mnemonic;
    }

    public void setMnemonic(String mnemonic) {
        this.mnemonic = mnemonic;
    }
}
