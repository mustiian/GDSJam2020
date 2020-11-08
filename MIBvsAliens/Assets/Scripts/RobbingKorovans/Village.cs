using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Village : MonoBehaviour
{
    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }
}
