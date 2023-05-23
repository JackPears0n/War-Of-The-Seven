using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBarScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] public  Camera cam;
    [SerializeField] public Transform target;
    [SerializeField] private Vector3 offset;

    private EnemyHealthScript eHS;

    void Start()
    {
        eHS = GetComponent<EnemyHealthScript>();
        slider.maxValue = eHS.maxHealth;
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHPBar();
        transform.rotation = cam.transform.rotation;
        transform.position = target.position + offset;
    }

    public void UpdateHPBar()
    {
        slider.value = eHS.currentHealth;
    }
}
