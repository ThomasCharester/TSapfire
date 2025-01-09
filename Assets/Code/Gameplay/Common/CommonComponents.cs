using Entitas;
using UnityEngine;

namespace Code.Gameplay.Common
{
    [Game] public class WorldPosition : IComponent { public Vector3 Value; }
    [Game] public class Speed : IComponent { public float Value; }
    [Game] public class WorldRotation : IComponent { public Quaternion Value; }
    [Game] public class RotationSpeed : IComponent { public float Value; }
    [Game] public class Direction : IComponent { public Vector3 Value; }
    [Game] public class TransformComponent : IComponent { public Transform Value; }
}