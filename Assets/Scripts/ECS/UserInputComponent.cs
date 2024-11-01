using System;
using UnityEngine.InputSystem;

namespace Sapfire.ECS
{
    [Serializable]
    public struct UserInputComponent
    {
        public InputActionAsset gameInput;
        public InputAction move;
        public InputAction run;
    
    }
}
