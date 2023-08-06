using UnityEngine;

namespace Code.Utils
{
    public class Timer
    {
        public bool available;
        private float _timer;
        private float _time;

        public Timer(float time)
        {
            _time = time;
        }

        public void UpdateTimer()
        {
            available = _timer >= _time;
            if (_timer >= _time)
            {
                _timer = _time;
                return;
            }

            _timer += Time.deltaTime;
        }

        public void TimerZero()
        {
            _timer = 0;
        }
    }
}
