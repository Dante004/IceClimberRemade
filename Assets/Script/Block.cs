using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    public bool youDestroy = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (youDestroy)
            Fall();
    }

    public void Fall()
    {
        boxCollider.enabled = false;
        Destroy(gameObject, 10);
        transform.position = new Vector3(transform.position.x, transform.position.y - 3 * Time.deltaTime, transform.position.z);
    }
}