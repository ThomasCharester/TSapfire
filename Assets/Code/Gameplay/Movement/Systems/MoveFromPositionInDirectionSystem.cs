using Code.Gameplay.Common.Time;
using Entitas;

namespace Code.Gameplay.Movement.Systems
{
    public class MoveFromPositionInDirectionSystem : IExecuteSystem
    {
        private readonly ITimeService _time;
        private readonly IGroup<GameEntity> _entities;

        public MoveFromPositionInDirectionSystem(GameContext game, ITimeService time)
        {
            _time = time;
            _entities = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.WorldPosition,
                GameMatcher.Direction,
                GameMatcher.Speed));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
            {
                entity.ReplaceWorldPosition(entity.WorldPosition + entity.Direction * entity.Speed * _time.DeltaTime);
            }
        }
    }
}