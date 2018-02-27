package wallet.controller;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import wallet.bindingModel.TransactionBindingModel;

@Controller
public class BalanceController {

    @GetMapping("/balance/create")
    public String create(Model model) {

        model.addAttribute("view", "balance/create");

        return "base-layout";
    }

    @PostMapping("/balance/create")
    public String createProcess(TransactionBindingModel transactionBindingModel) {


        return "redirect:/balance/details";
    }

    @GetMapping("/balance/details")
    public String details(Model model) {

        model.addAttribute("view", "balance/details");

        return "base-layout";
    }
}
