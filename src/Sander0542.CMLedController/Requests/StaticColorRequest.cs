using System.Drawing;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.Requests
{
    public class StaticColorRequest : IRequest
    {
        public Color Color { get; }

        public StaticColorRequest(Color color)
        {
            Color = color;
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
                0,
                5,
                0,
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
                40
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
    }
}
