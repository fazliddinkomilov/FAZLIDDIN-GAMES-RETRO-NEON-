using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace MegaSkill.Main
{
    public static class GoogleSheetsDataSender{
        public static UnityWebRequest GetRequest(string _baseURL, List<GoogleSheetData> datas){
            WWWForm form = new WWWForm();

            for (int i = 0; i < datas.Count; i++)
                form.AddField(datas[i].target, datas[i].value);

            byte[] rawData = form.data;
            string url = _baseURL;

            return UnityWebRequest.Post(url, form);
        }
    }


    [System.Serializable]
    public struct GoogleSheetData{
        public string target;
        public string value;

        public GoogleSheetData(string target, string value){
            this.target = target;
            this.value = value;
        }
    }
}