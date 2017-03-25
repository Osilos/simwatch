using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : CanvasScreen
{
    [SerializeField] private Button playButton;
    [SerializeField] private GameScreen gameScreen;
    [SerializeField] private TransitionScreen transitionScreen;
    [SerializeField] private Text scoreText;

    private void OnEnable()
    {
        int highscore = Game.GetHighscore();
        scoreText.text = (highscore == 0) ? "" : "HIGHSCORE : " + highscore;
    }

    private void Start()
    {
        playButton.onClick.AddListener(PlayButton_OnClicked);
    }

    private void PlayButton_OnClicked()
    {
        CloseTransitToLeft();
        transitionScreen.SetSequenceID(1);
        transitionScreen.OpenTransitFromRight(() =>
        {
            transitionScreen.CloseTransitToLeft();
            gameScreen.OpenTransitFromRight();
            Game game = new Game(gameScreen, transitionScreen);
            game.OnLoosed += Game_OnLoosed;
            game.Start();
        });
    }

    private void Game_OnLoosed(Game sender)
    {
        sender.Destroy();
        gameScreen.EnableClick = false;
        gameScreen.StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        gameScreen.SetErrorVisible(true);
        yield return new WaitForSeconds(2);
        gameScreen.SetErrorVisible(false);

        gameScreen.CloseTransitToLeft();
        OpenTransitFromRight();
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(PlayButton_OnClicked);
    }
}
