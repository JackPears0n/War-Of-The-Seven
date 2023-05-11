using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyProjectileScript : MonoBehaviour
{
    public LayerMask players;
    public LayerMask destroy;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == players)
        {
            return;
        }
        else
        {
            //StartCoroutine("DestroyProj");
            Destroy(gameObject);           
        }

    }

    IEnumerable DestroyProj()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
