using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numScenePersist = FindObjectsByType<ScenePersist>(FindObjectsSortMode.None).Length;
        // FindObjectsByType<GameSession>().Length;
        if (numScenePersist>1){
            Destroy(gameObject);
        }else {
            DontDestroyOnLoad(gameObject);
        }    
    }

    public void ResetScenePersist(){
        // SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
