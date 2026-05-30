using BatchDeviationCapaLedger.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BatchDeviationCapaLedger.Tests;

public sealed class BatchDeviationCapaLedgerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public BatchDeviationCapaLedgerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Overview_route_renders_batch_deviation_shell()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/");
        var html = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);
        Assert.Contains("Batch Deviation CAPA Ledger", html);
        Assert.Contains("root-cause evidence", html);
    }

    [Fact]
    public async Task Api_summary_returns_expected_counts()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/dashboard/summary");
        var json = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);
        Assert.Contains("\"batches\":2", json);
        Assert.Contains("\"blockingGaps\":4", json);
    }

    [Fact]
    public void Analysis_flags_high_risk_capa_gaps()
    {
        var report = AnalysisService.Analyze(SampleData.Payload);

        Assert.Equal(2, report.Snapshots);
        Assert.Equal(6, report.Gaps);
        Assert.Contains(report.Findings, finding => finding.Code == "root-cause-packet-gap");
        Assert.Contains(report.Findings, finding => finding.Code == "batch-disposition-gap");
    }
}
