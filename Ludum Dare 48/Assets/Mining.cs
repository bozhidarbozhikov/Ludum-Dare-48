using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Mining : MonoBehaviour
{
    public Transform minePoint;

    public Tilemap tilemap;

    public int mineRadius;

    BoundsInt bounds;

    public Sprite crackedWallSprite;
    public Tile crackedTile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Mine();
        }
    }

    void Mine()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(-0.5f, -0.5f);

        Vector3Int roundedPos = Vector3Int.RoundToInt(minePoint.position);

        bounds.SetMinMax(roundedPos - new Vector3Int(mineRadius, mineRadius, 0), roundedPos + new Vector3Int(mineRadius, mineRadius, 0));

        Vector3Int mouseTilePos = tilemap.WorldToCell(Vector3Int.RoundToInt(mouseWorldPos));

        for (int i = bounds.xMin; i < bounds.xMax; i++)
        {
            for (int j = bounds.yMin; j < bounds.yMax; j++)
            {
                if (mouseTilePos == new Vector3Int(i, j, 0))
                {
                    if (tilemap.GetSprite(mouseTilePos) != null)
                    {
                        if (tilemap.GetSprite(mouseTilePos) != crackedWallSprite)
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), crackedTile);
                        }
                        else
                        {
                            tilemap.SetTile(new Vector3Int(i, j, 0), null);

                            FindObjectOfType<ShadowCasterTilemapCreator>().RemoveShadowCasterFromPosition(new Vector3Int(i, j, 0));

                        }
                    }
                }
            }
        }
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
