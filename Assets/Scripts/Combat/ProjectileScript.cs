using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [Header("Ranged")]
    public Transform projectile;
    public Transform shootPoint;
    public LayerMask collisions;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            DestroyProj();
        }
    }

    IEnumerable DestroyProj()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
