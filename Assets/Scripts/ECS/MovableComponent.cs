using System;
using UnityEngine;

namespace Sapfire.ECS
{
    [Serializable]
    public struct MovableComponent
    {

        [Min(0f)] public float speed;
        [Min(0f)] public float rotatingSpeed;
        [Min(1f)] public float runModifier;

        public CharacterController characterController;
        public Transform transform;

        public bool canMove;
    }
}
