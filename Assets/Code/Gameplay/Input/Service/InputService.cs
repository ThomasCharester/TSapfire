using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Gameplay.Input
{
    public class InputService : IInputService
    {
        private InputActionAsset _gameInput;
        private InputAction _movement;

        public InputService(InputActionAsset gameInput)
        {
            _gameInput = gameInput;
            gameInput.FindActionMap("Game").Enable();
            _movement = gameInput.FindActionMap("Game").FindAction("Movement");
        }

        public float GetHorizontalAxis() => _movement.ReadValue<Vector2>().x;
        public float GetVerticalAxis() => _movement.ReadValue<Vector2>().y;
        
        public bool HasAxisInput() => _movement.ReadValue<Vector2>() != Vector2.zero;
        
        public bool GetActionButton(string actionName) => _gameInput.FindAction(actionName).ReadValue<bool>();// Looks pretty shitty
    }
}