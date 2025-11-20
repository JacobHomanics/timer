using UnityEngine;
using UnityEditor;
using JacobHomanics.TrickedOutUI;

namespace JacobHomanics.TrickedOutUI.Editor
{
    [CustomEditor(typeof(CurrentMaxTextComponent))]
    public class CurrentMaxTextComponentEditor : UnityEditor.Editor
    {
        private static GUIStyle _headerStyle;
        private static GUIStyle _headerStyleSubtitle;

        private static GUIStyle HeaderStyle
        {
            get
            {
                if (_headerStyle == null)
                {
                    _headerStyle = new GUIStyle(EditorStyles.label);
                    _headerStyle.fontSize = 18;
                    _headerStyle.fontStyle = FontStyle.Bold;
                    _headerStyle.normal.textColor = EditorStyles.label.normal.textColor;
                    _headerStyle.alignment = TextAnchor.MiddleCenter;
                }
                return _headerStyle;
            }
        }

        private static GUIStyle HeaderStyleSubtitle
        {
            get
            {
                if (_headerStyleSubtitle == null)
                {
                    _headerStyleSubtitle = new GUIStyle(EditorStyles.label);
                    _headerStyleSubtitle.fontSize = 24;
                    _headerStyleSubtitle.fontStyle = FontStyle.Normal;
                    _headerStyleSubtitle.normal.textColor = EditorStyles.label.normal.textColor;
                    _headerStyleSubtitle.alignment = TextAnchor.MiddleCenter;
                }
                return _headerStyleSubtitle;
            }
        }


        public enum ValueComponentType
        {
            Current,
            Max,
            Difference,
            CurrentPercentage
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var targetComponent = (CurrentMaxTextComponent)target;

            // Store the current feature components array before drawing
            BaseTextFeatureComponent[] previousFeatureComponents = new BaseTextFeatureComponent[targetComponent.featureComponents.Length];
            System.Array.Copy(targetComponent.featureComponents, previousFeatureComponents, targetComponent.featureComponents.Length);

            // Draw only the script reference at the top
            EditorGUI.BeginDisabledGroup(true);
            SerializedProperty scriptProp = serializedObject.FindProperty("m_Script");
            if (scriptProp != null)
            {
                EditorGUILayout.PropertyField(scriptProp);
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();

            // Text Field at the top
            SerializedProperty textProp = serializedObject.FindProperty("text");
            if (textProp != null)
            {
                EditorGUILayout.PropertyField(textProp);
            }
            else
            {
                EditorGUILayout.HelpBox("Unable to find text field.", MessageType.Warning);
            }

            EditorGUILayout.Space();

            // Value Component Section
            DrawValueComponentSection(targetComponent);

            EditorGUILayout.Space();

            // Feature Components Section
            DrawFeatureComponentsSection(targetComponent);

            serializedObject.ApplyModifiedProperties();

            // Check for removed components and clean them up
            CleanupRemovedFeatureComponents(targetComponent, previousFeatureComponents);
        }

        private void DrawValueComponentSection(CurrentMaxTextComponent targetComponent)
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Type", HeaderStyle, GUILayout.ExpandWidth(false));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (targetComponent.valueComponent != null)
            {
                string componentName = GetValueComponentName(targetComponent.valueComponent);
                EditorGUILayout.LabelField(componentName, HeaderStyleSubtitle, GUILayout.ExpandWidth(false));
            }
            else
            {
                var style = new GUIStyle(HeaderStyleSubtitle);
                style.normal.textColor = Color.red;
                EditorGUILayout.LabelField("None", style, GUILayout.ExpandWidth(false));
            }
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            // Show the valueComponent field reference
            SerializedProperty valueComponentProp = serializedObject.FindProperty("_valueComponent");
            if (valueComponentProp != null)
            {
                Rect rect = EditorGUILayout.GetControlRect(false, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(rect, valueComponentProp, GUIContent.none);
            }

            EditorGUILayout.Space();

            string buttonText = targetComponent.valueComponent != null ? "Change" : "Add Value Component";
            if (GUILayout.Button(buttonText))
            {
                ShowValueComponentMenu();
            }
        }

