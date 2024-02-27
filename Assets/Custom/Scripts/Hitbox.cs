using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    private void OnTriggerEnter()
    {
        Debug.Log("Attack hits");
    }
}
