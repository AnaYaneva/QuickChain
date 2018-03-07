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
import wallet.service.crypto.JsonReader;

import java.io.IOException;

@Controller
public class BalanceController {

    private Balance balance;

    @GetMapping("/balance/create")
    public String create(Model model) {

        model.addAttribute("view", "balance/create");

        return "base-layout";
    }

    @PostMapping("/balance/create")
    public String createProcess(AddressBalanceBindingModel addressBalanceBindingModel) throws IOException, JSONException {
        balance=new Balance();
        balance.setAddress(addressBalanceBindingModel.getAddress());

        String url="http://quickchain.azurewebsites.net/api/Address/"+addressBalanceBindingModel.getAddress()+"/balance";
        System.out.println(url);

        JSONObject json=JsonReader.readJsonFromUrl(url);

        System.out.println(json);

        return "redirect:/balance/details";
    }

    @GetMapping("/balance/details")
    public String details(Model model) {

//GET /api/Address/{address}/balance

        model.addAttribute("view", "balance/details");

        return "base-layout";
    }
}
