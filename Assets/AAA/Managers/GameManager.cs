using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        UIManager.Instance.Initialize();
        //SoundManager.Instance.PlayGameMusic();
    }
}
