using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Mechanics.Inventory
{
    [CustomEditor(typeof(ScriptableInventory))]
    public class InventoryDatabaseGUI : Editor
    {
        private string[] _itemNameArray;
        private Type[] _itemTypeArray;
        private int _selectedOption = 0;

        private void OnEnable()
        {
            SetArrayOfTypes<InventoryItem>();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            DrawDefaultInspector();

            EditorGUI.BeginChangeCheck();
            _selectedOption = EditorGUILayout.Popup("Add item", _selectedOption, _itemNameArray);

            if (GUILayout.Button("Add currently selected option"))
            {
                AddSelectedOption(_itemTypeArray[_selectedOption]);
            }
        }

        /// <summary>
        /// Functionality for the GUI button to add a class of the current selected class in the dropdown and add it to the ItemList
        /// </summary>
        /// <param name="itemType"></param>
        public void AddSelectedOption(Type itemType)
        {
            SerializedProperty property = serializedObject.FindProperty("ItemList");
            InventoryItem newItem = Activator.CreateInstance(itemType) as InventoryItem;
            (property.serializedObject.targetObject as ScriptableInventory).ItemList.Add(newItem);

            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Gets all classes inheriting from InventoryItem using reflection and adds them to a list of types and list of names
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void SetArrayOfTypes<T>() where T : InventoryItem
        {
            List<string> classNameList = new();
            List<Type> classTypeList = new();
            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
            {
                classNameList.Add(type.Name);
                classTypeList.Add(type);
            }
            _itemNameArray = classNameList.ToArray();
            _itemTypeArray = classTypeList.ToArray();
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Sets an Integer ID if one does not previously exist. Sets the field to read only.
    /// </summary>
    [CustomPropertyDrawer(typeof(ObjectID))]
    public class ObjectIDDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            if (string.IsNullOrEmpty(property.stringValue))
            {
                property.stringValue = Guid.NewGuid().ToString();
            }
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
#endif 
}
