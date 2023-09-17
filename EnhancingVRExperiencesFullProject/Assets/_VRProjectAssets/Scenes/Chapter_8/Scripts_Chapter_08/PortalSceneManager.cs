using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSceneManager : MonoBehaviour
{
    public string nextScene;
    public string playerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            
            SceneManager.LoadScene(nextScene);
            
        }
    }
}
