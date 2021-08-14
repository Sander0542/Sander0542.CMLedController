using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Sander0542.CMLedController.Abstractions.Enums;
using Sander0542.CMLedController.Abstractions.Extensions;

namespace Sander0542.CMLedController.Abstractions
{
    public class Packet
    {
        private readonly byte[] _data;

        public Packet()
        {
            _data = new byte[Constants.PacketSize];
        }

        private Packet(byte[] data)
        {
            _data = data;
        }

        public OpCode Operation
        {
            get => _data[PacketOffset.Op];
            set => _data[PacketOffset.Op] = value;
        }

        public OpCodeType OperationType
        {
            get => _data[PacketOffset.Type];
            set => _data[PacketOffset.Type] = value;
        }

        public byte Multilayer
        {
            get => _data[PacketOffset.Multilayer];
            set => _data[PacketOffset.Multilayer] = value;
        }

        public Mode Mode
        {
            get => _data[PacketOffset.Mode];
            set => _data[PacketOffset.Mode] = value;
        }

        public byte Speed
        {
            get => _data[PacketOffset.Speed];
            set => _data[PacketOffset.Speed] = value;
        }

        public byte Brightness
        {
            get => _data[PacketOffset.Brightness];
            set => _data[PacketOffset.Brightness] = value;
        }

        public Color Color1
        {
            get => _data.GetColor(PacketOffset.Color1);
            set => _data.SetColor(PacketOffset.Color1, value);
        }

        public Color Color2
        {
            get => _data.GetColor(PacketOffset.Color2);
            set => _data.SetColor(PacketOffset.Color2, value);
        }

        public Color MultipleColor1
        {
            get => _data.GetColor(PacketOffset.MultipleColor1);
            set => _data.SetColor(PacketOffset.MultipleColor1, value);
        }

        public Color MultipleColor2
        {
            get => _data.GetColor(PacketOffset.MultipleColor2);
            set => _data.SetColor(PacketOffset.MultipleColor2, value);
        }

        public Color MultipleColor3
        {
            get => _data.GetColor(PacketOffset.MultipleColor3);
            set => _data.SetColor(PacketOffset.MultipleColor3, value);
        }

        public Color MultipleColor4
        {
            get => _data.GetColor(PacketOffset.MultipleColor4);
            set => _data.SetColor(PacketOffset.MultipleColor4, value);
        }

        public int Length => _data.Length;

        public byte Get(PacketOffset packetOffset) => _data[packetOffset];

        public byte Get(int index) => _data[index];

        public void Set(PacketOffset packetOffset, byte value) => _data[packetOffset] = value;

        public void Set(int index, byte value) => _data[index] = value;

        public static implicit operator byte[](Packet packet) => packet._data.PreparePacket();
        
        public static implicit operator Packet(byte[] data) => new Packet(data.PrepareResponse());

        public override bool Equals(object obj)
        {
            return obj is Packet packet ? Equals(packet) : obj.Equals(this);
        }

        private bool Equals(Packet other)
        {
            return _data.SequenceEqual(other._data);
        }
        
        public override int GetHashCode()
        {
            var result = 0;
            var shift = 0;
            foreach (var value in _data)
            {
                shift = (shift + 11) % 21;
                result ^= (value+1024) << shift;
            }
            return result;
        }
    }
}
