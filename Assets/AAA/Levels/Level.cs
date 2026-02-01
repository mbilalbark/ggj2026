using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    [SerializeField] private ObstacleCapsule[] obstacleCapsules;
    [SerializeField] private int obstaclesToOpen = 5;
    private List<int> ObstacleIndicesOpened = new List<int>();
    public void Initialize()
    {
       OpenRandomObstacle();
    }

    private void OpenRandomObstacle()
    {
        print(obstacleCapsules.Length);
        for (int i = 0; i < obstacleCapsules.Length; i++)
        {
            obstacleCapsules[i].Initialize(true);
        }
    }
}
