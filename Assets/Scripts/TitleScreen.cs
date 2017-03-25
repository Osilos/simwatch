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
            new Game(gameScreen, transitionScreen).Start();
        });
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(PlayButton_OnClicked);
    }
}
