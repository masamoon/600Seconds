using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text diff = GetComponent<Text>();
        diff.text = PlayerStats.getCurrentDifficulty().ToString();
        if (diff.text == "EASY")
            diff.color = Color.green;
        if (diff.text == "MEDIUM")
            diff.color = Color.yellow;
        if (diff.text == "HARD")
            diff.color = Color.red;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
