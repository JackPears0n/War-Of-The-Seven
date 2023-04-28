using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyProjectileScript : MonoBehaviour
{
    public float lifetime;
    public float maxlifetime;

    void Update()
    {
        lifetime = Time.deltaTime;

        if (lifetime >= maxlifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Kris")
        {
            return;
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
