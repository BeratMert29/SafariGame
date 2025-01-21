using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Character
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Flip();
        animator.SetBool("run", moveX != 0);
    }
}
