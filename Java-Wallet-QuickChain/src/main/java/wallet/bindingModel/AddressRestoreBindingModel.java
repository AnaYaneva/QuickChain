package wallet.bindingModel;

import javax.validation.constraints.NotNull;

public class AddressRestoreBindingModel {
    @NotNull
    private String password;

    @NotNull
    private String mnemonic;

    public String getMnemonic() {
        return this.mnemonic;
    }

    public void setMnemonic(String mnemonic) {
        this.mnemonic = mnemonic;
    }

    public String getPassword() {
        return this.password;
    }

    public void setPassword(String password) {
        this.password = password;
    }
}
