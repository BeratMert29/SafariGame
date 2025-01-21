using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class man : Character
{
    // Start is called before the first frame update
    public GameObject kollar;
    void Start()
    {
        kollar = Instantiate(kollar, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        kollar.SetActive(true);
        Move(kollar, spiderv2.isWebing);
        Jump();
        Flip();

    }
}
