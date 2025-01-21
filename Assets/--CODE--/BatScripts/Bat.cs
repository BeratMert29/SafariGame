using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Character
{
    void Update()
    {
        Move();
        Jump();
        Flip();
        animator.SetBool("run", moveX != 0);
    }
}
