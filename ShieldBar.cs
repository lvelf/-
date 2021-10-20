using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShieldBar : MonoBehaviour
{
    public static int ShieldCurrent;
    public static int ShieldMax;
    public Text ShieldBarText;
    private Image shieldBar;
    // Start is called before the first frame update
    void Start()
    {
        shieldBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        shieldBar.fillAmount = (float)ShieldCurrent / (float)ShieldMax;
        ShieldBarText.text = ShieldCurrent.ToString() + "/" +ShieldMax.ToString();
    }
}
