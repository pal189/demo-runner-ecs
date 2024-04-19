using DemoRunnerECS._Project.Scripts.Components;
using DemoRunnerECS._Project.Scripts.Data;
using Leopotam.EcsLite;
using UnityEngine;

namespace DemoRunnerECS._Project.Scripts.Systems.Init
{
    public class PlatformsInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld world = default;
        private readonly StaticData staticData;
        private readonly SceneData sceneData;
        
        private EcsPool<Platform> _platformsPool;
        private EcsPool<TransformComponent> _transformsPool;
        private EcsFilter _platformsFilter;

        public void Init(IEcsSystems systems)
        {
            var tempWolrd = world;
            _platformsFilter = world.Filter<Platform>().End();
            _platformsPool = world.GetPool<Platform>();

            foreach (var spawnPoint in sceneData.platformSpawnPoints)
            {
                var entity = world.NewEntity();
                _platformsPool.Add(entity);

                GameObject _platformPrefab = Resources.Load<GameObject>(spawnPoint.PrefabPath);
                GameObject instance = GameObject.Instantiate(_platformPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
                instance.transform.parent = sceneData.PlatformsParent;
                
                _transformsPool.Add(entity)._transform = instance.transform;
            }
        }
    }
}