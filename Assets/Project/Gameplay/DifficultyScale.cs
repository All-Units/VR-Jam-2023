using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Difficulty Scale", fileName = "New Difficulty Scale")]
public class DifficultyScale : ScriptableObject
{
    public int currentLevel = 0;
    public List<DifficultyLevel> levels = new List<DifficultyLevel>();

    public void CheckScore(int score)
    {
        if(currentLevel == levels.Count - 1)
            return;

        if (score >= levels[currentLevel].scoreToReach)
        {
            IncreaseLevel();
        }
    }

    private void IncreaseLevel()
    {
        foreach (var difficultyModifier in levels[currentLevel].modifiers)
        {
            difficultyModifier.Apply();
        }

        currentLevel++;
    }

    private void OnValidate()
    {
        levels = levels.OrderBy(lvl => lvl.scoreToReach).ToList();
    }
}

[Serializable]
public class DifficultyLevel
{
    public int scoreToReach = -1;
    public List<DifficultyModifier> modifiers = new List<DifficultyModifier>();
    
    public int CompareTo(DifficultyLevel other)
    {
        if (other == null)
            return 1;

        return scoreToReach.CompareTo(other.scoreToReach);
    }
}