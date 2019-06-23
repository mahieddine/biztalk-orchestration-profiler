using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Microsoft.Services.Tools.BizTalkOM;
using Microsoft.BizTalk.ExplorerOM;

namespace Microsoft.Sdc.OrchestrationProfiler.Core
{
    public delegate void UpdatePercentageComplete(int percentage);

    /// <summary>
    /// Summary description for ReportGenerator.
    /// </summary>
    public class ReportGenerator
    {
        public event UpdatePercentageComplete PercentageDocumentationComplete;

        private string targetDir = Path.GetTempPath();
        private string resourceFolder = string.Empty;
        private HelpFileWriter hfw;
        string btsOutputDir = Path.GetTempPath();
        string FILE_NAME = "BTS2K4Coverage.chm";
        private const string resourcePrefix = "Microsoft.Services.Tools.BizTalkOrchestrationProfiler.Core";

        /// <summary>
        /// Constructor
        /// </summary>
        public ReportGenerator()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coll"></param>
        public void GenerateDocumentation(ReportingConfiguration reportingConfiguration, AssemblyOrchestrationPairCollection coll)
        {
            try
            {
                if (coll != null && coll.Count > 0)
                {
                    int progressStep = 90 / coll.Count;
                    int progress = 0;

                    FILE_NAME = Path.Combine(reportingConfiguration.OutputDir, reportingConfiguration.ReportTitle + ".chm");
                    this.PrepareFilesAndDirectories(reportingConfiguration);

                    string tmpDir1 = Path.Combine(targetDir, "Images");
                    string tmpDir2 = Path.Combine(targetDir, "Orchestration");

                    #region Load the required XSL stylesheets ready

                    //============================================
                    // Load the required XSL stylesheets ready
                    // for processing
                    //============================================
                    Assembly a = Assembly.GetExecutingAssembly();

                    XmlTextReader xr = new XmlTextReader(a.GetManifestResourceStream(resourcePrefix + ".Res.OrchestrationImage.xslt"));
                    XslTransform orchTransform = new XslTransform();
                    orchTransform.Load(xr, new XmlUrlResolver(), a.Evidence);
                    xr.Close();

                    xr = new XmlTextReader(a.GetManifestResourceStream(resourcePrefix + ".Res.Overview.xslt"));
                    XslTransform ovTransform = new XslTransform();
                    ovTransform.Load(xr, new XmlUrlResolver(), a.Evidence);
                    xr.Close();

                    xr = new XmlTextReader(a.GetManifestResourceStream(resourcePrefix + ".Res.ShapeMetrics.xslt"));
                    XslTransform shapeTransform = new XslTransform();
                    shapeTransform.Load(xr, new XmlUrlResolver(), a.Evidence);

                    xr = new XmlTextReader(a.GetManifestResourceStream(resourcePrefix + ".Res.Longest20.xslt"));
                    XslTransform longest20Transform = new XslTransform();
                    longest20Transform.Load(xr, new XmlUrlResolver(), a.Evidence);

                    xr = new XmlTextReader(a.GetManifestResourceStream(resourcePrefix + ".Res.LeastSuccess20.xslt"));
                    XslTransform leastSuccess20Transform = new XslTransform();
                    leastSuccess20Transform.Load(xr, new XmlUrlResolver(), a.Evidence);

                    xr = new XmlTextReader(a.GetManifestResourceStream(resourcePrefix + ".Res.OrchErrors.xslt"));
                    XslTransform errorsTransform = new XslTransform();
                    errorsTransform.Load(xr, new XmlUrlResolver(), a.Evidence);

                    xr.Close();

                    #endregion

                    HelpFileNode hfn20 = hfw.RootNode.CreateChild("Under 20% Coverage");
                    HelpFileNode hfn40 = hfw.RootNode.CreateChild("20% - 40% Coverage");
                    HelpFileNode hfn60 = hfw.RootNode.CreateChild("40% - 60% Coverage");
                    HelpFileNode hfn80 = hfw.RootNode.CreateChild("60% - 80% Coverage");
                    HelpFileNode hfn100 = hfw.RootNode.CreateChild("80% - 100% Coverage");

                    int countUnder20 = 0;
                    int count20to40 = 0;
                    int count40to60 = 0;
                    int count60to80 = 0;
                    int count80to100 = 0;

                    BizTalkBaseObjectCollection orchestrations = this.RetrieveDeployedOrchestrations(coll, reportingConfiguration);

                    #region Write to Overview Doc

                    MemoryStream ms = new MemoryStream();
                    XmlTextWriter xw = new XmlTextWriter(ms, Encoding.ASCII);
                    xw.Formatting = Formatting.Indented;

                    xw.WriteStartDocument();
                    xw.WriteStartElement("CoverageReport");
                    xw.WriteStartElement("Orchestrations");

                    #endregion

                    #region Loop the orchs

                    foreach (Orchestration orchestration in orchestrations)
                    {
                        string mainImgFile = Path.Combine(tmpDir2, orchestration.Id + ".jpg");
                        string mainHtmFile = Path.Combine(tmpDir2, orchestration.Id + ".html");
                        string shapeDetailFile = Path.Combine(tmpDir2, orchestration.Id + "ShapeDetail.html");
                        string shapeDetailLongest20File = Path.Combine(tmpDir2, orchestration.Id + "ShapeDetailL20.html");
                        string shapeDetailLeastSuccess20File = Path.Combine(tmpDir2, orchestration.Id + "ShapeDetailLS20.html");
                        string errorsFile = Path.Combine(tmpDir2, orchestration.Id + "Errors.html");

                        string pieOverall = "";
                        string pieSuccess = "";
                        Color red = Color.FromArgb(50, 255, 0, 0);
                        Color green = Color.FromArgb(50, 0, 255, 0);
                        Color blue = Color.FromArgb(50, 0, 0, 255);

                        OrchestrationCoverageInfo info = orchestration.GetCoverageInfo(
                            reportingConfiguration.DtaServerName,
                            reportingConfiguration.DtaDatabaseName,
                            reportingConfiguration.DateFrom,
                            reportingConfiguration.DateTo,
                            reportingConfiguration.InstancesIds);

                        OrchMetrics metrics = orchestration.GetMetrics(
                            reportingConfiguration.DtaServerName,
                            reportingConfiguration.DtaDatabaseName,
                            reportingConfiguration.DateFrom,
                            reportingConfiguration.DateTo,
                             reportingConfiguration.InstancesIds);

                        orchestration.PopulateErrorInfo(
                            reportingConfiguration.DtaServerName,
                            reportingConfiguration.DtaDatabaseName,
                            reportingConfiguration.DateFrom,
                            reportingConfiguration.DateTo,
                             reportingConfiguration.InstancesIds);

                        OrchViewer.SaveOrchestrationToJpg(orchestration, mainImgFile, true);

                        //================================================
                        // 
                        //================================================
                        PieChart pc = new PieChart();
                        int percentComplete = info.totalCoverageCompletePercentage;
                        int percentFailed = info.totalCoverageFailedPercentage;

                        pc.Segments.Add("Failed", percentFailed, red);
                        pc.Segments.Add("Completed", percentComplete, green);

                        pieOverall = Path.Combine(tmpDir1, orchestration.Id + "PieOverall.jpg");
                        pc.SaveToFile(pieOverall);

                        //================================================
                        // 
                        //================================================
                        PieChart pc2 = new PieChart();
                        int percentOk = info.successRatePercentage;
                        int percentEx = info.failureRatePercentage;

                        Color color1 = green;
                        Color color2 = blue;

                        if (percentComplete == 0)
                        {
                            percentOk = 0;
                            percentEx = 100;
                            color1 = red;
                            color2 = red;
                        }

                        pc2.Segments.Add("OK", percentOk, color1);
                        pc2.Segments.Add("Exceptioned", percentEx, color2);

                        pieSuccess = Path.Combine(tmpDir1, orchestration.Id + "PieSuccess.jpg");
                        pc2.SaveToFile(pieSuccess);

                        XmlDocument shapeDetailDoc = orchestration.GetShapeMetricsAsDom();
                        bool writeFurtherInfo = false;

                        if (shapeDetailDoc != null)
                        {
                            writeFurtherInfo = true;
                        }

                        XsltArgumentList orchXsltArgs = new XsltArgumentList();
                        orchXsltArgs.AddParam("ImgFile", "", mainImgFile);
                        orchXsltArgs.AddParam("OrchId", "", orchestration.Id);
                        orchXsltArgs.AddParam("OrchName", "", orchestration.Name);
                        orchXsltArgs.AddParam("AsmName", "", orchestration.ParentAssemblyFormattedName);
                        orchXsltArgs.AddParam("ImgFileOverall", "", pieOverall);
                        orchXsltArgs.AddParam("ImgFileSuccess", "", pieSuccess);
                        orchXsltArgs.AddParam("PcOverall", "", percentComplete);
                        orchXsltArgs.AddParam("PcSuccess", "", percentOk);

                        orchXsltArgs.AddParam("WriteFurtherInfo", "", writeFurtherInfo ? "yes" : "");
                        orchXsltArgs.AddParam("AvgDurationMillis", "", metrics.avgDurationMillis);
                        orchXsltArgs.AddParam("MaxDurationMillis", "", metrics.maxDurationMillis);
                        orchXsltArgs.AddParam("MinDurationMillis", "", metrics.minDurationMillis);
                        orchXsltArgs.AddParam("NumCompleted", "", metrics.numCompleted);
                        orchXsltArgs.AddParam("NumStarted", "", metrics.numStarted);
                        orchXsltArgs.AddParam("NumTerminated", "", metrics.numTerminated);
                        orchXsltArgs.AddParam("ErrorCount", "", orchestration.OrchestrationErrors.Count);

                        if (writeFurtherInfo)
                        {
                            this.WriteTransformedXmlDataToFile(
                                shapeDetailFile,
                                shapeDetailDoc.OuterXml,
                                shapeTransform,
                                orchXsltArgs);

                            this.WriteTransformedXmlDataToFile(
                                shapeDetailLongest20File,
                                shapeDetailDoc.OuterXml,
                                longest20Transform,
                                orchXsltArgs);

                            this.WriteTransformedXmlDataToFile(
                                shapeDetailLeastSuccess20File,
                                shapeDetailDoc.OuterXml,
                                leastSuccess20Transform,
                                orchXsltArgs);
                        }

                        if (orchestration.OrchestrationErrors.Count > 0)
                        {
                            this.WriteTransformedXmlDataToFile(
                                errorsFile,
                                orchestration.GetXml(),
                                errorsTransform,
                                orchXsltArgs);
                        }

                        this.WriteTransformedXmlDataToFile(
                            mainHtmFile,
                            orchestration.GetXml(),
                            orchTransform,
                            orchXsltArgs);

                        #region Write Help Nodes

                        if (percentComplete < 20)
                        {
                            hfn20.CreateChild(orchestration.Name, mainHtmFile);
                            countUnder20++;
                        }
                        else if (percentComplete >= 20 && percentComplete < 40)
                        {
                            hfn40.CreateChild(orchestration.Name, mainHtmFile);
                            count20to40++;
                        }
                        else if (percentComplete >= 40 && percentComplete < 60)
                        {
                            hfn60.CreateChild(orchestration.Name, mainHtmFile);
                            count40to60++;
                        }
                        else if (percentComplete >= 60 && percentComplete < 80)
                        {
                            hfn80.CreateChild(orchestration.Name, mainHtmFile);
                            count60to80++;
                        }
                        else if (percentComplete >= 80)
                        {
                            hfn100.CreateChild(orchestration.Name, mainHtmFile);
                            count80to100++;
                        }

                        #endregion

                        xw.WriteStartElement("Orchestration");
                        xw.WriteElementString("Id", orchestration.Id);
                        xw.WriteElementString("Name", orchestration.Name);
                        xw.WriteElementString("OverallCoverage", percentComplete.ToString());
                        xw.WriteEndElement();

                        progress += progressStep;
                        PercentageDocumentationComplete(progress);
                    }

                    #endregion

                    #region Tidy nodes

                    hfn20.SortChildren();
                    hfn40.SortChildren();
                    hfn60.SortChildren();
                    hfn80.SortChildren();
                    hfn100.SortChildren();

                    if (hfn20.ChildNodes.Count == 0)
                    {
                        hfw.RootNode.ChildNodes.Remove(hfn20);
                    }
                    if (hfn40.ChildNodes.Count == 0)
                    {
                        hfw.RootNode.ChildNodes.Remove(hfn40);
                    }
                    if (hfn60.ChildNodes.Count == 0)
                    {
                        hfw.RootNode.ChildNodes.Remove(hfn60);
                    }
                    if (hfn80.ChildNodes.Count == 0)
                    {
                        hfw.RootNode.ChildNodes.Remove(hfn80);
                    }
                    if (hfn100.ChildNodes.Count == 0)
                    {
                        hfw.RootNode.ChildNodes.Remove(hfn100);
                    }

                    #endregion

                    #region Write to Overview Doc

                    xw.WriteEndElement(); //Orchestrations
                    xw.WriteStartElement("Summary");

                    xw.WriteElementString("numOrchestrations", coll.Count.ToString());
                    xw.WriteElementString("countUnder20", countUnder20.ToString());
                    xw.WriteElementString("count20to40", count20to40.ToString());
                    xw.WriteElementString("count40to60", count40to60.ToString());
                    xw.WriteElementString("count60to80", count60to80.ToString());
                    xw.WriteElementString("count80to100", count80to100.ToString());

                    xw.WriteEndElement(); //Summary
                    xw.WriteEndElement(); //CoverageReport
                    xw.WriteEndDocument();
                    xw.Flush();

                    ms.Position = 0;

#if (DEBUG)
                    string strTmp = Encoding.ASCII.GetString(ms.GetBuffer());
#endif

                    XmlDocument doc = new XmlDocument();
                    doc.Load(ms);

                    this.WriteTransformedXmlDataToFile(
                        Path.Combine(targetDir, "overview.html"),
                        doc.OuterXml,
                        ovTransform,
                        null);

                    #endregion

                    hfw.RootNode.CreateChild("Report Guide", "Guide.html");
                }

                if (hfw != null)
                {
                    hfw.Compile();
                }

                PercentageDocumentationComplete(100);

                if (reportingConfiguration.ShowOutputOnCompletion)
                {
                    this.ShowOutput();
                }
            }
            finally
            {
                this.Cleanup();
            }

            return;
        }

