using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace CsharpBenchmark
{
    public class ByteArrayEqualBenchmark
    {
        [ParamsSource(nameof(MakeTestData))]
        public byte[] TestData { get; set; } = new byte[] { 1, 2, 3 };

        public ByteArrayEqualBenchmark()
        {
        }

        public void Test()
        {
            var funcs = new (string, Func<byte[], byte[], bool>)[]{
                ("BuiltIn_SequenceEqual", (a,b) => a.SequenceEqual(b)),
                ("BuiltIn_StructuralEqualityComparer", (a,b) => System.Collections.StructuralComparisons.StructuralEqualityComparer.Equals(a, b)),
                ("BuiltIn_SpanSequenceEqual", (a,b) => a.AsSpan().SequenceEqual(b.AsSpan())),
                ("SimpleCompare", (a,b) => SimpleCompare(a, b)),
                ("MultiTaskCompare", (a,b) => MultiTaskCompare(a, b)),
                ("ArrayCompare32", (a,b) => ArrayCompare32(a, b)),
                ("ArrayCompare64", (a,b) => ArrayCompare64(a, b)),
                ("memcmp", (a,b) => memcmp(a, b, a.Length) == 0),
            };
            foreach (var a in MakeTestData())
            {
                var b = MakeArray(a);
                foreach (var (name, func) in funcs)
                {
                    if (func(a, b) == false)
                    {
                        throw new Exception($"{name} was not true. (equals test)");
                    }
                }
                b[b.Length - 1] = (byte)(b[b.Length - 1] ^ 0xFF);

                foreach (var (name, func) in funcs)
                {
                    if (func(a, b) == true)
                    {
                        throw new Exception($"{name} was not true. (not equals test)");
                    }
                }
            }
        }

        public static IEnumerable<byte[]> MakeTestData()
        {
            var rand = new RNGCryptoServiceProvider();
            var bytes = new[] {
                new byte[16],
                new byte[1024],
                new byte[1024*1024 - 1],
                new byte[1024*1024],
            };

            foreach (byte[] b in bytes)
            {
                rand.GetBytes(b);
            }
            return bytes;
        }
        public static T[] MakeArray<T>(T[] source)
        {
            var b = new T[source.Length];
            Array.Copy(source, b, source.Length);
            return b;
        }


        // [Benchmark]
        // public bool ArrayEquals()
        // {
        //     var b = new byte[TestData.Length];
        //     Array.Copy(TestData, b, TestData.Length);

        //     return Array.Equals(TestData, b);
        // }

        [Benchmark]
        public bool StructuralEqualityComparerEquals()
        {
            var b = MakeArray(TestData);
            // implement `IStructuralEquatable`

            return System.Collections.StructuralComparisons.StructuralEqualityComparer.Equals(TestData, b);
        }

        [Benchmark]
        public bool SequenceEqual()
        {
            var b = MakeArray(TestData);
            return TestData.SequenceEqual(b);
        }

        [Benchmark]
        public bool SimpleCompare()
        {
            var b = MakeArray(TestData);
            return SimpleCompare(TestData, b);
        }

        [Benchmark]
        public bool SpanSequenceEqual()
        {
            var b = MakeArray(TestData);
            return TestData.AsSpan().SequenceEqual(b.AsSpan());
        }

        [Benchmark]
        public bool MultiTaskCompare()
        {
            var b = MakeArray(TestData);
            return MultiTaskCompare(TestData, b);
        }
        [Benchmark]
        public bool ArrayCompare32()
        {
            var b = MakeArray(TestData);
            return ArrayCompare32(TestData, b);
        }
        [Benchmark]
        public bool ArrayCompare64()
        {
            var b = MakeArray(TestData);
            return ArrayCompare64(TestData, b);
        }
        [Benchmark]
        public bool MemCmp()
        {
            var b = MakeArray(TestData);
            return memcmp(TestData, b, TestData.Length) == 0;
        }

        // ref: http://uhurumkate.blogspot.com/2012/07/byte-array-comparison-benchmarks.html
        public static bool SimpleCompare<T>(T[] a, T[] b) where T : IEquatable<T>
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            for (int i = 0; i < a.Length; i++)
            {
                if (!a[i].Equals(b[i]))
                {
                    return false;
                }
            }

            return true;
        }
        

        public static bool SequenceEqual<T>(Span<T> a, Span<T> b) where T : IEquatable<T>
        {
            if (a == b)
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            return a.SequenceEqual(b);
        }

        public static bool MultiTaskCompare<T>(T[] a, T[] b) where T : IEquatable<T>
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            int N = 8;
            int NL = a.Length / N;
            if (NL == 0)
            {
                return SequenceEqual(a.AsSpan(), b.AsSpan());
            }
            Task<bool>[] tasks = new Task<bool>[N];

            for (int i = 0; i < N - 1; ++i)
            {
                tasks[i] = Task.Run<bool>(() => SequenceEqual(a.AsSpan(i * NL, NL), b.AsSpan(i * NL, NL)));
            }
            
            tasks[N - 1] = Task.Run<bool>(() => SequenceEqual(a.AsSpan((N - 1) * NL), b.AsSpan((N - 1) * NL)));

            Task.WaitAll(tasks);
            foreach (var task in tasks)
            {
                if (task.Result == false)
                {
                    return false;
                }
            }
            return true;
        }

        // ref: https://stackoverflow.com/questions/2173414/c-sharp-byte-comparison-without-bound-checks
        public static bool ArrayCompare32(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int len = a.Length;
            unsafe
            {
                fixed (byte* ap = a, bp = b)
                {
                    int* aip = (int*)ap, bip = (int*)bp;
                    for (; len >= 4; len -= 4)
                    {
                        if (*aip != *bip) return false;
                        aip++;
                        bip++;
                    }
                    byte* ap2 = (byte*)aip, bp2 = (byte*)bip;
                    for (; len > 0; len--)
                    {
                        if (*ap2 != *bp2) return false;
                        ap2++;
                        bp2++;
                    }
                }
            }
            return true;
        }

        // ref: https://stackoverflow.com/questions/2173414/c-sharp-byte-comparison-without-bound-checks
        public static bool ArrayCompare64(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            int len = a.Length;
            unsafe
            {
                fixed (byte* ap = a, bp = b)
                {
                    long* alp = (long*)ap, blp = (long*)bp;
                    for (; len >= 8; len -= 8)
                    {
                        if (*alp != *blp) return false;
                        alp++;
                        blp++;
                    }
                    byte* ap2 = (byte*)alp, bp2 = (byte*)blp;
                    for (; len > 0; len--)
                    {
                        if (*ap2 != *bp2) return false;
                        ap2++;
                        bp2++;
                    }
                }
            }
            return true;
        }

        [DllImport("msvcrt.dll")]
        private static extern int memcmp(byte[] arr1, byte[] arr2, int cnt);
    }
}
