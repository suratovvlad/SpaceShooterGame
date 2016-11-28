using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;

    private int _score;
    private bool _gameOver;
    private bool _restart;

    private void Start()
    {
        _score = 0;
        _gameOver = false;
        _restart = false;
        restartText.text= "";
        gameOverText.text = "";
        UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void Update()
    {
        if (!_restart) return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    private IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x),
                    spawnValues.y, spawnValues.z);

                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (_gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                _restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        _score += newScoreValue;
        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreText.text = "Score: " + _score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        _gameOver = true;
    }
}
