using OodleWrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME3Explorer.Packages.Compressed {
    public class Chunk {

        public uint MagicNumber { get; private set; }
        public uint BlockSize { get; private set; }
        public uint CompressedSize { get; private set; }
        public uint DecompressedSize { get; private set; }
        public List<Block> Blocks { get; private set; }

        protected uint NumBlocks { get; private set; }

        public Chunk(BinaryReader file) {
            MagicNumber = file.ReadUInt32();
            BlockSize = file.ReadUInt32();
            CompressedSize = file.ReadUInt32();
            DecompressedSize = file.ReadUInt32();
            NumBlocks = (uint)Math.Ceiling(((float)DecompressedSize) / BlockSize);
            Blocks = new List<Block>();
            for(int i = 0; i < NumBlocks; i++) {
                Blocks.Add(new Block(file));
            }
            foreach (Block block in Blocks) block.SetupBlock(file);
        }

        public void Decompress(Oodle oodle, byte[] decompressedFile, uint offset) {
            foreach (Block block in Blocks) {
                uint expectedEnd = offset + block.DecompressedSize;
                block.Decompress(oodle, decompressedFile, offset);
                offset += block.DecompressedSize;
            }
        }

    }
}