        private void AddValueComponent<T>() where T : BaseValueComponent
        {
            var targetComponent = (CurrentMaxTextComponent)target;
            var gameObject = targetComponent.gameObject;

            // Store the old component to remove it later
            BaseValueComponent oldComponent = targetComponent.valueComponent;

            // Check if component already exists on the GameObject
            T existingComponent = gameObject.GetComponent<T>();
            if (existingComponent != null)
            {
                // Use existing component
                Undo.RecordObject(targetComponent, "Change Value Component");
                targetComponent.valueComponent = existingComponent;
            }
            else
            {
                // Create new component and set it
                T newComponent = Undo.AddComponent<T>(gameObject);
                Undo.RecordObject(targetComponent, "Change Value Component");
                targetComponent.valueComponent = newComponent;
            }

            // Remove the old component if it's different and not used elsewhere
            if (oldComponent != null && oldComponent != targetComponent.valueComponent)
            {
                // Check if the old component is used by other CurrentMaxTextComponent instances
                bool isUsedElsewhere = false;
                var allTextComponents = gameObject.GetComponents<CurrentMaxTextComponent>();
                foreach (var textComp in allTextComponents)
                {
                    if (textComp != targetComponent && textComp.valueComponent == oldComponent)
                    {
                        isUsedElsewhere = true;
                        break;
                    }
                }

                if (!isUsedElsewhere)
                {
                    Undo.DestroyObjectImmediate(oldComponent);
                }
            }

            EditorUtility.SetDirty(targetComponent);
            serializedObject.Update();
        }

        private void DrawFeatureComponentsSection(CurrentMaxTextComponent targetComponent)
        {
            SerializedProperty featureComponentsProp = serializedObject.FindProperty("_featureComponents");

            if (featureComponentsProp == null)
            {
                EditorGUILayout.HelpBox("Unable to find feature components property.", MessageType.Warning);
                return;
            }

            // Draw the array with descriptive labels
            EditorGUILayout.LabelField("Feature Components", EditorStyles.boldLabel);

            // Draw array size
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Size", GUILayout.Width(EditorGUIUtility.labelWidth - EditorGUI.indentLevel * 15));
            int newSize = EditorGUILayout.IntField(featureComponentsProp.arraySize);
            if (newSize != featureComponentsProp.arraySize)
            {
                if (newSize < 0) newSize = 0;
                featureComponentsProp.arraySize = newSize;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel++;

            for (int i = 0; i < featureComponentsProp.arraySize; i++)
            {
                EditorGUILayout.BeginHorizontal();

                SerializedProperty elementProp = featureComponentsProp.GetArrayElementAtIndex(i);
                BaseTextFeatureComponent feature = elementProp.objectReferenceValue as BaseTextFeatureComponent;

                string label = feature != null ? GetFeatureComponentName(feature) : $"Element {i}";
                EditorGUILayout.PropertyField(elementProp, new GUIContent(label));

                if (GUILayout.Button("−", GUILayout.Width(20)))
                {
                    featureComponentsProp.DeleteArrayElementAtIndex(i);
                    break;
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.indentLevel--;

            // Add Component Button (alternative way to add components)
            if (GUILayout.Button("Add Feature Component"))
            {
                ShowFeatureComponentMenu(targetComponent);
            }
        }

        private void ShowValueComponentMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Current"), false, () => AddValueComponent<CurrentValueComponent>());
            menu.AddItem(new GUIContent("Max"), false, () => AddValueComponent<MaxValueComponent>());
            menu.AddItem(new GUIContent("Difference"), false, () => AddValueComponent<DifferenceValueComponent>());
            menu.AddItem(new GUIContent("Percentage"), false, () => AddValueComponent<CurrentPercentageValueComponent>());
            menu.ShowAsContext();
        }

        private void ShowFeatureComponentMenu(CurrentMaxTextComponent targetComponent)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent("Clamp At Zero"), false, () => AddFeatureComponent<ClampAtZeroComponent>());
            menu.AddItem(new GUIContent("Clamp At Max"), false, () => AddFeatureComponent<ClampAtMaxComponent>());
            menu.AddItem(new GUIContent("Ceil"), false, () => AddFeatureComponent<CeilComponent>());
            menu.AddItem(new GUIContent("Floor"), false, () => AddFeatureComponent<FloorComponent>());
            menu.AddItem(new GUIContent("Enclose In Braces"), false, () => AddFeatureComponent<EncloseInBracesComponent>());
            menu.AddItem(new GUIContent("Format"), false, () => AddFeatureComponent<FormatFeatureComponent>());

            menu.ShowAsContext();
        }


        private string GetValueComponentName(BaseValueComponent component)
        {
            if (component is CurrentValueComponent)
                return "Current";
            else if (component is MaxValueComponent)
                return "Max";
            else if (component is DifferenceValueComponent)
                return "Difference";
            else if (component is CurrentPercentageValueComponent)
                return "Percentage";
            else
                return component.GetType().Name;
        }

