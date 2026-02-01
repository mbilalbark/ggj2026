using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private Level currentLevel;
    
    [SerializeField] private Level[] levels;
    private int currentLevelIndex = -1;

    public void Initialize()
    {
        currentLevelIndex =  0;
        LoadLevel(currentLevelIndex);
    }

    private void LoadLevel(int levelIndex)
    {
        currentLevel = Instantiate(levels[levelIndex]) as Level;
        print("LoadLevel");
        currentLevel.Initialize();
    }

    private void UnloadLevel()
    {
        Destroy(currentLevel.gameObject);
    }

    public void ReStartLevel()
    {
        UnloadLevel();
        LoadLevel(currentLevelIndex);
    }

    public void GoToNextLevel()
    {
        UnloadLevel();
        currentLevelIndex = (currentLevelIndex + 1) % levels.Length;
        LoadLevel(currentLevelIndex);
    }
}

