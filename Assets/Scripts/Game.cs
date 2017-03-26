using System.Collections.Generic;
using UnityEngine;

public delegate void GameEventHandler(Game sender);

public class Game
{
    private GameScreen gameScreen;
    private TransitionScreen transitionScreen;
    private List<int> currentSequence;
    private int difficulty = 1;
    public bool IsStarted { get; private set; }

    public event GameEventHandler OnLoosed;

    public Game(GameScreen gameScreen, TransitionScreen transitionScreen)
    {
        this.gameScreen       = gameScreen;
        this.transitionScreen = transitionScreen;
    }

    public void Start()
    {
        if (IsStarted)
            return;

        IsStarted = true;
        gameScreen.OnColorClicked += GameScreen_OnColorClicked;
        SetupNextSequence(true);
    }

    private void SetupNextSequence(bool isFirstSequence = false)
    {
        currentSequence = GetNewNextSequence(difficulty);
        gameScreen.EnableClick = false;
        gameScreen.PlaySequence(currentSequence, () => {
            gameScreen.EnableClick = true;
        }, isFirstSequence);
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
        if (currentSequence.Count == 0)
            return;

        if (currentSequence[0] == colorIndex)
        {
            currentSequence.RemoveAt(0);

            if (currentSequence.Count == 0)
            {
                if (GetHighscore() < difficulty)
                    SetHighscore(difficulty);

                transitionScreen.OpenShowSequenceAndClose(++difficulty, ()=> { SetupNextSequence(); });
            }
        }
        else if (OnLoosed != null)
        {
            OnLoosed(this);
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
