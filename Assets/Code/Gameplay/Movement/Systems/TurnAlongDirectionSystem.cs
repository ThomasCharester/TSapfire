using Code.Gameplay.Common;
using Code.Gameplay.Common.Time;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Movement.Systems
{
    public class TurnAlongDirectionSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _entities;

        public TurnAlongDirectionSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _entities = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.WorldRotation,
                GameMatcher.Direction,
                GameMatcher.RotationSpeed));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
            {
                entity.ReplaceWorldRotation(Quaternion.Slerp(
                    entity.Transform.rotation,
                    Quaternion.LookRotation(entity.Direction),
                    entity.RotationSpeed * _time.DeltaTime));
            }
        }
    }
}