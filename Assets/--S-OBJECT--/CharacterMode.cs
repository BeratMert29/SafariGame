using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AnimalMode", menuName = "Characters/AnimalMode")]
public class CharacterMode : ScriptableObject
{
    public string modeName;
    public Sprite texture;

    [Header("Physics")]
    public float speed = 10;
    public float jumpForce = 10;
    public float jumpRayDistance = 1.1f;
    public float damageReduction = 0;
    public float baseGravityScale = 2;
    public float fallGravityScale = 5;
    public float maxFallSpeed = 15;

}
