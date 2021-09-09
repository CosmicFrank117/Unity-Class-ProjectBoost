using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
   [SerializeField] float reloadDelay = 1f;
   [SerializeField] float nextLevelDelay = 1f;
   [SerializeField] AudioClip successSound;
   [SerializeField] AudioClip deathSound;
   
   [SerializeField] ParticleSystem successParticles;
   [SerializeField] ParticleSystem deathParticles;
   
    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) { return; }
        
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
        isTransitioning = true;
        
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        
        deathParticles.Play();
        
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", reloadDelay);
        
    }

    void StartNextLevelSequence()
    {
        isTransitioning = true;
        
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        
        successParticles.Play(successParticles);

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
