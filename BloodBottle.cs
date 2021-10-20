using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodBottle : MonoBehaviour
{
    public Transform playerTransform;
    public int AddHealth;
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
            Destroy(gameObject);
            playerTransform.gameObject.GetComponent<PlayerController>().health += AddHealth;
           
        }
    }
}
