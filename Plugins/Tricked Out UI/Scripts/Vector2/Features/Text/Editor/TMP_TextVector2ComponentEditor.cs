using UnityEngine;
using UnityEditor;

namespace JacobHomanics.TrickedOutUI.Editor
{
    [CustomEditor(typeof(TMP_TextVector2Component))]
    public class TMP_TextVector2ComponentEditor : UnityEditor.Editor
    {
        protected static GUIStyle _headerStyle;
        protected static GUIStyle _headerStyleSubtitle;

        protected static GUIStyle HeaderStyle
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

        protected static GUIStyle HeaderStyleSubtitle
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

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            var targetComponent = (TMP_TextVector2Component)target;

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

            // Vector2 Adapter Field
            SerializedProperty adapterProp = serializedObject.FindProperty("vector2Adapter");
            if (adapterProp != null)
            {
                EditorGUILayout.PropertyField(adapterProp);
            }


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

        protected virtual void DrawValueComponentSection(TMP_TextVector2Component targetComponent)
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

        protected virtual void AddValueComponent<T>() where T : BaseValueComponent
        {
            var targetComponent = (TMP_TextVector2Component)target;
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
                // Check if the old component is used by other TMP_TextVector2Component instances
                bool isUsedElsewhere = false;
                var allTextComponents = gameObject.GetComponents<TMP_TextVector2Component>();
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

        protected virtual void DrawFeatureComponentsSection(TMP_TextVector2Component targetComponent)
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

        protected virtual string XValueComponentName { get => "X"; }
        protected virtual string YValueComponentName { get => "Y"; }
        protected virtual string DifferenceValueComponentName { get => "Difference"; }
        protected virtual string PercentageValueComponentName { get => "Percentage"; }

        protected virtual void ShowValueComponentMenu()
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent(XValueComponentName), false, () => AddValueComponent<XValueComponent>());
            menu.AddItem(new GUIContent(YValueComponentName), false, () => AddValueComponent<YValueComponent>());
            menu.AddItem(new GUIContent(DifferenceValueComponentName), false, () => AddValueComponent<DifferenceValueComponent>());
            menu.AddItem(new GUIContent(PercentageValueComponentName), false, () => AddValueComponent<PercentageValueComponent>());
            menu.ShowAsContext();
        }

        protected virtual string ClampAtZeroComponentName { get => "Clamp At Zero"; }
        protected virtual string ClampAtYComponentName { get => "Clamp At Y"; }
        protected virtual string CeilComponentName { get => "Ceil"; }
        protected virtual string FloorComponentName { get => "Floor"; }
        protected virtual string EncloseInBracesComponentName { get => "Enclose In Braces"; }
        protected virtual string FormatFeatureComponentName { get => "Format"; }

        protected virtual void ShowFeatureComponentMenu(TMP_TextVector2Component targetComponent)
        {
            GenericMenu menu = new GenericMenu();
            menu.AddItem(new GUIContent(ClampAtZeroComponentName), false, () => AddFeatureComponent<ClampAtZeroComponent>());
            menu.AddItem(new GUIContent(ClampAtYComponentName), false, () => AddFeatureComponent<ClampAtMaxComponent>());
            menu.AddItem(new GUIContent(CeilComponentName), false, () => AddFeatureComponent<CeilComponent>());
            menu.AddItem(new GUIContent(FloorComponentName), false, () => AddFeatureComponent<FloorComponent>());
            menu.AddItem(new GUIContent(EncloseInBracesComponentName), false, () => AddFeatureComponent<EncloseInBracesComponent>());
            menu.AddItem(new GUIContent(FormatFeatureComponentName), false, () => AddFeatureComponent<FormatFeatureComponent>());

            menu.ShowAsContext();
        }


        protected virtual string GetValueComponentName(BaseValueComponent component)
        {
            if (component is XValueComponent)
                return XValueComponentName;
            else if (component is YValueComponent)
                return YValueComponentName;
            else if (component is DifferenceValueComponent)
                return DifferenceValueComponentName;
            else if (component is PercentageValueComponent)
                return PercentageValueComponentName;
            else
                return component.GetType().Name;
        }

        protected virtual string GetFeatureComponentName(BaseTextFeatureComponent component)
        {
            if (component is ClampAtZeroComponent)
                return ClampAtZeroComponentName;
            else if (component is ClampAtMaxComponent)
                return ClampAtYComponentName;
            else if (component is CeilComponent)
                return CeilComponentName;
            else if (component is FloorComponent)
                return FloorComponentName;
            else if (component is EncloseInBracesComponent)
                return EncloseInBracesComponentName;
            else if (component is FormatFeatureComponent formatComponent)
                return FormatFeatureComponentName;
            else
                return component.GetType().Name;
        }

        protected virtual void AddFeatureComponent<T>() where T : BaseTextFeatureComponent
        {
            var targetComponent = (TMP_TextVector2Component)target;
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

        protected virtual void CleanupRemovedFeatureComponents(TMP_TextVector2Component targetComponent, BaseTextFeatureComponent[] previousComponents)
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
                    var allTextComponents = targetComponent.gameObject.GetComponents<TMP_TextVector2Component>();
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

        protected virtual void RemoveFeatureComponentAt(int index)
        {
            var targetComponent = (TMP_TextVector2Component)target;

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
                    // Check if this component is used by other TMP_TextVector2Component instances
                    bool isUsedElsewhere = false;
                    var allTextComponents = targetComponent.gameObject.GetComponents<TMP_TextVector2Component>();
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
