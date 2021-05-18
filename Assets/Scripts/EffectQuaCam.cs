using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectQuaCam : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] 
    private float h;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up*h);
        Destroy(gameObject, 1);
    }
}
