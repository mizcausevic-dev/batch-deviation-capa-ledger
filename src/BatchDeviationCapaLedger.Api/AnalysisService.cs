namespace BatchDeviationCapaLedger.Api;

public static class AnalysisService
{
    public static PostureReport Analyze(BatchDeviationExport payload)
    {
        var findings = new List<BatchFinding>();

        foreach (var snapshot in payload.Snapshots)
        {
            if (snapshot.SnapshotStatus == "STALE")
            {
                findings.Add(new BatchFinding(
                    "stale-batch-snapshot",
                    "medium",
                    snapshot.Name,
                    $"Snapshot \"{snapshot.Name}\" is stale and should be refreshed before batch disposition is asserted.",
                    snapshot.Owner
                ));
            }
        }

        foreach (var gap in payload.Gaps)
        {
            var code = gap.ControlFamily switch
            {
                "Investigation" => "root-cause-packet-gap",
                "Ownership" => "capa-owner-gap",
                "Effectiveness" => "effectiveness-check-gap",
                "Recurrence" => "deviation-recurrence-risk",
                "Training" => "operator-retraining-gap",
                "Disposition" => "batch-disposition-gap",
                _ => "batch-gap"
            };

            findings.Add(new BatchFinding(
                code,
                gap.Severity,
                gap.Subject,
                gap.ObservedState,
                ResolveOwner(gap.ControlFamily)
            ));

            if (gap.HoursOpen > 24)
            {
                findings.Add(new BatchFinding(
                    "stale-gap-window",
                    gap.HoursOpen > 36 ? "medium" : "low",
                    gap.Subject,
                    $"Gap \"{gap.Subject}\" has remained open for {gap.HoursOpen} hours.",
                    ResolveOwner(gap.ControlFamily)
                ));
            }
        }

        var blocking = payload.Gaps.Count(g => g.BlocksRelease);
        var deviationRisks = payload.Gaps.Count(g => g.ControlFamily is "Investigation" or "Recurrence" or "Training");
        var capaRisks = payload.Gaps.Count(g => g.ControlFamily is "Ownership" or "Effectiveness" or "Disposition");

        return new PostureReport(
            payload.Snapshots.Count,
            payload.Snapshots.Count(s => s.SnapshotStatus == "CURRENT"),
            payload.Gaps.Count,
            blocking,
            deviationRisks,
            capaRisks,
            findings,
            !findings.Any(f => f.Severity == "high")
        );
    }

    public static object Summary()
    {
        var report = Analyze(SampleData.Payload);

        return new
        {
            batches = report.Snapshots,
            currentBundles = report.CurrentSnapshots,
            gaps = report.Gaps,
            blockingGaps = report.BlockingGaps,
            deviationRisks = report.DeviationRisks,
            capaRisks = report.CapaRisks,
            recommendation = "Close root-cause evidence, CAPA ownership, effectiveness checks, and final disposition signoff before batch release."
        };
    }

    private static string ResolveOwner(string family) => family switch
    {
        "Investigation" => "Manufacturing QA",
        "Ownership" => "Quality Systems",
        "Effectiveness" => "Process Excellence",
        "Recurrence" => "Process Excellence",
        "Training" => "Manufacturing QA",
        "Disposition" => "Quality Systems",
        _ => "Manufacturing QA"
    };
}
