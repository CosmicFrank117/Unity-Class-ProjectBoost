using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
   [SerializeField] float reloadDelay = 1f;
   [SerializeField] float nextLevelDelay = 1f;
   
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You hit friendly");
                break;
            case "Fuel":
                Debug.Log("You picked up fuel!");
                break;
            case "Finish":
                StartNextLevelSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        //add SFX upon crash
        //add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", reloadDelay);
    }

    void StartNextLevelSequence()
    {
        //add SFX upon crash
        //add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", nextLevelDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
