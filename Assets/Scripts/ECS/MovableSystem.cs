using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sapfire.ECS
{
    sealed class UserMovableSystem : IEcsSystem
    {
        private readonly EcsWorld _world = null;
        private EcsFilter<MovableComponent, UserInputComponent, AnimableComponent> userFilter = null;
        public void Init()
        {
            _world.NewEntity().Get<UserInputComponent>();

            foreach (int user in userFilter)
            {
                userFilter.Get2(user).gameInput.FindActionMap("Game").Enable();
                userFilter.Get2(user).move = userFilter.Get2(user).gameInput.FindActionMap("Game").FindAction("Move");
                userFilter.Get2(user).run = userFilter.Get2(user).gameInput.FindActionMap("Game").FindAction("Run");
            }
        }
        public void Run()
        {
            foreach (int user in userFilter)
                Movement(ref userFilter.Get1(user), ref userFilter.Get2(user), ref userFilter.Get3(user));
        }
        private void Movement(ref MovableComponent movableComponent, ref UserInputComponent userInputComponent, ref AnimableComponent animableComponent)
        {
            Vector2 direction = userInputComponent.move.ReadValue<Vector2>();

            if (!movableComponent.canMove) return;

            if (direction == Vector2.zero)
            {
                animableComponent.animator.SetFloat("VectorForward", -0.01f); // Костыль для обмана аниматора
                return;
            }

            Vector3 movement3D = new Vector3(direction.x * movableComponent.speed, 0, direction.y * movableComponent.speed);
            animableComponent.animator.SetFloat("VectorForward", movement3D.magnitude - 0.01f); // Костыль для обмана аниматора

            bool isRunning = userInputComponent.run.ReadValue<float>() > 0;
            animableComponent.animator.SetBool("isRunning", isRunning);

            if (isRunning) movement3D *= movableComponent.runModifier;

            //print(movement3D.magnitude);

            movableComponent.characterController.Move(movement3D);

            movableComponent.characterController.transform.rotation = Quaternion.Slerp(movableComponent.transform.rotation, Quaternion.LookRotation(movement3D), movableComponent.rotatingSpeed * Time.deltaTime);

        }
    }
}
