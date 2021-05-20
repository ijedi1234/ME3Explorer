using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME3Explorer.Packages.Decompressed {
    public class ImportTableEntry {

        public ulong PackageName { get; set; }
        public string PackageNameStr { get { return NameTable[(int)PackageName]; } }
        public ulong ClassName { get; set; }
        public string ClassNameStr { get { return NameTable[(int)ClassName]; } }
        public int Link { get; set; }
        public ulong ImportName { get; set; }
        public string ImportNameStr { get { return NameTable[(int)ImportName]; } }

        private List<string> NameTable;

        public ImportTableEntry(List<string> nameTable) {
            NameTable = nameTable;
        }

    }
}
