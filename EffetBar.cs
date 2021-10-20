using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffetBar : MonoBehaviour
{
    public static int EffectCurrent;
    public static int EffectMax;
    private Image EffectBar;
    // Start is called before the first frame update
    void Start()
    {
        EffectBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
       EffectBar.fillAmount = (float)EffectCurrent / (float)EffectMax;
    }
}
