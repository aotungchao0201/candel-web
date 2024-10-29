using Newtonsoft.Json;
using Service.ZaloPayService.Helpers;
using Service.ZaloPayService.Respond;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ZaloPayService.Request
{
    public class CreateZaloPayRequest
    {
        public CreateZaloPayRequest(int? appid, string? appuser, long? apptime, 
            long? amount, string? apptransid, string? bankcode, string? description)
        {
            Appid = appid;
            Appuser = appuser;
            Apptime = apptime;
            Amount = amount;
            Apptransid = apptransid;
            Bankcode = bankcode;
            Description = description;
        }

        public int? Appid { get; set; }
        public string? Appuser { get; set; } = string.Empty;
        public long? Apptime { get; set; }
        public long? Amount { get; set; }
        public string? Apptransid { get; set; } = string.Empty;
        public string? Embeddata { get; set; } = string.Empty;
        public string? Mac { get; set; } = string.Empty;
        public string? Bankcode { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Returnurl { get; set; }

        public void MakeSignature(string key)
        {
            var data = Appid + "|" + Apptransid + "|" + Appuser + "|" + Amount + "|"
              + Apptime + "|" + "|";

            this.Mac = HashHelper.HmacSHA256(data, key);
        }

        public Dictionary<string, string> GetContent()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            keyValuePairs.Add("appid", Appid.ToString());
            keyValuePairs.Add("appuser", Appuser);
            keyValuePairs.Add("apptime", Apptime.ToString());
            keyValuePairs.Add("amount", Amount.ToString());
            keyValuePairs.Add("apptransid", Apptransid);
            keyValuePairs.Add("description", Description);
            keyValuePairs.Add("bankcode", "zalopayapp");
            keyValuePairs.Add("mac", Mac);

            return keyValuePairs;
        }

        public (bool, string) GetLink(string paymentUrl)
        {
            using var client = new HttpClient();
            var content = new FormUrlEncodedContent(GetContent());
            var response = client.PostAsync(paymentUrl, content).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var responseData = JsonConvert
                    .DeserializeObject<CreateZaloPayRespond>(responseContent);
                if (responseData.returnCode == 1)
                {
                    return (true, responseData.orderUrl);
                }
                else
                {
                    return (false, responseData.returnMessage);
                }

            }
            else
            {
                return (false, response.ReasonPhrase ?? string.Empty);
            }
        }
    }
}
