package wallet.entity;

public class Balance {

    private BalanceAndCorfirmations confirmedBalance;

    private String address;

    private BalanceAndCorfirmations pendingBalance;

    private BalanceAndCorfirmations lastMinedBalance;

    public Balance() {
    }

    public String getAddress() {
        return this.address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public BalanceAndCorfirmations getConfirmedBalance() {
        return this.confirmedBalance;
    }

    public void setConfirmedBalance(BalanceAndCorfirmations confirmedBalance) {
        this.confirmedBalance = confirmedBalance;
    }

    public BalanceAndCorfirmations getPendingBalance() {
        return this.pendingBalance;
    }

    public void setPendingBalance(BalanceAndCorfirmations pendingBalance) {
        this.pendingBalance = pendingBalance;
    }

    public BalanceAndCorfirmations getLastMinedBalance() {
        return this.lastMinedBalance;
    }

    public void setLastMinedBalance(BalanceAndCorfirmations lastMinedBalance) {
        this.lastMinedBalance = lastMinedBalance;
    }
}
