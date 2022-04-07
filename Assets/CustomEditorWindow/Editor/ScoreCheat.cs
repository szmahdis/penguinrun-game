using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProgressController))]
public class ScoreCheat : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        ProgressController progressController = (ProgressController)target;

        progressController.score = (int)EditorGUILayout.Slider(progressController.score, 0, 9950);
        progressController.scoreText.text = "Score: " + progressController.score.ToString();

    }
}
