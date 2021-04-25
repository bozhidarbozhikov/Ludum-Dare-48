using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelfShadowAdjuster : MonoBehaviour
{
    public Tilemap tilemap;

    public Transform player;

    public float lightRadius;

    BoundsInt bounds;

    [HideInInspector]
    public List<GameObject> shadowCasterObjects;
    [HideInInspector]
    public List<Vector3> shadowCasterPositions;

    public Tile crackedTile;
    public Tile ruleTile;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            AdjustSelfShadows();
        }
    }

    void AdjustSelfShadows()
    {
        // get bounds
        bounds.SetMinMax(Vector3Int.FloorToInt(player.position - new Vector3(lightRadius, lightRadius)), Vector3Int.RoundToInt(player.position + new Vector3(lightRadius, lightRadius)));

        for (int i = bounds.xMin; i < bounds.xMax; i++)
        {
            for (int j = bounds.yMin; j < bounds.yMax; j++)
            {
                if (tilemap.GetTile(new Vector3Int(i, j, 0)) != null)
                {
                    Vector3Int hitPos = RaycastHitInTile(player.position, new Vector3Int(i, j, 0));

                    Debug.Log("Original: " + new Vector3Int(i, j, 0) + " ; Raycasted: " + hitPos);
                }
            }
        }
    }

    private Vector3Int RaycastHitInTile(Vector3 startPoint, Vector3 endPoint)
    {
        Vector3 direction = (endPoint - startPoint).normalized;

        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, Vector3.Distance(startPoint, endPoint) + 0.5f);

        if (hit)
        {
            //Debug.Log(hit.point + (Vector2)direction / 5f);

            Debug.Log("Direction normilized " + direction);
            
            return tilemap.WorldToCell(hit.point + (Vector2)direction * 0.75f);
        }

        return Vector3Int.zero;
    }



    private void OnDrawGizmosSelected()
    {
        DrawBounds(bounds);
    }

    void DrawBounds(BoundsInt b)
    {
        // bottom
        var p1 = new Vector3(b.min.x, b.min.y, b.min.z);
        var p2 = new Vector3(b.max.x, b.min.y, b.min.z);
        var p3 = new Vector3(b.max.x, b.min.y, b.max.z);
        var p4 = new Vector3(b.min.x, b.min.y, b.max.z);

        Debug.DrawLine(p1, p2, Color.blue);
        Debug.DrawLine(p2, p3, Color.red);
        Debug.DrawLine(p3, p4, Color.yellow);
        Debug.DrawLine(p4, p1, Color.magenta);

        // top
        var p5 = new Vector3(b.min.x, b.max.y, b.min.z);
        var p6 = new Vector3(b.max.x, b.max.y, b.min.z);
        var p7 = new Vector3(b.max.x, b.max.y, b.max.z);
        var p8 = new Vector3(b.min.x, b.max.y, b.max.z);

        Debug.DrawLine(p5, p6, Color.blue);
        Debug.DrawLine(p6, p7, Color.red);
        Debug.DrawLine(p7, p8, Color.yellow);
        Debug.DrawLine(p8, p5, Color.magenta);

        // sides
        Debug.DrawLine(p1, p5, Color.white);
        Debug.DrawLine(p2, p6, Color.gray);
        Debug.DrawLine(p3, p7, Color.green);
        Debug.DrawLine(p4, p8, Color.cyan);
    }
}
