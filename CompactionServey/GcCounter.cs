using System;

namespace CompactionServey
{
	public struct GcCounter
	{
		public static GcCounter Create(bool collect = false)
		{
			if (!collect) return new GcCounter(GC.CollectionCount(0), GC.CollectionCount(1), GC.CollectionCount(2));

			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

			return new GcCounter(GC.CollectionCount(0), GC.CollectionCount(1), GC.CollectionCount(2));
		}

		private GcCounter(int gen0, int gen1, int gen2)
		{
			Generation0 = gen0;
			Generation1 = gen1;
			Generation2 = gen2;
		}

		public int Generation0 { get; }
		public int Generation1 { get; }
		public int Generation2 { get; }

		public int Total => Generation0 + Generation1 + Generation2;

		public override string ToString()
		{
			return $"Gen0:{Generation0} Gen1:{Generation1} Gen2:{Generation2} Total:{Total}";
		}

		public GcCounter GetOffset(GcCounter counter)
		{
			return new GcCounter(Math.Abs(Generation0 - counter.Generation0),
				Math.Abs(Generation1 - counter.Generation1), Math.Abs(Generation2 - counter.Generation2));
		}
	}
}