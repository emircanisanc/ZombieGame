using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupController : MonoBehaviour
{
    public static List<EnemyBaseAbstract> Enemies {
         get {
             if(enemies == null) {
              enemies = new List<EnemyBaseAbstract>(); }
            return enemies;
              } }
    private static List<EnemyBaseAbstract> enemies;
    public int enemiesPerBatch = 3;
    private int currentEnemyIndex = 0;

    void Update()
    {
        Vector3 playerPos = PlayerManager.Instance.position;
        for (int i = 0; i < enemiesPerBatch; i++)
        {
            if (currentEnemyIndex >= enemies.Count)
            {
                // All enemies have been updated, exit the loop
                break;
            }

            var enemy = enemies[currentEnemyIndex];
            enemy.UpdateMethod(playerPos);

            currentEnemyIndex++;
        }

        // Reset the enemy index if all enemies have been updated
        if (currentEnemyIndex >= enemies.Count)
        {
            currentEnemyIndex = 0;
        }
    }
}
