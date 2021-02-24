using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolVFX: PoolObject
{
    [HideInInspector]
    public bool control;
    public float timeOfEffect;
    private void OnEnable()
    {
        StartCoroutine(FireWaiter());
    }
    private void Update()
    {
        if(control)
        {
            OnHideObject(this);
        }
    }

    private IEnumerator FireWaiter()
    {
        control = false;
        yield return new WaitForSeconds(timeOfEffect);
        control = true;
    }
}
