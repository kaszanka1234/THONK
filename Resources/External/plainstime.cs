using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace THONK.Resources.External{
    /* Calculate time on plains */
    public class PlainsTime_obj{
        bool _isDay;
        string _timeLeft;
        int _minLeft;
        public PlainsTime_obj(){
            DateTime startOffset = new DateTime(2019,2,17,15,38,20);
            TimeSpan cetusCycle = DateTime.UtcNow - startOffset;
            int cycleSeconds = (int)cetusCycle.TotalSeconds%(150*60);
            if(cycleSeconds<(100*60)){
                _isDay=true;
                cycleSeconds=100*60-cycleSeconds;
                _minLeft=cycleSeconds/60;
            }else{
                _isDay=false;
                cycleSeconds=150*60-cycleSeconds;
                _minLeft=cycleSeconds/60;
            }
            cetusCycle = new TimeSpan(cycleSeconds*TimeSpan.TicksPerSecond);
            if(cetusCycle.Hours==0){
                _timeLeft=$"{cetusCycle.Minutes}m {cetusCycle.Seconds}s";
            }else{
                _timeLeft=$"{cetusCycle.Hours}h {cetusCycle.Minutes}m {cetusCycle.Seconds}s";
            }
        }
        public bool GetIsDay(){return _isDay;}
        public string GetTimeLeft(){return _timeLeft;}
        public int GetMinLeft(){return _minLeft;}
    }
}