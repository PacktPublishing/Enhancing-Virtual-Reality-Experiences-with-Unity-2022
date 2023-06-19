using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalSceneManager : MonoBehaviour
{
    // The SceneAsset representing the next scene to load
    public SceneAsset nextScene;

    // The tag assigned to the player GameObject
    public string playerTag;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider's GameObject tag matches the player tag
        if (other.CompareTag(playerTag))
        {
            // Get the path of the nextScene asset
            string path = AssetDatabase.GetAssetPath(nextScene);

            // Load the next scene based on the asset path
            SceneManager.LoadScene(path);
        }
    }
}
