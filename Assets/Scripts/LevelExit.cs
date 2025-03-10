using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    BoxCollider2D myCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player"){
            StartCoroutine(LoadNextLevel());
        }
    }
    
    IEnumerator LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene=0;
        
        if(SceneManager.sceneCountInBuildSettings>currentSceneIndex+1){
            nextScene = currentSceneIndex+1; 
        }
        Debug.Log("==>LoadNextLevel CurrentSceneIndex:"+currentSceneIndex + " Next Scene:"+nextScene +" Scenecount:"+SceneManager.sceneCountInBuildSettings);
        yield return new WaitForSeconds(2f);

        
        FindFirstObjectByType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextScene);
    }
}
