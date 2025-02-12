using System.Collections.Generic;
using System.Linq;
using Spectre.Console;

namespace Coding_Tracker
{
    internal class TableVisualisationEngine
    {
        internal void ShowTable(List<CodingSession> tableData)
        {
            if (tableData == null || !tableData.Any())
            {
                AnsiConsole.Markup("[red]No data to display.[/]");
                return;
            }

            var table = new Table()
            .Border(TableBorder.Simple)
            .AddColumn("Id")
            .AddColumn("Date")
            .AddColumn("Start Time")
            .AddColumn("End Time")
            .AddColumn("Duration");

            int id = 0;
            foreach (var item in tableData)
            {
                id += 1;
                table.AddRow(
                    new Markup($"{item.Id}"),
                    new Markup($"[green]{item.Date}[/]"),
                    new Markup($"[green]{item.StartTime}[/]"),
                    new Markup($"[green]{item.EndTime}[/]"),
                    new Markup($"[green]{item.Duration}[/]")
                );
            }

            AnsiConsole.Write(table);
        }
    }
   
}
