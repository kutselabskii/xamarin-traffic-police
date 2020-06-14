using SQLite;

namespace Woodpecker
{
	[Table("Profiles")]
	public class Profile
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }

		public string Name { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string DestRegion { get; set; }
		public string Unit { get; set; }
		public string Region { get; set; }
	}
}
