using System;
using System.Collections.Generic;
using TaskManagerApp.Models;
using System.Text;

namespace TaskManagerApp.Services
{
    /**
     * @class ReportGenerator
     * @brief Utility class to generate reports using the ITaskReporter interface.
     */
    public class ReportGenerator
    {
        private readonly string _reportHeader;

        /**
         * @brief Initializes the generator with a custom header.
         */
        public ReportGenerator(string header)
        {
            _reportHeader = header;
        }

        /**
         * @brief Generates a consolidated report from a list of ITaskReporter objects.
         * @param reporters A collection of objects implementing ITaskReporter.
         * @return A consolidated report string.
         */
        public string GenerateConsolidatedReport(IEnumerable<ITaskReporter> reporters)
        {
            var report = new System.Text.StringBuilder();
            report.AppendLine($"=== {_reportHeader} ===");
            report.AppendLine($"Report Generated: {DateTime.Now}");

            int count = 1;
            foreach (var reporter in reporters)
            {
                report.AppendLine($"\n[Item {count++}]");
                
                report.AppendLine(reporter.GetTaskReport()); 
                report.AppendLine("--------------------------");
            }

            return report.ToString();
        }
    }
}