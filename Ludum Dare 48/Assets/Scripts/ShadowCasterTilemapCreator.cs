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

    private BoundsInt bounds;
    public int boundsSize;

    // Start is called before the first frame update
    void Start()
    {
        CreateShadowCasters();

    }

    // Update is called once per frame
    void FixedUpdate ()
    {


        //DisableAllCastersNotWithinRange();
    }

    public void CreateShadowCasters()
    {
        foreach (Transform child in shadowCasterHolder)
        {
            Destroy(child.gameObject);
        }

        int br = 0;

        foreach (Vector3Int tilePos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(tilePos) != null)// && TileIsExposed(tilePos, tilemap))
            {
                CreateShadowCaster(tilePos);

                br++;
            }
        }

        Debug.Log(br);
    }

    void CreateShadowCaster(Vector3Int tilePos)
    {
        GameObject shadowCaster = Instantiate(shadowCasterPrefab, tilemap.CellToWorld(tilePos) + offset, Quaternion.identity);

        shadowCaster.transform.parent = shadowCasterHolder;

        shadowCasterObjects.Add(shadowCaster);
        shadowCasterPositions.Add(shadowCaster.transform.position - new Vector3(0.5f, 0.5f));
    }

    bool TileIsExposed(Vector3Int tilePos, Tilemap tilemap)
    {
        if (tilemap.GetTile(tilePos + Vector3Int.up) == null) return true;
        if (tilemap.GetTile(tilePos + Vector3Int.down) == null) return true;
        if (tilemap.GetTile(tilePos + Vector3Int.right) == null) return true;
        if (tilemap.GetTile(tilePos + Vector3Int.left) == null) return true;

        return false;
    }

    void DisableAllCastersNotWithinRange()
    {
        Debug.Log("why bro");

        foreach (GameObject caster in shadowCasterObjects)
        {
            caster.SetActive(false);
        }

        Vector3Int roundedPos = Vector3Int.RoundToInt(transform.position);

        bounds.SetMinMax(roundedPos - new Vector3Int(boundsSize, boundsSize, 0), roundedPos + new Vector3Int(boundsSize, boundsSize, 0));

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                if (shadowCasterPositions.Contains(tilemap.CellToWorld(new Vector3Int(x, y, 0))))
                {
                    int index = shadowCasterPositions.IndexOf(tilemap.CellToWorld(new Vector3Int(x, y, 0)));

                    shadowCasterObjects[index].SetActive(true);
                }
            }
        }
    }

    public void RemoveShadowCasterFromPosition(Vector3Int position)
    {
        int index = shadowCasterPositions.IndexOf(position);

        /*if (tilemap.GetTile(position + Vector3Int.up) != null)
        {
            CreateShadowCaster(position);
        }
        if (tilemap.GetTile(position + Vector3Int.down) != null)
        {
            CreateShadowCaster(position);
        }
        if (tilemap.GetTile(position + Vector3Int.right) != null)
        {
            CreateShadowCaster(position);
        }
        if (tilemap.GetTile(position + Vector3Int.left) != null)
        {
            CreateShadowCaster(position);
        }*/

        Destroy(shadowCasterObjects[index]);
    }
}
