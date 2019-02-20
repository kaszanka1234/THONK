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
            const double cycleTime = 8998.8748;
            DateTime startOffset = new DateTime(2019,2,17,15,38,5);
            TimeSpan cetusCycle = DateTime.UtcNow - startOffset;
            int cycleSeconds = (int)(cetusCycle.TotalSeconds%cycleTime);
            if(cycleSeconds<(cycleTime*2/3)){
                _isDay=true;
                cycleSeconds=((int)(cycleTime*2/3))-cycleSeconds;
                _minLeft=cycleSeconds/60;
            }else{
                _isDay=false;
                cycleSeconds=(int)cycleTime-cycleSeconds;
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