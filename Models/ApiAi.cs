using System.Collections.Generic;

namespace TuvaletBekcisi
{
    public class ApiAi
    {
        public string id { get; set; }
        public string timestamp { get; set; }
        public string lang { get; set; }
        public Result result { get; set; }
        public Status status { get; set; }
        public string sessionId { get; set; }
    }


    public class Parameters
    {
    }

    public class Metadata
    {
        public string intentId { get; set; }
        public string webhookUsed { get; set; }
        public string webhookForSlotFillingUsed { get; set; }
        public string intentName { get; set; }
    }

    public class Message
    {
        public int type { get; set; }
        public string speech { get; set; }
    }

    public class Fulfillment
    {
        public string speech { get; set; }
        public List<Message> messages { get; set; }
    }

    public class Result
    {
        public string source { get; set; }
        public string resolvedQuery { get; set; }
        public string action { get; set; }
        public bool actionIncomplete { get; set; }
        public Parameters parameters { get; set; }
        public List<dynamic> contexts { get; set; }
        public Metadata metadata { get; set; }
        public Fulfillment fulfillment { get; set; }
        public int score { get; set; }
    }

    public class Status
    {
        public int code { get; set; }
        public string errorType { get; set; }
    }





}