using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/**
		Contains useful extensions for Select.
		@author Herman Tulleken

		@version1_0
	*/
namespace Framework
{
    public static class TransformExtensions
    {
        #region Position

        /**
                Sets the X position of this transform.
            */
        public static void SetX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        /**
                Sets the Y position of this transform.
            */
        public static void SetY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        /**
                Sets the Z position of this transform.
            */
        public static void SetZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }

        /**
                Sets the X and Y position of this transform.
            */
        public static void SetXY(this Transform transform, float x, float y)
        {
            transform.position = new Vector3(x, y, transform.position.z);
        }

        /**
                Sets the X and Z position of this transform.
            */
        public static void SetXZ(this Transform transform, float x, float z)
        {
            transform.position = new Vector3(x, transform.position.y, z);
        }

        /**
                Sets the Y and Z position of this transform.
            */
        public static void SetYZ(this Transform transform, float y, float z)
        {
            transform.position = new Vector3(transform.position.x, y, z);
        }

        /**
                Sets the X, Y and Z position of this transform.
            */
        public static void SetXYZ(this Transform transform, float x, float y, float z)
        {
            transform.position = new Vector3(x, y, z);
        }

        /**
                Translates this transform along the X axis.
            */
        public static void TranslateX(this Transform transform, float x)
        {
            transform.position += new Vector3(x, 0f, 0f);
        }

        /**
                Translates this transform along the Y axis.
            */

        public static void TranslateY(this Transform transform, float y)
        {
            transform.position += new Vector3(0f, y, 0f);
        }

        /**
                Translates this transform along the Z axis.
            */
        public static void TranslateZ(this Transform transform, float z)
        {
            transform.position += new Vector3(0f, 0f, z);
        }

        /**
                Translates this transform along the X and Y axes.
            */
        public static void TranslateXY(this Transform transform, float x, float y)
        {
            transform.position += new Vector3(x, y, 0f);
        }

        /**
                Translates this transform along the X and Z axes.
            */
        public static void TranslateXZ(this Transform transform, float x, float z)
        {
            transform.position += new Vector3(x, 0f, z);
        }

        /**
                Translates this transform along the Y and Z axes.
            */
        public static void TranslateYZ(this Transform transform, float y, float z)
        {
            transform.position += new Vector3(0f, y, z);
        }

        /**
                Translates this transform along the X, Y and Z axis.
            */
        public static void TranslateXYZ(this Transform transform, float x, float y, float z)
        {
            transform.position += new Vector3(x, y, z);
        }

