using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [Header("Target layers")]
    public LayerMask enemyLayers;

    [Header("Ranged")]
    public Transform projectile;
    public Transform shootPoint;

    [Header("Damage")]
    public int projectileDamage;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
