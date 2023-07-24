using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static System.Collections.Specialized.BitVector32;

public class CanvasUiManager : Singleton<CanvasUiManager>
{
    private float Currenthealth = 10;
    private float Currentmana = 10;
    private float Currentstamina = 10;

    public float Stamina => Currentstamina;

    public TextMeshProUGUI[] TextMeshProUGUIs;
    
    //public TextMeshProUGUI healthPoint;
    //public TextMeshProUGUI manaPoint;
    //public TextMeshProUGUI staminaPoint;

    public Slider healthSlider;
    public Slider manaSlider;
    public Slider staminaSlider;


    //private float smoothDecreaseDuration = 0.5f;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateInfomationtext();

        healthSlider.maxValue = Currenthealth;
        healthSlider.value = Currenthealth;

        manaSlider.maxValue = Currentmana;
        manaSlider.value = Currentmana;

        staminaSlider.maxValue = Currentstamina;
        staminaSlider.value = Currentstamina;
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //private void ReducCurrent(float name, float number)
    //{
    //    name -= number;
    //}
    //public void ReduceSmooth(float number, Slider name , System.Action action)
    //{
    //    StartCoroutine(SmoothDecrease(number, name, action));
    //}
    //private IEnumerator SmoothDecrease(float number, Slider name, System.Action action)
    //{
    //    float numberPerTich = number / smoothDecreaseDuration;
    //    float elapsedTime = 0f;
    //    Currentstamina -= number;
    //    //ReducCurrent();
    //    while (elapsedTime < smoothDecreaseDuration)
    //    {
    //        float CrurentNumber = numberPerTich * Time.deltaTime;
    //        name.value -= CrurentNumber;
            
    //        elapsedTime += Time.deltaTime;
    //        UpdateInfomationtext();

    //        if (name.value <= 0)
    //        {
    //            name.value = 0;
    //            Currentstamina = -1;
    //            if (action != null)
    //            {
    //                action();
    //            }
                    
    //            break;
    //        }
    //        yield return null;
    //    }
    //}
    //private void UpdateInfomationtext()
    //{
    //    healthPoint.text = Currenthealth.ToString();
    //    manaPoint.text = Currentmana.ToString();
    //    staminaPoint.text = Currentstamina.ToString();
    //}
    private void UpdateInfomationtext()
    {
        TextMeshProUGUIs[0].text = Currenthealth.ToString();
        TextMeshProUGUIs[1].text = Currentmana.ToString();
        TextMeshProUGUIs[2].text = Currentstamina.ToString();
    }
    //public void ReduceText(int reduce)
    //{
    //    Currentstamina -= reduce;
    //    staminaPoint.text = Currentstamina.ToString();
    //    staminaSlider.value = Currentstamina;
    //}
}
