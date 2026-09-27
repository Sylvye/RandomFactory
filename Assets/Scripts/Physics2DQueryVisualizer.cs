using UnityEngine;

/// <summary>
/// Debug drawing helpers for Physics2D box queries.
/// </summary>
public static class Physics2DQueryVisualizer
{
    public static void DrawCircle(Vector2 center, float radius, Color color, float duration = 0f)
    {
        const int segments = 32;
        var previousPoint = GetCirclePoint(center, radius, 0f);

        for (var i = 1; i <= segments; i++)
        {
            var angle = i * Mathf.PI * 2f / segments;
            var nextPoint = GetCirclePoint(center, radius, angle);
            Debug.DrawLine(previousPoint, nextPoint, color, duration);
            previousPoint = nextPoint;
        }
    }

    public static void DrawBox(Vector3 center, Vector2 size, float angle, Color color, float duration = 0f)
    {
        var corners = GetBoxCorners(center, size, angle);
        DrawOutline(corners, color, duration);
    }

    public static void DrawBoxCast(
        Vector3 origin,
        Vector2 size,
        float angle,
        Vector2 direction,
        float distance,
        Color color,
        float duration = 0f)
    {
        var startCorners = GetBoxCorners(origin, size, angle);
        var offset = (Vector3)(direction.normalized * distance);
        var endCorners = new Vector3[startCorners.Length];

        for (var i = 0; i < startCorners.Length; i++)
        {
            endCorners[i] = startCorners[i] + offset;
        }

        DrawOutline(startCorners, color, duration);
        DrawOutline(endCorners, color, duration);

        for (var i = 0; i < startCorners.Length; i++)
        {
            Debug.DrawLine(startCorners[i], endCorners[i], color, duration);
        }
    }

    private static Vector3[] GetBoxCorners(Vector3 center, Vector2 size, float angle)
    {
        var radians = angle * Mathf.Deg2Rad;
        var right = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * (size.x * 0.5f);
        var up = new Vector2(-Mathf.Sin(radians), Mathf.Cos(radians)) * (size.y * 0.5f);

        return new[]
        {
            center + (Vector3)right + (Vector3)up,
            center - (Vector3)right + (Vector3)up,
            center - (Vector3)right - (Vector3)up,
            center + (Vector3)right - (Vector3)up
        };
    }

    private static Vector3 GetCirclePoint(Vector2 center, float radius, float angle)
    {
        return new Vector3(
            center.x + Mathf.Cos(angle) * radius,
            center.y + Mathf.Sin(angle) * radius,
            0f);
    }

    private static void DrawOutline(Vector3[] corners, Color color, float duration)
    {
        for (var i = 0; i < corners.Length; i++)
        {
            Debug.DrawLine(corners[i], corners[(i + 1) % corners.Length], color, duration);
        }
    }
}
