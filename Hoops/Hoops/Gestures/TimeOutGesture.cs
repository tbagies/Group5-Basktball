using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Gestures
{
    class TimeOutGesture
    {
        readonly int WINDOW_SIZE = 50;

        IGestureSegment[] _segments;
        int _currentSegment =0;
        int _frameCount = 0;

        public event EventHandler GestureRecognized;
        
        Boolean[] isSegmentStarted = new Boolean[1];

        public TimeOutGesture()
        {
            TimeOutSegment1 timeOut1 = new TimeOutSegment1();
            TimeOutSegment2 timeOut2 = new TimeOutSegment2();

            _segments = new IGestureSegment[]
            {
                timeOut1
      //          timeOut2
            };

            for (int i = 0; i < 1; i++)
            {
                isSegmentStarted[i] = false;
            }   
        }

        public void Update(Skeleton skeleton)
        {
            GesturePartResult result = _segments[_currentSegment].Update(skeleton);
            if (result == GesturePartResult.Succeeded && !isSegmentStarted[0])
            {
                isSegmentStarted[_currentSegment] = true;
                Console.WriteLine("\n" + _currentSegment);
                if (_currentSegment + 1 < _segments.Length)
                {
                    _currentSegment++;
                    _frameCount = 0;
                }
                else
                {
                    Console.WriteLine("\nNull gesture");
                    if (GestureRecognized != null)
                    {
                        Console.WriteLine("\nCall TimeOut gestureRec");
                        GestureRecognized(this, new EventArgs());
                  //      Reset();
                    }
                }
            }
             else if (result == GesturePartResult.Failed || _frameCount == WINDOW_SIZE)
            {
                if (_currentSegment > 0)
                {
                    if (!isSegmentStarted[_currentSegment - 1])
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
            _currentSegment = 0;
            _frameCount = 0;
            //Reset flag
            for (int i = 0; i < _segments.Length; i++) isSegmentStarted[i] = false;
            
        }

    }
}
