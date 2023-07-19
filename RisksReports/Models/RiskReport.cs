// using CloudKit;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisksReports.Models {
	public class RiskReport {
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public string Title { get; set; }

		public string Description { get; set; }

		public string Place { get; set; }

		public string Time { get; set; }

		public SeverityLevel Severity { get; set; } = SeverityLevel.Average;

		public ProbabilityLevel Probability { get; set; } = ProbabilityLevel.Average;
	}
}
