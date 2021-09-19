using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    [SerializeField] private Transform ScoreBoardTemplates;
    [SerializeField] private Transform ScoreBoardTemplate;
    [SerializeField] private Transform scoreBoardPanel;
    [SerializeField] private Text playerName;
    public GameObject inputPanel;

    private List<Transform> highscoreEntryTransformList;
    private bool isAlive;
    private bool hasAddFinished;

    private void Awake()
    {
        Time.timeScale = 0;
        //PlayerPrefs.DeleteAll();
        inputPanel.SetActive(true);
        scoreBoardPanel.gameObject.SetActive(false);
        isAlive = true;
        hasAddFinished = false;
    }
    private void CreateHighscoreEntryTransform(Highscore highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 30f;
        Transform entryTransform = Instantiate(ScoreBoardTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        entryTransform.Find("rankText").GetComponent<Text>().text = rank.ToString() + ".";

        //string nameee = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = highscoreEntry.name;

        string scoree = highscoreEntry.score.ToString();
        Transform temptr = entryTransform.GetChild(2);
        Text tempte = temptr.GetComponent<Text>();
        tempte.text = scoree;

        transformList.Add(entryTransform);
    }
    void OpenScoreTable()
    {
        string jsonString = PlayerPrefs.GetString("Score");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        if (highscores == null)
        {
            // Reload
            jsonString = PlayerPrefs.GetString("Score");
            highscores = JsonUtility.FromJson<Highscores>(jsonString);
            Debug.Log("highscores is null");
        }

        // Sort entry list by Score
        if (highscores.highscoreList.Count > 1)
        {
            for (int i = 0; i < highscores.highscoreList.Count; i++)
            {
                for (int j = i + 1; j < highscores.highscoreList.Count; j++)
                {
                    if (highscores.highscoreList[j].score > highscores.highscoreList[i].score)
                    {
                        // Swap
                        Highscore tmp = highscores.highscoreList[i];
                        highscores.highscoreList[i] = highscores.highscoreList[j];
                        highscores.highscoreList[j] = tmp;
                    }
                }
            }

        }

        highscoreEntryTransformList = new List<Transform>();
        #region Option 1(show all scores)

        //foreach (Highscore highscoreEntry in highscores.highscoreList)
        //{
        //    CreateHighscoreEntryTransform(highscoreEntry, ScoreBoardTemplates, highscoreEntryTransformList);
        //}
        #endregion
        #region Option 2(show only 9 score)

        for (int i = 0; i < 10; i++)
        {
            CreateHighscoreEntryTransform(highscores.highscoreList[i], ScoreBoardTemplates, highscoreEntryTransformList);
        }
        #endregion


    }
    private void AddScoreList(int score, string name)
    {
        // Create HighscoreEntry
        Highscore highscoreEntry = new Highscore { score = score, name = name };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("Score");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize (create sample)
            Debug.Log("highscores is null");
            highscores = new Highscores() { highscoreList = new List<Highscore>() };
        }

        // Add new entry to Highscores
        highscores.highscoreList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("Score", json); //it set as json 
        PlayerPrefs.Save();
        hasAddFinished = true;
    }

    private class Highscores
    {
        public List<Highscore> highscoreList;
    }

    /*Represents a single High score entry*/
    [System.Serializable]
    private class Highscore
    {
        public int score;
        public string name;
    }
    string nameSample;
    float timeSinceNameEntered = 1000;
    public void StartGetName()
    {
        inputPanel.SetActive(false);
        nameSample = playerName.GetComponent<Text>().text;
        timeSinceNameEntered = Time.timeSinceLevelLoad;
        Time.timeScale = 1;
    }

    int GetPlayerScore()
    {
       // int score = PlayerMovement.singleton.playerPoint;
        return 0;
    }

    void Dead()
    {
        isAlive = false;
        int tempScore = GetPlayerScore();
        AddScoreList(tempScore, nameSample);
        scoreBoardPanel.gameObject.SetActive(true);
        if (hasAddFinished)
        {
            OpenScoreTable();
        }

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        //Death stuation
        if ((Time.timeSinceLevelLoad > timeSinceNameEntered + 5) && isAlive)
        {
            Dead();
        }
    }

}