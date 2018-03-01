package wallet.controller;

import wallet.bindingModel.AddressNewBindingModel;
import wallet.bindingModel.AddressRestoreBindingModel;
import wallet.entity.*;
import wallet.service.AddressService;
import wallet.service.crypto.EnglishWords;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;

@Controller
public class AddressController {

    private Address address;
    private Address addressCreated;

    @Autowired
    private AddressService addressService;

    @GetMapping("/address/create")
    public String create(Model model) {

        model.addAttribute("view", "address/create");

        return "base-layout";
    }

    @PostMapping("/address/create")
    public String createProcess(AddressNewBindingModel addressNewBindingModel) throws NoSuchAlgorithmException {

        EnglishWords englishWords = new EnglishWords();
        addressCreated = new Address();

        String mnemonic = englishWords.getMnemonic();
        addressCreated.setMnemonic(mnemonic);

        String password = addressNewBindingModel.getPassword();
        byte[] privateKey=this.addressService.getPrivateKeyFromMnemonic(mnemonic.replace(" ","")+password);
        addressCreated.setPrivateKey(this.addressService.getStringFromBytes(privateKey));


        String publicKey=this.addressService.getStringFromBytes(this.addressService.getPublicKey(privateKey));
        addressCreated.setPublicKey(publicKey);


        addressCreated.setAddress(this.addressService.getAddressFromPublicKey(publicKey));

        return "redirect:/address/details";
    }

    @GetMapping("/address/details")
    public String details(Model model) throws IOException, NoSuchAlgorithmException {

        model.addAttribute("address", addressCreated);
        model.addAttribute("view", "address/details");

        return "base-layout";
    }


    @GetMapping("/address/restore")
    public String restore(Model model) {

        model.addAttribute("view", "address/restore");

        return "base-layout";
    }

    @PostMapping("/address/restore")
    public String restoreProcess(AddressRestoreBindingModel addressRestoreBindingModel) throws NoSuchAlgorithmException {

        address = new Address();
        String mnemonic = addressRestoreBindingModel.getMnemonic();
        String password = addressRestoreBindingModel.getPassword();

        address.setMnemonic(mnemonic);

        byte[] privateKeyBytes=this.addressService.getPrivateKeyFromMnemonic(mnemonic.replace(" ","")+password);
        address.setPrivateKey(this.addressService.getStringFromBytes(privateKeyBytes));

        String publicKey=this.addressService.getStringFromBytes(this.addressService.getPublicKey(privateKeyBytes));
        address.setPublicKey(publicKey);

        String addresss =this.addressService.getAddressFromPublicKey(publicKey);
        address.setAddress(addresss);

        return "redirect:/address/detailsRestored";
    }

    @GetMapping("/address/detailsRestored")
    public String detailsRestored(Model model) throws IOException, NoSuchAlgorithmException {

        model.addAttribute("address", address);
        model.addAttribute("view", "address/detailsRestored");

        return "base-layout";
    }

    /*private byte[] getPrivateKeyFromMnemonic(String mnemonic) throws NoSuchAlgorithmException {
        MessageDigest digest = MessageDigest.getInstance("SHA-256");
        byte[] hash = digest.digest(
                mnemonic.getBytes(StandardCharsets.UTF_8));
        return hash;
    }

    private String getStringFromBytes(byte[] hash){
        return new String(Hex.encode(hash));
    }

    public static byte[] getPublicKey(byte[] privateKey) {
        try {
            ECNamedCurveParameterSpec spec = ECNamedCurveTable.getParameterSpec("secp256k1");
            ECPoint pointQ = spec.getG().multiply(new BigInteger(1, privateKey));

            return pointQ.getEncoded(false);
        } catch (Exception e) {
            //StringWriter errors = new StringWriter();
            //e.printStackTrace(new PrintWriter(errors));
            //logger.error(errors.toString());
            return new byte[0];
        }
    }


    private String getAddressFromPublicKey(String publicKey) {

        byte[] r = publicKey.getBytes(StandardCharsets.UTF_8);
        RIPEMD160Digest d = new RIPEMD160Digest();
        d.update(r, 0, r.length);
        byte[] o = new byte[d.getDigestSize()];
        d.doFinal(o, 0);
        return new String(Hex.encode(o));
    }*/


    //public static void main(String[] args) throws IOException {
    //    Address address = new Address();
    //    address.setPublicKey("0431bde103b37da9818dc46b391364d455a3fc57312a48292d5a798c4b07fee917465afc067fe6259a3886fff3cb2e5cc5be930095efda9061baed663dc7f846a0");
//
    //    //RIPEMD160Digest
    //    byte[] r = address.getPublicKey().getBytes(StandardCharsets.UTF_8);
    //    RIPEMD160Digest d = new RIPEMD160Digest();
    //    d.update (r, 0, r.length);
    //    byte[] o = new byte[d.getDigestSize()];
    //    d.doFinal (o, 0);
    //    //Hex.encode (o, System.out);
    //    address.setAddress( new String(Hex.encode(o)));
//
    //    System.out.println(address.getAddress());
    //}


}
