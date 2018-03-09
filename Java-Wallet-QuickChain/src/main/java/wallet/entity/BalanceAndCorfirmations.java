package wallet.entity;

public class BalanceAndCorfirmations {
    private Long balance;
    private Long confirmations;

    public BalanceAndCorfirmations() {
    }

    public Long getBalance() {
        return this.balance;
    }

    public void setBalance(Long balance) {
        this.balance = balance;
    }

    public Long getConfirmations() {
        return this.confirmations;
    }

    public void setConfirmations(Long confirmations) {
        this.confirmations = confirmations;
    }
}
