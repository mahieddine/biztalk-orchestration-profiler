
namespace Microsoft.Sdc.OrchestrationProfiler.Core
{
	using System;
	using System.IO;
	using Microsoft.Win32;

	/// <summary>
	/// ReportingConfiguration.
	/// </summary>
	public class ReportingConfiguration
	{
		public string MgmtDatabaseName = string.Empty;
		public string MgmtServerName = string.Empty;
		public string DtaDatabaseName = string.Empty;
		public string DtaServerName = string.Empty;
		public string OutputDir = string.Empty;
		public DateTime DateFrom = DateTime.Now;
		public DateTime DateTo = DateTime.Now;
        public string[] InstancesIds = new string[0];
		public ExecutionMode ExecutionMode = ExecutionMode.CommandLine;
		public string ReportTitle = string.Empty;
		public bool ShowOutputOnCompletion = true;
        public string OrcsList = string.Empty;

		/// <summary>
		/// 
		/// </summary>
		public ReportingConfiguration()
		{
			this.SetDefaults();
		}

		public string DateString
		{
			get
			{
				return string.Format("Profile Range: {0} - {1}", 
					this.DateFrom.ToString("dd-MMM-yy HH:mm:ss"), 
					this.DateTo.ToString("dd-MMM-yy HH:mm:ss"));
			}
		}

		#region SetDefaults

		/// <summary>
		/// 
		/// </summary>
		private void SetDefaults()
		{
			RegistryKey bizTalkKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\BizTalk Server\3.0");
			RegistryKey bizTalkAdminKey = bizTalkKey.OpenSubKey(@"Administration");
            
			this.MgmtDatabaseName = (string)bizTalkAdminKey.GetValue("MgmtDBName", "BizTalkMgmtDb");
			this.MgmtServerName = (string)bizTalkAdminKey.GetValue("MgmtDBServer", Environment.MachineName);

			this.DtaServerName = (string)bizTalkAdminKey.GetValue("MgmtDBServer", Environment.MachineName);
			this.DtaDatabaseName = "BizTalkDtaDb";

			bizTalkKey.Close();
			bizTalkAdminKey.Close();

			this.DateFrom = DateTime.Now.Subtract(new TimeSpan(7, 0, 0, 0));
			this.DateTo = DateTime.Now;

			this.ReportTitle = "BizTalk Orchestration Profile Report";
			this.OutputDir = Path.GetTempPath();
		}

		#endregion
	}
}
