using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kangaroo : Character
{
    void Update()
    {
        Move();
        Flip();
        Jump();
        animator.SetBool("run", moveX != 0);
    }
}
