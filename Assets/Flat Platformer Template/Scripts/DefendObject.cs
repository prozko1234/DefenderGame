using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendObject : MonoBehaviour
{
    HealthSystem defendObjectHealthSystem;
    void Start()
    {
        defendObjectHealthSystem = transform.GetComponent<HealthSystem>();
        defendObjectHealthSystem.SetHp(50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
