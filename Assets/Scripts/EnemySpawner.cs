using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Transform> _paths;
    [SerializeField] Enemy _template;
   
    private void Start()
    {
        foreach (Transform path in _paths)
        {
           
        }

        
    }

}
