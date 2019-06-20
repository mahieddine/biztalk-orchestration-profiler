using System;

namespace Microsoft.Sdc.OrchestrationProfiler.Core
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class AssemblyOrchestrationPair
	{
		private string asmName = string.Empty;
		private string orchName = string.Empty;

		public AssemblyOrchestrationPair()
		{
		}

		public string AssemblyName
		{
			get { return this.asmName; }
			set { this.asmName = value; }
		}

		public string OrchestrationName
		{
			get { return this.orchName; }
			set { this.orchName = value; }
		}

		public override string ToString()
		{
			return string.Format("{0}-{1}", this.asmName, this.orchName);
		}
	}
}
