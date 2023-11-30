using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public static class ProgressExtension
    {
        public static int GetMileStone(this List<int> milestones, int progress)
        {
            int mileStone = -1;
            for (int i = 0; i < milestones.Count; i++)
            {
                if (progress > milestones[i])
                {
                    mileStone++;
                }
            }
            return Mathf.Clamp(mileStone,0, milestones.Count - 1);
        }
    }

}
