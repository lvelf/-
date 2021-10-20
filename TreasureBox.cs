using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    public bool isOpened;
    public GameObject BloodBottle;
    // Start is called before the first frame update
    void Start()
    {
        isOpened = false;
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && isOpened == false)
        {
            Vector3 BottlePos = new Vector3(transform.position.x,transform.position.y - 0.5f,0);
            Instantiate(BloodBottle,BottlePos,Quaternion.identity);
            isOpened = true;
        }
    }
}
