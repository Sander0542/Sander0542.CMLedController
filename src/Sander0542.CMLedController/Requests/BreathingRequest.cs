using System;
using System.Drawing;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.Requests
{
    public class StarRequest : IRequest
    {
        public Color Color { get; }
        public byte Speed { get; }

        public StarRequest(Color color, int speed) : this(color, ConvertSpeed(speed))
        {
            if (speed < 0 || speed > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(speed), "The speed parameter needs to be between 1 and 5");
            }
        }

        public StarRequest(Color color, byte speed)
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
                3,
                Speed,
                0,
                25,
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
                3
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
                1 => 70,
                2 => 65,
                3 => 60,
                4 => 55,
                5 => 50,
                _ => 255
            };
        }
    }
}
