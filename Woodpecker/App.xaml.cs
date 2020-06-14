using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Woodpecker
{
	public partial class App : Application
	{
		public const string DatabaseName = "database.db";
		private static Repository database;

		public static Repository Database
		{
			get {
				if (database == null) {
					database = new Repository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DatabaseName));
				}
				return database;
			}
		}

		public App()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
