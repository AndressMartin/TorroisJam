using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class GridManagement : MonoBehaviour
{
    private int linhas = 12;
    private int colunas = 16;
    private float tileSize = 1;
    private int indice = 0;
    public GameObject MyGrid;
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateGrid()
    {

        //GameObject GridTile = new GameObject("GridTile" + indice);
        for (int linha = 0; linha < linhas; linha++)
        {
            for (int coluna = 0; coluna < colunas; coluna++)
            {
                GameObject GridTile = new GameObject("GridTile" + indice);
                GridTile.transform.SetParent(MyGrid.transform);

                float posX = (coluna * tileSize)+0.5f;
                float posY = (linha * -tileSize)+0.5f;
                
                GridTile.transform.position = new Vector2(posX-8, posY+5);
                DrawIcon(GridTile, 2);
                indice++;
            }
        }
    }

    private void DrawIcon(GameObject gameObject, int idx)
    {
        var largeIcons = GetTextures("sv_icon_dot", "_sml", 0, 16);
        var icon = largeIcons[idx];
        var egu = typeof(EditorGUIUtility);
        var flags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
        var args = new object[] { gameObject, icon.image };
        var setIcon = egu.GetMethod("SetIconForObject", flags, null, new Type[] { typeof(UnityEngine.Object), typeof(Texture2D) }, null);
        setIcon.Invoke(null, args);
    }
    private GUIContent[] GetTextures(string baseName, string postFix, int startIndex, int count)
    {
        GUIContent[] array = new GUIContent[count];
        for (int i = 0; i < count; i++)
        {
            array[i] = EditorGUIUtility.IconContent(baseName + (startIndex + i) + postFix);
        }
        return array;
    }
}

