using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace Runtime
{
    public class TimeController : MonoBehaviour
    {
        private readonly JsonSerializerSettings _serializerSettings = new()
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };

        [DllImport("__Internal")]
        private static extern void Alert(string message);

        private async void Start()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            GetMoscowTime();
        }

        private async void GetMoscowTime()
        {
            try
            {
                var response = await UnityWebRequest
                    .Get("https://worldtimeapi.org/api/timezone/Europe/Moscow")
                    .SendWebRequest();

                var data = JsonConvert.DeserializeObject<MoscowTime>(response.downloadHandler.text, _serializerSettings);
                Alert(data.Datetime.DateTime.ToString(CultureInfo.InvariantCulture));
            }
            catch
            {
                Alert("SSL Connection error");
            }
        }
    }
}