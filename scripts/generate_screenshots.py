from pathlib import Path
import textwrap


ROOT = Path(__file__).resolve().parents[1]
SHOT_DIR = ROOT / "screenshots"
SHOT_DIR.mkdir(exist_ok=True)


def wrap(text: str, width: int):
    return textwrap.wrap(text, width=width) or [text]


def draw_lines(lines, x, y, font_size=22, color="#e9f3ff", weight="400", family="Inter,Segoe UI,Arial"):
    parts = [f'<text x="{x}" y="{y}" fill="{color}" font-size="{font_size}" font-weight="{weight}" font-family="{family}">']
    dy = 0
    for line in lines:
        parts.append(f'<tspan x="{x}" dy="{dy}">{line}</tspan>')
        dy = int(font_size * 1.25)
    parts.append("</text>")
    return "\n".join(parts)


def card(x, y, w, h, title, body, accent="#19c7ff"):
    body_lines = wrap(body, 34)
    return f"""
    <rect x="{x}" y="{y}" width="{w}" height="{h}" rx="20" fill="#0b1220" stroke="rgba(120,255,170,.18)" />
    {draw_lines([title.upper()], x + 28, y + 36, 12, accent, "700", "Consolas, monospace")}
    {draw_lines(wrap(title, 20), x + 28, y + 82, 30, "#f5f7ff", "700", "Georgia, serif")}
    {draw_lines(body_lines, x + 28, y + 132, 16, "#b8c6db", "400")}
    """


def shell(title, subtitle, inner):
    return f"""<svg xmlns="http://www.w3.org/2000/svg" width="1400" height="860" viewBox="0 0 1400 860">
    <rect width="1400" height="860" fill="#070a0f"/>
    <rect x="28" y="28" width="1344" height="804" rx="30" fill="#0a1426" stroke="rgba(120,255,170,.18)"/>
    <rect x="58" y="58" width="1284" height="110" rx="24" fill="#0b1220" stroke="rgba(120,255,170,.12)"/>
    {draw_lines([title.upper()], 94, 96, 14, "#37ff8b", "700", "Consolas, monospace")}
    {draw_lines(wrap(title, 26), 94, 140, 38, "#f5f7ff", "700", "Georgia, serif")}
    {draw_lines(wrap(subtitle, 92), 94, 188, 18, "#b8c6db", "400")}
    {inner}
    </svg>"""


overview = shell(
    "Batch Deviation CAPA Ledger",
    "C# control plane for deviation intake, root-cause packet quality, CAPA ownership, effectiveness review, and final batch disposition posture.",
    f"""
    <rect x="58" y="206" width="306" height="154" rx="22" fill="#0b1220" stroke="rgba(120,255,170,.12)"/>
    {draw_lines(["2"], 88, 274, 50, "#19c7ff", "700")}
    {draw_lines(["batch snapshots", "one current export"], 88, 316, 16, "#b8c6db")}

    <rect x="382" y="206" width="306" height="154" rx="22" fill="#0b1220" stroke="rgba(120,255,170,.12)"/>
    {draw_lines(["6"], 412, 274, 50, "#b88cff", "700")}
    {draw_lines(["deviation gaps", "4 blocking quality issues"], 412, 316, 16, "#b8c6db")}

    <rect x="706" y="206" width="306" height="154" rx="22" fill="#0b1220" stroke="rgba(120,255,170,.12)"/>
    {draw_lines(["4"], 736, 274, 50, "#ffcc66", "700")}
    {draw_lines(["CAPA risks", "ownership, recurrence, disposition"], 736, 316, 16, "#b8c6db")}

    <rect x="1030" y="206" width="312" height="154" rx="22" fill="#0b1220" stroke="rgba(120,255,170,.12)"/>
    {draw_lines(["Embedded"], 1060, 274, 40, "#37ff8b", "700")}
    {draw_lines(["hosted preview planned", "delivery by engagement"], 1060, 316, 16, "#b8c6db")}

    {card(58, 392, 620, 390, "Deviation evidence stays visible before batch handoff", "Root-cause packet continuity, operator retraining, effectiveness checks, and final disposition posture sit on the same surface so biotech teams can correct weak quality packets before downstream pressure wins.", "#19c7ff")}
    {card(708, 392, 634, 390, "Blocking gaps are routed by owner and action", "Quality Systems, Manufacturing QA, and site supervision each get visible routing pressure with synthetic packets that show missing root cause, stale effectiveness review, and unresolved batch disposition without using live biotech data.", "#37ff8b")}
    """,
)

batch_lane = shell(
    "Batch Lane",
    "Named ownership and next actions across deviation intake, root-cause evidence, CAPA ownership, recurrence risk, and final disposition control families.",
    f"""
    {card(58, 206, 620, 250, "Deviation intake lane", "Manufacturing QA owns packet continuity, operator statements, and event timestamps before CAPA routing moves downstream.", "#19c7ff")}
    {card(708, 206, 634, 250, "Root-cause lane", "Quality Systems keeps causality notes, containment evidence, and investigation freshness on one lane.", "#ffcc66")}
    {card(58, 488, 620, 294, "CAPA ownership lane", "Supervisors see due dates, overdue owners, retraining posture, and effectiveness review timing in one glance.", "#b88cff")}
    {card(708, 488, 634, 294, "Disposition lane", "Synthetic packet IDs keep the final story concrete: blocker, owner, decision note, and next checkpoint without overclaiming compliance.", "#37ff8b")}
    """,
)

capa_posture = shell(
    "CAPA Posture",
    "Packet readiness, blocker pressure, and the next batch checkpoint stay readable for operators and buyers.",
    f"""
    {card(58, 206, 402, 256, "BDL-12 · root-cause packet", "57 percent complete. Batch hold remains until cause evidence and supervisor notes reconcile.", "#ff5c7a")}
    {card(498, 206, 402, 256, "BDL-18 · owner packet", "62 percent complete. Posture is recoverable if CAPA ownership and effectiveness dates close in the next cycle.", "#ffcc66")}
    {card(938, 206, 404, 256, "BDL-27 · disposition packet", "72 percent complete. Safe for governed handoff once the final disposition note is restored.", "#37ff8b")}
    {card(58, 494, 1284, 288, "Why this monetizes cleanly", "Hosted preview planned gives the public lane. Paid templates can adapt deviation packet families and QA owners. Embedded delivery fits biotech teams that need CAPA routing and batch posture inside their real quality workflow.", "#19c7ff")}
    """,
)

(SHOT_DIR / "01-overview.svg").write_text(overview, encoding="utf-8")
(SHOT_DIR / "02-batch-lane.svg").write_text(batch_lane, encoding="utf-8")
(SHOT_DIR / "03-capa-posture.svg").write_text(capa_posture, encoding="utf-8")
