using System;
using System.Collections.Generic;
using Code.Update;
using Code.Utils;
using Zenject;

namespace Code.TrainingTarget
{
    public class TargetsManager : IUpdate, IDisposable
    {
        private List<TargetView> _views;
        private List<Target> _targets = new();
        private List<RespawnTimer> _respawnTimers = new();
        private float _respawnTime;

        [Inject]
        public TargetsManager(UpdateManager updateManager,TargetsManagerSettings targetsManagerSettings)
        {
            updateManager.AddUpdate(this);
            _views = targetsManagerSettings.Views;
            _respawnTime = targetsManagerSettings.RespawnTime;
            CreateTargets();
        }

        private void CreateTargets()
        {
            for (int i = 0, len = _views.Count; i < len; ++i)
            {
                var target = new Target();
                target.Death += _views[i].SetDeath;
                target.Death += OnTargetDeath;
                _views[i].Hited += target.HealthRemove;
                _targets.Add(target);
                _respawnTimers.Add(new RespawnTimer(_respawnTime, i));
            }
        }
        public void Update()
        {
            for (int i = 0, len = _respawnTimers.Count; i < len; ++i)
            {
                if (_respawnTimers[i].respawned)
                {
                    continue;
                }
                var respawnTimer = _respawnTimers[i];
                respawnTimer.Timer.UpdateTimer();
                if (respawnTimer.Timer.available)
                {
                    Respawn(_targets[respawnTimer.TargetIndex]);
                }
            }
        }
        
        public void Dispose()
        {
            for (int i = 0, len = _views.Count; i < len; ++i)
            {
                _targets[i].Death -= _views[i].SetDeath;
                _targets[i].Death -= OnTargetDeath;
                _views[i].Hited -= _targets[i].HealthRemove;
            }
        }
        
        private void OnTargetDeath(object o, bool isDeath)
        {
            if (o is not Target target)
            {
                return;
            }

            if (!isDeath)
            {
                return;
            }
            
            Death(target);
        }

        private void Respawn(Target target)
        {
            var index= _targets.IndexOf(target);
            _respawnTimers[index].respawned = true;
            _targets[index].Refresh();
            _respawnTimers[index].Timer.TimerZero();
        }

        private void Death(Target target)
        {
            var index= _targets.IndexOf(target);
            _respawnTimers[index].respawned = false;
        }
        
        private class RespawnTimer
        {
            public bool respawned;
            private Timer _timer;
            private int _targetIndex;

            public Timer Timer => _timer;
            public int TargetIndex => _targetIndex;

            public RespawnTimer(float time, int targetIndex)
            {
                respawned = true;
                _timer = new Timer(time);
                _targetIndex = targetIndex;
            }
        }
    }
}
