using System;
using UnityEngine;

namespace EscapeTheHackerNetwork
{
    [Serializable]
    public class EmailData
    {
        public string emailId;
        public string senderAddress;
        public string senderDisplayName;
        public string subject;
        [TextArea(3, 10)]
        public string bodyText;
        public string previewLink;
        public string attachmentName;
        public bool hasAttachment;
        public bool isPhishing;
        [TextArea(2, 5)]
        public string educationalExplanation;
    }
}
