using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    int maxLives = 3;

    int lives;

    void Start()
    {
        lives = maxLives;
    }

    void Update()
    {
        
    }
}
