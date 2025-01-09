using Entitas;
using UnityEngine;

namespace Code.Gameplay.Input.Systems
{
    public class GetNSetInputSystem : IExecuteSystem
    {
        private readonly IInputService _inputService;
        private readonly IGroup<GameEntity> _inputs;

        public GetNSetInputSystem(GameContext game, IInputService inputService)
        {
            _inputService = inputService;
            _inputs = game.GetGroup(GameMatcher.Input);
        }
        public void Execute()
        {
            foreach (var input in _inputs)
            {
                if (_inputService.HasAxisInput())
                    input.ReplaceAxisInput(new Vector2(_inputService.GetHorizontalAxis(),_inputService.GetVerticalAxis()));
                else if (input.hasAxisInput)
                    input.RemoveAxisInput();
            }
        }
    }
}