using UnityEngine;

public class ExplosionDestroyer : MonoBehaviour
{
    private void OnEnable()
    {
        // Destroy the game object after 3 seconds
        Destroy(gameObject, 3f);
    }
}
