using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

#if false
namespace Framework
{
    [CustomPropertyDrawer(typeof(SoundConfig))]
    public class AudioEditor : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();
            root.Add(new PropertyField(property.FindPropertyRelative("type")));
            root.Add(new PropertyField(property.FindPropertyRelative("clipConfigs")));
            root.Add(new PropertyField(property.FindPropertyRelative("spatial")));
            root.Add(new PropertyField(property.FindPropertyRelative("isFollow")));
            root.Add(new PropertyField(property.FindPropertyRelative("maxActiveSound")));

            var spawnInspector = new Box();
            root.Add(spawnInspector);
            spawnInspector.RegisterCallback<ChangeEvent<Object>, VisualElement>(SpawnChanged, spawnInspector);
            return root;
        }

        void SpawnChanged(ChangeEvent<Object> @event, VisualElement spawnInspector)
        {
            spawnInspector.Clear();
            var t = @event.newValue;
            if (t != null)
            {
                spawnInspector.Add(new InspectorElement(t));
            }
        }
    }
}
#endif
