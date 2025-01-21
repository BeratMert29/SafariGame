using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInTime : MonoBehaviour
{
    [SerializeField] float lifeTime;
    void Start()
    {
        StartCoroutine(StartDestroyTime());
    }

    private IEnumerator StartDestroyTime(){
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    } 

}
