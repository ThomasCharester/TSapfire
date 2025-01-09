using Code.Gameplay.Movement.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Movement
{
    public class MovementFeature : Feature
    {
        public MovementFeature(ISystemFactory systems)
        {
            Add(systems.Create<MoveFromPositionInDirectionSystem>());
            Add(systems.Create<WorldPositionIsRealPositionSystem>());
            
            Add(systems.Create<TurnAlongDirectionSystem>());
            Add(systems.Create<WorldRotationIsRealRotationSystem>());
        }
    }
}