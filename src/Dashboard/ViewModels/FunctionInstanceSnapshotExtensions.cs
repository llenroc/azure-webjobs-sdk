﻿using System;
using Dashboard.Data;

namespace Dashboard.ViewModels
{
    [CLSCompliant(false)]
    public static class FunctionInstanceSnapshotExtensions
    {
        // null if job hasn't finished yet.
        public static TimeSpan? GetFinalDuration(this FunctionInstanceSnapshot snapshot)
        {
            if (snapshot == null)
            {
                throw new ArgumentNullException("snapshot");
            }

            DateTimeOffset? endTime = snapshot.EndTime;

            if (!endTime.HasValue)
            {
                return null;
            }

            DateTimeOffset? startTime = snapshot.StartTime;

            if (!startTime.HasValue)
            {
                return null;
            }

            return endTime.Value - startTime.Value;
        }

        public static FunctionInstanceStatus GetStatusWithoutHeartbeat(this FunctionInstanceSnapshot snapshot)
        {
            return GetStatusWithHeartbeat(snapshot, null);
        }

        public static FunctionInstanceStatus GetStatusWithHeartbeat(this FunctionInstanceSnapshot snapshot, DateTime? heartbeatExpires)
        {
            if (snapshot == null)
            {
                throw new ArgumentNullException("snapshot");
            }

            if (snapshot.EndTime.HasValue)
            {
                if (snapshot.Succeeded.Value)
                {
                    return FunctionInstanceStatus.CompletedSuccess;
                }
                else
                {
                    return FunctionInstanceStatus.CompletedFailed;
                }
            }
            else if (heartbeatExpires.HasValue)
            {
                if (heartbeatExpires.Value < DateTime.UtcNow)
                {
                    return FunctionInstanceStatus.NeverFinished;
                }
                else
                {
                    return FunctionInstanceStatus.Running;
                }
            }
            else if (snapshot.StartTime.HasValue)
            {
                return FunctionInstanceStatus.Running;
            }
            else
            {
                return FunctionInstanceStatus.Queued;
            }
        }
    }
}