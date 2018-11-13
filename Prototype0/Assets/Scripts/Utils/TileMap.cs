using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour {

    public Vector2 mapDimensions = new Vector2(30, 15);
    public Texture2D texture2D;
    public Vector2 tileDimensions = new Vector2();
    public Vector2 tilePadding = new Vector2();
    public Object[] spriteLink;
    public Vector2 gridDimensions = new Vector2();
    public int convertPixelsToUnits = 100;
    public int tileId = 0;

    public GameObject tiles;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    private void OnDrawGizmosSelected()
    {
        var pos = transform.position;

        if(texture2D != null)
        {
            Gizmos.color = Color.gray;
            

            var row = 0;
            var maxColumns = mapDimensions.x;
            var numberOfCells = mapDimensions.x * mapDimensions.y;
            var tile = new Vector3(tileDimensions.x / convertPixelsToUnits, tileDimensions.y / convertPixelsToUnits);
            var offset = new Vector2(tile.x / 2, tile.y / 2);

            for(int i = 0; i < numberOfCells; i++)
            {
                var column = i % maxColumns;
                
                var newXPosition = (column * tile.x) + offset.x + pos.x;
                var newYPosition = -(row * tile.y) - offset.y + pos.y;
                Gizmos.DrawWireCube(new Vector2(newXPosition,newYPosition), tile);

                if(column == maxColumns - 1)
                {
                    row++;
                }
            }

            Gizmos.color = Color.white;
            var centerX = pos.x + (gridDimensions.x / 2);
            var centerY = pos.y - (gridDimensions.y / 2);

            Gizmos.DrawWireCube(new Vector2(centerX, centerY), gridDimensions);

           
        }
    }

    public Sprite CurrentBrush
    {
        get { return spriteLink[tileId] as Sprite; }
    }
}
