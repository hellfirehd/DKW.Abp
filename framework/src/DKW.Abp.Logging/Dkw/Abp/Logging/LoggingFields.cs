// Canadian Professional Counsellors Association Application Suite
// Copyright (C) 2024 Doug Wilson
//
// This program is free software: you can redistribute it and/or modify it under the terms of
// the GNU Affero General Public License as published by the Free Software Foundation, either
// version 3 of the License, or (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY
// without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
// See the GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License along with this
// program. If not, see <https://www.gnu.org/licenses/>.

namespace Dkw.Abp.Logging;

public static class LoggingFields
{
    public static String Action { get; set; } = "Action";
    public static String Application { get; set; } = "Application";
    public static String Claims { get; set; } = "Claims";
    public static String Context { get; set; } = "Context";
    public static String CorrelationId { get; set; } = "CorrelationId";
    public static String CurrentLocation { get; set; } = "CurrentLocation";
    public static String CurrentUser { get; set; } = "CurrentUser";
    public static String Department { get; set; } = "Department";
    public static String Endpoint { get; set; } = "Endpoint";
    public static String Environment { get; set; } = "EnvironmentName";
    public static String Impersonator { get; set; } = "Impersonator";
    public static String Instance { get; set; } = "Instance";
    public static String Method { get; set; } = "Method";
    public static String LogDatabase { get; set; } = "LogDatabase";
    public static String LogTableName { get; set; } = "LogEntries";
    public static String Member { get; set; } = "Member";
    public static String MemberNumber { get; set; } = "MemberNumber";
    public static String Request { get; set; } = "Request";
    public static String RequestMethod { get; set; } = "RequestMethod";
    public static String RequestUri { get; set; } = "RequestUri";
    public static String RetryCount { get; set; } = "RetryCount";
    public static String RetryDelay { get; set; } = "RetryDelay";
    public static String SagaState { get; set; } = "SagaState";
    public static String Subject { get; set; } = "Subject";
    public static String UserId { get; set; } = "UserId";
    public static String Version { get; set; } = "Version";
}

