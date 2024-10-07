using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject dirtPrefab;
    [SerializeField] private GameObject GroundParent;
    [SerializeField] private GameObject BuildingsParent;
    [SerializeField] private GameObject OreParent;
    [SerializeField] private GameObject[] OrePrefabs;
    void Start()
    {
        Vector2 MapSize = new Vector2(19*(1/dirtPrefab.transform.localScale.x),7.5f*(1/dirtPrefab.transform.localScale.x));
        for (int i = 0; i < MapSize.x; i++)
        {
            for (int j = 0; j < MapSize.y; j++)
            {
                Instantiate(dirtPrefab, new Vector3(((float)i) * dirtPrefab.transform.localScale.x, ((float)j)*dirtPrefab.transform.localScale.y,0) + this.transform.position , Quaternion.identity, GroundParent.transform);
            }
        }
        
    }
}
