using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseFriendly : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<FriendlyWarrior>() != null)
        {
            collision.GetComponent<FriendlyWarrior>().OnHideObject(collision.GetComponent<PoolObject>());
        }
    }
}
