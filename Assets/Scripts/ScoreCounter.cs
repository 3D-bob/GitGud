using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Made by Tuomas

public class ScoreCounter : MonoBehaviour {

    public Text score;  
    int corrupted;

    void Start()
    {
        corrupted = 0;
        //UpdateScore();     
    }

    public void UpdateScore()
    {
        corrupted += 1;
        score.text = "Corrupted: " + corrupted.ToString();
    }
}
