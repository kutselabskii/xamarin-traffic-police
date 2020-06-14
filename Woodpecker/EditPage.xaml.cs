using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Woodpecker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditPage : ContentPage
	{
		public static Profile profile;
		public static string text;
		public static ObservableCollection<Photo> photos = new ObservableCollection<Photo>();

		public EditPage(Profile profile)
		{
			EditPage.profile = profile;
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			var src = App.Database.GetDeclarations().ToList();
			DeclarationsList.ItemsSource = src;
			if (!string.IsNullOrEmpty(text)) {
				Editor.Text = text;
			}

			PhotosList.ItemsSource = photos;
			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			text = Editor.Text;
			base.OnDisappearing();
		}

		private async void OnItemTapped(object sender, ItemTappedEventArgs e)
		{
			Editor.Text = ((Declaration) e.Item).Text;
			text = Editor.Text;
			DeclarationsList.SelectedItem = null;
		}

		private async void OnPhotoTapped(object sender, ItemTappedEventArgs e)
		{
			PhotosList.SelectedItem = null;
			photos.RemoveAt(e.ItemIndex);
		}

		private async void TakePhotoClicked(object sender, EventArgs args)
		{
			await CrossMedia.Current.Initialize();

			var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions {
					SaveToAlbum = true
				}
			);

			if (file == null) {
				return;
			}

			photos.Add(new Photo {Path = file.AlbumPath, Source = ImageSource.FromStream(() => file.GetStream())});
		}

		private async void PickPhotoClicked(object sender, EventArgs args)
		{
			await CrossMedia.Current.Initialize();

			var file = await CrossMedia.Current.PickPhotoAsync();

			if (file == null) {
				return;
			}

			photos.Add(new Photo { Path = file.AlbumPath, Source = ImageSource.FromStream(() => file.GetStream())});
		}

		private async void AllDoneClicked(object sender, EventArgs args)
		{
			text = Editor.Text;

			if (string.IsNullOrEmpty(text) || photos.Count < 1) {
				return;
			}

			var current = App.Database.GetDeclarations();
			bool found = false;
			foreach (var decl in current) {
				if (decl.Text == text) {
					found = true;
					break;
				}
			}

			if (!found) {
				App.Database.SaveDeclaration(new Declaration {Text = text});
			}

			await Navigation.PushAsync(new Overview(profile, text, photos));
		}
	}
}