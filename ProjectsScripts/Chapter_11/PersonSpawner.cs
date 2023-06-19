using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using System;

public class PersonSpawner : MonoBehaviour
{
    // Array of person prefabs to spawn
    public GameObject[] personPrefabs;

    // Number of people to spawn
    public int numberOfPeople = 10;

    // The selected tag from the custom tag selector
    [TagSelector]
    public string tagOptions = "";

    private Mesh mesh;                  // Mesh of objects with the selected tag
    private Vector3[] spawnPoints;      // Array of spawn points for the people

    void Start()
    {
        // Get the mesh of objects with the selected tag
        mesh = GetMeshWithTag(tagOptions);

        // Generate spawn points for the specified number of people
        spawnPoints = GenerateSpawnPoints(numberOfPeople);

        // Spawn people at the generated spawn points
        for (int i = 0; i < numberOfPeople; i++)
        {
            Vector3 spawnPosition = spawnPoints[i];
            Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            Instantiate(personPrefabs[Random.Range(0, personPrefabs.Length)], spawnPosition, spawnRotation, transform);
        }
    }

    // Get the mesh of objects with the specified tag
    Mesh GetMeshWithTag(string tag)
    {
        // Find all game objects with the specified tag
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

        // Get the MeshFilters of the game objects
        MeshFilter[] meshFilters = gameObjects.SelectMany(go => go.GetComponentsInChildren<MeshFilter>()).ToArray();

        // Combine the meshes of the MeshFilters into a single mesh
        CombineInstance[] combineInstances = meshFilters.Select(mf => new CombineInstance() { mesh = mf.sharedMesh, transform = mf.transform.localToWorldMatrix }).ToArray();
        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combineInstances, true, true);

        return mesh;
    }

    // Generate an array of random spawn points on the mesh
    Vector3[] GenerateSpawnPoints(int count)
    {
        // Get the vertices of the mesh
        Vector3[] vertices = mesh.vertices;

        // Create an array to store the spawn points
        Vector3[] spawnPoints = new Vector3[count];

        // Generate random spawn points by selecting random vertices
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, vertices.Length);
            Vector3 vertex = vertices[randomIndex];
            spawnPoints[i] = vertex;
        }

        return spawnPoints;
    }
}





[CustomPropertyDrawer(typeof(TagSelectorAttribute))]
public class TagSelectorPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Check if the serialized property type is string
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            var attrib = this.attribute as TagSelectorAttribute;

            if (attrib.UseDefaultTagFieldDrawer)
            {
                // If the attribute specifies to use the default tag field drawer, use EditorGUI.TagField
                property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
            }
            else
            {
                // Generate the tag list, including custom tags
                List<string> tagList = new List<string>();
                tagList.Add("<NoTag>");
                tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
                string propertyString = property.stringValue;
                int index = -1;

                if (propertyString == "")
                {
                    // The tag is empty
                    index = 0; // First index is the special <notag> entry
                }
                else
                {
                    // Check if there is an entry that matches the tag and get its index
                    // We skip index 0 as that is a special custom case
                    for (int i = 1; i < tagList.Count; i++)
                    {
                        if (tagList[i] == propertyString)
                        {
                            index = i;
                            break;
                        }
                    }
                }

                // Draw the popup box with the current selected index
                index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());

                // Adjust the actual string value of the property based on the selection
                if (index == 0)
                {
                    property.stringValue = "";
                }
                else if (index >= 1)
                {
                    property.stringValue = tagList[index];
                }
                else
                {
                    property.stringValue = "";
                }
            }

            EditorGUI.EndProperty();
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}

public class TagSelectorAttribute : PropertyAttribute
{
    public bool UseDefaultTagFieldDrawer = false;
}

