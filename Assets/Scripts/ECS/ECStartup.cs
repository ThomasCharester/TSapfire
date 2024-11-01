using System.Collections;
using System.Collections.Generic;
using Leopotam;
using Leopotam.Ecs;

using UnityEngine;
using Voody.UniLeo;

namespace Sapfire.ECS
{
    public class ECStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private EcsSystems _systems;
        void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems.ConvertScene();

            AddSystems();
            AddOneFrames();
            AddInjections();

            _systems.Init();
        }
        private void AddSystems()
        {

        }
        private void AddOneFrames()
        {

        }
        private void AddInjections()
        {

        }
        void Update()
        {
            _systems.Run();
        }
        private void OnDestroy()
        {
            if (_systems == null) return;

            _world.Destroy();
            _world = null;
            _systems.Destroy();
            _systems = null;
        }
    }
}
