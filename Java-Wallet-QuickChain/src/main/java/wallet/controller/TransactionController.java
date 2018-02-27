package wallet.controller;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import wallet.bindingModel.AddressBindingModel;
import wallet.bindingModel.TransactionBindingModel;
import wallet.entity.Address;
import wallet.entity.Transaction;

import java.security.NoSuchAlgorithmException;

@Controller
public class TransactionController {

    private Transaction transaction;

    @GetMapping("/transaction/create")
    public String create(Model model) {

        model.addAttribute("view", "transaction/create");

        return "base-layout";
    }

    @PostMapping("/transaction/create")
    public String createProcess(TransactionBindingModel transactionBindingModel) {


        return "redirect:/transaction/details";
    }

    @GetMapping("/transaction/details")
    public String details(Model model) {

        Transaction transaction=new Transaction();

        transaction.setFrom("senderAddress");
        transaction.setTo("receiverAddress");
        transaction.setValue(10);

        model.addAttribute("transaction", transaction);
        model.addAttribute("view", "transaction/details");

        return "base-layout";
    }

    @PostMapping("/transaction/details")
    public String send(TransactionBindingModel transactionBindingModel) {


        return "redirect:/transaction/send";
    }

    @GetMapping("/transaction/send")
    public String sendMessage(Model model) {

        model.addAttribute("view", "transaction/send");

        return "base-layout";
    }
}
