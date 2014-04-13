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
        private float old_positionLeft = 0;
        private float old_positionRight = 0;
        private float th = 0.01f;
        public GesturePartResult Update(Skeleton skeleton)
        {
            // HWrisr above head
            if (old_positionLeft != 0 && old_positionRight != 0 
               && skeleton.Joints[JointType.HandRight].Position.X == skeleton.Joints[JointType.ShoulderCenter].Position.X
            && skeleton.Joints[JointType.HandLeft].Position.X == skeleton.Joints[JointType.ShoulderCenter].Position.X
             && skeleton.Joints[JointType.WristLeft].Position.X < old_positionLeft + th
             && skeleton.Joints[JointType.WristRight].Position.X < old_positionRight + th)
            {
                    return GesturePartResult.Succeeded;
            }
            old_positionRight = skeleton.Joints[JointType.WristRight].Position.X;
            old_positionLeft = skeleton.Joints[JointType.WristLeft].Position.X;
            // Hand dropped
            return GesturePartResult.Failed;
        }
    }

    class PassingSegment2 : IGestureSegment
    {
        private float old_positionLeft = 0;
        private float old_positionRight = 0;
        private float th = 0.01f;
        public GesturePartResult Update(Skeleton skeleton)
        {
            // HWrisr above head
            if (old_positionLeft != 0 && skeleton.Joints[JointType.WristLeft].Position.X < old_positionLeft + th
                 && old_positionRight != 0 && skeleton.Joints[JointType.WristRight].Position.X < old_positionRight + th)
            {
                    return GesturePartResult.Succeeded;
            }

            old_positionLeft = skeleton.Joints[JointType.WristLeft].Position.X;
            old_positionRight = skeleton.Joints[JointType.WristRight].Position.X;
            // Hand dropped
            return GesturePartResult.Failed;
        }
    }
}
