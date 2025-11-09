// using UnityEngine;
// using UnityEditor;
// using UnityEditorInternal;
// using TMPro;
// using JacobHomanics.TimerSystem;
// using JacobHomanics.UI;

// namespace JacobHomanics.TimerSystem.Editor
// {

//     [CustomEditor(typeof(TimerImage))]
//     [CanEditMultipleObjects]
//     public class TimerImageEditor : UnityEditor.Editor
//     {
//         private TimerImage timerImage;

//         // Serialized Properties
//         private SerializedProperty timerProp;
//         private SerializedProperty imageProp;
//         private SerializedProperty reverseFillProp;

//         private SerializedProperty featureTogglesProp;

//         // Feature type identifiers
//         private const string TextDisplayType = "TextDisplayFeature2";
//         private const string ColorGradientType = "ColorGradientFeature";
//         private const string BackgroundFillType = "BackgroundFillFeature";
//         private const string FlashingType = "FlashingFeature";

//         // Available features mapping
//         private static readonly System.Collections.Generic.Dictionary<string, string> AvailableFeatures = new System.Collections.Generic.Dictionary<string, string>
//     {
//         { "Text Display", TextDisplayType },
//         { "Color Gradient", ColorGradientType },
//         { "Background Fill", BackgroundFillType },
//         { "Flashing", FlashingType }
//     };

//         // Foldout states
//         private bool showReferences = true;

//         // ReorderableList for features
//         private ReorderableList featuresList;
//         private System.Collections.Generic.Dictionary<int, bool> featureFoldoutStates = new System.Collections.Generic.Dictionary<int, bool>();

//         private void OnEnable()
//         {
//             timerImage = (TimerImage)target;

//             // Find all serialized properties
//             timerProp = serializedObject.FindProperty("timer");
//             imageProp = serializedObject.FindProperty("image");
//             reverseFillProp = serializedObject.FindProperty("reverseFill");
//             featureTogglesProp = serializedObject.FindProperty("featureToggles");

//             // Setup ReorderableList for features
//             featuresList = new ReorderableList(serializedObject, featureTogglesProp, true, true, true, true);
//             featuresList.drawHeaderCallback = (Rect rect) =>
//             {
//                 EditorGUI.LabelField(rect, "Features");
//             };
//             featuresList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
//             {
//                 var element = featureTogglesProp.GetArrayElementAtIndex(index);
//                 string featureType = GetFeatureType(element);
//                 string displayName = GetFeatureDisplayName(featureType);

//                 // Check if managed reference is broken
//                 bool isBroken = string.IsNullOrEmpty(element.managedReferenceFullTypename) ||
//                                (element.managedReferenceValue == null && !string.IsNullOrEmpty(element.managedReferenceFullTypename));

//                 if (isBroken && string.IsNullOrEmpty(featureType))
//                 {
//                     displayName = "Missing Type (Click to Fix)";
//                 }

//                 // Get or create foldout state
//                 if (!featureFoldoutStates.ContainsKey(index))
//                 {
//                     featureFoldoutStates[index] = true;
//                 }

//                 rect.y += 2;
//                 float yPos = rect.y;

//                 // Offset to account for drag handle (typically 20px)
//                 float dragHandleWidth = 20f;
//                 float foldoutX = rect.x + dragHandleWidth;
//                 float foldoutWidth = rect.width - dragHandleWidth;

//                 // Draw foldout
//                 featureFoldoutStates[index] = EditorGUI.Foldout(
//                     new Rect(foldoutX, yPos, foldoutWidth, EditorGUIUtility.singleLineHeight),
//                     featureFoldoutStates[index],
//                     displayName,
//                     true
//                 );

//                 yPos += EditorGUIUtility.singleLineHeight + 2;

//                 // Draw properties if expanded
//                 if (featureFoldoutStates[index])
//                 {
//                     float propertyY = yPos;
//                     float indentOffset = 15f; // Indent for properties under foldout

//                     // Show fix button for broken references
//                     if (isBroken && string.IsNullOrEmpty(featureType))
//                     {
//                         EditorGUI.HelpBox(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight * 2),
//                             "This feature references a missing type. Click 'Fix' to recreate it.", MessageType.Warning);
//                         propertyY += EditorGUIUtility.singleLineHeight * 2 + 2;

