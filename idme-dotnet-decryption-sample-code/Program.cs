using System;
using System.IO;
using System.Security.Cryptography;
using Jose;

public class Program
{
    public static void Main(string[] args)
    {
        string jwe = "[Insert JWE Token]";
        string privateKeyPath = "[Insert private-key.pem path]";

        // Validate and print information about the private key
        if (ValidatePrivateKey(privateKeyPath))
        {
            string decryptedPayload = DecryptJWE(jwe, privateKeyPath);
            Console.WriteLine("Decrypted Payload:");
            Console.WriteLine(decryptedPayload);
        }
        else
        {
            Console.WriteLine("Private key validation failed.");
        }
    }

    private static bool ValidatePrivateKey(string privateKeyPath)
    {
        try
        {
            // Read the PEM file content
            string pemContent;
            using (var reader = new StreamReader(privateKeyPath))
            {
                pemContent = reader.ReadToEnd();
            }

            // Check if the PEM file contains the expected headers
            if (!pemContent.Contains("-----BEGIN PRIVATE KEY-----") &&
                !pemContent.Contains("-----BEGIN RSA PRIVATE KEY-----"))
            {
                Console.WriteLine("The PEM file does not contain a valid RSA private key.");
                return false;
            }

            // Load the private key from the PEM file
            RSA privateKey = RSA.Create();
            privateKey.ImportFromPem(pemContent.ToCharArray());

            // Get key parameters to validate
            var rsaParameters = privateKey.ExportParameters(false);

            // Print non-sensitive information about the key
            Console.WriteLine("Private Key Information:");
            Console.WriteLine($"Key Size: {privateKey.KeySize} bits");
            Console.WriteLine($"Public Exponent: {BitConverter.ToString(rsaParameters.Exponent)}");

            // Perform additional validation if necessary
            // For example, check if the key size meets security requirements
            if (privateKey.KeySize < 2048)
            {
                Console.WriteLine("Warning: The RSA key size is less than 2048 bits. It is recommended to use a key size of at least 2048 bits for security reasons.");
            }

            // The private key is valid
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error validating private key: {ex.Message}");
            return false;
        }
    }

    private static string DecryptJWE(string jwe, string privateKeyPath)
    {
        try
        {
            // Load the private key from PEM file
            RSA privateKey;
            using (var reader = new StreamReader(privateKeyPath))
            {
                var pem = reader.ReadToEnd();
                privateKey = RSA.Create();
                privateKey.ImportFromPem(pem.ToCharArray());
            }

            // Decrypt the JWE token using the private key
            string decryptedPayload = JWT.Decode(jwe, privateKey);

            return decryptedPayload;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during JWE decryption: {ex.Message}");
            return null;
        }
    }
}