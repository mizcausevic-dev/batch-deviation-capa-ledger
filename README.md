# batch-deviation-capa-ledger

Biotech / diagnostics operator surface in C# for keeping batch deviations, root-cause packets, CAPA ownership, effectiveness checks, and final disposition posture in one readable control plane.

## Why this matters

Biotech quality teams do not need another vague compliance landing page. They need a board that keeps deviation evidence, CAPA ownership, recurrence pressure, retraining status, and final batch disposition visible together before weak packets escape into the next release cycle.

This repo is the public proof surface for that pattern:

- `Hosted preview planned` for a browser-based deviation and CAPA control plane
- `Embedded by engagement` for teams that need the routing model inside a regulated biotech quality workflow

## Product depth

Batch Deviation CAPA Ledger turns quality events into a governed release-readiness packet. It gives QA, manufacturing, process excellence, quality systems, and release stakeholders one shared view of deviation evidence before root-cause gaps, unowned CAPAs, recurrence pressure, or missing disposition signoff weaken batch decisions.

- **For executives:** shows where batch release exposure is building and which quality decisions need escalation.
- **For operators:** maps every deviation or CAPA gap to owner, control family, batch path, and next remediation move.
- **For technical reviewers:** ships a C# analyzer, minimal API, static proof pages, synthetic fixtures, screenshots, and CI verification.

## What these repos have in common

This is part of the Kinetic Gain control-plane pattern: narrow operating problems packaged as buyer-readable product surfaces with evidence, data contracts, verification routes, screenshots, and deployment metadata.

- named business lane with a board question, operating owner, and remediation motion
- offline-safe sample data so the surface can prove value without exposing patient, batch, customer, credential, or production system data
- public page, API routes, analyzer path, README, screenshots, and CI checks that support a real diligence trail

## Operating workflow

1. Ingest synthetic batch snapshots, deviation gaps, and CAPA packets.
2. Score release-blocking gaps across investigation, ownership, effectiveness, recurrence, training, and disposition.
3. Route each gap to the owner who can repair it before the next quality checkpoint.
4. Publish a static operator page and structured JSON endpoints that non-technical and technical reviewers can inspect.

## What it includes

- ASP.NET Core minimal API in C#
- synthetic batch snapshots, deviation gaps, and CAPA packets
- operator surfaces for:
  - `/batch-lane`
  - `/deviation-findings`
  - `/capa-posture`
  - `/verification`
  - `/docs`
- structured JSON endpoints under `/api/*`
- static Pages export with `robots.txt`, `sitemap.xml`, and `CNAME`

## Screenshots

![Overview](./screenshots/01-overview.svg)
![Batch lane](./screenshots/02-batch-lane.svg)
![CAPA posture](./screenshots/03-capa-posture.svg)

## Verification

- synthetic biotech quality evidence only
- no patient, clinician, or proprietary biotech secrets
- no claim of CLIA, GxP, FDA, or clinical compliance
- this is a control-plane proof surface for workflow depth, not a compliance certification claim

## Local run

```powershell
dotnet test
dotnet run --project src/BatchDeviationCapaLedger.Api -- --demo
dotnet run --project src/BatchDeviationCapaLedger.Api
```

Then open:

- `http://127.0.0.1:5088/`
- `http://127.0.0.1:5088/batch-lane`
- `http://127.0.0.1:5088/deviation-findings`
- `http://127.0.0.1:5088/capa-posture`

## Render static site

```powershell
dotnet run --project src/BatchDeviationCapaLedger.Api -- --prerender
```

## Related docs

- [Embedded framing](./docs/KINETIC_GAIN_EMBEDDED.md)
- [Origin story](./docs/ORIGIN.md)
