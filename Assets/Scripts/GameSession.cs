using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{


    [SerializeField] int playerLives = 3;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    int playerScore = 0;
    void Awake()
    {
        int numGameSessions = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        // FindObjectsByType<GameSession>().Length;
        if (numGameSessions>1){
            Destroy(gameObject);
        }else {
            DontDestroyOnLoad(gameObject);
        }    
    }

    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = playerScore.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddToScore(int points){
        playerScore+=points;
        scoreText.text = playerScore.ToString();
        // Debug.Log("Score:"+playerScore + " Lives:" + playerLives);
    }
    public void ProcessPlayerDeath(){
        if(playerLives > 1){
            TakeLife();
        }else{
            ResetGameSession();
        }
    }
    public void ResetGameSession(){
        FindFirstObjectByType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    public void TakeLife(){
        playerLives--;
        livesText.text = playerLives.ToString();
        StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene(){
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
}
