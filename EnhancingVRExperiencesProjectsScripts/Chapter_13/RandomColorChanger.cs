using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColorChanger : MonoBehaviour
{
    private void Awake()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material[] materials = renderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].color = new Color(Random.value, Random.value, Random.value, 1f);
            }
            renderer.materials = materials;
        }
    }
}
