using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParticles : MonoBehaviour
{
    public ParticleSystem particle;

    [SerializeField] Transform obj;

    public GameObject player;

    public Light light;

    // Start is called before the first frame update
    void Start()
    {
        particle.Pause();
        light.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((obj.transform.position - player.transform.position).magnitude < 10.0f)
        {
            particle.Play();
            light.enabled = true;
        }
        else
        {
            particle.Pause();
            light.enabled = false;
        }
            
    }
}
