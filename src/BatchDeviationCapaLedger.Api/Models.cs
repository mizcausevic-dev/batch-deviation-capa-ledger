namespace BatchDeviationCapaLedger.Api;

public sealed record BatchSnapshot(
    string Id,
    string Name,
    string BatchLane,
    string Status,
    string SnapshotStatus,
    string Owner,
    int OpenDeviationCases,
    int BlockingCapas,
    DateTimeOffset CollectedAt
);

public sealed record DeviationGap(
    string Id,
    string SnapshotId,
    string ControlFamily,
    string Severity,
    string Subject,
    string ExpectedState,
    string ObservedState,
    int HoursOpen,
    bool BlocksRelease
);

public sealed record LanePacket(
    string Id,
    string Lane,
    string Owner,
    string Status,
    string Focus,
    string NextAction,
    string Note
);

public sealed record CapaPacket(
    string PacketId,
    string Lane,
    string Owner,
    string Status,
    int CompletenessScore,
    string Blocker,
    string DecisionNote,
    int LaunchWindowHours
);

public sealed record BatchDeviationExport(
    IReadOnlyList<BatchSnapshot> Snapshots,
    IReadOnlyList<DeviationGap> Gaps
);

public sealed record BatchFinding(
    string Code,
    string Severity,
    string Subject,
    string Message,
    string Owner
);

public sealed record PostureReport(
    int Snapshots,
    int CurrentSnapshots,
    int Gaps,
    int BlockingGaps,
    int DeviationRisks,
    int CapaRisks,
    IReadOnlyList<BatchFinding> Findings,
    bool Ok
);
