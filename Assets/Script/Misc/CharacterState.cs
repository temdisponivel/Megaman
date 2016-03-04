using UnityEngine;
using System.Collections;
using System;

[Serializable]
[Flags]
public enum CharacterState
{
    Initialized = 0x01,
    OnGround = 0x02,
    OnJump = 0x04,
    OnWall = 0x08,
    Shooting = 0x16,
}
