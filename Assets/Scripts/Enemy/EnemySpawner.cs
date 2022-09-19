using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private Enemy[] _templates;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        GameObject[] prefabs =new GameObject[_templates.Length];
        for(int i = 0; i < prefabs.Length; i++)        
            prefabs[i] = _templates[i].gameObject;

        Initialize(prefabs);

        foreach(Transform path in _spawnPoints)
        {   
            if (TryGetObject(out GameObject enemy))
            {
                enemy.GetOrAddComponent<PathPatroler>().SetParameters(path);                
                SetEnemy(enemy, path.position);
            } 
        }
    }

    private void SetEnemy(GameObject enemy, Vector3 spawnPoint)
    {
        enemy.SetActive(true);
        enemy.transform.position = spawnPoint;
    }
}
