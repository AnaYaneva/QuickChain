package wallet.entity;

public class Constants {
    public static final String URL_BALANCE="http://quickchain.azurewebsites.net/api/Address/%s/balance";

    public static final String URL_TRANSACTION="http://quickchain.azurewebsites.net/api/Transactions";

    public static final String FAUCET_PRIVATE_KEY = "e9be959ab3a9ecfad16058293535e853bb9b3ba25abb4b50c1f35ee9eb5df819";

    public static final String FAUCET_PUBLIC_KEY = "046e7acee31d8c6f74472ebc62a77c3e1fb6bc38262e9ddab8a126677dff17204f8623fbd65f6bf67aebd0ede72450dcc3324beefdfaef073b88c3cad1f4e24d3f";

    public static final String FAUCET_ADDRESS = "5b474adb3f0a01d058dca0e576afb7957be53018";

    public static final Long FAUCET_VALUE = 1000000L;

    public static final Long TRANSACTION_FEE = 0L;

    public static final Long FAUCET_TIME = 60*1000L;
}
