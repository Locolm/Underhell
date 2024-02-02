using UnityEngine;

public class ChangeCursor : MonoBehaviour
{
    public Texture2D cursorTexture; // Texture du curseur personnalis�
    public CursorMode cursorMode = CursorMode.Auto; // Mode du curseur
    public Vector2 hotSpot = Vector2.zero; // Point chaud du curseur

    void Start()
    {
        // Changer le curseur de la souris au d�marrage
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
}
