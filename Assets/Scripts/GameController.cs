using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text _scoreText = null;
    public Button _gameOver = null;

    private int _score = 0;

    public void BackToHomeScreen()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void PlayerKilled()
    {
        Time.timeScale = 0;
        _gameOver.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void IncrementScore(float multiplier)
    {
        if (_scoreText == null) return;

        _score += 1 * Mathf.RoundToInt(multiplier);
        _scoreText.text = "Score: " + _score; 
    }

    private void Awake()
    {
        IncrementScore(0);
    }
}
