using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Woodpecker
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		public static int selected = -1;
		public MainPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			var src = App.Database.GetProfiles().ToArray();
			ProfilesList.ItemsSource = src;
			if (selected != -1) {
				if (src.Length > selected) {
					ProfilesList.SelectedItem = src[selected];
				} else {
					selected = -1;
				}
			}
			base.OnAppearing();
		}

		private async void CreateNewProfileClicked(object sender, EventArgs args)
		{
			Profile profile = new Profile();
			ProfilePage page = new ProfilePage(false);
			page.BindingContext = profile;
			await Navigation.PushAsync(page);
		}

		private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			selected = e.SelectedItemIndex;
		}

		private async void EditProfileClicked(object sender, EventArgs args)
		{
			if (ProfilesList.SelectedItem == null || selected == -1) {
				return;
			}

			Profile profile = (Profile)ProfilesList.SelectedItem;
			ProfilePage page = new ProfilePage(true);
			page.BindingContext = profile;
			await Navigation.PushAsync(page);
		}

		private async void FillFormClicked(object sender, EventArgs args)
		{
			if (ProfilesList.SelectedItem != null && selected != -1) {
				var page = new EditPage((Profile) ProfilesList.SelectedItem);
				await Navigation.PushAsync(page);
			}
		}
	}
}
