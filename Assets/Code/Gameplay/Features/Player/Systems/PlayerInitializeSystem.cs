using Code.Gameplay.Features.Player.Factory;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Player.Systems
{
    public class PlayerInitializeSystem : IInitializeSystem
    {
        private readonly IPlayerFactory _playerFactory;

        public PlayerInitializeSystem(IPlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }

        public void Initialize()
        {
            _playerFactory.CreatePlayer(new Vector3(0, 0, 0));
        }
    }
}