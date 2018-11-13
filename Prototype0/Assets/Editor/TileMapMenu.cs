using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileMapMenu
{


    [MenuItem("GameObject/Tile Map")]
    public static void CreateMap()
    {
        GameObject tileMap = new GameObject("Tile Map");
        tileMap.AddComponent<TileMap>();
    }




}
