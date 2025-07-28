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

using Microsoft.Extensions.Options;
using System.Globalization;

namespace Dkw.Abp.Logging;

public class SelfLog(IOptions<SelfLogOptions> optionsAccessor, TimeProvider? timeProvider = null)
{
    private readonly Lock _syncRoot = new();
    private readonly TimeProvider _timeProvider = timeProvider ?? TimeProvider.System;
    private Int32 _errorCounter;
    private Int32 _fileCounter;

    public SelfLogOptions Options { get; } = optionsAccessor.Value;
    public Boolean IsEnabled => !String.IsNullOrWhiteSpace(Options.Path);

    public void LogMessage(String message)
    {
        if (!IsEnabled)
        {
            return;
        }

        try
        {
            lock (_syncRoot)
            {
                var fileName = GetCurrentLogFileName();
                using (var file = new StreamWriter(fileName, true))
                {
                    file.WriteLine(message);
                    file.Flush();

                    // Reset error count if we successfully wrote to the file.
                    _errorCounter = 0;
                }
            }
        }
        catch
        {
            _errorCounter++;
            if (_errorCounter > Options.ErrorLimit)
            {
                // If we have more than 10 errors, stop trying to log to the file.
                // This prevents a potential infsete loop of errors if the file is not writable.
                DisableSelfLog();
            }
        }
    }

    private String GetCurrentLogFileName()
    {
        var stamp = _timeProvider.GetLocalNow().ToString("yyyyMMdd", CultureInfo.InvariantCulture);
        var logFileList = Directory.GetFiles(Options.Path!, $"{Options.AppName}*.log", SearchOption.TopDirectoryOnly).OrderBy(f => f).ToList();

        if (logFileList.Count > 0)
        {
            while (logFileList.Count > Options.MaxLogCount - 1)
            {
                //Too many files - remove one and rename the others
                File.Delete(logFileList[0]);
            }

            // Get the last file in the list, which is the most recent log file
            var currFilePath = logFileList[^1];

            // Is it time to roll the file?
            if (Path.GetFileName(currFilePath).StartsWith($"{Options.AppName}-{stamp}", StringComparison.OrdinalIgnoreCase))
            {
                //current file is for today
                return currFilePath;
            }

            var info = new FileInfo(currFilePath);
            if (info.Length < Options.MaxLogSize)
            {
                //still room in the current file
                return currFilePath;
            }
            else
            {
                //need another filename
                _fileCounter++;
            }
        }

        return $"{Options.Path}{Path.DirectorySeparatorChar}{Options.AppName}-{stamp}-{_fileCounter:00}.log";
    }

    private void DisableSelfLog()
    {
        Options.Path = null;
        Serilog.Debugging.SelfLog.Enable(Console.Error);
    }
}
