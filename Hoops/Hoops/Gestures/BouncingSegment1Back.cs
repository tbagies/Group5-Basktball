using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Gestures
{
        public class BounceingSegment1Back : IGestureSegment
        {
            private float old_position=0;
            private float th = 0.01f;
            public GesturePartResult Update(Skeleton skeleton)
            {
                // Hand above elbow
                if (old_position!=0 && skeleton.Joints[JointType.HandLeft].Position.Y < old_position + th)
                {
                    if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ElbowLeft].Position.X)
                    {
                        Console.WriteLine("Down " + skeleton.Joints[JointType.HandLeft].Position.Y + "\n");

                        return GesturePartResult.Succeeded;
                    }
                }
                // Hand dropped
                old_position = skeleton.Joints[JointType.HandLeft].Position.Y;
                return GesturePartResult.Failed;
            }
        }

        public class BouncingSegment2Back : IGestureSegment
        {
            private float old_position = 0;
            private float th = 0.01f;
            public GesturePartResult Update(Skeleton skeleton)
            {
                // Hand above elbow
                if (old_position != 0 && skeleton.Joints[JointType.HandLeft].Position.Y > old_position + th)
                {
                    if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ElbowLeft].Position.X)
                    {
                        Console.WriteLine("UP " + skeleton.Joints[JointType.HandLeft].Position.Y + "\n");

                        return GesturePartResult.Succeeded;
                    }
                }

                // Hand dropped
                old_position = skeleton.Joints[JointType.HandLeft].Position.Y;
                return GesturePartResult.Failed;
            }
        }
    }

