using UnityEngine;
using UnityEditor;
using TMPro;

public class ClickDetector : Upgradeable
{
    public ulong ClickStrength, TotalSouls, CurrentYear, YearsPassed;
    public ulong Population;
    private ulong m_initialPopulation;
    private float m_iniPopFloat;
    const float EulerNumber = 2.71828182845904523536028f;
    public float GrowthRate;
    public TextMeshProUGUI PopulationText, TotalSoulsText, ClickStrengthText, CurrentYearText, GrowthRateText;
    public GameObject[] Effects;
    Vector3 targetPosition;
    public int SingleSoulHarvestIndex;
    private Vector3 m_prevClickPlace;

    public static ClickDetector Instance;

    public PopUpMessage popUpMessage;

    public GameObject Fail1, Fail0, BlackScreen;
    [Header("---------")]
    public GameObject[] Deactivateable;


    void Start()
    {
        BlackScreen.SetActive(false);
        Fail0.SetActive(false);
        Fail1.SetActive(false);
        m_initialPopulation = Population;

        CurrentYearText.text = "Year: " + CurrentYear.ToString();
        PopulationText.text = "Population: " + Population.ToString();
        TotalSoulsText.text = "Souls: " + TotalSouls.ToString();

        GrowthRateText.text = GrowthRate.ToString();
        InvokeRepeating("TimeTick", 0.0f, 1.0f);
        InvokeRepeating("PopulationGrowth", 0.0f, 1.0f);


        //Debug.Log(CalculatePopGrowth(m_initialPopulation, 5));
    }

    void Update()
    {
        CheckPopulation();
    }

    void OnMouseDown()
    {
        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;
            if(targetPosition == m_prevClickPlace)
            {
                popUpMessage.PopMessage("This soul is already yours!");
            }
            else
            {
                HarvestSoul(SingleSoulHarvestIndex, targetPosition, (int)ClickStrength);
                m_prevClickPlace = targetPosition;
            }
        }
     }

    public void HarvestSoul(int effectIndex, Vector3 pos, int soulAmount)
    {
        if(Effects[effectIndex])Instantiate(Effects[effectIndex], pos, Quaternion.identity);
        CollectSouls(soulAmount);
        CheckPopulation();
    }

    void TimeTick()
    {
        YearsPassed++;
        CurrentYear++;
        CurrentYearText.text = "Year: " + CurrentYear.ToString();
    }

    void CollectSouls(int howMany)
    {
        if((ulong)howMany > Population)
        {
            Population = 0;
        }
        else
        {
            RemovePopulation((ulong)howMany);
            AddSouls(howMany);
            YearsPassed = 1;
            m_initialPopulation = Population;
            CheckPopulation();
        }

        //}
    }
    
    public void AddPopulation(ulong p)
    {
        Population += p;
        PopulationText.text = "Population: " + Population.ToString();
    }

    public void RemovePopulation(ulong p)
    {
        if (p > Population)
        {
            Population = 0;
        }
        else
        {
            Population -= p;
        }
        CheckPopulation();
        PopulationText.text = "Population: " + Population.ToString();
    }

    public void AddSouls(int s)
    {
        TotalSouls += (ulong)s;
        TotalSoulsText.text = "Souls: " + TotalSouls.ToString();
    }

    public void RemoveSouls(int s)
    {
        TotalSouls -= (ulong)s;
        TotalSoulsText.text = "Souls: " + TotalSouls.ToString();
    }

    void PopulationGrowth()
    {
        Population = CalculatePopGrowth(m_initialPopulation, (int)YearsPassed);
        PopulationText.text = "Population: " + Population.ToString();

        //Debug.Log(Population);
    }

    //1.087629
    //2.77996E+09 as float
    //2779960385 as ulong
    //Taken from: http://www.coolmath.com/algebra/17-exponentials-logarithms/06-population-exponential-growth-01
    ulong CalculatePopGrowth(ulong initPop, int yearsPassed)
    {
        m_iniPopFloat = initPop;
        return (ulong)(m_iniPopFloat * Mathf.Pow(EulerNumber, GrowthRate * yearsPassed));
    }

    void CheckPopulation()
    {
        if(Population == 1 || Population <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        if (Population == 1)
        {
            Fail1.SetActive(true);
            BlackScreen.SetActive(true);
        }
        else
        {
            Fail0.SetActive(true);
            BlackScreen.SetActive(true);
        }

        foreach (GameObject i in Deactivateable)
        {
            i.SetActive(false);
        }
    }

    public override void IncreaseStrength(ulong str)
    {
        ClickStrength = str;
    }
}
