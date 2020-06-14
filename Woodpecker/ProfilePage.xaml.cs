using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Woodpecker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProfilePage : ContentPage
	{
		private readonly bool editing;
		public ProfilePage(bool editing)
		{
			this.editing = editing;
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			if (!editing) {
				DeleteButton.IsVisible = false;
			}
			base.OnAppearing();
		}

		async void SaveButtonClicked(object sender, EventArgs args)
		{
			var profile = (Profile) BindingContext;
			if (
				!string.IsNullOrEmpty(profile.Name) &&
				!string.IsNullOrEmpty(profile.Email) &&
				!string.IsNullOrEmpty(profile.Phone) &&
				!string.IsNullOrEmpty(profile.DestRegion) &&
				!string.IsNullOrEmpty(profile.Unit) &&
				!string.IsNullOrEmpty(profile.Region)
				) {
				App.Database.SaveProfile(profile);
				await Navigation.PopAsync();
			}
		}

		async void DeleteButtonClicked(object sender, EventArgs args)
		{
			var profile = (Profile) BindingContext;
			App.Database.DeleteProfile(profile.Id);
			await Navigation.PopAsync();
		}

		async void BackButtonClicked(object sender, EventArgs args)
		{
			await Navigation.PopAsync();
		}
	}
}