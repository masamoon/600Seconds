using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaunchPlanet : MonoBehaviour
{
    public Button launchbutton;
    //public GameObject planet_gobj;
    public Planet planet;
    public Timer timer;
    
    // Start is called before the first frame update
    void Start()
    {
        Button btn = launchbutton.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        //Planet planet = planet_gobj.GetComponent<Planet>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            LaunchToPlanet();
        }
    }

    void OnClick()
    {
        LaunchToPlanet();
        /*if(planet.planetDifficulty == Planet.Difficulty.Easy)
        {
            PlayerStats.setCurrentDifficulty(PlayerStats.Difficulty.EASY);
        }
        else if (planet.planetDifficulty == Planet.Difficulty.Medium)
        {
            PlayerStats.setCurrentDifficulty(PlayerStats.Difficulty.MEDIUM);
        }
        else if (planet.planetDifficulty == Planet.Difficulty.Hard)
        {
            PlayerStats.setCurrentDifficulty(PlayerStats.Difficulty.HARD);
        }

        // print(PlayerStats.getCurrentDifficulty().ToString());

        PlayerStats.setCurrentUpgrade(planet.planetUpgrade);


        PlayerStats.setChestOpened(false);

        PlayerStats.setTimeRemaining(timer.timeRemaining);

        SceneManager.LoadScene("Game");*/
    }

    void LaunchToPlanet()
    {
        if (planet.planetDifficulty == Planet.Difficulty.Easy)
        {
            PlayerStats.setCurrentDifficulty(PlayerStats.Difficulty.EASY);
        }
        else if (planet.planetDifficulty == Planet.Difficulty.Medium)
        {
            PlayerStats.setCurrentDifficulty(PlayerStats.Difficulty.MEDIUM);
        }
        else if (planet.planetDifficulty == Planet.Difficulty.Hard)
        {
            PlayerStats.setCurrentDifficulty(PlayerStats.Difficulty.HARD);
        }

        // print(PlayerStats.getCurrentDifficulty().ToString());

        PlayerStats.setCurrentUpgrade(planet.planetUpgrade);


        PlayerStats.setChestOpened(false);

        PlayerStats.setTimeRemaining(timer.timeRemaining);

        SceneManager.LoadScene("Game");
    }
}
