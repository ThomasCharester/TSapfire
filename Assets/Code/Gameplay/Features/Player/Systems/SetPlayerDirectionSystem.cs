using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Player.Systems
{
    public class SetPlayerDirectionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<GameEntity> _axisInputs;

        public SetPlayerDirectionSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.Player);
            _axisInputs = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.AxisInput
            ));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
            {
                if (_axisInputs.count > 0)
                    foreach (var axisinput in _axisInputs)
                        entity.ReplaceDirection(new Vector3(axisinput.AxisInput.x, 0, axisinput.AxisInput.y));
                else if (entity.hasDirection)
                    entity.RemoveDirection();
            }
        }
    }
}