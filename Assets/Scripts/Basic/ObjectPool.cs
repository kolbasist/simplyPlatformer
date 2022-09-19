using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{    
    [SerializeField] private int _capacity;    
    [SerializeField] private GameObject _contianer;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject[] templates)
    {
        for (int i = 0; i < _capacity; i++)
        {
            int index = Random.Range(0, templates.Length);
            GameObject spawned = Instantiate(templates[index], _contianer.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);
       
        return result != null;
    }
}
