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

        byte[] privateKey=this.addressService.getHashFromString(mnemonic.replace(" ","")+password);
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


   /* @GetMapping("/address/restore")
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

        byte[] privateKeyBytes=this.addressService.getHashFromString(mnemonic.replace(" ","")+password);
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
    }*/
}
