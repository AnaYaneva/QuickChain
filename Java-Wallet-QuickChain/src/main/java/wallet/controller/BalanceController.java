package wallet.controller;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.JSONException;
import org.json.JSONObject;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import wallet.bindingModel.AddressBalanceBindingModel;
import wallet.bindingModel.TransactionBindingModel;
import wallet.entity.Balance;
import wallet.entity.Constants;
import wallet.service.crypto.JsonReader;

import java.io.IOException;
import java.net.URL;

import static wallet.entity.Constants.URL_BALANCE;

@Controller
public class BalanceController {

    private Balance balance;

    @GetMapping("/balance/create")
    public String create(Model model) {

        model.addAttribute("view", "balance/create");

        return "base-layout";
    }

    @PostMapping("/balance/create")
    public String createProcess(AddressBalanceBindingModel addressBalanceBindingModel) throws IOException {
        balance=new Balance();

        String url=String.format(URL_BALANCE,addressBalanceBindingModel.getAddress());
        System.out.println(url);

        ObjectMapper mapper = new ObjectMapper();

//JSON from URL to Object
        balance = mapper.readValue(new URL(url), Balance.class);

        return "redirect:/balance/details";
    }

    @GetMapping("/balance/details")
    public String details(Model model) {

        model.addAttribute("balance", balance);
        model.addAttribute("view", "balance/details");

        return "base-layout";
    }
}
