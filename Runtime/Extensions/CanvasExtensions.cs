using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.UI
{
    public static class CanvasExtensions
    {
        public static Vector3 CanvasToViewportPosition(this Canvas canvas, Vector3 canvasPosition)
        {
            var canvasRect = canvas.GetComponent<RectTransform>();
            var scale = canvasRect.rect.size;
            var centerBasedCanvasPosition = Vector3.Scale(canvasPosition, new Vector3(1 / scale.x, 1 / scale.y, 1));
            return centerBasedCanvasPosition + new Vector3(0.5f, 0.5f, 0);
        }
        
        public static Vector3 WorldToCanvasPosition(this Canvas canvas, Vector3 worldPosition, Camera camera = null, bool useNormalizeViewPort = false)
        {
            if (camera == null)
                camera = Camera.main;
            if (camera == null)
                return Vector3.zero;
        
            var viewportPosition = camera.WorldToViewportPoint(worldPosition);
        
            if (useNormalizeViewPort)
            {
                var normalizedViewPort = camera.rect;
                viewportPosition.x = viewportPosition.x * normalizedViewPort.width + normalizedViewPort.x;
                viewportPosition.y = viewportPosition.y * normalizedViewPort.height + normalizedViewPort.y;
            }
        
            return canvas.ViewportToCanvasPosition(viewportPosition);
        }
        
        public static Vector3 ViewportToCanvasPosition(this Canvas canvas, Vector3 viewportPosition)
        {
            var centerBasedViewPortPosition = viewportPosition - new Vector3(0.5f, 0.5f, 0);
            var canvasRect = canvas.GetComponent<RectTransform>();
            var scale = canvasRect.rect.size;
            return Vector3.Scale(centerBasedViewPortPosition, scale);
        }
    }
}