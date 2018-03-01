package wallet.service;


import org.bouncycastle.crypto.digests.RIPEMD160Digest;
import org.bouncycastle.jce.ECNamedCurveTable;
import org.bouncycastle.jce.spec.ECNamedCurveParameterSpec;
import org.bouncycastle.math.ec.ECPoint;
import org.bouncycastle.util.encoders.Hex;
import org.springframework.stereotype.Service;

import java.math.BigInteger;
import java.nio.charset.StandardCharsets;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

@Service
public class AddressService {

    public byte[] getPrivateKeyFromMnemonic(String mnemonic) throws NoSuchAlgorithmException {
        MessageDigest digest = MessageDigest.getInstance("SHA-256");
        byte[] hash = digest.digest(
                mnemonic.getBytes(StandardCharsets.UTF_8));
        return hash;
    }

    public String getStringFromBytes(byte[] hash){
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

    public String getAddressFromPublicKey(String publicKey) {

        byte[] r = publicKey.getBytes(StandardCharsets.UTF_8);
        RIPEMD160Digest d = new RIPEMD160Digest();
        d.update(r, 0, r.length);
        byte[] o = new byte[d.getDigestSize()];
        d.doFinal(o, 0);
        return new String(Hex.encode(o));
    }


    /*   MessageDigest digest = MessageDigest.getInstance("SHA-256");
        byte[] hash = digest.digest(
          originalString.getBytes(StandardCharsets.UTF_8));
        String sha256hex = new String(Hex.encode(hash));*/

 /*private static readonly X9ECParameters Curve = SecNamedCurves.GetByName("secp256k1");
        private static readonly ECDomainParameters Domain = new ECDomainParameters(Curve.Curve, Curve.G, Curve.N, Curve.H);

        public AddressDto CreateAddress(string mnemonic)
        {
            var privateKeyBytes = this.Sha(mnemonic);
            var publicKeyParameters = this.ToPublicKey(privateKeyBytes);
            var publicKeyData = publicKeyParameters.Q.GetEncoded();

            var privateKey = this.ByteToHex(privateKeyBytes);
            var publicKey = this.ByteToHex(publicKeyData);

            var addressRipe = this.HexToRipe(publicKey);
            var address = this.ByteToHex(addressRipe);

            return new AddressDto
            {
                Mnemonic = mnemonic,
                PrivateKey = privateKey,
                PublicKey = publicKey,
                Address = address
            };
        }

        public byte[] GetBytes(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            return bytes;
        }

        public ECPublicKeyParameters ToPublicKey(string privateKey)
        {
            return this.ToPublicKey(this.GetBytes(privateKey));
        }

        public ECPublicKeyParameters ToPublicKey(byte[] privateKey)
        {
            BigInteger d = new BigInteger(privateKey);
            var q = Domain.G.Multiply(d);

            var publicParams = new ECPublicKeyParameters(q, Domain);
            return publicParams;
        }

        public string GetPublicKey(ECPublicKeyParameters publicKeyParameters)
        {
            var publicKeyData = publicKeyParameters.Q.GetEncoded();
            var publicKey = this.ByteToHex(publicKeyData);

            return publicKey;
        }

        private byte[] Sha(string data)
        {
            using (var sha256 = new SHA256Managed())
            {
                var hash = sha256.ComputeHash(Encoding.Unicode.GetBytes(data));
                return hash;
            }
        }

        public string ByteToHex(byte[] data)
        {
            return string.Join("", data.Select(h => h.ToString("x2")));
        }

        private byte[] HexToRipe(string data)
        {
            using (var ripe = new RIPEMD160Managed())
            {
                return ripe.ComputeHash(Encoding.Unicode.GetBytes(data));
            }
        }

        public byte[] SignData(string msg, string privateKey)
        {
            return this.SignData(msg, this.GetBytes(privateKey));
        }

        public byte[] SignData(string msg, byte[] privateKey)
        {
            BigInteger privateKeyInt = new BigInteger(privateKey);
            ECPrivateKeyParameters privateKeyParameters = new ECPrivateKeyParameters(privateKeyInt, Domain);
            byte[] msgBytes = Encoding.UTF8.GetBytes(msg);

            ISigner signer = SignerUtilities.GetSigner("SHA-256withECDSA");
            signer.Init(true, privateKeyParameters);
            signer.BlockUpdate(msgBytes, 0, msgBytes.Length);
            byte[] sigBytes = signer.GenerateSignature();

            return sigBytes;
        }

        public bool VerifySignature(ECPublicKeyParameters pubKey, byte[] signature, string msg)
        {
            byte[] msgBytes = Encoding.UTF8.GetBytes(msg);

            ISigner signer = SignerUtilities.GetSigner("SHA-256withECDSA");
            signer.Init(false, pubKey);
            signer.BlockUpdate(msgBytes, 0, msgBytes.Length);
            return signer.VerifySignature(signature);
        }*/
}
