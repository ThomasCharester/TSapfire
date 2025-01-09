using Entitas;

namespace Code.Gameplay.Movement.Systems
{
    public class WorldPositionIsRealPositionSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public WorldPositionIsRealPositionSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.WorldPosition,
                GameMatcher.Transform));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
                entity.Transform.position = entity.WorldPosition;
        }
    }
}