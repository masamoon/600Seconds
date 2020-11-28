using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static int score;
    //public static int bombs;
    public Text score_text;
    public Text bomb_text;
    public Text difficulty_text;

    // Start is called before the first frame update
    void Start()
    {
        //bombs = PlayerStats.getBombs();
        score = 0;
        //score_text = GetComponent<Text>();
        bomb_text.text = PlayerStats.getBombs().ToString();

    }

    // Update is called once per frame
    void Update()
    {
        int bombs = PlayerStats.getBombs();
       // print("bombs: " + bombs);
        score_text.text = score.ToString();
       // bomb_text.text = bombs.ToString();
        

    }
}
