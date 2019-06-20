
using Microsoft.Services.Tools.BizTalkOM.Diagnostics;

namespace Microsoft.Services.Tools.BizTalkOM
{
    using System;
    using System.Xml.Serialization;
    using BizTalkCore = Microsoft.BizTalk.ExplorerOM;

    /// <summary>
    /// SendPort.
    /// </summary>
    public class SendPort : BizTalkBaseObject
    {
        private bool dynamic;
        private bool twoWay;
        private TransportInfo primaryTransport;
        private TransportInfo secondaryTransport;
        private string sendPipeline;
        private string receivePipeline;
        private string filter;
        private int priority;
        private bool routeFailedMessage;
        private bool stopSendingOnFailure;
        private TrackingType trackingType;
        private BizTalkBaseObjectCollectionEx filterGroups;
        private NameIdPairCollection outboundMaps;
        private NameIdPairCollection inboundMaps;
        private EncryptionCert encryptionCert;
        private NameIdPairCollection parentGroups;
        private NameIdPairCollection boundOrchestrations;

        #region Constructors

        /// <summary>
        /// Creates a new <see cref="SendPort"/>
        /// </summary>
        public SendPort()
        {
            this.priority = 5;
            this.filterGroups = new BizTalkBaseObjectCollectionEx();
            this.outboundMaps = new NameIdPairCollection();
            this.inboundMaps = new NameIdPairCollection();
            this.parentGroups = new NameIdPairCollection();
            this.boundOrchestrations = new NameIdPairCollection();
        }

        /// <summary>
        /// Creates a new <see cref="SendPort"/>
        /// </summary>
        /// <param name="name">Default name</param>
        public SendPort(string name)
            : this()
        {
            this.Name = name;
        }

        #endregion

        #region Public properties

        public int Priority
        {
            get { return this.priority; }
            set { this.priority = value; }
        }

        public string Filter
        {
            get { return this.filter; }
            set { this.filter = value; }
        }

        public TrackingType TrackingType
        {
            get { return this.trackingType; }
            set { this.trackingType = value; }
        }

        public TransportInfo PrimaryTransport
        {
            get { return this.primaryTransport; }
            set { this.primaryTransport = value; }
        }

        public TransportInfo SecondaryTransport
        {
            get { return this.secondaryTransport; }
            set { this.secondaryTransport = value; }
        }

        public string ReceivePipeline
        {
            get { return this.receivePipeline; }
            set { this.receivePipeline = value; }
        }

        public string SendPipeline
        {
            get { return this.sendPipeline; }
            set { this.sendPipeline = value; }
        }

        public bool Dynamic
        {
            get { return this.dynamic; }
            set { this.dynamic = value; }
        }

        public bool TwoWay
        {
            get { return this.twoWay; }
            set { this.twoWay = value; }
        }

        public bool RouteFailedMessage
        {
            get { return this.routeFailedMessage; }
            set { this.routeFailedMessage = value; }
        }

        public bool StopSendingOnFailure
        {
            get { return this.stopSendingOnFailure; }
            set { this.stopSendingOnFailure = value; }
        }

        public EncryptionCert EncryptionCert
        {
            get { return this.encryptionCert; }
            set { this.encryptionCert = value; }
        }

        [XmlArrayItem("FilterGroup", typeof(FilterGroup))]
        public BizTalkBaseObjectCollectionEx FilterGroups
        {
            get { return this.filterGroups; }
            set { this.filterGroups = value; }
        }

        [XmlArrayItem("Transform", typeof(NameIdPair))]
        public NameIdPairCollection OutboundMaps
        {
            get { return this.outboundMaps; }
            set { this.outboundMaps = value; }
        }

