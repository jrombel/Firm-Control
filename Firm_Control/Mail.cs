using System;
using System.Xml.Serialization;

namespace Firm_Control
{
    [Serializable]
    [XmlType("Mail")]
    [XmlInclude(typeof(Mail))]
    public class Mail
    {
        [XmlElement("Fields")]
        public string to { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
        public string content { get; set; }
        public bool seen { get; set; }

        public Mail()
        {
        }

        public Mail(string to, string from, string subject, string content)
        {
            this.to = to;
            this.from = from;
            this.subject = subject;
            this.content = content;
        }

        public void Send(Person recipient)
        {
            recipient.messages.Add(this);
        }
    }
}