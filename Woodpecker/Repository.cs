using System.Collections.Generic;
using SQLite;

namespace Woodpecker
{
	public class Repository
	{
		private SQLiteConnection database;

		public Repository(string databasePath)
		{
			database = new SQLiteConnection(databasePath);
			database.CreateTable<Profile>();
			database.CreateTable<Declaration>();
		}

		public IEnumerable<Declaration> GetDeclarations()
		{
			return database.Table<Declaration>().ToList();
		}

		public int SaveDeclaration(Declaration item)
		{
			if (item.Id != 0) {
				database.Update(item);
				return item.Id;
			}
			return database.Insert(item);
		}

		public IEnumerable<Profile> GetProfiles()
		{
			return database.Table<Profile>().ToList();
		}

		public Profile GetProfile(int id)
		{
			return database.Get<Profile>(id);
		}

		public int DeleteProfile(int id)
		{
			return database.Delete<Profile>(id);
		}

		public int SaveProfile(Profile item)
		{
			if (item.Id != 0) {
				database.Update(item);
				return item.Id;
			}
			return database.Insert(item);
		}
	}
}
