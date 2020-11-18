using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    public static bool isloading;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Image>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isloading == false)
            gameObject.GetComponent<Image>().enabled = false;
        else
            gameObject.GetComponent<Image>().enabled = true;
    }
}
