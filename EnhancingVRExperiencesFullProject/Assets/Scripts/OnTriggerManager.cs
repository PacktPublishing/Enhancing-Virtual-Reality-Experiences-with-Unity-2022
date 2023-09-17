using UnityEngine;
using UnityEngine.Events;
using UnityEditor;
using System;

//[System.Serializable]
public class TriggerEvents
{
   
    public GameObject triggerObject;

   
    public UnityEvent triggerEnterEvent;
}

//[CustomPropertyDrawer(typeof(TriggerEvents))]
//public class TriggerEventsDrawer : PropertyDrawer
//{
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        EditorGUI.BeginProperty(position, label, property);
//        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
//        var indent = EditorGUI.indentLevel;
//        EditorGUI.indentLevel = 0;

//        var triggerObjectRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
//        var triggerEventRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);

//        EditorGUI.PropertyField(triggerObjectRect, property.FindPropertyRelative("triggerObject"), GUIContent.none);
//        EditorGUI.PropertyField(triggerEventRect, property.FindPropertyRelative("triggerEnterEvent"), new GUIContent("Trigger Enter Event"));

//        EditorGUI.indentLevel = indent;
//        EditorGUI.EndProperty();
//    }

//    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
//    {
//        return base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
//    }
//}


public class OnTriggerManager : MonoBehaviour
{
    [Header("Trigger Events")]
    public TriggerEvents[] triggers;

        private void Start()
        {
            for (int i = 0; i < triggers.Length; i++)
            {
            // Make sure the triggerObject is not null
            if (triggers[i].triggerObject != null)
            {
                // Add the OnTriggerEnter event to the trigger object
                triggers[i].triggerObject.AddComponent<OnTriggerEnterEvent>().onTriggerEnterEvent.AddListener(() => triggers[i].triggerEnterEvent.Invoke());
            }
        }
    }
}

public class OnTriggerEnterEvent : MonoBehaviour
{
    public UnityEvent onTriggerEnterEvent;
    private void OnTriggerEnter(Collider other)
    {
        // Invoke the OnTriggerEnterEvent when the trigger is entered
        onTriggerEnterEvent.Invoke();
    }

}
