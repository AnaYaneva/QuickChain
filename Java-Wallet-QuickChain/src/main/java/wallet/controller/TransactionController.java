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
import wallet.service.AddressService;
import wallet.service.crypto.Secp256k1;

import javax.xml.bind.DatatypeConverter;
import java.io.IOException;
import java.security.NoSuchAlgorithmException;

import static wallet.entity.Constants.URL_TRANSACTION;


@Controller
public class TransactionController {

    private ObjectMapper mapper = new ObjectMapper();

    private TransactionToSend transactionToSend;

    private TransactionUnsigned transactionUnsigned;

    private String message;

    private String jsonString;

    private String[] signature=new String[2];

    private String transactionHash;


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


        //Object to JSON in String
        String transactionJson = mapper.writeValueAsString(transactionUnsigned);
        System.out.println(transactionJson);
        byte[] hash=this.addressService.getHashFromString(transactionJson);
        byte[][] signed=Secp256k1.signTransaction(hash,DatatypeConverter.parseHexBinary(transactionBindingModel.getPrivateKey()));

        signature[0]=this.addressService.getStringFromBytes(signed[0]);
        signature[1]=this.addressService.getStringFromBytes(signed[1]);

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
        transactionToSend.setTransactionIdentifier(java.util.UUID.randomUUID().toString());
        transactionToSend.setSignatureR(signature[0]);
        transactionToSend.setSignatureS(signature[1]);

        jsonString = mapper.writeValueAsString(transactionToSend);
        System.out.println(jsonString);
        byte[] hash=this.addressService.getHashFromString(jsonString);

        transactionHash=this.addressService.getStringFromBytes(hash);
        System.out.println(transactionHash);

        transactionToSend.setTransactionHash(transactionHash);

        jsonString = mapper.writeValueAsString(transactionToSend);
        System.out.println(jsonString);

        model.addAttribute("transaction", transactionToSend);
        model.addAttribute("view", "transaction/signed");

        return "base-layout";
    }

    @PostMapping("/transaction/signed")
    public String send(TransactionBindingModel transactionBindingModel) throws IOException {

        postJson(jsonString, transactionToSend.getTransactionHash());
        System.out.println(jsonString +" \n"+transactionToSend.getTransactionHash());
        return "redirect:/transaction/send";
    }

    @GetMapping("/transaction/send")
    public String sendMessage(Model model) {
        String url=URL_TRANSACTION+"/"+transactionHash;

        model.addAttribute("hash", transactionHash);
        model.addAttribute("url", url);
        model.addAttribute("view", "transaction/send");

        return "base-layout";
    }


    public void postJson(String json, String hash)
            throws ClientProtocolException, IOException {
        CloseableHttpClient client = HttpClients.createDefault();
        String url=URL_TRANSACTION;
        HttpPost httpPost = new HttpPost(url);


        StringEntity entity = new StringEntity(json);
        httpPost.setEntity(entity);
        httpPost.setHeader("Accept", "application/json");
        httpPost.setHeader("Content-type", "application/json");

        CloseableHttpResponse response = client.execute(httpPost);
        client.close();
    }


}
