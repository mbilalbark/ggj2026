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
        for (int i = 0; i < obstaclesToOpen; i++)
        {
            var randomIndex = -1;
            do 
            {
                randomIndex = Random.Range(0, obstacleCapsules.Length);
            }while (ObstacleIndicesOpened.Contains(randomIndex));

            obstacleCapsules[randomIndex].Initialize(true);
            ObstacleIndicesOpened.Add(randomIndex); 
        }
    }
}
