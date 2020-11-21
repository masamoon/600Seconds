using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reroute : MonoBehaviour
{
    public Button reroute_btn;
    public GameObject[] planets;
    // Start is called before the first frame update

    public Text buttonText;
    void Start()
    {
        Button btn = reroute_btn.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);

        //Text buttonText = GetComponent<Text>();
        buttonText.text = "Reroute (R) (" + PlayerStats.getNumRerouts() + ")";

    }

    // Update is called once per frame
    void Update()
    {

        buttonText.text = "Reroute (R) (" + PlayerStats.getNumRerouts() + ")";

        if (Input.GetKeyDown(KeyCode.R))
        {
            DoReroute();
        }
    }

    void DoReroute()
    {
        if (PlayerStats.getNumRerouts() > 0)
        {
            planets[0].GetComponent<Planet>().Reroll();
            int rerolls = PlayerStats.getNumRerouts() - 1;
            PlayerStats.setNumRerouts(rerolls);
        }
    }

    void OnClick()
    {
        
        DoReroute();
        
    }
}
