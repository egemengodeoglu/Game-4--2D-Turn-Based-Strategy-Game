using System;
using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    /// <summary>
    /// Referance To Prefab Id Used In PoolManager As Key Value
    /// </summary>
    [HideInInspector]
    public int poolid;
    /// <summary>
    /// OnHideObject is The Trigger To Alert PoolManager To Disable Object And Return To Queue
    /// </summary>
    public Action<PoolObject> OnHideObject;

    /// <summary>
    /// If Set To True Object is Hidden After Delay. Can Be Used For Pooled Particle Effects
    /// </summary>

    protected void HideObject()
    {
        if (OnHideObject != null)
        {
            OnHideObject.Invoke(this);
            return;
        }
    }
}