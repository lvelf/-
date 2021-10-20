using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
     public Transform playerTransform;
    public int effect;
    // Start is called before the first frame update
    void Start()
    {
         playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            playerTransform.gameObject.GetComponent<PlayerController>().Effect += effect;
            Destroy(gameObject,0.3f);
        }
    }
}
