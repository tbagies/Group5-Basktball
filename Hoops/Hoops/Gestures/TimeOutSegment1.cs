using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Gestures
{
    class TimeOutSegment1: IGestureSegment
    {
        float th = 0.1f;
        public GesturePartResult Update(Skeleton skeleton)
        {
            // HWrisr above head
            if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.HandLeft].Position.Y 
               && Math.Abs(skeleton.Joints[JointType.HandRight].Position.Y - skeleton.Joints[JointType.HandLeft].Position.Y) >  th
                && skeleton.Joints[JointType.ElbowLeft].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y)
            {
                
                {
                    Console.WriteLine(skeleton.Joints[JointType.HandRight].Position.X + " " +
                    skeleton.Joints[JointType.ElbowRight].Position.X);
                    return GesturePartResult.Succeeded;
                }
            }

            // Hand dropped
            return GesturePartResult.Failed;
        }
    }
    
    class TimeOutSegment2 : IGestureSegment
    {
        float th = 0.01f;
        public GesturePartResult Update(Skeleton skeleton)
        {
            // HWrisr above head
            if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.HandLeft].Position.Y
               && skeleton.Joints[JointType.ElbowLeft].Position.Y < skeleton.Joints[JointType.ElbowRight].Position.Y)
            
            {

                {
                    //Console.WriteLine(skeleton.Joints[JointType.HandRight].Position.X + " " +
                    //skeleton.Joints[JointType.ElbowRight].Position.X);
                    return GesturePartResult.Succeeded;
                }
            }

            // Hand dropped
            return GesturePartResult.Failed;
        }
    }
}
