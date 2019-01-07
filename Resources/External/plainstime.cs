using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace THONK.Resources.External{
    public class PlainsTime{
        public static async Task<PlainsTime_obj> time(){
            var client = new HttpClient();
            string url = "https://api.warframestat.us/pc/cetusCycle";
            using(HttpResponseMessage resp = await client.GetAsync(url)){
                using (HttpContent cont = resp.Content){
                    string data = await cont.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(data);
                    bool day = json.isDay;
                    string tLeft = json.timeLeft;
                    return new PlainsTime_obj(day,tLeft);
                }
            }
        }
    }
    public class PlainsTime_obj{
        bool _isDay;
        string _timeLeft;
        int _minLeft;
        public PlainsTime_obj(bool a, string b){
            _isDay = a;
            int e=0;
            string[] c = b.Split(' ');
            if(c.Length==3){
                e=60;
                c[0]=c[1];
            }
            int d;
            int.TryParse(c[0].Remove(c[0].Length-1),out d);
            e+=d;
            _minLeft += e;
            _timeLeft = b;
            //
        }
        public bool GetIsDay(){return _isDay;}
        public string GetTimeLeft(){return _timeLeft;}
        public int GetMinLeft(){return _minLeft;}
    }
}