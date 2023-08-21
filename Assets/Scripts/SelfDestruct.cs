using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeTillDestroy;
    void Start()
    {
        Destroy(gameObject,timeTillDestroy);  
    }
}
