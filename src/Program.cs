using ET.ETG;
using ET.ETInterface;

using Common = ET.Common;
using ETG = ET.ETG;
using ETI = ET.ETInterface;



using System;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Toolchains.NativeAot;

namespace ET.Benchmarks
{
    // [SimpleJob(RuntimeMoniker.Net80)]
    // [SimpleJob(RuntimeMoniker.NativeAot80)]
    public class VecBench
    {
        [Params(10, 1000)]
        public int N;

        private Common.Vector v1;
        private Common.Vector v2;
        private Common.Vector v3;
        private Common.Vector vOut;
        [GlobalSetup]
        public void Setup()
        {
            v1 = new(N);
            v2 = new(N);
            v3 = new(N);
            vOut = new(N);

            v1.SetRandom();
            v2.SetRandom();
            v3.SetRandom();
        }

        public VecBench()
        {
        }

        [Benchmark]
        public Common.Vector VecAddEager()
        {
            return v1 + v2 + v3;
        }

        [Benchmark]
        public Common.Vector VecAddET()
        {
            return ETG.VectorExpression.Add(
                v1.ET(),
                ETG.VectorExpression.Add(
                    v2.ET(),
                    v3.ET()
                )
            ).Eval();
        }
        [Benchmark]
        public void VecAddETInplace()
        {
            ETG.VectorExpression.Add(
                v1.ET(),
                ETG.VectorExpression.Add(
                    v2.ET(),
                    v3.ET()
                )
            ).EvalTo(vOut);
        }

        [Benchmark]
        public Common.Vector VecAddETInterface()
        {
            return ETI.VectorExpression.Add(
                v1.ETI(),
                ETI.VectorExpression.Add(
                    v2.ETI(),
                    v3.ETI()
                )
            ).Eval();
        }
        [Benchmark]
        public void VecAddETInterfaceInplace()
        {
            ETI.VectorExpression.Add(
                v1.ETI(),
                ETI.VectorExpression.Add(
                    v2.ETI(),
                    v3.ETI()
                )
            ).EvalTo(vOut);
        }




    }

    // [SimpleJob(RuntimeMoniker.Net80)]
    // [SimpleJob(RuntimeMoniker.NativeAot80)]
    public class MatMulBench
    {
        [Params(10, 1000)]
        public int N;

        private Common.Vector v1;
        private Common.Vector v2;
        private Common.Vector v3;
        private Common.Vector vOut;
        private Common.Matrix m1;
        [GlobalSetup]
        public void Setup()
        {
            v1 = new(N);
            v2 = new(N);
            v3 = new(N);
            m1 = new(N, N);
            vOut = new(N);

            v1.SetRandom();
            v2.SetRandom();
            v3.SetRandom();
            m1.SetRandom();
        }

        public MatMulBench()
        {
        }


        [Benchmark]
        public Common.Vector MatMulEager()
        {
            return m1 * (v1 + v2 + v3);
        }

        [Benchmark]
        public Common.Vector MatVecET()
        {
            return ETG.MatrixExpression.Mul(
                m1.ET(),
                ETG.VectorExpression.Add(
                    v1.ET(),
                    ETG.VectorExpression.Add(
                        v2.ET(),
                        v3.ET()
                    )
                )
            ).Eval();
        }

        [Benchmark]
        public void MatVecETInplace()
        {
            ETG.MatrixExpression.Mul(
                m1.ET(),
                ETG.VectorExpression.Add(
                    v1.ET(),
                    ETG.VectorExpression.Add(
                        v2.ET(),
                        v3.ET()
                    )
                )
            ).EvalTo(vOut);
        }

        [Benchmark]
        public Common.Vector MatVecETAfterEager()
        {
            return ETG.MatrixExpression.Mul(
                m1.ET(),
                ETG.VectorExpression.Add(
                    v1.ET(),
                    ETG.VectorExpression.Add(
                        v2.ET(),
                        v3.ET()
                    )
                ).Eval().ET()
            ).Eval();
        }

        [Benchmark]
        public void MatVecETInplaceAfterEager()
        {
            ETG.MatrixExpression.Mul(
               m1.ET(),
               ETG.VectorExpression.Add(
                   v1.ET(),
                   ETG.VectorExpression.Add(
                       v2.ET(),
                       v3.ET()
                   )
               ).Eval().ET()
           ).EvalTo(vOut);
        }

        [Benchmark]
        public Common.Vector MatMulETInterface()
        {
            return ETI.MatrixExpression.Mul(
                m1.ETI(),
                ETI.VectorExpression.Add(
                    v1.ETI(),
                    ETI.VectorExpression.Add(
                        v2.ETI(),
                        v3.ETI()
                    )
                )
            ).Eval();
        }
        [Benchmark]
        public void MatMulETInterfaceInplace()
        {
            ETI.MatrixExpression.Mul(
                m1.ETI(),
                ETI.VectorExpression.Add(
                    v1.ETI(),
                    ETI.VectorExpression.Add(
                        v2.ETI(),
                        v3.ETI()
                    )
                )
            ).EvalTo(vOut);
        }

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                // .AddJob(Job.ShortRun
                //     .WithToolchain(NativeAotToolchain.CreateBuilder()
                //         .UseNuGet()
                //         .IlcInstructionSet("avx2")
                //         .ToToolchain())
                //     .WithRuntime(NativeAotRuntime.Net80))
                .WithOptions(ConfigOptions.JoinSummary)
                .AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core80));


            BenchmarkRunner.Run(new[]{
                BenchmarkConverter.TypeToBenchmarks( typeof(VecBench), config),
                BenchmarkConverter.TypeToBenchmarks( typeof(MatMulBench), config)
            });

        }
    }
}
