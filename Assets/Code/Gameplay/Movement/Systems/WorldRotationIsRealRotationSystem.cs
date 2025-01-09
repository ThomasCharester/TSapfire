using Entitas;

namespace Code.Gameplay.Movement.Systems
{
    public class WorldRotationIsRealRotationSystem: IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public WorldRotationIsRealRotationSystem(GameContext game)
        {
            _entities = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.WorldRotation,
                GameMatcher.Transform));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
                entity.Transform.rotation = entity.WorldRotation;
        }
    }
}