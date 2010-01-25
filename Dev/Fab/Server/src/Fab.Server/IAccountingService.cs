﻿using System.Runtime.Serialization;
using System.ServiceModel;
using Fab.Server.Core;

namespace Fab.Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAccountingService" in both code and config file together.
    [ServiceContract]
    public interface IAccountingService
    {
        [OperationContract]
        string GetJournalTypeName(int value);

        [OperationContract]
        JournalType GetJournalType(int value);

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        private bool boolValue = true;
        private string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}