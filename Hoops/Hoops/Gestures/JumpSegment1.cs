using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Gestures
{
        public class JumpSegment1 : IGestureSegment
        {
            private float old_position=0;
            private float th = 0.01f;
            public GesturePartResult Update(Skeleton skeleton)
            {
                // Hand above elbow
                if (old_position!=0 && skeleton.Joints[JointType.HipCenter].Position.Y > old_position + th)
                {
                    Console.WriteLine("UP " + skeleton.Joints[JointType.Spine].Position.Y + "\n");
                     return GesturePartResult.Succeeded;
                }
                // Hand dropped
                old_position = skeleton.Joints[JointType.HipCenter].Position.Y;
                return GesturePartResult.Failed;
            }
        }

        public class JumpSegment2 : IGestureSegment
        {
            private float old_position = 0;
            private float th = 0.1f;
            public GesturePartResult Update(Skeleton skeleton)
            {
                // Hand above elbow
                if (old_position != 0 && skeleton.Joints[JointType.HipCenter].Position.Y < old_position + th)
                {
                    Console.WriteLine("DOWN " + skeleton.Joints[JointType.HipCenter].Position.Y + "\n");
                    return GesturePartResult.Succeeded;
                }

                // Hand dropped
                old_position = skeleton.Joints[JointType.HipCenter].Position.Y;
                return GesturePartResult.Failed;
            }
        }
    }

