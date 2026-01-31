using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Level[] levels;
    private int currentLevelIndex = -1;
    protected override void Awake()
    {
        base.Awake();
    }

    public void Initialize()
    {
        currentLevelIndex =  0;
        LoadLevel(currentLevelIndex);
    }

    private void LoadLevel(int levelIndex)
    {
        var level = Instantiate(levels[levelIndex]) as Level;
        level.Initialize();
    }

    private void UnloadLevel(int levelIndex)
    {
        
    }

    private void OpenLevel(int levelIndex)
    {
        
    }

    public void LevelOver() 
    {
        
    }
    


}

