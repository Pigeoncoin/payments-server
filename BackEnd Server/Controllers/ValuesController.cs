using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using BitcoinLib;
using BitcoinLib.Services.Coins;
using BitcoinLib.Services.Coins.Bitcoin;
using BitcoinLib.Responses;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Web.Http.Results;

namespace OneClickMinerPool.Controllers
{
    public class ValuesController : ApiController
    {
        public static String rpcuser = "os8912";
        public static String rpcpassword = "foo523";
        public static String WalletPassword = "";
        public static String IP = "http://127.0.0.1:16318";
        // GET /api/Values/SendTX?Transaction
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public string SendTX(String Transaction)
        {
            BitcoinService foo = new BitcoinService(IP, rpcuser, rpcpassword, WalletPassword,120);

            String TXID = foo.SendRawTransaction(Transaction, true);
            return TXID;
        }

        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public decimal GetBalance(String address)
        {
            BitcoinService foo = new BitcoinService(IP, rpcuser, rpcpassword, WalletPassword, 120);
            foo.ImportAddress(address, "", false);
            return foo.GetReceivedByAddress(address, 6);
        }
        // GET /api/Values/GETTX?
        [HttpGet]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IHttpActionResult GetUnspentTX(String Address)
        {
            BitcoinService foo = new BitcoinService(IP, rpcuser, rpcpassword, WalletPassword, 120);


            List<ListUnspentResponse> hello = foo.ListUnspent(0, 100000000, new List<string>() { Address });
            List<UnspentTransaction> outputs = new List<UnspentTransaction>();
            foreach (ListUnspentResponse a in hello)
            {
                UnspentTransaction unspent = new UnspentTransaction();
                unspent.txid = a.TxId;
                unspent.vout = a.Vout;
                unspent.scriptPubKey = a.ScriptPubKey;
                unspent.amount = a.Amount;
                outputs.Add(unspent);
            }

            return Json(outputs); // JsonConvert.SerializeObject(outputs);
        }
        public String GetUnsignedTransaction(String FromAddress, String ToAddress, decimal amount)
        {

            return "";
        }

    }
    class UnspentTransaction
    {
        public String txid { get; set; }

        public int vout { get; set; }
        public String scriptPubKey { get; set; }
        public decimal amount { get; set; }
    }
}
