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
    [SerializeField] private PlatfromController player;

    [Header("Text")]
    //[SerializeField] private TextMeshProUGUI hpPoint;
    //[SerializeField] private TextMeshProUGUI mpPoint;
    //[SerializeField] private TextMeshProUGUI spPoint;
    [SerializeField] private TMP_Text hpPoint;
    [SerializeField] private TMP_Text mpPoint;
    [SerializeField] private TMP_Text spPoint;
    [Header("Slider")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider manaSlider;
    [SerializeField] private Slider staminaSlider;

    public TMP_Text HPPoint => hpPoint;
    public TMP_Text MPPoint => mpPoint;
    public TMP_Text SPPoint => spPoint;
    public Slider HPSlider => healthSlider;
    public Slider MPSlider => manaSlider;
    public Slider SPSlider => staminaSlider;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlatfromController>();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        healthSlider.maxValue = player.HP;
        healthSlider.value = player.HP;
        manaSlider.maxValue = player.MP;
        manaSlider.value = player.MP;
        staminaSlider.maxValue = player.SP;
        staminaSlider.value = player.SP;
        hpPoint.text = player.HP.ToString();
        mpPoint.text = player.MP.ToString();
        spPoint.text = player.SP.ToString();
    }
    public void Reduce(Slider slider, TMP_Text text,float reduce)
    {
        slider.value -= reduce;
        text.text = ((int)slider.value).ToString();
    }
    public void Regenerate(Slider slider, TMP_Text text, float reduce)
    {
        slider.value += reduce;
        text.text = ((int)slider.value).ToString();
    }
}
