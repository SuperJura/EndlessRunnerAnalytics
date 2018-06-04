using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashboard.Services
{
	public static class CollectionsUtil
	{
		public static List<T> TakeLastN<T>(this List<T> list, int count)
		{
			return list.Skip(Math.Max(0, list.Count - count)).ToList();
		}
	}
}