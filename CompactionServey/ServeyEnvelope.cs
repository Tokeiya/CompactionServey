using System;
using System.Runtime.CompilerServices;

namespace CompactionServey
{
    internal class ServeyEnvelope
    {
		public static int CurrentAge { get; private set; }


	    public static unsafe void Check(ServeyEnvelope target)
	    {
		    var current = ((UIntPtr) Unsafe.AsPointer(ref target)).ToUInt64();

		    if (current != target.InitialAddress)
		    {
			    Console.WriteLine("Detect moving!");
			    Console.WriteLine($"AgeOffset:{CurrentAge-target.Age}");
			    Console.WriteLine(GcCounter.Create().GetOffset(target.Counter));
			    Console.WriteLine($"InitialAddress:{target.InitialAddress:X}");
			    Console.WriteLine($"CurrentAddress:{current:X}");

			    Console.WriteLine("press enter to continue.");
			    Console.ReadLine();
		    }

		}


	    public unsafe ServeyEnvelope()
	    {
		    Age = ++CurrentAge;
		    Counter = GcCounter.Create();

		    fixed (int* ptr = &FixedPoint)
		    {
			    InitialAddress = ((UIntPtr) ptr).ToUInt64();
		    }
	    }

	    public int FixedPoint;

		public long Age { get; }
	    public GcCounter Counter { get; }
		public ulong InitialAddress { get; }

	    public unsafe bool Check()
	    {
		    ulong current;
		    fixed (int* ptr = &FixedPoint)
		    {
			    current = ((UIntPtr) ptr).ToUInt64();
		    }

		    if (current != InitialAddress)
		    {
			    Console.WriteLine("Detect moving!");
			    Console.WriteLine($"InitialAddr:0x{InitialAddress:x16}");
			    Console.WriteLine($"CurrentAddr:0x{current:x16}");
			    Console.WriteLine(GcCounter.Create().GetOffset(Counter));
			    Console.WriteLine($"AgeOffset:{CurrentAge-Age}");

				//Console.WriteLine("Press enter to continue.");
			 //   Console.ReadLine();

			    return false;
		    }
		    return true;
	    }

    }
}
