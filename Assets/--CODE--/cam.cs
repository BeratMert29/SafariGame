using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam : MonoBehaviour
{
    public Transform target;

   

    void Update()
    {
        transform.position=new Vector3(target.position.x,target.position.y+4f,transform.position.z);
    }
}