        private string GetFeatureComponentName(BaseTextFeatureComponent component)
        {
            if (component is ClampAtZeroComponent)
                return "Clamp At Zero";
            else if (component is ClampAtMaxComponent)
                return "Clamp At Max";
            else if (component is CeilComponent)
                return "Ceil";
            else if (component is FloorComponent)
                return "Floor";
            else if (component is EncloseInBracesComponent)
                return "Enclose In Braces";
            else if (component is FormatFeatureComponent formatComponent)
                return $"Format ({formatComponent.format})";
            else
                return component.GetType().Name;
        }

        private void AddFeatureComponent<T>() where T : BaseTextFeatureComponent
        {
            var targetComponent = (CurrentMaxTextComponent)target;
            var gameObject = targetComponent.gameObject;

            // Check if component already exists on the GameObject
            T existingComponent = gameObject.GetComponent<T>();
            if (existingComponent != null)
            {
                // Check if it's already in the array
                if (System.Array.Exists(targetComponent.featureComponents, f => f == existingComponent))
                {
                    EditorUtility.DisplayDialog("Component Already Added",
                        $"A {typeof(T).Name} is already in the feature components list.", "OK");
                    return;
                }

                // Add existing component to array
                Undo.RecordObject(targetComponent, "Add Feature Component");
                var currentArray = targetComponent.featureComponents;
                System.Array.Resize(ref currentArray, currentArray.Length + 1);
                currentArray[currentArray.Length - 1] = existingComponent;
                targetComponent.featureComponents = currentArray;
            }
            else
            {
                // Create new component and add to array
                T newComponent = Undo.AddComponent<T>(gameObject);
                Undo.RecordObject(targetComponent, "Add Feature Component");
                var currentArray = targetComponent.featureComponents;
                System.Array.Resize(ref currentArray, currentArray.Length + 1);
                currentArray[currentArray.Length - 1] = newComponent;
                targetComponent.featureComponents = currentArray;
            }

            EditorUtility.SetDirty(targetComponent);
            serializedObject.Update();
        }

        private void CleanupRemovedFeatureComponents(CurrentMaxTextComponent targetComponent, BaseTextFeatureComponent[] previousComponents)
        {
            BaseTextFeatureComponent[] currentComponents = targetComponent.featureComponents;

            // Find components that were in the previous array but not in the current array
            foreach (var previousComponent in previousComponents)
            {
                if (previousComponent == null)
                    continue;

                // Check if this component still exists in the current array
                bool stillExists = System.Array.Exists(currentComponents, c => c == previousComponent);

                if (!stillExists)
                {
                    // Component was removed from the array, check if it should be destroyed
                    bool isUsedElsewhere = false;
                    var allTextComponents = targetComponent.gameObject.GetComponents<CurrentMaxTextComponent>();
                    foreach (var textComp in allTextComponents)
                    {
                        if (textComp != targetComponent && System.Array.Exists(textComp.featureComponents, f => f == previousComponent))
                        {
                            isUsedElsewhere = true;
                            break;
                        }
                    }

                    if (!isUsedElsewhere)
                    {
                        Undo.DestroyObjectImmediate(previousComponent);
                        EditorUtility.SetDirty(targetComponent);
                    }
                }
            }
        }

        private void RemoveFeatureComponentAt(int index)
        {
            var targetComponent = (CurrentMaxTextComponent)target;

            if (index >= 0 && index < targetComponent.featureComponents.Length)
            {
                var componentToRemove = targetComponent.featureComponents[index];

                // Remove from array
                var newArray = new BaseTextFeatureComponent[targetComponent.featureComponents.Length - 1];
                for (int i = 0, j = 0; i < targetComponent.featureComponents.Length; i++)
                {
                    if (i != index)
                    {
                        newArray[j++] = targetComponent.featureComponents[i];
                    }
                }
                targetComponent.featureComponents = newArray;

                // Remove the component from the GameObject if it's not used by other text components
                if (componentToRemove != null)
                {
                    // Check if this component is used by other CurrentMaxTextComponent instances
                    bool isUsedElsewhere = false;
                    var allTextComponents = targetComponent.gameObject.GetComponents<CurrentMaxTextComponent>();
                    foreach (var textComp in allTextComponents)
                    {
                        if (textComp != targetComponent && System.Array.Exists(textComp.featureComponents, f => f == componentToRemove))
                        {
                            isUsedElsewhere = true;
                            break;
                        }
                    }

                    if (!isUsedElsewhere)
                    {
                        Undo.DestroyObjectImmediate(componentToRemove);
                    }
                }
            }
        }
    }
}
