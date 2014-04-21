using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Gestures
{
    class BouncingGestureBack
    {
        readonly int WINDOW_SIZE = 50;

        IGestureSegment[] bouncing;
      
        int _frameCount = 0;
        int _Count = 0;
        int length;
        Boolean[] isStarted;
        public event EventHandler GestureRecognized;

        public BouncingGestureBack()
        {
            BounceingSegment1Back bouncing0 = new BounceingSegment1Back();
            BouncingSegment2Back bouncing1 = new BouncingSegment2Back();

             bouncing = new IGestureSegment[]
            {
                bouncing0,
                bouncing1
            };
             length = bouncing.Length;
             isStarted = new Boolean[length];
            for (int i = 0; i < length; i++)
            {
                isStarted[i] = false;
            }
        }

        public void Update(Skeleton skeleton)
        {
            if (_Count < length)
            {
                GesturePartResult result = bouncing[_Count].Update(skeleton);
                if (result == GesturePartResult.Succeeded)
                {
                    isStarted[_Count] = true;
                    if (_Count + 1 < length)
                    {
                        _Count++;
                        _frameCount = 0;
                    }
                    else
                    {
                        Console.WriteLine("\nNull gesture " + GestureRecognized);
                        Console.WriteLine("\n counter " + _Count);
                        Console.WriteLine("\n GestureRecognized " + GestureRecognized);
                        if (GestureRecognized != null)
                        {
                            Console.WriteLine("\nCall gestureRec Bouncing Back");
                            GestureRecognized(this, new EventArgs());
                            Reset();
                        }
                    }
                }
                else if (result == GesturePartResult.Failed || _frameCount == WINDOW_SIZE)
                {
                    Reset();
                }
                else
                {
                    _frameCount++;
                }
            }
        }

        public void Reset()
        {
            _Count = 0;
            _frameCount = 0;
            //Reset flag
            for (int i = 0; i < length; i++)
            {
                isStarted[i] = false;
            }
        }

    }
}
