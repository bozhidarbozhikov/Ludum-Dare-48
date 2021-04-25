using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallLight : MonoBehaviour
{
    public Tilemap tilemap;

    public float lightRadius;

    public Collider2D[] collidersInLOS;

    public LayerMask linecastMask;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Vector3Int tilePos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(tilePos) != null && Vector3.Distance(transform.position, tilemap.CellToWorld(tilePos)) <= lightRadius && (!Physics.Linecast(transform.position, tilemap.CellToWorld(tilePos), linecastMask)))
            {
                Debug.Log(tilePos);
            }
        }
    }

    private void FixedUpdate()
    {
        //collidersInLOS = Physics2D.OverlapCircleAll(transform.position, lightRadius);

        /*foreach (Collider2D col in collidersInLOS)
        {
            if (!Physics.Linecast(transform.position, col.transform.position))
            {
                
            }
        }*/

        
    }
}
