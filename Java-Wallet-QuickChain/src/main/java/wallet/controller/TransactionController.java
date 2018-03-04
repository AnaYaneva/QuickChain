package wallet.controller;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.fasterxml.jackson.databind.ObjectMapper;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.methods.CloseableHttpResponse;

import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import wallet.bindingModel.TransactionBindingModel;
import wallet.bindingModel.TransactionToSend;
import wallet.bindingModel.TransactionUnsigned;
import wallet.entity.Transaction;
import wallet.service.AddressService;
import wallet.service.crypto.Json;
import wallet.service.crypto.Secp256k1;

import javax.xml.bind.DatatypeConverter;
import java.io.IOException;
import java.security.NoSuchAlgorithmException;


@Controller
public class TransactionController {

    private ObjectMapper mapper = new ObjectMapper();

    private TransactionToSend transactionToSend;

    private TransactionUnsigned transactionUnsigned;

    private String message;

    private String transactionSignature;

    private String jsonString;

    @Autowired
    private AddressService addressService;

    @GetMapping("/transaction/error")
    public String error(Model model) {

        model.addAttribute("error", message);
        model.addAttribute("view", "transaction/error");

        return "base-layout";
    }


    @GetMapping("/transaction/create")
    public String create(Model model) {

        model.addAttribute("view", "transaction/create");

        return "base-layout";
    }

    @PostMapping("/transaction/create")
    public String createProcess(TransactionBindingModel transactionBindingModel) throws JsonProcessingException, NoSuchAlgorithmException {
        transactionUnsigned =new TransactionUnsigned();

        transactionUnsigned.setFrom(transactionBindingModel.getFrom());
        transactionUnsigned.setTo(transactionBindingModel.getTo());
        transactionUnsigned.setValue(transactionBindingModel.getValue());

       String publicKey=this.addressService.getStringFromBytes
               (this.addressService.getPublicKey
                       (transactionBindingModel.getPrivateKey()));
       String address=this.addressService.getAddressFromPublicKey(publicKey);

        transactionUnsigned.setSenderPubKey(publicKey);

      if (!address.equals(transactionBindingModel.getFrom())){
          message="Wrong Private Key or Address";
          return "redirect:/transaction/error";
      }
        //ObjectMapper mapper = new ObjectMapper();

        //Object to JSON in String
        //String transactionJson = mapper.writeValueAsString(transactionUnsigned);
        String transactionJson = mapper.writeValueAsString(transactionUnsigned);
        System.out.println(transactionJson);
        byte[] hash=this.addressService.getPrivateKeyFromMnemonic(transactionJson);
        byte[][] signed=Secp256k1.signTransaction(hash,DatatypeConverter.parseHexBinary(transactionBindingModel.getPrivateKey()));

        String[] signature=new String[2];
        signature[0]=this.addressService.getStringFromBytes(signed[0]);
        signature[1]=this.addressService.getStringFromBytes(signed[1]);

        transactionSignature=signature[0]+",\n"+signature[1];

       return "redirect:/transaction/details";
    }

    @GetMapping("/transaction/details")
    public String details(Model model) {

        model.addAttribute("transactionUnsigned", transactionUnsigned);
        model.addAttribute("view", "transaction/details");

        return "base-layout";
    }

    @PostMapping("/transaction/details")
    public String sign(TransactionBindingModel transactionBindingModel) {


        return "redirect:/transaction/signed";
    }

    @GetMapping("/transaction/signed")
    public String signedMessage(Model model) throws JsonProcessingException, NoSuchAlgorithmException {
        transactionToSend=new TransactionToSend();

        transactionToSend.setFrom(transactionUnsigned.getFrom());
        transactionToSend.setTo(transactionUnsigned.getTo());
        transactionToSend.setSenderPubKey(transactionUnsigned.getSenderPubKey());
        transactionToSend.setValue(transactionUnsigned.getValue());

        transactionToSend.setSenderSignature(transactionSignature.split(",\n"));

        String jsonString = mapper.writeValueAsString(transactionToSend);
        System.out.println(jsonString);
        model.addAttribute("jsonString", jsonString);
        model.addAttribute("signature", transactionSignature);
        model.addAttribute("transaction", transactionToSend);
        model.addAttribute("view", "transaction/signed");

        return "base-layout";
    }

    @PostMapping("/transaction/signed")
    public String send(TransactionBindingModel transactionBindingModel) throws IOException {

        postJson(jsonString);
        return "redirect:/transaction/send";
    }

    @GetMapping("/transaction/send")
    public String sendMessage(Model model) {

        model.addAttribute("view", "transaction/send");

        return "base-layout";
    }


    public void postJson(String json)
            throws ClientProtocolException, IOException {
        CloseableHttpClient client = HttpClients.createDefault();
        HttpPost httpPost = new HttpPost("http://192.168.164.22:5555/transaction/send");


        StringEntity entity = new StringEntity(json);
        httpPost.setEntity(entity);
        httpPost.setHeader("Accept", "application/json");
        httpPost.setHeader("Content-type", "application/json");

        CloseableHttpResponse response = client.execute(httpPost);
        //assertThat(response.getStatusLine().getStatusCode(), equalTo(200));
        client.close();
    }


}
