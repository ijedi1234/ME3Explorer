using Gammtek.Conduit.Extensions.IO;
using OodleWrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME3Explorer.Packages.Compressed {
    public class Partition {

        public uint DecompressOffset { get; private set; }
        public uint DecompressSize { get; private set; }
        public uint CompressOffset { get; private set; }
        public uint CompressSize { get; private set; }
        public Chunk Chunk { get; private set; }

        public Partition(BinaryReader file) {
            DecompressOffset = file.ReadUInt32();
            DecompressSize = file.ReadUInt32();
            CompressOffset = file.ReadUInt32();
            CompressSize = file.ReadUInt32();
        }

        public void SetupChunk(BinaryReader file) {
            file.Seek(CompressOffset);
            Chunk = new Chunk(file);
        }

        public void Decompress(Oodle oodle, byte[] decompressedFile) {
            Chunk.Decompress(oodle, decompressedFile, DecompressOffset);
        }

    }
}
