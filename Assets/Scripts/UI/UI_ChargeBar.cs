using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ChargeBar : MonoBehaviour
{
   public Slider ChargeBar;

    [SerializeField]
    Gradient color;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChargeBar.value = PlayerState.charge;
        ChargeBar.fillRect.GetComponent<Image>().color = color.Evaluate(ChargeBar.normalizedValue);
    }
}
