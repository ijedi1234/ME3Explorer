using ME3Explorer.Packages.Decompressed;
using OodleWrapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME3Explorer.Packages.Compressed {
    public class ME3PackageCompressed {

        public FileHeader FileHeader { get; private set; }
        public List<Partition> Partitions { get; private set; }
        public ulong Unknown { get; private set; }

        public ME3PackageCompressed(BinaryReader file, FileHeader header) {
            FileHeader = header;
            Partitions = new List<Partition>();
            for (int i = 0; i < FileHeader.PartitionCount; i++) {
                Partitions.Add(new Partition(file));
            }
            Unknown = file.ReadUInt64();
            foreach (Partition partition in Partitions)
                partition.SetupChunk(file);
        }

        public ME3PackageDecompressed Decompress() {
            string lib = @"G:\SteamSecondary\steamapps\common\Mass Effect Legendary Edition\Game\ME3\Binaries\Win64\oo2core_8_win64.dll";
            uint maxOffset = Partitions.Max(i => i.DecompressOffset);
            long decompressTotalSize = Partitions.First(i => i.DecompressOffset == maxOffset).Chunk.DecompressedSize;
            decompressTotalSize += maxOffset;
            byte[] decompressedFile = new byte[decompressTotalSize];
            using (Oodle oodle = new Oodle(lib)) {
                foreach (Partition partition in Partitions) partition.Decompress(oodle, decompressedFile);
            }
            return new ME3PackageDecompressed(FileHeader, decompressedFile);
        }

    }
}
