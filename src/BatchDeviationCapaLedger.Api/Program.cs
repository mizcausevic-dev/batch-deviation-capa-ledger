using System.Text.Json;
using BatchDeviationCapaLedger.Api;

var app = BatchDeviationCapaApplication.BuildApp(args);

if (args.Contains("--prerender"))
{
    await SiteBuilder.WriteAsync();
    return;
}

if (args.Contains("--demo"))
{
    Console.WriteLine(JsonSerializer.Serialize(AnalysisService.Summary(), new JsonSerializerOptions { WriteIndented = true }));
    Console.WriteLine(JsonSerializer.Serialize(SampleData.BatchLane, new JsonSerializerOptions { WriteIndented = true }));
    return;
}

app.Run();

public partial class Program;

public static class BatchDeviationCapaApplication
{
    public static WebApplication BuildApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => Results.Content(RenderService.Overview(), "text/html"));
        app.MapGet("/batch-lane", () => Results.Content(RenderService.BatchLane(), "text/html"));
        app.MapGet("/deviation-findings", () => Results.Content(RenderService.DeviationFindings(), "text/html"));
        app.MapGet("/capa-posture", () => Results.Content(RenderService.CapaPosture(), "text/html"));
        app.MapGet("/verification", () => Results.Content(RenderService.Verification(), "text/html"));
        app.MapGet("/docs", () => Results.Content(RenderService.Docs(), "text/html"));

        app.MapGet("/api/dashboard/summary", () => Results.Json(AnalysisService.Summary()));
        app.MapGet("/api/batch-lane", () => Results.Json(SampleData.BatchLane));
        app.MapGet("/api/deviation-findings", () => Results.Json(SampleData.Payload.Gaps));
        app.MapGet("/api/capa-posture", () => Results.Json(SampleData.CapaPackets));
        app.MapGet("/api/verification", () => Results.Json(new[]
        {
            "Synthetic deviation and CAPA evidence only; no patient, lab, or proprietary biotech data is published.",
            "Deviation intake, CAPA ownership, effectiveness checks, recurrence pressure, and final disposition are modeled as operator surfaces.",
            "This repo demonstrates biotech quality workflow depth without claiming CLIA, GxP, FDA, or clinical compliance."
        }));
        app.MapGet("/api/sample", () => Results.Text(RenderService.Sample(), "application/json"));

        return app;
    }
}

public static class SiteBuilder
{
    public static async Task WriteAsync()
    {
        var root = FindRepoRoot();
        var siteDir = Path.Combine(root, "site");
        Directory.CreateDirectory(siteDir);

        var pages = new Dictionary<string, string>
        {
            ["index.html"] = RenderService.Overview(),
            [Path.Combine("batch-lane", "index.html")] = RenderService.BatchLane(),
            [Path.Combine("deviation-findings", "index.html")] = RenderService.DeviationFindings(),
            [Path.Combine("capa-posture", "index.html")] = RenderService.CapaPosture(),
            [Path.Combine("verification", "index.html")] = RenderService.Verification(),
            [Path.Combine("docs", "index.html")] = RenderService.Docs()
        };

        foreach (var (relative, html) in pages)
        {
            var target = Path.Combine(siteDir, relative);
            Directory.CreateDirectory(Path.GetDirectoryName(target)!);
            await File.WriteAllTextAsync(target, html);
        }

        var apiDir = Path.Combine(siteDir, "api");
        Directory.CreateDirectory(Path.Combine(apiDir, "dashboard"));
        await File.WriteAllTextAsync(Path.Combine(apiDir, "dashboard", "summary.json"), JsonSerializer.Serialize(AnalysisService.Summary(), new JsonSerializerOptions { WriteIndented = true }));
        await File.WriteAllTextAsync(Path.Combine(apiDir, "batch-lane.json"), JsonSerializer.Serialize(SampleData.BatchLane, new JsonSerializerOptions { WriteIndented = true }));
        await File.WriteAllTextAsync(Path.Combine(apiDir, "deviation-findings.json"), JsonSerializer.Serialize(SampleData.Payload.Gaps, new JsonSerializerOptions { WriteIndented = true }));
        await File.WriteAllTextAsync(Path.Combine(apiDir, "capa-posture.json"), JsonSerializer.Serialize(SampleData.CapaPackets, new JsonSerializerOptions { WriteIndented = true }));
        await File.WriteAllTextAsync(Path.Combine(apiDir, "verification.json"), JsonSerializer.Serialize(new[]
        {
            "Synthetic deviation and CAPA evidence only; no patient, lab, or proprietary biotech data is published.",
            "Deviation intake, CAPA ownership, effectiveness checks, recurrence pressure, and final disposition are modeled as operator surfaces.",
            "This repo demonstrates biotech quality workflow depth without claiming CLIA, GxP, FDA, or clinical compliance."
        }, new JsonSerializerOptions { WriteIndented = true }));
        await File.WriteAllTextAsync(Path.Combine(apiDir, "sample.json"), RenderService.Sample());

        const string domain = "capa.kineticgain.com";
        await File.WriteAllTextAsync(
            Path.Combine(siteDir, "robots.txt"),
            $"User-agent: *{Environment.NewLine}Allow: /{Environment.NewLine}Sitemap: https://{domain}/sitemap.xml{Environment.NewLine}");
        await File.WriteAllTextAsync(Path.Combine(siteDir, "sitemap.xml"), """
<?xml version="1.0" encoding="UTF-8"?>
<urlset xmlns="http://www.sitemaps.org/schemas/sitemap/0.9">
  <url><loc>https://capa.kineticgain.com/</loc></url>
  <url><loc>https://capa.kineticgain.com/batch-lane/</loc></url>
  <url><loc>https://capa.kineticgain.com/deviation-findings/</loc></url>
  <url><loc>https://capa.kineticgain.com/capa-posture/</loc></url>
  <url><loc>https://capa.kineticgain.com/verification/</loc></url>
  <url><loc>https://capa.kineticgain.com/docs/</loc></url>
</urlset>
""");
        await File.WriteAllTextAsync(Path.Combine(siteDir, "CNAME"), domain + Environment.NewLine);
    }

    private static string FindRepoRoot()
    {
        var current = AppContext.BaseDirectory;
        for (var i = 0; i < 8; i++)
        {
            if (File.Exists(Path.Combine(current, "batch-deviation-capa-ledger.sln")))
            {
                return current;
            }

            current = Directory.GetParent(current)?.FullName
                ?? throw new DirectoryNotFoundException("Unable to resolve repo root.");
        }

        throw new DirectoryNotFoundException("Unable to resolve repo root.");
    }
}
