using UnityEngine;
using UnityEditor;
using JacobHomanics.TimerSystem;

namespace JacobHomanics.TimerSystem.Editor
{
    [CustomEditor(typeof(Timer))]
    [CanEditMultipleObjects]
    public class TimerEditor : UnityEditor.Editor
    {
        private Timer timer;
        private SerializedProperty durationProp;
        private SerializedProperty elapsedTimeProp;
        private SerializedProperty tickTypeProp;
        private SerializedProperty onTickProp;
        private SerializedProperty onDurationElapsedProp;

        private void OnEnable()
        {
            timer = (Timer)target;

            durationProp = serializedObject.FindProperty("duration");
            elapsedTimeProp = serializedObject.FindProperty("elapsedTime");
            tickTypeProp = serializedObject.FindProperty("tickType");
            onTickProp = serializedObject.FindProperty("OnTick");
            onDurationElapsedProp = serializedObject.FindProperty("OnDurationElapsed");
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

            EditorGUILayout.Space(10);

            // Elapsed Time (read-only display with slider)
            EditorGUI.BeginChangeCheck();
            float newElapsed = EditorGUILayout.Slider("Elapsed Time", timer.ElapsedTime, 0, timer.Duration);
            if (EditorGUI.EndChangeCheck())
            {
                timer.ElapsedTime = newElapsed;
                EditorUtility.SetDirty(timer);
            }

            // Time Left Display
            float timeLeft = timer.GetTimeLeft();
            EditorGUILayout.LabelField($"Time Left: {timeLeft:F2}s", EditorStyles.centeredGreyMiniLabel);

            // Duration Reached Status
            bool isReached = timer.IsDurationReached();
            EditorGUILayout.LabelField($"Duration Reached: {(isReached ? "Yes" : "No")}",
                isReached ? EditorStyles.centeredGreyMiniLabel : EditorStyles.miniLabel);

            EditorGUILayout.Space(10);

            // Quick Test Actions
            EditorGUILayout.LabelField("Quick Test Actions", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add 1s", GUILayout.Height(25)))
            {
                timer.ElapsedTime += 1f;
                EditorUtility.SetDirty(timer);
            }

            if (GUILayout.Button("Add 10s", GUILayout.Height(25)))
            {
                timer.ElapsedTime += 10f;
                EditorUtility.SetDirty(timer);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Subtract 1s", GUILayout.Height(25)))
            {
                timer.ElapsedTime = Mathf.Max(0, timer.ElapsedTime - 1f);
                EditorUtility.SetDirty(timer);
            }

            if (GUILayout.Button("Subtract 10s", GUILayout.Height(25)))
            {
                timer.ElapsedTime = Mathf.Max(0, timer.ElapsedTime - 10f);
                EditorUtility.SetDirty(timer);
            }

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5);
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
            EditorGUILayout.LabelField("Events", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(onTickProp);
            EditorGUILayout.PropertyField(onDurationElapsedProp);
            EditorGUI.indentLevel--;

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawTimerBar()
        {
            Rect rect = GUILayoutUtility.GetRect(18, 18, GUILayout.ExpandWidth(true));

            float timerPercent = timer.Duration > 0 ? timer.ElapsedTime / timer.Duration : 0;
            timerPercent = Mathf.Clamp01(timerPercent);

            // Background
            EditorGUI.DrawRect(rect, new Color(0.2f, 0.2f, 0.2f, 1f));

            // Timer bar (blue to red gradient as time progresses)
            Rect timerRect = new Rect(rect.x, rect.y, rect.width * timerPercent, rect.height);

            // Color gradient: blue (start) -> cyan -> yellow -> red (end)
            Color timerColor;
            // if (timerPercent > 0.66f)
            // {
            //     // Yellow to red
            //     float t = (timerPercent - 0.66f) / 0.34f;
            //     timerColor = Color.Lerp(Color.yellow, Color.red, t);
            // }
            // else if (timerPercent > 0.33f)
            // {
            //     // Cyan to yellow
            //     float t = (timerPercent - 0.33f) / 0.33f;
            //     timerColor = Color.Lerp(Color.cyan, Color.yellow, t);
            // }
            // else
            // {
            //     // Blue to cyan
            //     float t = timerPercent / 0.33f;
            //     timerColor = Color.Lerp(Color.blue, Color.cyan, t);
            // }

            // Blue to cyan
            float t = timerPercent;// / 0.33f;
            timerColor = Color.Lerp(Color.cyan, Color.blue, t);

            EditorGUI.DrawRect(timerRect, timerColor);

            // Border
            EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width, 1), Color.black);
            EditorGUI.DrawRect(new Rect(rect.x, rect.y + rect.height - 1, rect.width, 1), Color.black);
            EditorGUI.DrawRect(new Rect(rect.x, rect.y, 1, rect.height), Color.black);
            EditorGUI.DrawRect(new Rect(rect.x + rect.width - 1, rect.y, 1, rect.height), Color.black);
        }
    }
}

