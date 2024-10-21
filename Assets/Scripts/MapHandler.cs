using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float mapQuality = 1f; //0.5-1.5;
    [SerializeField] private GameObject dirtPrefab;
    [SerializeField] private GameObject GroundParent;
    [SerializeField] private GameObject BuildingsParent;
    [SerializeField] private GameObject OreParent;
    [SerializeField] private GameObject[] OrePrefabs;
    private Vector3 MapTopLeft = new Vector2(8.5f, 1.5f);
    
    void Start()
    {
        SpawnOres();
        //SpawnDirt();
    }
    private void SpawnDirt()
    {
       Vector2 MapSize = new Vector2(19*(1/dirtPrefab.transform.localScale.x),6.5f*(1/dirtPrefab.transform.localScale.y));
       for (int i = 0; i < MapSize.x; i++)
       {
           for (int j = 0; j < MapSize.y; j++)
           {
               Instantiate(dirtPrefab, new Vector3(((float)i) * dirtPrefab.transform.localScale.x, ((float)j)*dirtPrefab.transform.localScale.y,0) +           this.transform.position , Quaternion.identity, GroundParent.transform);
           }
       }
    }
    private void SpawnOres()
    {
        int oreCount = (int)(8f * mapQuality);
        for (int i = 0; i < oreCount; i++)
        {
            int oreIndex = Random.Range(0, OrePrefabs.Length);
            Vector2 orePosition = new Vector2(Random.Range(transform.position.x+0.5f, MapTopLeft.x), Random.Range(transform.position.y + 0.5f, MapTopLeft.y));
            GameObject ore = Instantiate(OrePrefabs[oreIndex], orePosition, Quaternion.identity, OreParent.transform);
            //rotate ore randomly and scale it randomly
            ore.transform.Rotate(0, 0, Random.Range(0, 360));
            float scaley = Random.Range(0.75f, 1.25f);
            float scalex = Random.Range(0.75f, 1.25f);
            ore.transform.localScale = new Vector3(scalex, scaley, 1);
            VeinInfo veinInfo = ore.GetComponent<VeinInfo>();
            veinInfo.SetVeinSize(scalex * scaley);
        }

    }
}
