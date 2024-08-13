using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ShoppingManagement : MonoBehaviour
{
    public int Ruby = 1000;
    public int CurrentRuby;

    public GameObject ShoppingCanvas;
    public UnityEngine.UI.Image ShoppingItem;
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemDescription;
    public UnityEngine.UI.Slider AmountSlider;
    public TextMeshProUGUI Rubytext;
    public TextMeshProUGUI ChosenAmount;
    public TextMeshProUGUI SliderMax;
    public GameObject InsNotice;

    public Sprite DynamiteImage;
    public Sprite ThunderImage;
    public Sprite StormImage;
    public Sprite FrostImage;
    public Sprite SpeedImage;

    public TextMeshProUGUI DynQuant;
    private int Dynnum = 0;
    public TextMeshProUGUI ThuQuant;
    private int Thunum = 0;
    public TextMeshProUGUI StoQuant;
    private int Stonum = 0;
    public TextMeshProUGUI SpeQuant;
    private int Spenum = 0;
    public TextMeshProUGUI FroQuant;
    private int Fronum = 0;

    private void Start()
    {
        CurrentRuby = Ruby;
    }
    private void Update()
    {
        DynQuant.text = Dynnum.ToString();
        ThuQuant.text = Thunum.ToString();
        StoQuant.text = Stonum.ToString();
        SpeQuant.text = Spenum.ToString();
        FroQuant.text = Fronum.ToString();
        Rubytext.text = CurrentRuby.ToString();
        ChosenAmount.text = AmountSlider.value.ToString();
    }
    public void Dynamite()
    {
        if(CurrentRuby >= 10)
        {
            ShoppingCanvas.SetActive(true);
            ShoppingItem.sprite = DynamiteImage;
            ItemName.text = "Dynamite";
            AmountSlider.maxValue = CurrentRuby / 10;
            SliderMax.text = AmountSlider.maxValue.ToString();
        }
        else
        {
            ShoppingCanvas.SetActive(true);
            ShoppingItem.sprite = DynamiteImage;
            ItemName.text = "Dynamite";
            AmountSlider.maxValue = 1;
            SliderMax.text = AmountSlider.maxValue.ToString();
        }
    }
    public void Thunder()
    {
        if (CurrentRuby >= 20)
        {
            ShoppingCanvas.SetActive(true);
            ShoppingItem.sprite = ThunderImage;
            ItemName.text = "Thunder";
            AmountSlider.maxValue = CurrentRuby / 20;
            SliderMax.text = AmountSlider.maxValue.ToString();
        }
        else
        {
            ShoppingCanvas.SetActive(true);
            ShoppingItem.sprite = ThunderImage;
            ItemName.text = "Thunder";
            AmountSlider.maxValue = 1;
            SliderMax.text = AmountSlider.maxValue.ToString();
        }
    }
    public void Storm()
    {
        if (CurrentRuby >= 30)
        {
            ShoppingCanvas.SetActive(true);
            ShoppingItem.sprite = StormImage;
            ItemName.text = "Storm";
            AmountSlider.maxValue = CurrentRuby / 30;
            SliderMax.text = AmountSlider.maxValue.ToString();
        }
        else
        {
            ShoppingCanvas.SetActive(true);
            ShoppingItem.sprite = StormImage;
            ItemName.text = "Storm";
            AmountSlider.maxValue = 1;
            SliderMax.text = AmountSlider.maxValue.ToString();
        }
    }
    public void Frost()
    {
        if (CurrentRuby >= 50)
        {
            ShoppingCanvas.SetActive(true);
            ShoppingItem.sprite = FrostImage;
            ItemName.text = "Frost";
            AmountSlider.maxValue = CurrentRuby / 50;
            SliderMax.text = AmountSlider.maxValue.ToString();
        }
        else
        {
            ShoppingCanvas.SetActive(true);
            ShoppingItem.sprite = FrostImage;
            ItemName.text = "Frost";
            AmountSlider.maxValue = 1;
            SliderMax.text = AmountSlider.maxValue.ToString();
        }
    }
    public void Cancel()
    {
        ShoppingCanvas.SetActive(false);
        AmountSlider.value = AmountSlider.minValue;
    }
    public void Confirm()
    {
        
        if(ItemName.text == "Dynamite")
        {
            if (CurrentRuby >= 10)
            {
                CurrentRuby = (int)(CurrentRuby - (10 * AmountSlider.value));
                Dynnum = (int)(Dynnum + AmountSlider.value);
                ShoppingCanvas.SetActive(false);
            }
            else
            {
                StartCoroutine(PopupCountDown());
            }
        }
        else if (ItemName.text == "Thunder")
        {
            if (CurrentRuby >= 10)
            {
                CurrentRuby = (int)(CurrentRuby - (20 * AmountSlider.value));
                Thunum = (int)(Thunum + AmountSlider.value);
                ShoppingCanvas.SetActive(false);
            }
            else
            {
                StartCoroutine(PopupCountDown());
            }
        }
        else if (ItemName.text == "Storm")
        {
            if (CurrentRuby >= 30)
            {
                CurrentRuby = (int)(CurrentRuby - (30 * AmountSlider.value));
                Stonum = (int)(Stonum + AmountSlider.value);
                ShoppingCanvas.SetActive(false);
            }
            else
            {
                StartCoroutine(PopupCountDown());
            }
        }
        else if (ItemName.text == "Frost")
        {
            if (CurrentRuby >= 50)
            {
                CurrentRuby = (int)(CurrentRuby - (50 * AmountSlider.value));
                Fronum = (int)(Fronum + AmountSlider.value);
                ShoppingCanvas.SetActive(false);
            }
            else
            {
                StartCoroutine(PopupCountDown());
            }
        }
        AmountSlider.value = AmountSlider.minValue;
    }
    IEnumerator PopupCountDown()
    {
        InsNotice.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        InsNotice.SetActive(false);
    }
}
