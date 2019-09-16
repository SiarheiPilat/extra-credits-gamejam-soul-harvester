using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public TextMeshProUGUI ButtonText;
    public ClickDetector clickDetector;
    public Upgradeable upgradeable;
    public PopUpMessage popUpMessage;
    public string Name;
    public ulong BaseCost;
    private ulong Price;
    const float Multiplier = 1.07f;
    int Level;
    public ulong Efficiency;
    private ulong m_baseEfficiency;

    public int opensNextOnLevel;
    public GameObject nextOne;

    void Start()
    {
        m_baseEfficiency = Efficiency;
        Level = 1;
        Price = BaseCost;
        UpdateText();
    }

    void UpdatePrice()
    {
        float newPrice = Mathf.Ceil(BaseCost * Mathf.Pow(Multiplier, Level));
        Price = (ulong)newPrice;
        Debug.Log(15.0f * Mathf.Pow(Multiplier, Level));
        UpdateText();
    }

    void IncreaseEfficiency()
    {
        Efficiency = m_baseEfficiency * ((ulong)Level + 1);
    }

    void UpdateText()
    {
        ButtonText.text = Name + " Lvl. " + Level.ToString() + " (" + Efficiency.ToString() + " souls/harvest)" + "\nUpgrade for " + Price.ToString() + " souls";
    }

    public void Upgrade()
    {
        if (Price <= clickDetector.TotalSouls)
        {
            IncreaseEfficiency();
            clickDetector.RemoveSouls((int)Price);
            Level++;
            UpdatePrice();
            upgradeable.IncreaseStrength(Efficiency);
        }
        else
        {
            popUpMessage.PopMessage("Not enough souls!");
        }

        if(opensNextOnLevel == Level)
        {
            nextOne.SetActive(true);
        }
    }
}
