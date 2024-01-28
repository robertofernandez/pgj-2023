using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    private Vector2 cursorHotstop;

    void Start()
    {
            cursorHotstop = new Vector2(cursorTexture.width/2, cursorTexture.height/2);
            Cursor.SetCursor(cursorTexture, cursorHotstop, CursorMode.Auto);
    }

}
