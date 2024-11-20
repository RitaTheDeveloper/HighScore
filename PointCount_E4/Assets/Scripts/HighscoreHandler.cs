using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreHandler : MonoBehaviour {
    List<HighscoreElement> highscoreList = new List<HighscoreElement> ();
    List<string> nameList = new List<string>();
    [SerializeField] int maxCount = 7;
    [SerializeField] string filename;
    [SerializeField] string filename2;

    public delegate void OnHighscoreListChanged (List<HighscoreElement> list);
    public static event OnHighscoreListChanged onHighscoreListChanged;

    public delegate void OnNameListChanged(List<string> list);
    public static event OnNameListChanged onNameListChanged;

    private void Start () {
        LoadHighscores ();
        LoadNameList();
    }

    private void LoadHighscores () {
        highscoreList = FileHandler.ReadListFromJSON<HighscoreElement> (filename);

        while (highscoreList.Count > maxCount) {
            highscoreList.RemoveAt (maxCount);
        }

        if (onHighscoreListChanged != null) {
            onHighscoreListChanged.Invoke (highscoreList);
        }
    }

    private void LoadNameList()
    {
        nameList = FileHandler.ReadListFromJSON<string>(filename2);

        while (nameList.Count > maxCount)
        {
            nameList.RemoveAt(maxCount);
        }

        if (onNameListChanged != null)
        {
            onNameListChanged.Invoke(nameList);
        }
    }

    private void SaveHighscore () {
        FileHandler.SaveToJSON<HighscoreElement> (highscoreList, filename);
    }

    public void AddHighscoreIfPossible (HighscoreElement element) {
        for (int i = 0; i < maxCount; i++) {
            if (i >= highscoreList.Count || element.points > highscoreList[i].points) {
                // add new high score
                highscoreList.Insert (i, element);

                while (highscoreList.Count > maxCount) {
                    highscoreList.RemoveAt (maxCount);
                }

                SaveHighscore ();

                if (onHighscoreListChanged != null) {
                    onHighscoreListChanged.Invoke (highscoreList);
                }

                break;
            }
        }
    }

    private void SaveNames()
    {
        FileHandler.SaveToJSON<string>(nameList, filename2);
    }

    public void AddPlayerName(string name)
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (i >= nameList.Count)
            {
                // add new high score
                nameList.Insert(i, name);

                while (nameList.Count > maxCount)
                {
                    nameList.RemoveAt(maxCount);
                }

                //SaveHighscore();
                SaveNames();

                if (onNameListChanged != null)
                {
                    onNameListChanged.Invoke(nameList);
                }

                break;
            }
        }
    }

}