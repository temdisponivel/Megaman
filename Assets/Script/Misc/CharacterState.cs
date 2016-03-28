using UnityEngine;
using System.Collections;
using System;

[Serializable]
[Flags]
public enum CharacterState
{
    Initialized = 1,
    OnGround = 2,
    OnJump = 4,
    OnWall = 8,
    Shooting = 16,
    Dashing = 32,
    Damage = 64,
}
