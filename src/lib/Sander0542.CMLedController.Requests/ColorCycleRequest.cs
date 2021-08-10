using System;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.Requests
{
    public class ColorCycleRequest : IRequest
    {
        public byte Brightness { get; }

        public byte Speed { get; }

        public ColorCycleRequest(int brightness, int speed) : this(ConvertBrightness(brightness), ConvertSpeed(speed))
        {
            if (brightness < 0 || brightness > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(brightness), "The brightness parameter needs to be between 1 and 5");
            }

            if (speed < 0 || speed > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(speed), "The speed parameter needs to be between 1 and 5");
            }
        }

        public ColorCycleRequest(byte brightness, byte speed)
        {
            Brightness = brightness;
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
                2,
                Speed,
                0,
                255,
                255,
                Brightness,
                255,
                255,
                255,
            },
            new DataRow
            {
                81,
                40,
                0,
                0,
                2
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

        private static byte ConvertBrightness(int brightness)
        {
            return brightness switch
            {
                1 => 63,
                2 => 90,
                3 => 127,
                4 => 153,
                5 => 223,
                _ => 255
            };
        }

        private static byte ConvertSpeed(int speed)
        {
            return speed switch
            {
                1 => 150,
                2 => 140,
                3 => 128,
                4 => 110,
                5 => 104,
                _ => 255
            };
        }
    }
}