        /**
                Sets the local X position of this transform.
            */
        public static void SetLocalX(this Transform transform, float x)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }

        /**
                Sets the local Y position of this transform.
            */
        public static void SetLocalY(this Transform transform, float y)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
        }

        /**
                Sets the local Z position of this transform.
            */
        public static void SetLocalZ(this Transform transform, float z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, z);
        }

        /**
                Sets the local X and Y position of this transform.
            */
        public static void SetLocalXY(this Transform transform, float x, float y)
        {
            transform.localPosition = new Vector3(x, y, transform.localPosition.z);
        }

        /**
                Sets the local X and Z position of this transform.
            */
        public static void SetLocalXZ(this Transform transform, float x, float z)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, z);
        }

        /**
                Sets the local Y and Z position of this transform.
            */
        public static void SetLocalYZ(this Transform transform, float y, float z)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, y, z);
        }

        /**
                Sets the local X, Y and Z position of this transform.
            */
        public static void SetLocalXYZ(this Transform transform, float x, float y, float z)
        {
            transform.localPosition = new Vector3(x, y, z);
        }

        /**
                Sets the position to 0, 0, 0.
            */

        public static void ResetPosition(this Transform transform)
        {
            transform.position = Vector3.zero;
        }

        /**
                Sets the local position to 0, 0, 0.
            */
        public static void ResetLocalPosition(this Transform transform)
        {
            transform.localPosition = Vector3.zero;
        }

        #endregion

        #region Scale

        /**
                Sets the local X scale of this transform.
            */
        public static void SetScaleX(this Transform transform, float x)
        {
            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
        }

        /**
                Sets the local Y scale of this transform.
            */
        public static void SetScaleY(this Transform transform, float y)
        {
            transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
        }

        /**
                Sets the local Z scale of this transform.
            */
        public static void SetScaleZ(this Transform transform, float z)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, z);
        }

        /**
                Sets the local X and Y scale of this transform.
            */
        public static void SetScaleXY(this Transform transform, float x, float y)
        {
            transform.localScale = new Vector3(x, y, transform.localScale.z);
        }

        public static void SetScaleXY(this Transform transform, float xy)
        {
            transform.localScale = new Vector3(xy, xy, transform.localScale.z);
        }

        /**
                Sets the local X and Z scale of this transform.
            */
        public static void SetScaleXZ(this Transform transform, float x, float z)
        {
            transform.localScale = new Vector3(x, transform.localScale.y, z);
        }

        /**
                Sets the local Y and Z scale of this transform.
            */
        public static void SetScaleYZ(this Transform transform, float y, float z)
        {
            transform.localScale = new Vector3(transform.localScale.x, y, z);
        }

        /**
                Sets the local X, Y and Z scale of this transform.
            */
        public static void SetScale(this Transform transform, float x, float y, float z)
        {
            transform.localScale = new Vector3(x, y, z);
        }

        /**
                Sets the local scale of this transform.
            */
        public static void SetScale(this Transform transform, float scale)
        {
            transform.localScale = Vector3.one * scale;
        }

        /**
                Scale this transform in the X direction.
            */
        public static void ScaleByX(this Transform transform, float x)
        {
            transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y, transform.localScale.z);
        }

        /**
                Scale this transform in the Y direction.
            */
        public static void ScaleByY(this Transform transform, float y)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * y, transform.localScale.z);
        }

        /**
                Scale this transform in the Z direction.
            */
        public static void ScaleByZ(this Transform transform, float z)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * z);
        }

        /**
                Scale this transform in the X, Y direction.
            */
        public static void ScaleByXY(this Transform transform, float x, float y)
        {
            transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y * y, transform.localScale.z);
        }

        /**
                Scale this transform in the X, Z directions.
            */
        public static void ScaleByXZ(this Transform transform, float x, float z)
        {
            transform.localScale = new Vector3(transform.localScale.x * x, transform.localScale.y, transform.localScale.z * z);
        }

        /**
                Scale this transform in the Y and Z directions.
            */
        public static void ScaleByYZ(this Transform transform, float y, float z)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * y, transform.localScale.z * z);
        }

        /**
                Scale this transform in the X and Y directions.
            */
        public static void ScaleByXY(this Transform transform, float r)
        {
            transform.ScaleByXY(r, r);
        }

        /**
                Scale this transform in the X and Z directions.
            */
        public static void ScaleByXZ(this Transform transform, float r)
        {
            transform.ScaleByXZ(r, r);
        }

        /**
                Scale this transform in the Y and Z directions.
            */
        public static void ScaleByYZ(this Transform transform, float r)
        {
            transform.ScaleByYZ(r, r);
        }

        /**
                Scale this transform in the X, Y and Z directions.
            */
        public static void ScaleByXYZ(this Transform transform, float x, float y, float z)
        {
            transform.localScale = new Vector3(
                x, y, z);
        }

        /**
                Scale this transform in the X, Y and Z directions.
            */
        public static void ScaleByXYZ(this Transform transform, float r)
        {
            transform.ScaleByXYZ(r, r, r);
        }


        /**
                Resets the local scale of this transform in to 1 1 1.
            */
        public static void ResetScale(this Transform transform)
        {
            transform.localScale = Vector3.one;
        }

        #endregion

        #region FlipScale

        /**
                Negates the X scale.
            */
        public static void FlipX(this Transform transform)
        {
            transform.SetScaleX(-transform.localScale.x);
        }

        /**
                Negates the Y scale.
            */
        public static void FlipY(this Transform transform)
        {
            transform.SetScaleY(-transform.localScale.y);
        }

        /**
                Negates the Z scale.
            */
        public static void FlipZ(this Transform transform)
        {
            transform.SetScaleZ(-transform.localScale.z);
        }

        /**
                Negates the X and Y scale.
            */
        public static void FlipXY(this Transform transform)
        {
            transform.SetScaleXY(-transform.localScale.x, -transform.localScale.y);
        }

        /**
                Negates the X and Z scale.
            */
        public static void FlipXZ(this Transform transform)
        {
            transform.SetScaleXZ(-transform.localScale.x, -transform.localScale.z);
        }

        /**
                Negates the Y and Z scale.
            */
        public static void FlipYZ(this Transform transform)
        {
            transform.SetScaleYZ(-transform.localScale.y, -transform.localScale.z);
        }

        /**
                Negates the X, Y and Z scale.
            */
        public static void FlipXYZ(this Transform transform)
        {
            transform.SetScale(-transform.localScale.z, -transform.localScale.y, -transform.localScale.z);
        }

        /**
                Sets all scale values to the absolute values.
            */
        public static void FlipPostive(this Transform transform)
        {
            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                Mathf.Abs(transform.localScale.y),
                Mathf.Abs(transform.localScale.z));
        }

        #endregion

        #region Rotation

        /**
                Rotates the transform around the X axis.
            */
        public static void RotateAroundX(this Transform transform, float angle)
        {
            transform.Rotate(new Vector3(angle, 0f, 0f));
        }

        /**
                Rotates the transform around the Y axis.
            */
        public static void RotateAroundY(this Transform transform, float angle)
        {
            transform.Rotate(new Vector3(0f, angle, 0f));
        }

        /**
                Rotates the transform around the Z axis.
            */
        public static void RotateAroundZ(this Transform transform, float angle)
        {
            transform.Rotate(new Vector3(0f, 0f, angle));
        }

        /**
                Sets the X rotation.
            */
        public static void SetRotationX(this Transform transform, float angle)
        {
            transform.eulerAngles = new Vector3(angle, transform.eulerAngles.y, transform.eulerAngles.z);
        }


        /**
                Sets the Y rotation.
            */
        public static void SetRotationY(this Transform transform, float angle)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
        }

        /**
                Sets the Z rotation.
            */
        public static void SetRotationZ(this Transform transform, float angle)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
        }

        /**
                Sets the local X rotation.
            */
        public static void SetLocalRotationX(this Transform transform, float angle)
        {
            transform.localEulerAngles = new Vector3(angle, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }

        /**
                Sets the local Y rotation.
            */
        public static void SetLocalRotationY(this Transform transform, float angle)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
        }

        /**
                Sets the local Z rotation.
            */
        public static void SetLocalRotationZ(this Transform transform, float angle)
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, angle);
        }

        /**
                Resets the rotation to 0, 0, 0.
            */
        public static void ResetRotation(this Transform transform)
        {
            transform.rotation = Quaternion.identity;
        }

        /**
                Resets the local rotation to 0, 0, 0.
            */
        public static void ResetLocalRotation(this Transform transform)
        {
            transform.localRotation = Quaternion.identity;
        }

        #endregion

        #region All

        /**
                Resets the ;local position, local rotation, and local scale.
            */
        public static void ResetLocal(this Transform transform)
        {
            transform.ResetLocalRotation();
            transform.ResetLocalPosition();
            transform.ResetScale();

        }

        /**
                Resets the position, rotation, and local scale.
            */
        public static void Reset(this Transform transform)
        {
            transform.ResetRotation();
            transform.ResetPosition();
            transform.ResetScale();
        }

        #endregion

        #region Children

        public static List<Transform> GetChildren(this Transform transform)
        {
            var children = new List<Transform>();

            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                children.Add(child);
            }

            return children;
        }

        public static void Sort(this Transform transform, Func<Transform, IComparable> sortFunction)
        {
            var children = transform.GetChildren();
            var sortedChildren = children.OrderBy(sortFunction).ToList();

            for (int i = 0; i < sortedChildren.Count(); i++)
            {
                sortedChildren[i].SetSiblingIndex(i);
            }
        }

        public static void SortAlphabetically(this Transform transform)
        {
            transform.Sort(t => t.name);
        }

        public static void DestroyChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.Destroy(transform.GetChild(i).gameObject);
            }
        }

        public static void DestroyChildrenImmediate(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                UnityEngine.Object.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        #endregion

        #region Extra

        public static void RecursiveAction(this Transform target, Action<Transform> action)
        {
            action(target);

            if (target.childCount > 0)
            {
                for (int i = 0; i < target.childCount; i++)
                {
                    target.GetChild(i).RecursiveAction(action);
                }
            }
        }

        public static void RecursiveAction<T>(this Transform target, Action<T> action) where T : Component
        {
            action(target.GetComponent<T>());

            if (target.childCount > 0)
            {
                for (int i = 0; i < target.childCount; i++)
                {
                    target.GetChild(i).RecursiveAction(action);
                }
            }
        }

        public static void SetActiveChildren(this Transform target, bool isActive)
        {
            for (int i = target.childCount - 1; i >= 0; i--)
            {
                target.GetChild(i).gameObject.SetActive(isActive);
            }
        }

        #endregion
    }
}