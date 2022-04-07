using System;
using UnityEditor;
using UnityEngine;

namespace EnemyTool
{
    [CustomEditor(typeof(EnemyCreator))]
    public class EnemyCreatorEditor : Editor
    {
        public Enemy[] enemyOptions;
        
        float thumbnailWidth = 70;
        float thumbnailHeight = 70;
        float labelWidth = 150f;

        public void OnEnable()
        {
            enemyOptions = Resources.LoadAll<Enemy>("EnemyData/");
        }

        public override void OnInspectorGUI()
        {
            
            DrawDefaultInspector();
            
            EnemyCreator enemyCreator = (EnemyCreator)target;

            GUILayout.BeginHorizontal();
            foreach (Enemy enemyOption in enemyOptions)
            {
                if (GUILayout.Button(Resources.Load<Texture>(enemyOption.texturePath), 
                    GUILayout.Width(thumbnailWidth), GUILayout.Height(thumbnailHeight)))
                {
                    enemyCreator.ChangeModel(enemyOption);
                }
            }
            GUILayout.EndHorizontal();
           
            if (GUILayout.Button("WayPoints"))
            {
                enemyCreator.ChangePattern();
            }
    
          
        }
    }
}