        [XmlArrayItem("Transform", typeof(NameIdPair))]
        public NameIdPairCollection InboundMaps
        {
            get { return this.inboundMaps; }
            set { this.inboundMaps = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlArrayItem("Orchestration", typeof(NameIdPair))]
        public NameIdPairCollection BoundOrchestrations
        {
            get { return this.boundOrchestrations; }
            set { this.boundOrchestrations = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlArrayItem("SendPortGroup", typeof(NameIdPair))]
        public NameIdPairCollection SendPortGroups
        {
            get { return this.parentGroups; }
        }

        #endregion

        #region Load

        /// <summary>
        /// 
        /// </summary>
        /// <param name="explorer"></param>
        /// <param name="port"></param>
        internal void Load(BizTalkCore.BtsCatalogExplorer explorer, BizTalkCore.SendPort port)
        {
            if (port != null)
            {
                this.QualifiedName = String.Empty;
                // Basic properties
                this.trackingType = (TrackingType)Enum.Parse(typeof(TrackingType), ((int)port.Tracking).ToString());
                this.priority = port.Priority;
                this.twoWay = port.IsTwoWay;
                this.dynamic = port.IsDynamic;
                this.sendPipeline = port.SendPipeline.FullName;
                this.ApplicationName = port.Application.Name;
                this.RouteFailedMessage = port.RouteFailedMessage;
                this.StopSendingOnFailure = port.StopSendingOnFailure;
                this.CustomDescription = port.Description;

                // Encryption Certificates
                if (port.EncryptionCert != null)
                {
                    this.encryptionCert = new EncryptionCert(port.EncryptionCert);
                }

                // Receive pipeline if two way
                if (this.twoWay)
                {
                    this.receivePipeline = port.ReceivePipeline.FullName;
                }

                // Primary transport
                if (port.PrimaryTransport != null)
                {
                    this.primaryTransport = new TransportInfo(port.PrimaryTransport, true);
                }

                // Secondary transport
                if (port.SecondaryTransport != null && port.SecondaryTransport.Address.Length > 0)
                {
                    this.secondaryTransport = new TransportInfo(port.SecondaryTransport, false);
                }

                // Filters
                if (port.Filter != string.Empty)
                {
                    this.FilterGroups = BizTalkInstallation.CreateFilterGroups(port.Filter);
                }
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        internal override void FixReferences(BizTalkCore.BtsCatalogExplorer explorer)
        {
            TraceManager.SmartTrace.TraceIn(explorer);

            BizTalkCore.SendPort port = explorer.SendPorts[this.Name];

            if (port != null)
            {
                // Outbound Transforms
                if (port.OutboundTransforms != null)
                {
                    foreach (BizTalkCore.Transform transform in port.OutboundTransforms)
                    {
                        Transform t = this.Application.Maps[transform.FullName] as Transform;

                        if (t != null)
                        {
                            t.SendPorts.Add(this.NameIdPair);
                            this.outboundMaps.Add(t.NameIdPair);
                        }
                    }
                }

                // Inbound Transforms
                if (port.InboundTransforms != null)
                {
                    foreach (BizTalkCore.Transform transform in port.InboundTransforms)
                    {
                        Transform t = this.Application.Maps[transform.FullName] as Transform;

                        if (t != null)
                        {
                            t.SendPorts.Add(this.NameIdPair);
                            this.inboundMaps.Add(t.NameIdPair);
                        }
                    }
                }

                // Primary Transport
                if (this.primaryTransport != null)
                {
                    Protocol p = this.Application.ParentInstallation.ProtocolTypes[this.primaryTransport.Type] as Protocol;

                    if (p != null)
                    {
                        p.SendPorts.Add(this.NameIdPair);

                        foreach (NameIdPair handler in p.SendHandlers)
                        {
                            Host h = this.Application.ParentInstallation.Hosts[handler.Name] as Host;

                            if (h != null)
                            {
                                h.HostedSendPorts.Add(this.NameIdPair);
                            }
                        }
                    }
                }

                TraceManager.SmartTrace.TraceOut();
                return;
            }
        }
    }
}
