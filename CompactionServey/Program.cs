using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CompactionServey
{
    class Program
    {
	    private const int Size = 1000000;

		static void Main(string[] args)
		{
			RemoveTest();
			Console.WriteLine();

			FixAndRemove();
		}

	    private static void BasicServey()
	    {
		    var array = new ServeyEnvelope[Size];

		    for (int i = 0; i < Size; i++)
		    {
			    array[i] = new ServeyEnvelope();
		    }

		    while (true)
		    {
			    Console.WriteLine(ServeyEnvelope.CurrentAge);
			    Console.WriteLine(GcCounter.Create());
			    Console.WriteLine();
			    for (var i = 0; i < Size; i++)
			    {
				    array[i].Check();
				    array[i] = new ServeyEnvelope();
			    }
		    }
	    }

	    static unsafe void UseAsPointer()
	    {
		    var array = new ServeyEnvelope[Size];
			array[0]=new ServeyEnvelope();
		    var addr = new ulong[3];
			var cache=new ServeyEnvelope[3];

		    for (int _ = 0; _ < 3; _++)
		    {
			    for (int i = 1; i < Size; i++)
			    {
				    array[i] = new ServeyEnvelope();
			    }

			    addr[_] = ((UIntPtr) Unsafe.AsPointer(ref array[1].FixedPoint)).ToUInt64();

		    }

		    //var current = ((UIntPtr) Unsafe.AsPointer(ref array[0].FixedPoint)).ToUInt64();

			Console.WriteLine(array[0].Check());
		    Console.WriteLine(array[1].Check());
			
	    }

	    static unsafe void RemoveTest()
	    {
		    var target=new ServeyEnvelope();
		    var gch = GCHandle.Alloc(target, GCHandleType.Weak);

		    void* ptr = Unsafe.AsPointer(ref target.FixedPoint);

		    target = null;

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

		    Console.WriteLine(gch.Target == null);
		    Console.WriteLine(*(int*)ptr);
	    }

	    static unsafe void FixAndRemove()
	    {
		    var target=new ServeyEnvelope();
		    var gch = GCHandle.Alloc(target, GCHandleType.Weak);

		    fixed (int* ptr = &target.FixedPoint)
		    {
			    target = null;
				
				GC.Collect();
				GC.WaitForPendingFinalizers();
				GC.Collect();

			    Console.WriteLine(gch.Target == null);
			    Console.WriteLine(*ptr);
			}

	    }
    }
}