//                         if (GUI.Button(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), "Fix (Recreate as Text Display)"))
//                         {
//                             element.managedReferenceValue = new TextDisplayFeature2();
//                             serializedObject.ApplyModifiedProperties();
//                             return;
//                         }
//                         propertyY += EditorGUIUtility.singleLineHeight + 2;
//                     }
//                     else if (featureType == TextDisplayType)
//                     {
//                         var textProp = element.FindPropertyRelative("text");
//                         var displayTypeProp = element.FindPropertyRelative("displayType");
//                         var formatProp = element.FindPropertyRelative("format");
//                         if (textProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), textProp, new GUIContent("Text"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (displayTypeProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), displayTypeProp, new GUIContent("Display Type"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (formatProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), formatProp, new GUIContent("Format"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                     }
//                     else if (featureType == ColorGradientType)
//                     {
//                         var colorAtMinProp = element.FindPropertyRelative("colorAtMin");
//                         var colorAtHalfwayProp = element.FindPropertyRelative("colorAtHalfway");
//                         var colorAtMaxProp = element.FindPropertyRelative("colorAtMax");
//                         var reverseFillProp = element.FindPropertyRelative("reverseFill");
//                         if (colorAtMinProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), colorAtMinProp, new GUIContent("Color at Min"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (colorAtHalfwayProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), colorAtHalfwayProp, new GUIContent("Color at Halfway"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (colorAtMaxProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), colorAtMaxProp, new GUIContent("Color at Max"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (reverseFillProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), reverseFillProp, new GUIContent("Reverse Fill"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                     }
//                     else if (featureType == BackgroundFillType)
//                     {
//                         var backgroundFillProp = element.FindPropertyRelative("backgroundFill");
//                         var keepSizeConsistentProp = element.FindPropertyRelative("keepSizeConsistent");
//                         var animationSpeedProp = element.FindPropertyRelative("animationSpeed");
//                         var speedCurveProp = element.FindPropertyRelative("speedCurve");
//                         var delayProp = element.FindPropertyRelative("delay");
//                         var reverseFillProp = element.FindPropertyRelative("reverseFill");


//                         if (backgroundFillProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), backgroundFillProp, new GUIContent("Background Fill Image"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (keepSizeConsistentProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), keepSizeConsistentProp, new GUIContent("Keep Size Consistent"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (animationSpeedProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), animationSpeedProp, new GUIContent("Animation Speed"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (speedCurveProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), speedCurveProp, new GUIContent("Speed Curve"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (delayProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), delayProp, new GUIContent("Delay"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (reverseFillProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), reverseFillProp, new GUIContent("Reverse Fill"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }

//                     }
//                     else if (featureType == FlashingType)
//                     {
//                         var flashImageProp = element.FindPropertyRelative("flashImage");
//                         var thresholdPercentProp = element.FindPropertyRelative("thresholdPercent");
//                         var flashColor1Prop = element.FindPropertyRelative("flashColor1");
//                         var flashColor2Prop = element.FindPropertyRelative("flashColor2");
//                         var flashSpeedProp = element.FindPropertyRelative("flashSpeed");
//                         var reverseFillProp = element.FindPropertyRelative("reverseFill");
//                         if (flashImageProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), flashImageProp, new GUIContent("Flash Image"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (thresholdPercentProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), thresholdPercentProp, new GUIContent("Threshold Percent"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (flashColor1Prop != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), flashColor1Prop, new GUIContent("Flash Color 1"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (flashColor2Prop != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), flashColor2Prop, new GUIContent("Flash Color 2"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (flashSpeedProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), flashSpeedProp, new GUIContent("Flash Speed"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                         if (reverseFillProp != null)
//                         {
//                             EditorGUI.PropertyField(new Rect(foldoutX + indentOffset, propertyY, foldoutWidth - indentOffset, EditorGUIUtility.singleLineHeight), reverseFillProp, new GUIContent("Reverse Fill"));
//                             propertyY += EditorGUIUtility.singleLineHeight + 2;
//                         }
//                     }
//                 }
//             };
//             featuresList.elementHeightCallback = (int index) =>
//             {
//                 if (!featureFoldoutStates.ContainsKey(index) || !featureFoldoutStates[index])
//                 {
//                     return EditorGUIUtility.singleLineHeight + 4;
//                 }

//                 var element = featureTogglesProp.GetArrayElementAtIndex(index);
//                 string featureType = GetFeatureType(element);

