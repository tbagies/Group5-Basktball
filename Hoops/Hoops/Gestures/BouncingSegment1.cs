using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Gestures
{
        public class BounceingSegment1 : IGestureSegment
        {
            private float old_position=0;
            private float th = 1.0f;
            public GesturePartResult Update(Skeleton skeleton)
            {
                // Hand above elbow
                if (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.HipRight].Position.Y)
                {
                    if (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.HipRight].Position.X)
                    {
                        Console.WriteLine("UP " + skeleton.Joints[JointType.HandRight].Position.Y + "\n");

                        return GesturePartResult.Succeeded;
                    }
                }
                // Hand dropped
                old_position = skeleton.Joints[JointType.HandRight].Position.Y;
                return GesturePartResult.Failed;
            }
        }

        public class BouncingSegment2 : IGestureSegment
        {
            private float old_position = 0;
            private float th = 0.01f;
            public GesturePartResult Update(Skeleton skeleton)
            {
                // Hand above elbow
                if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.HipRight].Position.Y)
                {
                    if (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.HipRight].Position.X)
                    {
                        Console.WriteLine("DOWN " + skeleton.Joints[JointType.HandRight].Position.Y + "\n");

                        return GesturePartResult.Succeeded;
                    }
                }

                // Hand dropped
                old_position = skeleton.Joints[JointType.HandRight].Position.Y;
                return GesturePartResult.Failed;
            }
        }
    }

