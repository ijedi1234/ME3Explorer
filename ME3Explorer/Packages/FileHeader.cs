using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME3Explorer.Packages {
    public class FileHeader {

        public const int SIZE = 0x8E;

        public uint MagicNumber { get; private set; }
        public ushort License { get; private set; }
        public ushort Version { get; private set; }
        public uint Unknown1 { get; private set; }
        public uint FolderNameSize { get; private set; }
        public string FolderName { get; private set; }
        public uint Flags { get; private set; }
        public uint PackageType { get; private set; }
        public uint NameCount { get; private set; }
        public uint NameTablePointer { get; private set; }
        public uint ExportCount { get; private set; }
        public uint ExportTablePointer { get; private set; }
        public uint ImportCount { get; private set; }
        public uint ImportTablePointer { get; private set; }
        public uint Unknown2 { get; private set; }
        public uint Unknown3 { get; private set; }
        public uint Unknown4 { get; private set; }
        public uint Unknown5 { get; private set; }
        public uint Unknown6 { get; private set; }
        public Guid GUID { get; private set; }
        public uint Generations { get; private set; }
        public uint ExportCountDup { get; private set; }
        public uint NameCountDup { get; private set; }
        public uint Unknown7 { get; private set; }
        public uint Engine { get; private set; }
        public uint Unknown8 { get; private set; }
        public uint Unknown9 { get; private set; }
        public uint Unknown10 { get; private set; }
        public uint CompressionType { get; private set; }
        public uint PartitionCount { get; private set; }

        public FileHeader(BinaryReader file) {
            MagicNumber = file.ReadUInt32();
            License = file.ReadUInt16();
            Version = file.ReadUInt16();
            Unknown1 = file.ReadUInt32();
            FolderNameSize = (uint)Math.Abs(file.ReadInt32());
            FolderName = "";
            for (int i = 0; i < FolderNameSize - 1; i++) {
                FolderName += (char)file.ReadByte();
                file.ReadByte();
            }
            file.ReadBytes(2);
            Flags = file.ReadUInt32();
            PackageType = file.ReadUInt32();
            NameCount = file.ReadUInt32();
            NameTablePointer = file.ReadUInt32();
            ExportCount = file.ReadUInt32();
            ExportTablePointer = file.ReadUInt32();
            ImportCount = file.ReadUInt32();
            ImportTablePointer = file.ReadUInt32();
            Unknown2 = file.ReadUInt32();
            Unknown3 = file.ReadUInt32();
            Unknown4 = file.ReadUInt32();
            Unknown5 = file.ReadUInt32();
            Unknown6 = file.ReadUInt32();
            GUID = ConstructGuid(file.ReadUInt32(), file.ReadUInt32(), file.ReadUInt32(), file.ReadUInt32());
            Generations = file.ReadUInt32();
            ExportCountDup = file.ReadUInt32();
            NameCountDup = file.ReadUInt32();
            Unknown7 = file.ReadUInt32();
            Engine = file.ReadUInt32();
            Unknown8 = file.ReadUInt32();
            Unknown9 = file.ReadUInt32();
            Unknown10 = file.ReadUInt32();
            CompressionType = file.ReadUInt32();
            PartitionCount = file.ReadUInt32();
        }

        public Guid ConstructGuid(uint val1, uint val2, uint val3, uint val4) {
            string guidString = val1.ToString("X8") + val2.ToString("X8") + val3.ToString("X8") + val4.ToString("X8");
            return new Guid(guidString);
        }

    }
}
