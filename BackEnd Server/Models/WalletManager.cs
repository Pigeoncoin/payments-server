using BitcoinLib.Responses;
using BitcoinLib.Services.Coins.Cryptocoin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneClickMinerPool.Models
{
    public class WalletManager
    {

    }
    class CryptoCoinManger
    {

        CryptocoinService Wallet;
        public decimal PayoutAmount { get; set; }
        public List<String> PaymentAddress { get; set; }
        public string WalletPassword { get; set; }
        public string RPCUsername { get; set; }
        public string RPCPassword { get; set; }
        public int port { get; set; }
        public string RewardPotAddress { get; set; }
        
        // public string Name { get; set; }
        public CryptoCoinManger()
        {



        }
        public void RUN()
        {
            //   GetTransactions("").ForEach(m => Console.WriteLine(m.TxId));
            Wallet = new CryptocoinService("http://127.0.0.1:" + port, RPCUsername, RPCPassword, "", 5);
           



        }

        public List<ListTransactionsResponse> GetTransactions(String Account)
        {
            return Wallet.ListTransactions(Account, 500, 0, true);
        }
        public void SendPayment(String Address, Decimal amount)
        {
            Wallet.SendToAddress(RewardPotAddress, amount, "", "", true);
        }
        public void GetNewTransactions(List<string> Addresses)
        {
            Wallet.ListUnspent(0, 99999999, Addresses);
            // Wallet.ListReceivedByAccount();
        }
        public Boolean Isconfirmed(String TX)
        {
            return (Wallet.GetTransaction(TX, true).Confirmations > 5);
        }
        public Boolean IsMemPool(String TX)
        {
            return (Wallet.GetRawMemPool(true).TxIds.Contains(TX));
        }

        public void WriteLine(Object obj)
        {
            Console.WriteLine(obj);
        }

    }
}