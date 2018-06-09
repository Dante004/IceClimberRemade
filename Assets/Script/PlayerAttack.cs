using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerController playerController;
    private float blockRadius = 0.2f;
    public Transform blockCheck;
    public bool blocked;
    public LayerMask whatIsBlock;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        blocked = Physics2D.OverlapCircle(blockCheck.position, blockRadius, whatIsBlock);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (!playerController.grounded && blocked)
        {
            playerController.stoppedJumping = true;
            var block = coll.transform.gameObject.GetComponent<Block>();
            block.youDestroy = true;
        }
    }
}