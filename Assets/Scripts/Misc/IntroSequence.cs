using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSequence : MonoBehaviour
{
    public GameObject[] txt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Begin()
    {
        StartCoroutine(NextText());
    }

    IEnumerator NextText()
    {
        foreach (GameObject t in txt)
        {
            t.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
        }
    }
}
