using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Kinect;

namespace Gestures
{
    class PassingGesture
    {
        readonly int WINDOW_SIZE = 50;

        IGestureSegment[] _segments;
        int _currentSegment =0;
        int _frameCount = 0;

        public event EventHandler GestureRecognized;

        Boolean[] isSegmentStarted = new Boolean[2];

        int length;
        public PassingGesture()
        {
          //  ShootingSegment2 shoot0 = new ShootingSegment2();
            PassingSegment1 seg1 = new PassingSegment1();
            PassingSegment2 seg2 = new PassingSegment2();

            _segments = new IGestureSegment[]
            {
             //   shoot0,
                seg1,
                seg2
            };

            length = _segments.Length;
            for (int i = 0; i <length; i++)
            {
                isSegmentStarted[i] = false;
            }   
        }

        public void Update(Skeleton skeleton)
        {
            GesturePartResult result = _segments[_currentSegment].Update(skeleton);

            if (result == GesturePartResult.Succeeded && _currentSegment<length)
            {
                isSegmentStarted[_currentSegment] = true;
                Console.WriteLine("\n" + _currentSegment);
                if (_currentSegment + 1 < length)
                {
                    _currentSegment++;
                    _frameCount = 0;
                }
                else
                {
                    Console.WriteLine("\nNull gesture");
                    if (GestureRecognized != null)
                    {
                        Console.WriteLine("\nCall Passing gestureRec");
                        GestureRecognized(this, new EventArgs());
                     //   Reset();
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
