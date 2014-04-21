using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Gestures
{
    class JumpGesture
    {
        readonly int WINDOW_SIZE = 50;

        IGestureSegment[] jump;
      
        int _frameCount = 0;
        int _jumpCount = 0;
        int length;
        Boolean[] isJumpStarted = new Boolean[1];
        public event EventHandler GestureRecognized;

        public JumpGesture()
        {
            JumpSegment2 jump0 = new JumpSegment2();
            JumpSegment1 jump1 = new JumpSegment1();
            JumpSegment2 jump2 = new JumpSegment2();

             jump = new IGestureSegment[]
            {
            //    jump0,
                jump1
          //      jump2
            };
             length = jump.Length;
            for (int i = 0; i < length; i++)
            {
                isJumpStarted[i] = false;
            }
        }

        public void Update(Skeleton skeleton)
        {
            if (_jumpCount < length)
            {
                GesturePartResult result = jump[_jumpCount].Update(skeleton);
                if (result == GesturePartResult.Succeeded && _jumpCount < length)
                {
                    isJumpStarted[_jumpCount] = true;
                    if (_jumpCount + 1 < length)
                    {
                        _jumpCount++;
                        _frameCount = 0;
                    }
                    else
                    {
                       _jumpCount++;
                        Console.WriteLine("\nNull gesture " + GestureRecognized);
                        Console.WriteLine("\n counter " + _jumpCount);
                        Console.WriteLine("\n GestureRecognized " + GestureRecognized);
                        if (GestureRecognized != null && _jumpCount<=length)
                        {
                            Console.WriteLine("\nCall gestureRec");
                            GestureRecognized(this, new EventArgs());
                        }
                    }
                }
                else if (result == GesturePartResult.Failed || _frameCount == WINDOW_SIZE)
                {
                    Reset();
                 /*   if (_jumpCount > 0)
                    {
                        if (!isJumpStarted[_jumpCount - 1])
                        {
                            Console.WriteLine("\nRESET, FAILED");
                            Reset();
                        }
                    }*/
                }
                else
                {
                    _frameCount++;
                }
            }
        }

        public void Reset()
        {
            _jumpCount = 0;
            _frameCount = 0;
            //Reset flag
            for (int i = 0; i < length; i++)
            {
                isJumpStarted[i] = false;
            }
        }

    }
}