        #region ShowOutput

        /// <summary>
        /// 
        /// </summary>
        public void ShowOutput()
        {
            if (hfw != null)
            {
                hfw.DisplayHelpFile();
            }
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Cleanup()
        {
            if (Directory.Exists(targetDir))
            {
                try
                {
#if (!DEBUG)
                    Directory.Delete(targetDir, true);
#endif
                }
                catch
                {
                }
            }

            return true;
        }

        #endregion

        #region RetrieveDeployedOrcestrations

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coll"></param>
        /// <returns></returns>
        private BizTalkBaseObjectCollection RetrieveDeployedOrchestrations(AssemblyOrchestrationPairCollection coll, ReportingConfiguration reportingConfiguration)
        {
            // darrenj
            BizTalkInstallation bi = new BizTalkInstallation();

            // Ilya - 08/2007: get the Mgmt DB server / name from the Reporting Configuration
            bi.MgmtDatabaseName = reportingConfiguration.MgmtDatabaseName;
            bi.Server = reportingConfiguration.MgmtServerName;

            BizTalkBaseObjectCollection orchestrations = new BizTalkBaseObjectCollection();

            string ConnectionStringFormat = "Server={0};Database={1};Integrated Security=SSPI";
            BtsCatalogExplorer explorer = new BtsCatalogExplorer();
            explorer.ConnectionString = string.Format(ConnectionStringFormat, reportingConfiguration.MgmtServerName, reportingConfiguration.MgmtDatabaseName);

            foreach (BtsAssembly btsAssembly in explorer.Assemblies)
            {
                string asmName = btsAssembly.DisplayName;

                if (!btsAssembly.IsSystem && coll.ContainsAssembly(asmName))
                {
                    foreach (BtsOrchestration btsOrchestration in btsAssembly.Orchestrations)
                    {
                        if (coll.ContainsOrchestration(asmName, btsOrchestration.FullName))
                        {
                            Orchestration orchestration = bi.GetOrchestration(btsAssembly.DisplayName, btsOrchestration.FullName);

                            orchestration.ParentAssemblyFormattedName = btsAssembly.DisplayName;
                            orchestrations.Add(orchestration);
                        }
                    }
                }
            }

            return orchestrations;
        }

        #endregion

        #region PrepareFilesAndDirectories

        private bool PrepareFilesAndDirectories(ReportingConfiguration reportingConfiguration)
        {
            targetDir = Path.Combine(Path.GetTempPath(), "BTS2K4Coverage");

            try
            {
                hfw = new HelpFileWriter();

                hfw.RootNode.CreateChild("Coverage Overview", "overview.html");

                //============================================
                // Delete the target directory if it
                // already exists
                //============================================
                if (Directory.Exists(targetDir))
                {
                    Directory.Delete(targetDir, true);
                }

                //============================================
                // Create the main and supporting directories
                //============================================
                Directory.CreateDirectory(targetDir);
                Directory.CreateDirectory(Path.Combine(targetDir, "Orchestration"));
                Directory.CreateDirectory(Path.Combine(targetDir, "Images"));

                //============================================
                // Read images and CSS files from resource file 
                // and write the to disk in temporary location
                //============================================
                Assembly a = Assembly.GetExecutingAssembly();

                StreamReader sr = new StreamReader(a.GetManifestResourceStream(resourcePrefix + ".Res.titlePage.htm"));
                string html = sr.ReadToEnd();
                sr.Close();

                sr = new StreamReader(a.GetManifestResourceStream(resourcePrefix + ".Res.legend.htm"));
                string legend = sr.ReadToEnd();
                sr.Close();

                sr = new StreamReader(a.GetManifestResourceStream(resourcePrefix + ".Res.styles.css"));
                string css = sr.ReadToEnd();
                sr.Close();

                Bitmap bmp = new Bitmap(a.GetManifestResourceStream(resourcePrefix + ".Res.logo.jpg"));
                bmp.Save(Path.Combine(targetDir, "title.jpg"));

                bmp = new Bitmap(a.GetManifestResourceStream(resourcePrefix + ".Res.Orchestration.jpg"));
                bmp.Save(Path.Combine(targetDir, "Orchestration.jpg"));

                bmp = new Bitmap(a.GetManifestResourceStream(resourcePrefix + ".Res.sample.jpg"));
                bmp.Save(Path.Combine(targetDir, "sample.jpg"));

                //============================================
                // Write out the CSS style sheet
                //============================================
                StreamWriter cssWriter = new StreamWriter(File.Create(Path.Combine(targetDir, "CommentReport.css")));
                cssWriter.Write(css);
                cssWriter.Close();

                //============================================
                // Write out the main title page
                //============================================
                StreamWriter titleWriter = new StreamWriter(File.Create(Path.Combine(targetDir, "CWP0.HTM")));
                html = html.Replace("#GENDATE#", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
                html = html.Replace("#DOCVERSION#", a.GetName().Version.ToString());
                html = html.Replace("#SERVER#", reportingConfiguration.MgmtServerName);
                html = html.Replace("#DATABASE#", reportingConfiguration.MgmtDatabaseName);
                html = html.Replace("#REPORTTITLE#", reportingConfiguration.ReportTitle);
                html = html.Replace("#DATESTRING#", reportingConfiguration.DateString);

                titleWriter.Write(html);
                titleWriter.Close();

                titleWriter = new StreamWriter(File.Create(Path.Combine(targetDir, "Guide.html")));
                titleWriter.Write(legend);
                titleWriter.Close();

                //============================================
                // Add the help file contents page meta data
                //============================================
                hfw.RootNode.Caption = reportingConfiguration.ReportTitle;
                hfw.Title = reportingConfiguration.ReportTitle;

                //============================================
                // Add the help file project meta data
                //============================================
                if (!Directory.Exists(btsOutputDir))
                {
                    Directory.CreateDirectory(btsOutputDir);
                }

                if (!Directory.Exists(Path.GetDirectoryName(FILE_NAME)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(FILE_NAME));
                }

                hfw.CompiledFileName = FILE_NAME;
                hfw.ContentsFileName = Path.Combine(targetDir, "BTS2K4Coverage.hhc");
                hfw.RootNode.Url = "CWP0.HTM";
                hfw.ProjectFileName = Path.Combine(targetDir, "BTS2K4Coverage.hhp");

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        private void WriteTransformedXmlDataToFile(string fileName, string xmlData, XslTransform transform, XsltArgumentList args)
        {
            StreamWriter sw = new StreamWriter(File.Create(fileName));
            this.WriteTransformedXmlDataToStream(sw.BaseStream, xmlData, transform, args);
            sw.Close();
        }

        private void WriteTransformedXmlDataToStream(Stream s, string xmlData, XslTransform transform, XsltArgumentList args)
        {
            XPathDocument data = new XPathDocument(new MemoryStream(Encoding.UTF8.GetBytes(xmlData)));
            transform.Transform(data, args, s, null);
            s.Flush();
        }
    }
}
