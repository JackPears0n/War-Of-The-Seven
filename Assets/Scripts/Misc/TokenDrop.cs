using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenDrop : MonoBehaviour
{
    public GameObject player;
    private StatScript ss;

    // Start is called before the first frame update
    void Start()
    {
        ss = player.GetComponent<StatScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddModToken()
    {
        ss.maxTokens++;
        ss.currentTokens++;
    }

    private void OnDestroy()
    {
        int rand = Random.Range(0, 9);
        if (rand == 0)
        {
            AddModToken();
        }
    }
}
