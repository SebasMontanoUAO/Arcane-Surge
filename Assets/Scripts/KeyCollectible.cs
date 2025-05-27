using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyCollectible : MonoBehaviour
{
    [SerializeField] private int nextSceneIndex = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}