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
import wallet.bindingModel.FaucetBindingModel;
import wallet.bindingModel.TransactionToSend;
import wallet.bindingModel.TransactionUnsigned;
import wallet.service.AddressService;
import wallet.service.crypto.Secp256k1;

import javax.xml.bind.DatatypeConverter;
import java.io.IOException;
import java.security.NoSuchAlgorithmException;
import java.util.Date;

import static wallet.entity.Constants.*;


@Controller
public class FaucetController {

    private ObjectMapper mapper = new ObjectMapper();

    private TransactionToSend transactionToSend;

    private TransactionUnsigned transactionUnsigned;

    private String transactionHash;

    private String jsonString;

    private String[] signature=new String[2];

    private long startTime = 0L;

    @Autowired
    private AddressService addressService;

    @GetMapping("/faucet/create")
    public String create(Model model) {

        model.addAttribute("view", "faucet/create");

        return "base-layout";
    }

    @PostMapping("/faucet/create")
    public String createProcess(FaucetBindingModel faucetBindingModel) throws IOException, NoSuchAlgorithmException {

        long elapsedTime = (new Date()).getTime() - startTime;

        if (elapsedTime < FAUCET_TIME) {
            return "redirect:/faucet/error";
        }


        transactionUnsigned =new TransactionUnsigned();

        transactionUnsigned.setFrom(FAUCET_ADDRESS);
        transactionUnsigned.setTo(faucetBindingModel.getAddress());
        transactionUnsigned.setValue(FAUCET_VALUE);
        transactionUnsigned.setSenderPubKey(FAUCET_PUBLIC_KEY);

        //Object to JSON in String
        String transactionJson = mapper.writeValueAsString(transactionUnsigned);

        byte[] hash=this.addressService.getHashFromString(transactionJson);
        byte[][] signed=Secp256k1.signTransaction(hash,DatatypeConverter.parseHexBinary(FAUCET_PRIVATE_KEY));

        signature[0]=this.addressService.getStringFromBytes(signed[0]);
        signature[1]=this.addressService.getStringFromBytes(signed[1]);



        transactionToSend=new TransactionToSend();

        transactionToSend.setFrom(transactionUnsigned.getFrom());
        transactionToSend.setTo(transactionUnsigned.getTo());
        transactionToSend.setSenderPubKey(transactionUnsigned.getSenderPubKey());
        transactionToSend.setValue(transactionUnsigned.getValue());
        transactionToSend.setTransactionIdentifier(java.util.UUID.randomUUID().toString());
        transactionToSend.setSignatureR(signature[0]);
        transactionToSend.setSignatureS(signature[1]);

        jsonString = mapper.writeValueAsString(transactionToSend);

        byte[] hashSigned=this.addressService.getHashFromString(jsonString);

        transactionHash=this.addressService.getStringFromBytes(hashSigned);
        System.out.println(transactionHash);

        transactionToSend.setTransactionHash(transactionHash);

        jsonString = mapper.writeValueAsString(transactionToSend);
        System.out.println(jsonString);

        postJson(jsonString, transactionToSend.getTransactionHash());

        return "redirect:/faucet/send";
    }

    @GetMapping("/faucet/send")
    public String sendMessage(Model model) {
        String url=URL_TRANSACTION+"/"+transactionHash;

        startTime = System.currentTimeMillis();
        model.addAttribute("hash", transactionHash);
        model.addAttribute("url", url);
        model.addAttribute("view", "faucet/send");

        return "base-layout";
    }

    @GetMapping("/faucet/error")
    public String error(Model model) {

        model.addAttribute("view", "faucet/error");

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
