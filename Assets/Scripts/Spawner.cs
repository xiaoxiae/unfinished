using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();

    public Player player;
    
    public GameObject enemyPrefab;
    public GameObject ammoPrefab;
    public GameObject healthPrefab;
    
    void Start()
    {
        foreach (EnemySpawner enemySpawner in GetComponentsInChildren<EnemySpawner>())
        {
            
            
            Enemy enemy = Instantiate(enemyPrefab, enemySpawner.GetComponent<Transform>().position, new Quaternion()).GetComponent<Enemy>();
            enemy.target = player.GetComponent<Rigidbody2D>();
            enemies.Add(enemy);
        }
    }

    void Update()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].CurrentHealth <= 0)
                enemies.RemoveAt(i--);
        }
    }
}
