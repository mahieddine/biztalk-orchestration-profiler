
namespace Microsoft.Services.Tools.BizTalkOM
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Xml.Serialization;

    //internal delegate void ObjectAddedEvent(BizTalkBaseObject obj);

    public class BizTalkBaseObjectCollection : CollectionBase
    {
        private Hashtable indexByName;

        /// <summary>
        /// Creates a new <see cref="BizTalkBaseObjectCollection"/>æ
        /// </summary>
        public BizTalkBaseObjectCollection()
        {
            this.indexByName = new Hashtable();
        }

        internal event ObjectAddedEvent OnObjectAdded;

        /// <summary>
        /// Indexer
        /// </summary>
        public BizTalkBaseObject this[int index]
        {
            get { return (BizTalkBaseObject)this.InnerList[index]; }
            set
            {
                this.InnerList[index] = value;
                if (value is BizTalkBaseObject && !this.indexByName.ContainsKey(value.Name))
                {
                    this.indexByName.Add(value.Name, value);
                }
            }
        }

        [XmlIgnore]
        public BizTalkBaseObject this[string index]
        {
            get { return this.indexByName[index] as BizTalkBaseObject; }
        }

        /// <summary>
        /// Adds a new object to the list
        /// </summary>
        /// <param name="obj"></param>
        public void Add(BizTalkBaseObject obj)
        {
            if (obj != null && !this.InnerList.Contains(obj))
            {
                this.InnerList.Add(obj);
                if (obj is BizTalkBaseObject)
                {
                    if (!string.IsNullOrEmpty(obj.Name))
                    {
                        this.indexByName.Add(obj.Name, obj);
                    }
                }

                if (this.OnObjectAdded != null)
                {
                    this.OnObjectAdded(obj);
                }
            }
        }

        /// <summary>
        /// Sorts the collection
        /// </summary>
        /// <param name="comparer"></param>
        public void Sort(IComparer comparer)
        {
            this.InnerList.Sort(comparer);
        }
    }
}
