using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{

    public static Slider fuelbar;

    public Text fuelnumber;

    // Start is called before the first frame update
    void Start()
    {
        fuelbar = gameObject.GetComponent<Slider>();
        fuelbar.maxValue = PlayerStats.getMaxFuel();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setFuel(int fuel)
    {
        fuelbar.value = fuel;
        fuelnumber.text = fuel.ToString();

    }

    public int getFuel()
    {
        return (int)fuelbar.value;

    }
}
