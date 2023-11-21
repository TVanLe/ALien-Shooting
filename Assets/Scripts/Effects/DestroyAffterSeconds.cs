using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAffterSeconds : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyObj", 2f);
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
