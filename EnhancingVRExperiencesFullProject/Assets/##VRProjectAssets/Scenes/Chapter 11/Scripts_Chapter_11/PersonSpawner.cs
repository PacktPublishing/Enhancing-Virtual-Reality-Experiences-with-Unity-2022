using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

public class PersonSpawner : MonoBehaviour
{
    public GameObject[] personPrefabs;
    public int numberOfPeople = 10;
    [TagSelector]
    public string tagOptions = "";

    private Mesh mesh;
    private Vector3[] spawnPoints;

    void Start()
    {
        mesh = GetMeshWithTag(tagOptions);
        spawnPoints = GenerateSpawnPoints(numberOfPeople);

        for (int i = 0; i < numberOfPeople; i++)
        {
            Vector3 spawnPosition = spawnPoints[i];
            //Quaternion spawnRotation = Quaternion.identity; 
            Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            Instantiate(personPrefabs[Random.Range(0, personPrefabs.Length)], spawnPosition, spawnRotation,transform);
        }
    }

    Mesh GetMeshWithTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        MeshFilter[] meshFilters = gameObjects.SelectMany(go => go.GetComponentsInChildren<MeshFilter>()).ToArray();
        CombineInstance[] combineInstances = meshFilters.Select(mf => new CombineInstance() { mesh = mf.sharedMesh, transform = mf.transform.localToWorldMatrix }).ToArray();

        Mesh mesh = new Mesh();
        mesh.CombineMeshes(combineInstances, true, true);

        return mesh;
    }

    Vector3[] GenerateSpawnPoints(int count)
    {
        Vector3[] vertices = mesh.vertices;
        Vector3[] spawnPoints = new Vector3[count];

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
        if (property.propertyType == SerializedPropertyType.String)
        {
            EditorGUI.BeginProperty(position, label, property);

            var attrib = this.attribute as TagSelectorAttribute;

            if (attrib.UseDefaultTagFieldDrawer)
            {
                property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
            }
            else
            {
                //generate the taglist + custom tags
                List<string> tagList = new List<string>();
                tagList.Add("<NoTag>");
                tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
                string propertyString = property.stringValue;
                int index = -1;
                if (propertyString == "")
                {
                    //The tag is empty
                    index = 0; //first index is the special <notag> entry
                }
                else
                {
                    //check if there is an entry that matches the entry and get the index
                    //we skip index 0 as that is a special custom case
                    for (int i = 1; i < tagList.Count; i++)
                    {
                        if (tagList[i] == propertyString)
                        {
                            index = i;
                            break;
                        }
                    }
                }

                //Draw the popup box with the current selected index
                index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());

                //Adjust the actual string value of the property based on the selection
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


