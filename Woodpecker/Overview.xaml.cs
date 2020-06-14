using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Woodpecker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Overview : ContentPage
	{
		private static Profile profile;
		private static string text;
		private static ObservableCollection<Photo> photos = new ObservableCollection<Photo>();

		public Overview(Profile profile, string text, ObservableCollection<Photo> photos)
		{
			Overview.profile = profile;
			Overview.text = text;
			Overview.photos = photos;
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			Name.Text = "Name: " + profile.Name;
			Email.Text = "E-mail: " + profile.Email;
			Phone.Text = "Phone number: " + profile.Phone;
			DestRegion.Text = "Destination region: " + profile.DestRegion;
			Unit.Text = "Administrative unit: " + profile.Unit;
			Region.Text = "Region of incident: " + profile.Region;

			Editor.Text = text;

			PhotosList.ItemsSource = photos;

			base.OnAppearing();
		}

		private async void OnPhotoTapped(object sender, ItemTappedEventArgs e)
		{
			PhotosList.SelectedItem = null;
			await Navigation.PushAsync(new PhotoPage((Photo) e.Item));
		}

		private async void SubmitClicked(object sender, EventArgs args)
		{
			profile = null;
			text = string.Empty;
			photos.Clear();

			EditPage.profile = null;
			EditPage.text = string.Empty;
			EditPage.photos.Clear();

			MainPage.selected = -1;

			await Navigation.PopToRootAsync();
		}
	}
}