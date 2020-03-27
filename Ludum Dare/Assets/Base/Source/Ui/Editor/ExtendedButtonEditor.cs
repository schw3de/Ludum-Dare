using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Base.Source.Ui
{
    [CustomEditor(typeof(ExtendedButton))]
    public class ExtendedButtonEditor : UnityEditor.UI.ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            ExtendedButton component = (ExtendedButton)target;

            base.OnInspectorGUI();

            EditorGUILayout.PropertyField(this.serializedObject.FindProperty("Test"), new GUIContent("XXX"), true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
