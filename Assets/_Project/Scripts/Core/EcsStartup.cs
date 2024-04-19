using DemoRunnerECS._Project.Scripts.Data;
using DemoRunnerECS._Project.Scripts.Systems.Init;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace DemoRunnerECS._Project.Scripts.Core
{
    sealed class EcsStartup : MonoBehaviour
    {
        public StaticData StaticData;
        public SceneData SceneData;
        
        private IEcsSystems _systems;
        private EcsWorld _world;

        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            RuntimeData runtimeData = new RuntimeData(StaticData.PlatformSpeed);

            _systems
                // register your systems here, for example:
                .Add(new PlatformsInitSystem())
                .Add(new PlatformsRunSystem())
                .Inject(StaticData)
                .Inject(SceneData)
                .Inject(runtimeData)
                // register additional worlds here, for example:
                // .AddWorld (new EcsWorld (), "events")
#if UNITY_EDITOR
                // add debug systems for custom worlds here, for example:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Init();
        }

        void Update()
        {
            // process systems here.
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                // list of custom worlds will be cleared
                // during IEcsSystems.Destroy(). so, you
                // need to save it here if you need.
                _systems.Destroy();
                _systems = null;
            }

            // cleanup custom worlds here.

            // cleanup default world.
            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}