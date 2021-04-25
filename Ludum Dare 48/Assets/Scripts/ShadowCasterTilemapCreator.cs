using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShadowCasterTilemapCreator : MonoBehaviour
{
    public GameObject shadowCasterPrefab;

    public Tilemap tilemap;

    public Transform shadowCasterHolder;

    public Vector3 offset;

    //[HideInInspector]
    public List<GameObject> shadowCasterObjects;
    //[HideInInspector]
    public List<Vector3> shadowCasterPositions;

    // Start is called before the first frame update
    void Start()
    {
        CreateShadowCasters();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateShadowCasters()
    {
        foreach (Vector3Int tilePos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(tilePos) != null)
            {
                GameObject shadowCaster = Instantiate(shadowCasterPrefab, tilemap.CellToWorld(tilePos) + offset, Quaternion.identity);

                shadowCaster.transform.parent = shadowCasterHolder;

                shadowCasterObjects.Add(shadowCaster);
                shadowCasterPositions.Add(shadowCaster.transform.position - new Vector3(0.5f, 0.5f));
            }
        }
    }

    public void RemoveShadowCasterFromPosition(Vector3Int position)
    {
        int index = shadowCasterPositions.IndexOf(position);

        Destroy(shadowCasterObjects[index]);
    }
}
