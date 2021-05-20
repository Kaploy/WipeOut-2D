using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : PowerUpBase
{
    private void Start()
    {
        transform.gameObject.tag = "Boost";
    }
}
