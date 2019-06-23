
namespace Microsoft.Sdc.OrchestrationProfiler.Core
{
	using System;
	using System.Collections;

	/// <summary>
	/// 
	/// </summary>
	public class AssemblyOrchestrationPairCollection : CollectionBase
	{
		/// <summary>
		/// 
		/// </summary>
		public AssemblyOrchestrationPairCollection()
		{
		}

		/// <summary>
		/// Indexer
		/// </summary>
		public AssemblyOrchestrationPair this[int index]
		{
			get { return (AssemblyOrchestrationPair) this.List[index]; }
			set { this.List[index] = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		public void Add(AssemblyOrchestrationPair obj) 
		{
			this.List.Add(obj);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="obj"></param>
		public void Insert(int index, AssemblyOrchestrationPair obj) 
		{
			this.List.Insert(index, obj);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		public void Remove(AssemblyOrchestrationPair obj) 
		{
			this.List.Remove(obj); 
		}

		/// <summary>
		/// Copies the collection into an array of objects
		/// </summary>
		/// <param name="objects"></param>
		/// <param name="index"></param>
		public void CopyTo(AssemblyOrchestrationPair[] objects, int index) 
		{
			((ICollection)this).CopyTo(objects, index);
		}

		/// <summary>
		/// IndexOf
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int IndexOf(AssemblyOrchestrationPair obj) 
		{
			return this.List.IndexOf(obj);
		}

		/// <summary>
		/// Checks to see if the specified object exsits in the collection
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public bool Contains(AssemblyOrchestrationPair obj) 
		{
			return this.List.Contains(obj);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="comparer"></param>
		public void Sort(IComparer comparer)
		{
			this.InnerList.Sort(comparer);
		}		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="asmName"></param>
		/// <param name="orchName"></param>
		/// <returns></returns>
		public bool ContainsOrchestration(string asmName, string orchName) 
		{
			foreach (AssemblyOrchestrationPair pair in this.InnerList)
			{
				if (string.Compare(orchName, pair.OrchestrationName, true) == 0 && 
					string.Compare(asmName, pair.AssemblyName, true) == 0)
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="asmName"></param>
		/// <param name="orchName"></param>
		/// <returns></returns>
		public bool ContainsAssembly(string asmName) 
		{
			foreach (AssemblyOrchestrationPair pair in this.InnerList)
			{
				if (string.Compare(asmName, pair.AssemblyName, true) == 0)
				{
					return true;
				}
			}

			return false;
		}
	}
}
