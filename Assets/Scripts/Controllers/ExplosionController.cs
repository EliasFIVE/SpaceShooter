using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private float effectTime = 1f;

    private void Start()
    {
        Destroy(gameObject, effectTime);
    }
}

