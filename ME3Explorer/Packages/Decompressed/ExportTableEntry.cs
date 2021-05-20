using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME3Explorer.Packages.Decompressed {
    public class ExportTableEntry {
        public uint Class { get; set; }
        public string ClassStr { get { return NameTable[(int)Class]; } }
        public uint SuperClass { get; set; }
        public string SuperClassStr { get { return NameTable[(int)SuperClass]; } }
        public uint Link { get; set; }
        public uint ObjectName { get; set; }
        public string ObjectNameStr {  get { return NameTable[(int)ObjectName]; } }
        public uint ObjectIndex { get; set; }
        public uint Archetype { get; set; }
        public uint ObjectFlags { get; set; }
        public uint DataSize { get; set; }
        public uint DataOffset { get; set; }
        public uint Unknown1 { get; set; }
        public uint StrangeCount { get; set; }
        public uint Unknown2 { get; set; }
        public Guid Guid { get; set; }
        public uint GuidPresent { get; set; }
        public List<uint> StrangeItems { get; set; }

        private List<string> NameTable;

        public ExportTableEntry(List<string> nameTable) {
            NameTable = nameTable;
        }

    }
}
