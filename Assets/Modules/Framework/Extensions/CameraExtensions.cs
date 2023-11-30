using UnityEngine;

namespace Framework
{
    public static class CameraExtensions
    {
        public static void SetHeight(this Camera camera, float height)
        {
            camera.orthographicSize = height * 0.5f;
        }

        public static void SetWidth(this Camera camera, float width)
        {
            camera.orthographicSize = width / camera.aspect * 0.5f;
        }

        public static float GetHeight(this Camera camera)
        {
            return camera.orthographicSize * 2f;
        }

        public static float GetWidth(this Camera camera)
        {
            return camera.GetHeight() * camera.aspect;
        }

        public static float GetHalfHeight(this Camera camera)
        {
            return camera.orthographicSize;
        }

        public static float GetHalfWidth(this Camera camera)
        {
            return camera.GetHalfHeight() * camera.aspect;
        }

        public static float Top(this Camera camera)
        {
            return camera.transform.position.y + camera.GetHalfHeight();
        }

        public static float Bottom(this Camera camera)
        {
            return camera.transform.position.y - camera.GetHalfHeight();
        }

        public static float Right(this Camera camera)
        {
            return camera.transform.position.x + camera.GetHalfWidth();
        }

        public static float Left(this Camera camera)
        {
            return camera.transform.position.x - camera.GetHalfWidth();
        }
    }
}