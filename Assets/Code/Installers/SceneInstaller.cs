using Code.Character;
using Code.CharacterUi;
using Code.Settings;
using Code.Shoot;
using Code.TrainingTarget;
using Code.Update;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _unitObject;
        [SerializeField] private UpdateManager _updateManager;
        [SerializeField] private CharacterSettings _characterSettings;
        [SerializeField] private Camera _unitCamera;
        [SerializeField] private ShootingImpactSettings shootingImpactSettings;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private CharacterWindow _characterWindow;
        [SerializeField] private TargetsManagerSettings _targetsManagerSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(_unitObject).AsSingle().NonLazy();
            Container.BindInstance(_updateManager).AsSingle().NonLazy();
            Container.BindInstance(_characterSettings).AsSingle().NonLazy();
            Container.BindInstance(_unitCamera).AsSingle().NonLazy();
            Container.BindInstance(shootingImpactSettings).AsSingle().NonLazy();
            Container.BindInstance(_weapon).AsSingle().NonLazy();
            Container.BindInstance(_characterWindow).AsSingle().NonLazy();
            Container.BindInstance(_targetsManagerSettings).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<TargetsManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<CharacterControl>().AsSingle().NonLazy();
        }
    }
}