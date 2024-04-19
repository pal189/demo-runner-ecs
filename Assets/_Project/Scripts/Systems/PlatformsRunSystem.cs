using System.Net.NetworkInformation;
using DemoRunnerECS._Project.Scripts.Components;
using DemoRunnerECS._Project.Scripts.Data;
using DemoRunnerECS._Project.Scripts.Systems.Init;
using Leopotam.EcsLite;
using UnityEngine;

namespace DemoRunnerECS._Project.Scripts.Core
{
    public class PlatformsRunSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld world;
        private RuntimeData _runtimeData;

        private EcsPool<TransformComponent> _transformsPool;
        private EcsFilter _platformsFilter;

        public void Init(IEcsSystems systems)
        {
            _platformsFilter = world.Filter<Platform>().Inc<TransformComponent>().End();
            _transformsPool = world.GetPool<TransformComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _platformsFilter)
            {
                ref TransformComponent tr = ref _transformsPool.Get(entity);
                tr._transform.Translate(0, 0, _runtimeData.PlatformSpeed * Time.deltaTime);
            }
        }
    }
}