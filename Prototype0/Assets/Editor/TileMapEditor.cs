using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor {

    public TileMap map;
    int choice = 0;
    private Vector2 tileDims;

    TileBrush tileBrush;

    Vector3 mouseHitPosition;

    bool MouseOnMap
    {
        get {
            return mouseHitPosition.x > 0 && mouseHitPosition.x < map.gridDimensions.x && mouseHitPosition.y < 0 &&
              mouseHitPosition.y > -map.gridDimensions.y;
        }
    }
    
    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        var previousDimensions = map.mapDimensions;
       
        map.mapDimensions = EditorGUILayout.Vector2Field("Map Dimensions: " , map.mapDimensions);

        if(previousDimensions != map.mapDimensions)
        {
            AutoUpdateDimensions();
        }


        var previousTexture = map.texture2D;
        map.texture2D = (Texture2D)EditorGUILayout.ObjectField("Texture2D: ", map.texture2D, typeof(Texture2D), false);

        if(previousTexture != map.texture2D)
        {
            AutoUpdateDimensions();
            map.tileId = 1;
            CreateBrush();
        }



        // case when no texture
        if (map.texture2D == null)
        {
            EditorGUILayout.HelpBox("No texture selected", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.LabelField("Tile Dimensions:", map.tileDimensions.x + "x" + map.tileDimensions.y);

            map.tilePadding = EditorGUILayout.Vector2Field("Tile Padding", map.tilePadding);

            EditorGUILayout.LabelField("Grid Dimensions:", map.gridDimensions.x + "x" + map.gridDimensions.y);

            EditorGUILayout.LabelField("Pixels -> Units:", map.convertPixelsToUnits.ToString());

            choice = EditorGUILayout.Popup("Tyle type", choice, new string[] { "Ground", "Other" }, EditorStyles.popup);

            UpdateBrush(map.CurrentBrush);
            if (GUILayout.Button("Clear All Tiles"))
            {
                if (EditorUtility.DisplayDialog("Clear all tiles?", "This will clear all the tiles \nConfirm?", "Yes", "No"))
                {
                    ClearAllTiles();
                }
            }
        }
        

        EditorGUILayout.EndVertical();
        
    }

    void OnEnable()
    {
        map = target as TileMap;
        Tools.current = Tool.View;

        if(map.tiles == null)
        {
            var gameObject = new GameObject("Tiles");
            gameObject.transform.SetParent(map.transform);
            gameObject.transform.position = Vector3.zero;
            map.tiles = gameObject;
        }

        if(map.texture2D != null)
        {
            AutoUpdateDimensions();
            NewBrush();
        }
    }

    void OnDisable()
    {
        DestroyBrush();
    }

    void OnSceneGUI()
    {
        if(tileBrush != null)
        {
            UpdateHitPosition();
            MoveBrush();

            if(map.texture2D != null && MouseOnMap)
            {
                Event current = Event.current;
                if (current.shift)
                {
                    DrawTile();
                }else if (current.alt)
                {
                    RemoveTile();
                }
            }

        }
    }

    public void AutoUpdateDimensions()
    {
        var assetPath = AssetDatabase.GetAssetPath(map.texture2D);
        map.spriteLink = AssetDatabase.LoadAllAssetsAtPath(assetPath);

        Sprite sprite = (Sprite)map.spriteLink[1];
        var height = sprite.textureRect.height;
        var width = sprite.textureRect.width;

        map.tileDimensions = new Vector2(width, height);

        map.convertPixelsToUnits = (int)(sprite.rect.width / sprite.bounds.size.x);
        map.convertPixelsToUnits = sprite.rect.width % sprite.bounds.size.x >= 0.5 ? map.convertPixelsToUnits + 1 : map.convertPixelsToUnits;
        tileDims = new Vector2((width / map.convertPixelsToUnits), (height / map.convertPixelsToUnits));

        map.gridDimensions = new Vector2((width / map.convertPixelsToUnits) * map.mapDimensions.x, (height / map.convertPixelsToUnits) * map.mapDimensions.y);

    }

    void CreateBrush()
    {
        var mapSprite = map.CurrentBrush;
        if(mapSprite != null)
        {
            GameObject gameObject = new GameObject("Brush");
            gameObject.transform.SetParent(map.transform);

            tileBrush = gameObject.AddComponent<TileBrush>();
            tileBrush.spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            tileBrush.spriteRenderer.sortingOrder = 1000;

            var convertPixelsToUnits = map.convertPixelsToUnits;
            tileBrush.brushSize = new Vector2(mapSprite.textureRect.width / convertPixelsToUnits,
                                                mapSprite.textureRect.height / convertPixelsToUnits);

            tileBrush.UpdateBrush(mapSprite);
        }

    }

    void NewBrush()
    {
        if(tileBrush == null)
        {
            CreateBrush();
        }
    }

    void DestroyBrush()
    {
        if(tileBrush != null)
        {
            DestroyImmediate(tileBrush.gameObject);
        }
    }

    public void UpdateBrush(Sprite sprite)
    {
        if(tileBrush != null)
        {
            tileBrush.UpdateBrush(sprite);
        }

    }

    void UpdateHitPosition()
    {
        var p = new Plane(map.transform.TransformDirection(Vector3.forward), Vector3.zero);
        var ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
        var hit = Vector3.zero;
        var distance = 0f;

        if(p.Raycast(ray, out distance))
        {
            hit = ray.origin + ray.direction.normalized * distance;
        }

        mouseHitPosition = map.transform.InverseTransformPoint(hit);
    }

    void MoveBrush()
    {
        var tileDimensions = map.tileDimensions.x / map.convertPixelsToUnits;

        var x = Mathf.Floor(mouseHitPosition.x / tileDimensions) * tileDimensions;
        var y = Mathf.Floor(mouseHitPosition.y / tileDimensions) * tileDimensions;

        var row = x / tileDimensions;
        var column = Mathf.Abs(y / tileDimensions) - 1;
        if (!MouseOnMap)
        {
            return;
        }
        var id = (int)((column * map.mapDimensions.x) + row);

        tileBrush.tileID = id;

        x += map.transform.position.x + tileDimensions / 2;
        y += map.transform.position.y + tileDimensions / 2;

        tileBrush.transform.position = new Vector3(x, y, map.transform.position.z);
    }

    void DrawTile()
    {
        var id = tileBrush.tileID.ToString();

        var posX = tileBrush.transform.position.x;
        var posY = tileBrush.transform.position.y;

        GameObject tileRef = GameObject.Find(map.name + "/Tiles/tile_" + id);

        if(tileRef == null)
        {
            tileRef = new GameObject("tile_" + id);
            tileRef.transform.SetParent(map.tiles.transform);
            tileRef.transform.position = new Vector3(posX, posY, 0);
            tileRef.AddComponent<SpriteRenderer>();
            if(choice == 0)//We want to add ground
            {
                tileRef.tag = "Ground";
                tileRef.layer = LayerMask.NameToLayer("Ground");
                tileRef.AddComponent<BoxCollider2D>();
                BoxCollider2D box = tileRef.GetComponent<BoxCollider2D>();
                box.size = tileDims;
                box.sharedMaterial = Resources.Load<PhysicsMaterial2D>("NoFriction");
            }

        }

        tileRef.GetComponent<SpriteRenderer>().sprite = tileBrush.spriteRenderer.sprite;
    }

    void RemoveTile()
    {
        var id = tileBrush.tileID.ToString();

        GameObject tileRef = GameObject.Find(map.name + "/Tiles/tile_" + id);

        if(tileRef != null)
        {
            DestroyImmediate(tileRef);
        }
    }

    void ClearAllTiles()
    {
        for(var i = 0; i < map.tiles.transform.childCount; ++i)
        {
            Transform childRef = map.tiles.transform.GetChild(i);
            DestroyImmediate(childRef.gameObject);
            i--;
        }
    }
}
