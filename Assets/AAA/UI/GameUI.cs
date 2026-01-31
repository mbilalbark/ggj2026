using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup[] hearts;
    private int currentHeartIndex;
    public void Initialize()
    {
        currentHeartIndex = 0;
        foreach (var heart in hearts)
        {
            heart.alpha = 1;
        }
    }
    
    public void RemoveHeart()
    {
        currentHeartIndex  = (currentHeartIndex + 1) % hearts.Length;
        hearts[currentHeartIndex].alpha = 0.3f;
    }

    public void Test()
    {
        print("Test");
    }
}
