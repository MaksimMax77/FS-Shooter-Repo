using System.Collections.Generic;
using UnityEngine;

namespace Code.Update
{
    public class UpdateManager: MonoBehaviour
    {
        private List<IUpdate> _updates = new List<IUpdate>();

        public void AddUpdate(IUpdate update)
        {
            _updates.Add(update);
        }
        
        public void RemoveUpdate(IUpdate update)
        {
            _updates.Remove(update);
        }

        private void Update()
        {
            for (int i = 0, len = _updates.Count; i < len; ++i)
            {
                _updates[i].Update();
            }
        }
    }
}
