using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DestructedDestroyObject : MonoBehaviour, IDestructable
{
    [SerializeField] float timeBeforeDestroction = 0.1f;
    public void OnDestruction(Vector3 deathPosition)
    {
        Destroy(gameObject, timeBeforeDestroction);
    }
}
