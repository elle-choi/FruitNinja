using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // using built-in Unity UI

//using UnityEngine.SceneManagement;

public class GameScore : MonoBehaviour
{  
    public Text scoreNum;

    private int gameScore;

    public void AddPoints()
    {
        gameScore = GameManager.score;
        scoreNum.text = gameScore.ToString();
    }
}
