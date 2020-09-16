using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace AssyCheck
{
    public static class AssyChecker
    {
        private static string s_assemblyFailureMessssage = null;
        private static string GetAssemblyFailureMessage()
        {
            string ComputeAssemblyFailureMessage()
            {
                List<string> failures = null;
                void AddFailure(string assembly)
                {
                    if (failures == null) failures = new List<string>();
                    failures.Add(assembly);
                }
                try { CheckBuffers(); } catch { AddFailure("System.Buffers"); }
                try { CheckUnsafe(); } catch { AddFailure("System.Runtime.CompilerServices.Unsafe"); }
                try { CheckNumerics(); } catch { AddFailure("System.Numerics.Vectors"); }

                if (failures == null || failures.Count == 0) return "";

                return "The assembly for " + string.Join(" + ", failures) + " could not be loaded; this usually means a missing assembly binding redirect - try checking this, and adding any that are missing;"
                    + " note that it is not always possible to add this redirects - for example 'azure functions v1'; it looks like you may need to use 'azure functions v2' for that - sorry, but that's out of our control";
            }
            return s_assemblyFailureMessssage ?? (s_assemblyFailureMessssage = ComputeAssemblyFailureMessage());
        }
        internal static void AssertDependencies()
        {
            string err = GetAssemblyFailureMessage();
            if (!string.IsNullOrEmpty(err))
                throw new InvalidOperationException(err);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void CheckBuffers()
        {
            var arr = System.Buffers.ArrayPool<byte>.Shared.Rent(64);
            GC.KeepAlive(arr);
            System.Buffers.ArrayPool<byte>.Shared.Return(arr);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void CheckUnsafe() => _ = System.Runtime.CompilerServices.Unsafe.SizeOf<int>();


        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void CheckNumerics() => _ = System.Numerics.Vector.IsHardwareAccelerated;

    }
}