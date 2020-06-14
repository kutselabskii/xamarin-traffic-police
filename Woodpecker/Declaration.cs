using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Woodpecker
{
	[Table("Declarations")]
	public class Declaration
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }

		public string Text { get; set; }
	}
}
