using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Gestures
{
    class ShootingSegment1: IGestureSegment
    {
        public GesturePartResult Update(Skeleton skeleton)
        {
            // HWrisr above head
            if (skeleton.Joints[JointType.WristRight].Position.Y > skeleton.Joints[JointType.Head].Position.Y &&
                skeleton.Joints[JointType.WristLeft].Position.Y > skeleton.Joints[JointType.Head].Position.Y)
            {
                    return GesturePartResult.Succeeded;
            }

            // Hand dropped
            return GesturePartResult.Failed;
        }
    }

    class ShootingSegment2 : IGestureSegment
    {
        public GesturePartResult Update(Skeleton skeleton)
        {
            // HWrisr above head
            if (skeleton.Joints[JointType.WristRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y && 
                skeleton.Joints[JointType.WristLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
            {
                    return GesturePartResult.Succeeded;
            }

            // Hand dropped
            return GesturePartResult.Failed;
        }
    }
}
