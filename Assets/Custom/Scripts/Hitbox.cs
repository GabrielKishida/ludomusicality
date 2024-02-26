using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public Collider hitboxCollider;
    private void Start()
    {
        hitboxCollider = GetComponent<Collider>();
        hitboxCollider.enabled = false;

    }
    private void OnTriggerEnter()
    {

        Debug.Log("Attack hits");
    }
}