//                 // Check if managed reference is broken
//                 bool isBroken = string.IsNullOrEmpty(element.managedReferenceFullTypename) ||
//                                (element.managedReferenceValue == null && !string.IsNullOrEmpty(element.managedReferenceFullTypename));

//                 if (isBroken && string.IsNullOrEmpty(featureType))
//                 {
//                     // Height for broken reference: foldout + help box + button
//                     return EditorGUIUtility.singleLineHeight + 2 +
//                            EditorGUIUtility.singleLineHeight * 2 + 2 +
//                            EditorGUIUtility.singleLineHeight + 2 + 4;
//                 }

//                 int propertyCount = GetPropertyCount(featureType);
//                 return (EditorGUIUtility.singleLineHeight + 2) * (1 + propertyCount) + 4;
//             };
//             featuresList.onAddDropdownCallback = (Rect buttonRect, ReorderableList list) =>
//             {
//                 ShowAddFeatureMenu();
//             };
//             featuresList.onRemoveCallback = (ReorderableList list) =>
//             {
//                 if (list.index >= 0 && list.index < featureTogglesProp.arraySize)
//                 {
//                     // Remove foldout state
//                     if (featureFoldoutStates.ContainsKey(list.index))
//                     {
//                         featureFoldoutStates.Remove(list.index);
//                     }

//                     // Update foldout state indices
//                     var keysToUpdate = new System.Collections.Generic.List<int>();
//                     foreach (var key in featureFoldoutStates.Keys)
//                     {
//                         if (key > list.index)
//                         {
//                             keysToUpdate.Add(key);
//                         }
//                     }
//                     foreach (var key in keysToUpdate)
//                     {
//                         bool value = featureFoldoutStates[key];
//                         featureFoldoutStates.Remove(key);
//                         featureFoldoutStates[key - 1] = value;
//                     }

//                     featureTogglesProp.DeleteArrayElementAtIndex(list.index);
//                 }
//             };
//             featuresList.onReorderCallback = (ReorderableList list) =>
//             {
//                 // Rebuild foldout states after reordering
//                 // This is a simple approach - we'll rebuild the dictionary
//                 var newFoldoutStates = new System.Collections.Generic.Dictionary<int, bool>();
//                 for (int i = 0; i < featureTogglesProp.arraySize; i++)
//                 {
//                     // Try to preserve foldout state if possible
//                     // Since we don't know the exact mapping, we'll default to true for all
//                     newFoldoutStates[i] = featureFoldoutStates.ContainsKey(i) ? featureFoldoutStates[i] : true;
//                 }
//                 featureFoldoutStates = newFoldoutStates;
//             };
//         }

//         public override void OnInspectorGUI()
//         {
//             serializedObject.Update();

//             EditorGUILayout.Space();
//             EditorGUILayout.LabelField("Timer Image", EditorStyles.boldLabel);
//             EditorGUILayout.Space();

//             // References Section
//             showReferences = EditorGUILayout.Foldout(showReferences, "References", true);
//             if (showReferences)
//             {
//                 EditorGUI.indentLevel++;
//                 EditorGUILayout.PropertyField(timerProp, new GUIContent("Timer"));
//                 EditorGUILayout.PropertyField(imageProp, new GUIContent("Image"));
//                 EditorGUILayout.PropertyField(reverseFillProp, new GUIContent("Reverse Fill"));
//                 EditorGUI.indentLevel--;
//                 EditorGUILayout.Space();
//             }

//             // Features Section Title
//             EditorGUILayout.Space(10);
//             EditorGUILayout.LabelField("Features", EditorStyles.boldLabel);
//             EditorGUILayout.Space(5);

//             // Display ReorderableList
//             featuresList.DoLayoutList();

//             serializedObject.ApplyModifiedProperties();
//         }

//         private string GetFeatureDisplayName(string featureType)
//         {
//             foreach (var kvp in AvailableFeatures)
//             {
//                 if (kvp.Value == featureType)
//                 {
//                     return kvp.Key;
//                 }
//             }
//             return featureType ?? "Unknown";
//         }

//         private int GetPropertyCount(string featureType)
//         {
//             if (featureType == TextDisplayType)
//             {
//                 return 3;
//             }
//             else if (featureType == ColorGradientType)
//             {
//                 return 4;
//             }
//             else if (featureType == BackgroundFillType)
//             {
//                 return 6;
//             }
//             else if (featureType == FlashingType)
//             {
//                 return 6;
//             }
//             return 0;
//         }

