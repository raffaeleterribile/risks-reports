using RisksReports.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RisksReports.Data {
	public class DatabaseManager {
		private SQLiteAsyncConnection connection;

		public async Task InitDatabase() {
			if (connection is not null) {
				return;
			}

			string databaseFilename = "RisksReports.db3";
			string databasePath = Path.Combine(FileSystem.AppDataDirectory, databaseFilename);
			SQLiteOpenFlags flags =
				// open the database in read/write mode
				SQLiteOpenFlags.ReadWrite |
				// create the database if it doesn't exist
				SQLiteOpenFlags.Create |
				// enable multi-threaded database access
				SQLiteOpenFlags.SharedCache;

			connection = new SQLiteAsyncConnection(databasePath, flags, true);
			await connection.CreateTableAsync<RiskReport>();

			System.Diagnostics.Debug.WriteLine("Database and tables created");
		}

		public async Task<List<RiskReport>> GetItemsAsync() {
			await InitDatabase();
			return await connection.Table<RiskReport>().ToListAsync();
		}

		public async Task<RiskReport> GetItemAsync(int id) {
			await InitDatabase();
			return await connection.Table<RiskReport>().Where(i => i.Id == id).FirstOrDefaultAsync();
		}

		public async Task<int> SaveItemAsync(RiskReport item) {
			await InitDatabase();
			if (item.Id != 0) {
				return await connection.UpdateAsync(item);
			} else {
				return await connection.InsertAsync(item);
			}
		}

		public async Task<int> DeleteItemAsync(RiskReport item) {
			await InitDatabase();
			return await connection.DeleteAsync(item);
		}
	}
}
