using System;
using System.Drawing;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.Requests
{
    public class BreathingRequest : IRequest
    {
        public Color Color { get; }
        public byte Speed { get; }

        public BreathingRequest(Color color, int speed) : this(color, ConvertSpeed(speed))
        {
            if (speed < 0 || speed > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(speed), "The speed parameter needs to be between 1 and 5");
            }
        }

        public BreathingRequest(Color color, byte speed)
        {
            Color = color;
            Speed = speed;
        }

        DataRow[] IRequest.Data => new[]
        {
            new DataRow
            {
                65,
                1
            },
            new DataRow
            {
                81,
                43,
                0,
                0,
                1,
                Speed,
                32,
                255,
                255,
                Color.A,
                Color.R,
                Color.G,
                Color.B
            },
            new DataRow
            {
                81,
                40,
                0,
                0,
                1
            },
            new DataRow
            {
                80,
                85
            },
            new DataRow
            {
                65
            },
        };

        private static byte ConvertSpeed(int speed)
        {
            return speed switch
            {
                1 => 60,
                2 => 55,
                3 => 49,
                4 => 44,
                5 => 38,
                _ => 255
            };
        }
    }
}
