using System.Linq;

namespace Sander0542.CMLedController.Abstractions
{
    public interface IRequest
    {
        protected DataRow[] Data { get; }

        public byte[][] BuildRequest()
        {
            var request = new byte[Data.Length][];

            for (var index = 0; index < Data.Length; index++)
            {
                var dataRow = Data[index];
                var data = dataRow.ToList();

                while (data.Count < 64)
                {
                    data.Add(dataRow.Filler);
                }

                data.Insert(0, 0);

                request[index] = data.ToArray();
            }

            return request;
        }
    }
}
