using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSceneManager : MonoBehaviour
{
    public SceneAsset nextScene;
    public string playerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            string path = AssetDatabase.GetAssetPath(nextScene);
            SceneManager.LoadScene(path);
            
        }
    }
}
