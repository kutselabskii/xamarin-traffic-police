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
	public partial class PhotoPage : ContentPage
	{
		private Photo photo;

		public PhotoPage(Photo photo)
		{
			this.photo = photo;
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			Img.Source = photo.Source;
			base.OnAppearing();
		}
	}
}