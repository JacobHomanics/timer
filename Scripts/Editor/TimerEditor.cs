using UnityEngine;
using UnityEditor;

namespace JacobHomanics.Timer.Editor
{
    [CustomEditor(typeof(Timer))]
    [CanEditMultipleObjects]
    public class TimerEditor : UnityEditor.Editor
    {
        private Timer timer;
        private SerializedProperty durationProp;
        private SerializedProperty elapsedTimeProp;
        private SerializedProperty loopProp;
        private SerializedProperty tickTypeProp;
        private SerializedProperty onTickProp;
        private SerializedProperty onDurationElapsedProp;
        private SerializedProperty onDurationNotElapsedProp;

        private void OnEnable()
        {
            timer = (Timer)target;

            durationProp = serializedObject.FindProperty("duration");
            elapsedTimeProp = serializedObject.FindProperty("elapsedTime");
            tickTypeProp = serializedObject.FindProperty("tickType");
            onTickProp = serializedObject.FindProperty("OnTick");
            onDurationElapsedProp = serializedObject.FindProperty("OnDurationReached");
            onDurationNotElapsedProp = serializedObject.FindProperty("OnNotDurationReached");
            loopProp = serializedObject.FindProperty("loop");

        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.Space();

            // Timer Header
            EditorGUILayout.LabelField("Timer System", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // Timer Bar Visualization
            DrawTimerBar();

            EditorGUILayout.Space();

            // Configuration Section
            EditorGUILayout.LabelField("Configuration", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(durationProp, new GUIContent("Duration"));
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                // Ensure elapsed time doesn't exceed new duration
                if (timer.ElapsedTime > timer.Duration)
                {
                    timer.ElapsedTime = timer.Duration;
                    EditorUtility.SetDirty(timer);
                }
            }

            EditorGUILayout.PropertyField(tickTypeProp, new GUIContent("Tick Type"));
            EditorGUILayout.PropertyField(loopProp, new GUIContent("Loop"));

            EditorGUILayout.Space(10);

            // Elapsed Time (read-only display with slider)
            EditorGUI.BeginChangeCheck();
            float newElapsed = EditorGUILayout.Slider("Elapsed Time", timer.ElapsedTime, 0, timer.Duration);
            if (EditorGUI.EndChangeCheck())
            {
                timer.ElapsedTime = newElapsed;
                EditorUtility.SetDirty(timer);
            }

            // Time Left (slider - read-only display)
            EditorGUI.BeginChangeCheck();
            float timeLeft = timer.GetTimeLeft();
            float newTimeLeft = EditorGUILayout.Slider("Time Left", timeLeft, 0, timer.Duration);
            if (EditorGUI.EndChangeCheck())
            {
                timer.ElapsedTime = timer.Duration - newTimeLeft;
                EditorUtility.SetDirty(timer);
            }

            // Duration Reached Status
            bool isReached = timer.IsDurationReached();
            EditorGUILayout.LabelField($"Duration Reached: {(isReached ? "Yes" : "No")}",
                isReached ? EditorStyles.centeredGreyMiniLabel : EditorStyles.miniLabel);

            EditorGUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Reset to 0", GUILayout.Height(25)))
            {
                timer.ElapsedTime = 0;
                EditorUtility.SetDirty(timer);
            }

            if (GUILayout.Button("Set to Duration", GUILayout.Height(25)))
            {
                timer.ElapsedTime = timer.Duration;
                EditorUtility.SetDirty(timer);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);

            // Events Section
            // EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            // EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(onTickProp);
            EditorGUILayout.PropertyField(onDurationElapsedProp);
            EditorGUILayout.PropertyField(onDurationNotElapsedProp);


            // EditorGUI.indentLevel--;

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawTimerBar()
        {
            Rect rect = GUILayoutUtility.GetRect(18, 18, GUILayout.ExpandWidth(true));

            float timerPercent = timer.Duration > 0 ? timer.ElapsedTime / timer.Duration : 0;
            float clampedPercent = Mathf.Clamp01(timerPercent);

            // Background
            EditorGUI.DrawRect(rect, new Color(0.2f, 0.2f, 0.2f, 1f));

            // Draw normal progress bar (cyan to blue gradient)
            Rect timerRect = new Rect(rect.x, rect.y, rect.width * clampedPercent, rect.height);
            float t = clampedPercent;
            Color timerColor = Color.Lerp(Color.gray, Color.green, t);
            EditorGUI.DrawRect(timerRect, timerColor);

            // Border
            EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width, 1), Color.black);
            EditorGUI.DrawRect(new Rect(rect.x, rect.y + rect.height - 1, rect.width, 1), Color.black);
            EditorGUI.DrawRect(new Rect(rect.x, rect.y, 1, rect.height), Color.black);
            EditorGUI.DrawRect(new Rect(rect.x + rect.width - 1, rect.y, 1, rect.height), Color.black);
        }
    }
}

