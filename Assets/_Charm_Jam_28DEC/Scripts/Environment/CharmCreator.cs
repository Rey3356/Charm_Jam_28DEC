using System.Collections.Generic;
using UnityEngine;

public class CharmCreator : MonoBehaviour
{
    [SerializeField] private BoxCollider2D bounds;
    [SerializeField] private GameObject[] charmPrefabs;
    [SerializeField] private List<GameObject> charms = new List<GameObject>();

    private void Awake()
    {
        bounds = GetComponent<BoxCollider2D>();
    }


    private void Update()
    {
        if (charms.Count < 50)
        {
            GameObject charmObj = Instantiate(charmPrefabs[Random.Range(0, charmPrefabs.Length)], transform);
            charmObj.transform.localPosition = new Vector3(Random.Range(bounds.bounds.min.x, bounds.bounds.max.x), Random.Range(bounds.bounds.min.y, bounds.bounds.max.y));
            charms.Add(charmObj);
        }
    }
}
