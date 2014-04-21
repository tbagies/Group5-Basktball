using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Gestures
{
    class PassingSegment1: IGestureSegment
    {
        private float th = 0.1f;
        public GesturePartResult Update(Skeleton skeleton)
        {
            // HWrisr above head
            if (//Math.Abs(skeleton.Joints[JointType.ElbowLeft].Position.Y - skeleton.Joints[JointType.ElbowRight].Position.Y) <= th
             skeleton.Joints[JointType.HandLeft].Position.Z > skeleton.Joints[JointType.ElbowLeft].Position.Z
             && skeleton.Joints[JointType.HandRight].Position.Z > skeleton.Joints[JointType.ElbowRight].Position.Z
                && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y
                && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y)
            {
                    return GesturePartResult.Succeeded;
            }
                         // Hand dropped
            return GesturePartResult.Failed;
        }
    }

    class PassingSegment2 : IGestureSegment
    {
        private float th = 0.01f;
        public GesturePartResult Update(Skeleton skeleton)
        {
            if (Math.Abs(skeleton.Joints[JointType.ElbowLeft].Position.Y - skeleton.Joints[JointType.ElbowRight].Position.Y) <= th
                && skeleton.Joints[JointType.HandLeft].Position.Z < skeleton.Joints[JointType.ElbowLeft].Position.Z
                && skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.ElbowRight].Position.Z
                && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y
                && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y)
            {
                return GesturePartResult.Succeeded;
            }
            return GesturePartResult.Failed; 
        }
    }
}
