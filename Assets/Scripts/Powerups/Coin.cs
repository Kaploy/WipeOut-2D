using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUpBase
{
    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.tag = "Coin";
    }

    
}
