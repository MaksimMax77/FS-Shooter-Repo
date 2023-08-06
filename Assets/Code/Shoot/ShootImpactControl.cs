using Code.Settings;
using UnityEngine;

public class ShootImpactControl
{
    private ShootingImpactSettings.ImpactContainer[] _impactContainers;

    public ShootImpactControl(ShootingImpactSettings.ImpactContainer[] containers)
    {
        _impactContainers = containers;
    }
    
    public void Impact(Vector3 impactPos, Vector3 shooterPos, int layer)
    {
        CreateEffect(impactPos, shooterPos, layer);
    }

    private void CreateEffect(Vector3 impactPos, Vector3 shooterPos, int layer)
    {
        var effect = FindEffect(layer);
        if (effect == null)
        {
            return;
        }
        Object.Instantiate(effect, impactPos, Quaternion.LookRotation(shooterPos - impactPos));
    }

    private GameObject FindEffect(int layer)
    {
        for (int i = 0, len = _impactContainers.Length; i < len; ++i)
        {
            if (_impactContainers[i].layerMask == 1 << layer)
            {
                return _impactContainers[i].effectPrefab;
            }
        }

        return null;
    }
}
