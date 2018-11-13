using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ChooseTile : EditorWindow {

    public enum Scale
    {
        Zoom_x1,
        Zoom_x2,
        Zoom_x3,
        Zoom_x4,
        Zoom_x5
    }

    Scale scale;

    public Vector2 scrollPosition = Vector2.zero;

    private Vector2 currentSelected = Vector2.zero;

    [MenuItem("Window/Choose Tile")]
    public static void ChooseTileScreen()
    {
        var window = EditorWindow.GetWindow(typeof(ChooseTile));
        var title = new GUIContent();
        title.text = "Choose Tile";
        window.titleContent = title;
    }

    private void OnGUI()
    {
        if(Selection.activeGameObject == null)
        {
            return;
        }

        var selected = Selection.activeGameObject.GetComponent<TileMap>();

        if(selected != null)
        {
            var texture2D = selected.texture2D;
            if(texture2D != null)
            {
                scale = (Scale)EditorGUILayout.EnumPopup("Zoom",scale);
                var newScale = ((int)scale) + 1;
                var newTextureDimensions = new Vector2(texture2D.width, texture2D.height) * newScale;
                var offset = new Vector2(10, 25);


                var viewPort = new Rect(0, 0, position.width - 5, position.height - 5);
                var contentDimensions = new Rect(0, 0, newTextureDimensions.x + offset.x, newTextureDimensions.y + offset.y);
                scrollPosition = GUI.BeginScrollView(viewPort, scrollPosition,contentDimensions);

                GUI.DrawTexture(new Rect(offset.x, offset.y, newTextureDimensions.x,newTextureDimensions.y), texture2D);

                var tile = selected.tileDimensions * newScale;
                tile.x += selected.tilePadding.x * newScale;
                tile.y += selected.tilePadding.y * newScale;
                var grid = new Vector2(newTextureDimensions.x / tile.x, newTextureDimensions.y / tile.y);

                var boxColor = new Texture2D(1,1);
                boxColor.SetPixel(0, 0, new Color(0, 0.5f, 1f, 0.4f));
                boxColor.Apply();

                var boxStyle = new GUIStyle(GUI.skin.customStyles[0]);
                boxStyle.normal.background = boxColor;

                var currentEvent = Event.current;
                Vector2 mouse = new Vector2(currentEvent.mousePosition.x, currentEvent.mousePosition.y);
                if(currentEvent.type == EventType.MouseDown && currentEvent.button == 0)
                {
                    currentSelected.x = Mathf.Floor((mouse.x + scrollPosition.x) / tile.x);
                    currentSelected.y = Mathf.Floor((mouse.y + scrollPosition.y) / tile.y);

                    if(currentSelected.x > grid.x - 1)
                    {
                        currentSelected.x = grid.x - 1;
                    }

                    if (currentSelected.y > grid.y - 1)
                    {
                        currentSelected.y = grid.y - 1;
                    }

                    selected.tileId = (int)(currentSelected.x + (grid.x * currentSelected.y) + 1);

                    Repaint();
                }

                var selectionPosition = new Vector2(tile.x * currentSelected.x + offset.x, tile.y * currentSelected.y + offset.y);
                GUI.Box(new Rect(selectionPosition.x, selectionPosition.y, tile.x, tile.y),"", boxStyle);
                GUI.EndScrollView();
            }
        }
    }
}
