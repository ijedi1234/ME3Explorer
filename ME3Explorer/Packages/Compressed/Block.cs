using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using KFreonLib.Helpers.ManagedLZO;
using OodleWrapper;

namespace ME3Explorer.Packages.Compressed {
    public class Block {

        public uint CompressedSize { get; private set; }
        public uint DecompressedSize { get; private set; }
        public byte[] Data { get; private set; }

        public Block(BinaryReader file) {
            CompressedSize = file.ReadUInt32();
            DecompressedSize = file.ReadUInt32();
        }

        public void SetupBlock(BinaryReader file) {
            Data = file.ReadBytes((int)CompressedSize);
        }

        public void Decompress(Oodle oodle, byte[] decompressedFile, uint offset) {
            byte[] result = new byte[DecompressedSize];
            oodle.Decompress(Data, result);
            Array.Copy(result, 0, decompressedFile, offset, result.Length);
        }

    }
}
