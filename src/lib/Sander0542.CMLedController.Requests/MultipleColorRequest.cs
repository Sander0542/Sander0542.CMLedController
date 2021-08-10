using System.Drawing;
using Sander0542.CMLedController.Abstractions;

namespace Sander0542.CMLedController.Requests
{
    public class MultipleColorRequest : IRequest
    {
        public Color Port1 { get; }
        public Color Port2 { get; }
        public Color Port3 { get; }
        public Color Port4 { get; }

        public MultipleColorRequest(Color port1, Color port2, Color port3, Color port4)
        {
            Port1 = port1;
            Port2 = port2;
            Port3 = port3;
            Port4 = port4;
        }

        DataRow[] IRequest.Data => new[]
        {
            new DataRow
            {
                65,
                128
            },
            new DataRow
            {
                81,
                40,
                0,
                0,
                4
            },
            new DataRow
            {
                81,
                48
            },
            new DataRow
            {
                81,
                168,
                0,
                0,
                Port1.R,
                Port1.G,
                Port1.B,
                Port2.R,
                Port2.G,
                Port2.B,
                Port3.R,
                Port3.G,
                Port3.B,
                Port4.R,
                Port4.G,
                Port4.B,
            },
            new DataRow
            {
                80,
                85
            },
            new DataRow(255)
            {
                81,
                44,
                0,
                0,
                4,
                0,
                0,
                255,
                255,
                255,
                255,
                255,
                255,
                0,
                0,
                0
            },
        };
    }
}
