package wallet.bindingModel;

import javax.validation.constraints.NotNull;

public class AddressNewBindingModel {
    @NotNull
    private String password;

    public String getPassword() {
        return this.password;
    }

    public void setPassword(String password) {
        this.password = password;
    }
}
