using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
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
        gameObject.SetActive(false);
        new Game(gameScreen, transitionScreen).Start();
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(PlayButton_OnClicked);
    }
}
