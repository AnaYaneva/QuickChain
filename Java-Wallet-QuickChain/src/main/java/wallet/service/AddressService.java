package wallet.service;

import org.bouncycastle.crypto.digests.RIPEMD160Digest;
import org.bouncycastle.jce.ECNamedCurveTable;
import org.bouncycastle.jce.spec.ECNamedCurveParameterSpec;
import org.bouncycastle.math.ec.ECPoint;
import org.bouncycastle.util.encoders.Hex;
import org.springframework.stereotype.Service;
import javax.xml.bind.DatatypeConverter;
import java.math.BigInteger;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

@Service
public class AddressService {

    public byte[] getHashFromString(String mnemonic) throws NoSuchAlgorithmException {
        MessageDigest digest = MessageDigest.getInstance("SHA-256");
        byte[] hash = digest.digest(
                mnemonic.getBytes(StandardCharsets.UTF_8));
        return hash;
    }

    public String getStringFromBytes(byte[] hash){
        return new String(Hex.encode(hash));
    }

    public  byte[] getPublicKey(byte[] privateKey) {
        try {
            ECNamedCurveParameterSpec spec = ECNamedCurveTable.getParameterSpec("secp256k1");
            ECPoint pointQ = spec.getG().multiply(new BigInteger(1, privateKey));

            return pointQ.getEncoded(false);
        } catch (Exception e) {
            return new byte[0];
        }
    }
    public  byte[] getPublicKey(String privateKey) {
        try {
            ECNamedCurveParameterSpec spec = ECNamedCurveTable.getParameterSpec("secp256k1");
            ECPoint pointQ = spec.getG().multiply(new BigInteger(1, DatatypeConverter.parseHexBinary(privateKey)));

            return pointQ.getEncoded(false);
        } catch (Exception e) {
            return new byte[0];
        }
    }
    public String getAddressFromPublicKey(String publicKey) {

        byte[] r = publicKey.getBytes(StandardCharsets.UTF_8);
        RIPEMD160Digest d = new RIPEMD160Digest();
        d.update(r, 0, r.length);
        byte[] o = new byte[d.getDigestSize()];
        d.doFinal(o, 0);
        return new String(Hex.encode(o));
    }
}
