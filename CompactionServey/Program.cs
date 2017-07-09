using System;
using System.Runtime.CompilerServices;

namespace CompactionServey
{
    class Program
    {
        static void Main(string[] args)
        {
	        const int size = 1000000;

	        var array = new ServeyEnvelope[size];

	        for (int i = 0; i < size; i++)
	        {
		        array[i]=new ServeyEnvelope();
	        }

	        while (true)
	        {
		        if (ServeyEnvelope.CurrentAge % size == 0)
		        {
			        Console.WriteLine(ServeyEnvelope.CurrentAge);
			        Console.WriteLine(GcCounter.Create());
			        Console.WriteLine();
		        }
		        for (var i = 0; i < size; i++)
		        {
			        array[i].Check();
			        array[i] = new ServeyEnvelope();
		        }
	        }

        }
    }
}