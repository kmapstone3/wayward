using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class Utils
{
    public static Vector2 SnapPosition(Vector3 position)
    {
        Vector2 flooredPos = new Vector2(Mathf.FloorToInt(position.x), Mathf.FloorToInt(position.y));
        
        return flooredPos + Vector2.one * 0.5f;
    }
}
