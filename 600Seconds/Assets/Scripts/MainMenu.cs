using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public float timer;
    //public GameObject MenuText;
    public static bool activated = false;

    [SerializeField]
    GameObject diffSlider_gobj;

    Slider diffSlider;

    // Start is called before the first frame update
    void Start()
    {
        diffSlider = diffSlider_gobj.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        BlinkText();
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (diffSlider.value == 0)
            {
                PlayerStats.setTimeRemaining(600f);
                PlayerStats.setHardMode(false);
            }
            else
            {
                PlayerStats.setTimeRemaining(180f);
                PlayerStats.setHardMode(true);
            }
            SceneManager.LoadScene("Intro");
        }

    }

    void BlinkText( )
    {
        timer = timer + Time.deltaTime;
        if (timer >= 0.5)
        {
            GetComponent<Text>().enabled = true;
        }
        if (timer >= 1)
        {
            GetComponent<Text>().enabled = false;
            timer = 0;
        }
    }

    void BlinkTextFaster()
    {
        timer = timer + Time.deltaTime;
        if (timer >= 0.20)
        {
            GetComponent<Text>().enabled = true;
        }
        if (timer >= 0.10)
        {
            GetComponent<Text>().enabled = false;
            timer = 0;
        }
    }
}
