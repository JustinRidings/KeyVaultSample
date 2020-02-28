using System;
using System.Runtime.CompilerServices;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace AzureKeyVaultConsoleEx
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader();
            Console.WriteLine("Whats your KeyVault URI? Format: https://<foo>.vault.azure.net/");
            string keyVaultUri = Console.ReadLine();
            Console.WriteLine("Gotcha. One sec");
            SecretClient secrets = CreateAndAuthClient(keyVaultUri);
            if(secrets != null)
            {
                Console.WriteLine("We're in.");
                Console.WriteLine("What's the secret you need?");
                string secretName = Console.ReadLine();
                Console.WriteLine("Gotcha. One sec");
                if(!string.IsNullOrEmpty(GetVaultSecret(secretName, secrets)))
                {
                    Console.WriteLine($"Your secret is: {GetVaultSecret(secretName, secrets)}");
                    Console.WriteLine("Do you want to continue? Y/N");
                    string reply = Console.ReadLine();
                    if(reply != "Y")
                    {
                        Console.WriteLine("Cya Later!");
                        GC.Collect();
                    }
                }
            }
        }

        static SecretClient CreateAndAuthClient(string keyVaultURI)
        {
            //Below is for a default Azure Credential..
            var client = new SecretClient(new Uri(keyVaultURI), new DefaultAzureCredential());
            return client;

            // alternatively, if authenticating from an app registration....
            //ClientSecretCredential c = new ClientSecretCredential(tenantId, clientId, clientSecret);
            //var client = new SecretClient(new Uri(keyVaultURI),c);
            //return client;
        }

        static string GetVaultSecret(string secretName, dynamic client)
        {
            KeyVaultSecret secret = client.GetSecret(secretName);
            return secret.Value ?? "Error";
        }

        static void PrintHeader()
        {
            Console.WriteLine("*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+");
            Console.WriteLine("Welcome to the Key-Vault-Secret-Getter-3000");
            Console.WriteLine("*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+*+");
            Console.WriteLine("\n");
        }
    }
}
