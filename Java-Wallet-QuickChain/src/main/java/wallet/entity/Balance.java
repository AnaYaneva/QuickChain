package wallet.entity;

public class Balance {

    private String address;

    private Long confirmedBalance;

    private Long lastMinedBalance;

    private Long pendingBalance;

    public Balance() {
    }

    public String getAddress() {
        return this.address;
    }

    public void setAddress(String address) {
        this.address = address;
    }


}
