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
        int length=1;
        Boolean[] isJumpStarted = new Boolean[1];
        public event EventHandler GestureRecognized;

        public JumpGesture()
        {
            JumpSegment1 jump1 = new JumpSegment1();

             jump = new IGestureSegment[]
            {
                jump1
            };
             length = jump.Length;
            for (int i = 0; i < length; i++)
            {
                isJumpStarted[i] = false;
            }
        }

        public void Update(Skeleton skeleton)
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
                        Console.WriteLine("\nNull gesture");
                        if (GestureRecognized != null)
                        {
                            Console.WriteLine("\nCall Jumping gestureRec");
                            GestureRecognized(this, new EventArgs());
                            //   Reset();
                        }
                    }
                }

                else if (result == GesturePartResult.Failed || _frameCount == WINDOW_SIZE)
                {
                    if (_jumpCount > 0)
                    {
                        if (!isJumpStarted[_jumpCount - 1])
                        {
                            Console.WriteLine("\nRESET, FAILED");
                            Reset();
                        }
                    }
                }
                else
                {
                    _frameCount++;
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
