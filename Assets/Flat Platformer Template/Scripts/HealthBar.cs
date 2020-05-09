using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Transform bar;
    Transform rootObject;
    HealthSystem rootObjectHealth;

    void Start()
    {
        bar = transform.Find("Bar");
        rootObject = transform.root;
        rootObjectHealth = rootObject.GetComponent<HealthSystem>();
    }

    void Update()
    {
        bar.localScale = new Vector3(rootObjectHealth.GetHealthPercent(), 1f);
    }
}
