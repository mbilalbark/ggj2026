using System.Collections;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private CanvasGroup[] screens;
    [SerializeField] private GameUI gameUI;
    public void Initialize()
    {
        StartCoroutine(OpenMenu());
    }

    private IEnumerator OpenMenu()
    {
        yield return new WaitForSeconds(3);
        screens[3].alpha = 0;
        screens[3].interactable = false;
        screens[3].blocksRaycasts = false;
        
        screens[0].alpha = 1;
        screens[0].interactable = true;
        screens[0].blocksRaycasts = true;
    }

    public void OpenCredits()
    {
        foreach (var screen in  screens)
        {
            screen.alpha = 0;
            screen.interactable = false;
            screen.blocksRaycasts = false;
        }
        
        screens[4].alpha = 1;
        screens[4].interactable = true;
        screens[4].blocksRaycasts = true;
    }

    public void OnStartLevel()
    {
        print("OnStartLevel");
        LevelManager.Instance.Initialize();
        OpenGameScreen();
        gameUI.Initialize();
    }

    public void OpenRestartMenu()
    {
        foreach (var screen in  screens)
        {
            screen.alpha = 0;
            screen.interactable = false;
            screen.blocksRaycasts = false;
        }
        
        screens[1].alpha = 1;
        screens[1].interactable = true;
        screens[1].blocksRaycasts = true;
    }

    public void TakeHit()
    {
        gameUI.RemoveHeart();
    }
    
    public void GoToMainMenu()
    {
        foreach (var screen in  screens)
        {
            screen.alpha = 0;
            screen.interactable = false;
            screen.blocksRaycasts = false;
        }
        
        screens[0].alpha = 1;
        screens[0].interactable = true;
        screens[0].blocksRaycasts = true;
    }

    public void OpenGameScreen()
    {
        foreach (var screen in  screens)
        {
            screen.alpha = 0;
            screen.interactable = false;
            screen.blocksRaycasts = false;
        }
        
        screens[2].alpha = 1;
        screens[2].interactable = true;
        screens[2].blocksRaycasts = true;
    }

    public void OnRestartLevel()
    {
        foreach (var screen in  screens)
        {
            screen.alpha = 0;
            screen.interactable = false;
            screen.blocksRaycasts = false;
        }
        
        screens[2].alpha = 1;
        screens[2].interactable = true;
        screens[2].blocksRaycasts = true;
        gameUI.Initialize();
        print("OnRestartLevel");
        LevelManager.Instance.ReStartLevel();
    }
}
