using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME3Explorer.Packages.Decompressed {
    public class ME3PackageDecompressed {

        public FileHeader FileHeader { get; private set; }
        public List<string> NameTable { get; private set; }
        public List<ImportTableEntry> ImportTable { get; private set; }
        public List<ExportTableEntry> ExportTable { get; private set; }
        public byte[] data;

        public ME3PackageDecompressed(FileHeader header, byte[] decompressedFile) {
            FileHeader = header;
            NameTable = new List<string>();
            ImportTable = new List<ImportTableEntry>();
            ExportTable = new List<ExportTableEntry>();
            using (MemoryStream stream = new MemoryStream(decompressedFile)) {
                using (BinaryReader reader = new BinaryReader(stream)) {
                    reader.ReadBytes(FileHeader.SIZE);
                    for(int i = 0; i < FileHeader.NameCount; i++) {
                        int NameTableEntrySize = -reader.ReadInt32();
                        string name = "";
                        for(int j = 0; j < NameTableEntrySize - 1; j++) {
                            name += (char)reader.ReadByte(); reader.ReadByte();
                        }
                        reader.ReadInt16();
                        NameTable.Add(name);
                    }
                    for (int i = 0; i < FileHeader.ImportCount; i++) {
                        ImportTableEntry entry = new ImportTableEntry(NameTable) {
                            PackageName = (uint)Math.Abs(reader.ReadInt64()),
                            ClassName = (uint)Math.Abs(reader.ReadInt64()),
                            Link = reader.ReadInt32(),
                            ImportName = (uint)Math.Abs(reader.ReadInt64())
                        };
                        ImportTable.Add(entry);
                    }
                    for (int i = 0; i < FileHeader.ExportCount; i++) {
                        ExportTableEntry entry = new ExportTableEntry(NameTable) {
                            Class = (uint)Math.Abs(reader.ReadInt32()),
                            SuperClass = (uint)Math.Abs(reader.ReadInt32()),
                            Link = (uint)Math.Abs(reader.ReadInt32()),
                            ObjectName = (uint)Math.Abs(reader.ReadInt32()),
                            ObjectIndex = (uint)Math.Abs(reader.ReadInt32()),
                            Archetype = (uint)Math.Abs(reader.ReadInt32()),
                            ObjectFlags = reader.ReadUInt32(),
                            DataSize = reader.ReadUInt32(),
                            DataOffset = reader.ReadUInt32(),
                            Unknown1 = reader.ReadUInt32(),
                            Unknown2 = reader.ReadUInt32(),
                            StrangeCount = reader.ReadUInt32(),
                            StrangeItems = new List<uint>()
                        };
                        for(int j = 0; j < entry.StrangeCount; j++) {
                            entry.StrangeItems.Add(reader.ReadUInt32());
                        }
                        entry.Guid = header.ConstructGuid(reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32(), reader.ReadUInt32());
                        entry.GuidPresent = reader.ReadUInt32();
                        ExportTable.Add(entry);
                    }
                }
            }
            data = decompressedFile;
        }

    }
}
