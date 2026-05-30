namespace BatchDeviationCapaLedger.Api;

public static class SampleData
{
    public static readonly BatchDeviationExport Payload = new(
        Snapshots:
        [
            new(
                "batch-core",
                "Deviation intake snapshot",
                "Manufacturing deviation lane",
                "WATCH",
                "CURRENT",
                "Manufacturing QA",
                11,
                4,
                DateTimeOffset.Parse("2026-05-30T15:10:00Z")
            ),
            new(
                "batch-release",
                "CAPA closure snapshot",
                "Final disposition lane",
                "CRITICAL",
                "STALE",
                "Quality Systems",
                7,
                3,
                DateTimeOffset.Parse("2026-05-28T09:00:00Z")
            )
        ],
        Gaps:
        [
            new(
                "gap-root-cause",
                "batch-core",
                "Investigation",
                "high",
                "Root-cause packet",
                "Every major deviation carries a signed root-cause packet before CAPA board review.",
                "One critical deviation is still missing the signed root-cause narrative after manufacturing handoff.",
                22,
                true
            ),
            new(
                "gap-capa-owner",
                "batch-release",
                "Ownership",
                "high",
                "CAPA ownership assignment",
                "Every release-blocking CAPA has a named owner and due date inside the quality queue.",
                "Two blocking CAPAs are still missing a final accountable owner in the release queue.",
                40,
                true
            ),
            new(
                "gap-effectiveness",
                "batch-release",
                "Effectiveness",
                "high",
                "Effectiveness check packet",
                "Closed CAPAs retain a completed effectiveness check before final disposition.",
                "One closed CAPA does not yet have an effectiveness verification packet attached.",
                28,
                true
            ),
            new(
                "gap-recurrence",
                "batch-core",
                "Recurrence",
                "medium",
                "Deviation recurrence trend",
                "Repeat deviations stay below the accepted recurrence threshold before batch disposition.",
                "Repeat deviation frequency rose above the internal recurrence threshold this week.",
                18,
                false
            ),
            new(
                "gap-training",
                "batch-core",
                "Training",
                "medium",
                "Operator retraining packet",
                "Affected operators complete retraining before the deviation is treated as contained.",
                "Retraining signoff is still missing for one operator cohort tied to the deviation cluster.",
                16,
                false
            ),
            new(
                "gap-disposition",
                "batch-release",
                "Disposition",
                "high",
                "Final batch disposition packet",
                "Disposition packets remain complete before batch release or destruction routing.",
                "The batch disposition packet is missing one reviewer signature and one linked evidence proof.",
                20,
                true
            )
        ]
    );

    public static readonly IReadOnlyList<LanePacket> BatchLane =
    [
        new(
            "deviation-intake",
            "Deviation intake lane",
            "Manufacturing QA",
            "red",
            "Deviation packet continuity, severity triage, and investigation readiness",
            "Restore the missing root-cause packet before CAPA board review continues.",
            "Deviation intake evidence is not strong enough for confident escalation."
        ),
        new(
            "capa-ownership",
            "CAPA ownership lane",
            "Quality Systems",
            "red",
            "Owner assignment, due dates, and blocking corrective-action routing",
            "Assign final accountable owners and due dates for the blocking CAPAs.",
            "Unowned CAPAs are blocking a credible quality posture."
        ),
        new(
            "effectiveness-watch",
            "Effectiveness and recurrence lane",
            "Process Excellence",
            "yellow",
            "Effectiveness checks, recurrence evidence, and second-pass quality review",
            "Close the missing effectiveness packet and route recurrence pressure into process review.",
            "Recurrence is recoverable if the effectiveness packet lands in the next window."
        ),
        new(
            "disposition-board",
            "Final disposition lane",
            "Quality Systems",
            "red",
            "Disposition completeness, reviewer signoff, and batch release timing",
            "Close the missing reviewer signature and evidence-link gaps before final disposition.",
            "The final batch disposition packet is still incomplete."
        )
    ];

    public static readonly IReadOnlyList<CapaPacket> CapaPackets =
    [
        new(
            "CAPA-12",
            "Deviation evidence packet",
            "Manufacturing QA",
            "red",
            56,
            "Root-cause packet and final owner assignment are still missing.",
            "Do not call the deviation cluster contained until the root-cause packet and owner assignment are complete.",
            8
        ),
        new(
            "CAPA-18",
            "Ownership checkpoint",
            "Quality Systems",
            "red",
            61,
            "Blocking CAPAs still lack a final accountable owner.",
            "Block batch disposition and reroute this queue through CAPA ownership review.",
            11
        ),
        new(
            "CAPA-21",
            "Effectiveness review packet",
            "Process Excellence",
            "yellow",
            75,
            "Effectiveness verification is still open on one closed CAPA.",
            "Disposition can recover if the effectiveness check closes in the next cycle.",
            15
        ),
        new(
            "CAPA-27",
            "Disposition packet",
            "Quality Systems",
            "yellow",
            71,
            "The disposition packet still needs one reviewer signature and linked evidence bridge.",
            "Readiness is recoverable if the disposition packet closes before the next checkpoint.",
            19
        )
    ];
}
