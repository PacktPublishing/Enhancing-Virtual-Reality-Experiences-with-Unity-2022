
using UnityEngine;

public class explosiondestroyer : MonoBehaviour
{

    private void OnEnable()
    {
        Destroy(gameObject, 3f);
    }
}
