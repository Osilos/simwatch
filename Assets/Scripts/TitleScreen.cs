using UnityEngine;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private GameScreen gameScreen;

    private void Start()
    {
        playButton.onClick.AddListener(PlayButton_OnClicked);
    }

    private void PlayButton_OnClicked()
    {
        gameObject.SetActive(false);
        new Game(gameScreen).Start();
    }
}
