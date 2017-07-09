using System;
using System.Runtime.CompilerServices;

namespace CompactionServey
{
    internal class ServeyEnvelope
    {
		public static int CurrentAge { get; private set; }

	    public static unsafe ServeyEnvelope Create()
	    {
		    var ret=new ServeyEnvelope();
		    ret.InitialAddress = ((UIntPtr) Unsafe.AsPointer(ref ret)).ToUInt64();

			return ret;
	    }

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


	    public ServeyEnvelope()
	    {
		    Age = ++CurrentAge;
		    Counter = GcCounter.Create();
	    }

		public long Age { get; }
	    public GcCounter Counter { get; }
		public ulong InitialAddress { get; private set; }


    }
}
