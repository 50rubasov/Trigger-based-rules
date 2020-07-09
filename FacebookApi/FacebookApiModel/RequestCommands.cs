using System;
using System.Collections.Generic;
using System.Text;

namespace FacebookApiModel
{
    /// <summary>
    /// Класс-словарь с именованными константами для хранения строк.
    /// </summary>
    public class RequestCommands
    {
        public readonly string OwnedAdAccounts = "owned_ad_accounts";
        public readonly string ClientAdAccounts = "client_ad_accounts"; 
        public readonly string AccountStatus = "account_status";
        public readonly string Id = "id";
        public readonly string AdrulesLibrary = "adrules_library";
        public readonly string Fields = "fields";
        public readonly string Value = "value";
        public readonly string Operator = "operator";
        public readonly string EntityType = "entity_type";
        public readonly string EvaluationSpec = "evaluation_spec";
        public readonly string ExecutionSpec = "execution_spec";
        public readonly string Name = "name";
        public readonly string ScheduleSpec = "schedule_spec";
        public readonly string ExecutionType = "execution_type";
        public readonly string Data = "data";
        public readonly string Filters = "filters";
        public readonly string Trigger = "trigger";
        public readonly string TimePreset = "time_preset";
        public readonly string AttributionWindow = "attribution_window";
        public RequestCommands() { }
    }
}
