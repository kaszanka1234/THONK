using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace THONK.Resources.External{
    /*public class Worldstate{
        private class Sortie{
            
            _Mission _m1;
            _Mission _m2;
            _Mission _m3;
            string _boss;


            private class _Mission{
                public string type;
                public string modifier;

                public _Mission(string mtype, string mmodifier){
                    type = mtype;
                    modifier = mmodifier;
                }
            }
            public Sortie(string[] types, string[] mods, string boss){
                _m1 = new _Mission(types[1],mods[1]);
                _m2 = new _Mission(types[2],mods[2]);
                _m3 = new _Mission(types[3],mods[3]);
                _boss = boss;
            }
            public string GetMission(int i=1){
                switch(i){
                    case 1:
                        return $"Mission: {_m1.type}\nModifier: {_m1.modifier}";
                    case 2:
                        return $"Mission: {_m2.type}\nModifier: {_m2.modifier}";
                    case 3:
                        return $"Mission: {_m3.type}\nModifier: {_m3.modifier}";
                    default:
                        return "Mission: null\nModifier: null";
                }
            }
            public string GetBoss(){
                return _boss;
            }
        }

        Sortie sortie;

        public Worldstate(){
            
        }

        public void Get(){
            //
        }

        public async Task<string> GetWorldstateString(){
            HttpClient httpClient = new HttpClient();
            using (HttpResponseMessage response = await httpClient.GetAsync("http://content.warframe.com/dynamic/worldState.php")){
                return await response.Content.ReadAsStringAsync();
            }
        }
    }*/
}