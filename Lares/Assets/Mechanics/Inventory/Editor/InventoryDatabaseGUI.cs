using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Codice.Client.BaseCommands.Merge.Xml;
using Yarn;
using Unity.VisualScripting;
using UnityEditor.UIElements;

namespace Lares.Inventory
{
    [CustomEditor(typeof(ScriptableInventory))]
    public class InventoryDatabaseGUI : Editor
    {
        string[] _itemNameArray;
        Type[] _itemTypeArray;
        int _selectedOption = 0;

        private void OnEnable()
        {
            GetArrayOfTypes<InventoryItem>();
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

        public void AddSelectedOption(Type itemType)
        {
            SerializedProperty property = serializedObject.FindProperty("ItemList");
            InventoryItem newItem = Activator.CreateInstance(itemType) as InventoryItem;
            (property.serializedObject.targetObject as ScriptableInventory).ItemList.Add(newItem);

            serializedObject.ApplyModifiedProperties();
        }

        public void GetArrayOfTypes<T> () where T : InventoryItem
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
    [CustomPropertyDrawer(typeof(ObjectID))]
    public class ObjectIDDrawer : PropertyDrawer 
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            if (property.intValue == 0)
            {
                property.intValue = Guid.NewGuid().ToString().GetHashCode();
            }
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
#endif 
}
