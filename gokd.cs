using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gokd : MonoBehaviour
{
    public Text goldtotalText;
    public static int goldCurrent;
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        goldtotalText.text = goldCurrent.ToString();
    }
}
