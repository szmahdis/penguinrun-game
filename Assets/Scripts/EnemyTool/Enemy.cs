using System;
using UnityEngine;

namespace EnemyTool
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemyScriptableObject", order = 1)]
    public class Enemy : ScriptableObject
    {
        public GameObject enemyModel;
        public String texturePath;

    }
}
