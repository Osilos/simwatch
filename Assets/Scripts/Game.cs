using System.Collections.Generic;
using UnityEngine;

public class Game
{
    private GameScreen gameScreen;
    private TransitionScreen transitionScreen;
    private List<int> currentSequence;
    private int difficulty = 0;

    public Game(GameScreen gameScreen, TransitionScreen transitionScreen)
    {
        this.gameScreen       = gameScreen;
        this.transitionScreen = transitionScreen;
    }

    public void Start()
    {
        NextSequence();
        gameScreen.OnColorClicked += GameScreen_OnColorClicked;
    }

    private void NextSequence()
    {
        currentSequence = GetNewNextSequence(++difficulty);
        gameScreen.PlaySequence(currentSequence);
    }

    private List<int> GetNewNextSequence(int sequenceLength)
    {
        List<int> currentSequence = new List<int>();

        for (int i = 0; i < sequenceLength; i++)
            currentSequence.Add(UnityEngine.Random.Range(0, gameScreen.ColorCount));

        return currentSequence;
    }

    private void GameScreen_OnColorClicked(GameScreen sender, int colorIndex)
    {
        if (currentSequence[0] == colorIndex)
        {
            currentSequence.RemoveAt(0);
            if (currentSequence.Count == 0)
            {
                Debug.ClearDeveloperConsole();
                Debug.Log("You WIN");
                NextSequence();
            }
        }
        else
        {
            Debug.Log("YOU LOSE !!!");
        }
    }

    public void Destroy()
    {
        gameScreen.OnColorClicked -= GameScreen_OnColorClicked;
    }




    private const string HIGHSCORE_KEY = "Highscore";
    public static int GetHighscore()
    {
        return PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
    }
    public static void SetHighscore(int score)
    {
        PlayerPrefs.SetInt(HIGHSCORE_KEY, score);
    }
}
