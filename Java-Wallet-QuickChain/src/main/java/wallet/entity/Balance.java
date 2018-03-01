package wallet.entity;

public class Balance {

    private String address;

    private Long balance;

    private Long block;

    public Balance() {
    }

    public String getAddress() {
        return this.address;
    }

    public void setAddress(String address) {
        this.address = address;
    }

    public Long getBalance() {
        return this.balance;
    }

    public void setBalance(Long balance) {
        this.balance = balance;
    }

    public Long getBlock() {
        return this.block;
    }

    public void setBlock(Long block) {
        this.block = block;
    }
}
