using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions;
using System.IO.Compression;
using OodleWrapper.Enums;

namespace OodleWrapper
{
    public class Oodle : IDisposable {

        private IntPtr libraryHandle;

        public delegate long OodleLZ_CompressDelegate(Compressor compressor, byte[] src, long src_size, byte[] dst, OodleCompressionLevel level, long opts, long context, long unused, long scratch, long scratch_size);
        public readonly OodleLZ_CompressDelegate Compress_Base;

        public delegate long OodleLZ_DecompressDelegate(byte[] src, long src_size, byte[] dst, long dst_size, FuzzSafe fuzz, CheckCRC crc, Verbosity verbosity, long context, long e, long callback, long callback_ctx, long scratch, long scratch_size, DecodeThreadPhase thread_phase);
        public readonly OodleLZ_DecompressDelegate Decompress_Base;

        public delegate long OodleLZDecoder_MemorySizeNeededDelegate(Compressor compressor, long size);
        public readonly OodleLZDecoder_MemorySizeNeededDelegate MemorySizeNeeded_Base;

        public Oodle(string libraryPath) {
            libraryPath = Path.GetFullPath(libraryPath);
            libraryHandle = NativeLibrary.Load(libraryPath);
            IntPtr compressPtr = NativeLibrary.GetExport(libraryHandle, "OodleLZ_Compress");
            IntPtr decompressPtr = NativeLibrary.GetExport(libraryHandle, "OodleLZ_Decompress");
            IntPtr memorySizeNeededPtr = NativeLibrary.GetExport(libraryHandle, "OodleLZDecoder_MemorySizeNeeded");
            Compress_Base = Marshal.GetDelegateForFunctionPointer<OodleLZ_CompressDelegate>(compressPtr);
            Decompress_Base = Marshal.GetDelegateForFunctionPointer<OodleLZ_DecompressDelegate>(decompressPtr);
            MemorySizeNeeded_Base = Marshal.GetDelegateForFunctionPointer<OodleLZDecoder_MemorySizeNeededDelegate>(memorySizeNeededPtr);
        }

        public void Decompress(byte[] src, byte[] dest) {
            Decompress_Base(src, src.Length, dest, dest.Length, FuzzSafe.No, CheckCRC.No, Verbosity.None, 0L, 0L, 0L, 0L, 0L, 0L, DecodeThreadPhase.Unthreaded);
        }

        public virtual void Dispose(bool disposing) {
            if (!disposing) return;
            if (libraryHandle != IntPtr.Zero) {
                NativeLibrary.Free(libraryHandle);
            }
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
