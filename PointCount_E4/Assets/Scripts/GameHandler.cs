using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {
    [SerializeField] PointCounter pointCounter;
    [SerializeField] HighscoreHandler highscoreHandler;
    [SerializeField] PointHUD pointHUD;
    [SerializeField] string playerName;
    [SerializeField] string playerName2;


    public void StartGame () {
        pointCounter.StartGame ();
    }
    public void StopGame () {
        highscoreHandler.AddHighscoreIfPossible (new HighscoreElement (playerName, pointHUD.Points));
        pointCounter.StopGame ();
    }
    public void AddPlayer()
    {
        highscoreHandler.AddPlayerName(playerName2);
    }
}