using BenchmarkDotNet.Configs;

// BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
BenchmarkRunner.Run<BenchMarkValidation>();