//         private void ShowAddFeatureMenu()
//         {
//             GenericMenu menu = new GenericMenu();

//             // Get currently added feature types
//             System.Collections.Generic.HashSet<string> addedFeatureTypes = new System.Collections.Generic.HashSet<string>();
//             for (int i = 0; i < featureTogglesProp.arraySize; i++)
//             {
//                 var element = featureTogglesProp.GetArrayElementAtIndex(i);
//                 string featureType = GetFeatureType(element);
//                 if (featureType != null)
//                 {
//                     addedFeatureTypes.Add(featureType);
//                 }
//             }

//             // Add menu items for available features that aren't already added
//             foreach (var kvp in AvailableFeatures)
//             {
//                 string displayName = kvp.Key;
//                 string featureType = kvp.Value;

//                 if (!addedFeatureTypes.Contains(featureType))
//                 {
//                     menu.AddItem(new GUIContent(displayName), false, () => AddFeature(featureType));
//                 }
//                 else
//                 {
//                     menu.AddDisabledItem(new GUIContent(displayName + " (Already Added)"));
//                 }
//             }

//             // If all features are added, show a message
//             if (addedFeatureTypes.Count >= AvailableFeatures.Count)
//             {
//                 menu.AddDisabledItem(new GUIContent("All features added"));
//             }

//             menu.ShowAsContext();
//         }

//         private void AddFeature(string featureType)
//         {
//             serializedObject.Update();
//             int index = featureTogglesProp.arraySize;
//             featureTogglesProp.arraySize++;
//             var element = featureTogglesProp.GetArrayElementAtIndex(index);

//             // Set the managed reference type based on feature type
//             element.managedReferenceValue = CreateFeatureInstance(featureType);

//             serializedObject.ApplyModifiedProperties();

//             // Initialize foldout state for new feature
//             featureFoldoutStates[index] = true;
//         }

//         private object CreateFeatureInstance(string featureType)
//         {
//             if (featureType == TextDisplayType)
//             {
//                 return new TextDisplayFeature2();
//             }
//             else if (featureType == ColorGradientType)
//             {
//                 return new ColorGradientFeature();
//             }
//             else if (featureType == BackgroundFillType)
//             {
//                 return new BackgroundFillFeature();
//             }
//             else if (featureType == FlashingType)
//             {
//                 return new FlashingFeature();
//             }
//             return null;
//         }

//         private string GetFeatureType(SerializedProperty element)
//         {
//             // First, try to get the managed reference type name directly
//             string managedRefType = element.managedReferenceFullTypename;
//             if (!string.IsNullOrEmpty(managedRefType))
//             {
//                 // Unity stores nested types as "Namespace.OuterClass+NestedClass" or "OuterClass+NestedClass"
//                 // Check if it matches our known types
//                 if (managedRefType.Contains("TextDisplayFeature"))
//                 {
//                     return TextDisplayType;
//                 }
//                 else if (managedRefType.Contains("ColorGradientFeature"))
//                 {
//                     return ColorGradientType;
//                 }
//                 else if (managedRefType.Contains("BackgroundFillFeature"))
//                 {
//                     return BackgroundFillType;
//                 }
//                 else if (managedRefType.Contains("FlashingFeature"))
//                 {
//                     return FlashingType;
//                 }
//             }

//             // Fallback: Check for type-specific properties to determine the feature type
//             if (element.FindPropertyRelative("text") != null && element.FindPropertyRelative("displayType") != null)
//             {
//                 return TextDisplayType;
//             }
//             else if (element.FindPropertyRelative("colorAtMin") != null)
//             {
//                 return ColorGradientType;
//             }
//             else if (element.FindPropertyRelative("backgroundFill") != null)
//             {
//                 return BackgroundFillType;
//             }
//             else if (element.FindPropertyRelative("thresholdPercent") != null)
//             {
//                 return FlashingType;
//             }
//             return null;
//         }

//         private SerializedProperty GetFeatureElementByType(string featureType)
//         {
//             for (int i = 0; i < featureTogglesProp.arraySize; i++)
//             {
//                 var element = featureTogglesProp.GetArrayElementAtIndex(i);
//                 if (GetFeatureType(element) == featureType)
//                 {
//                     return element;
//                 }
//             }
//             return null;
//         }
//     }

// }
