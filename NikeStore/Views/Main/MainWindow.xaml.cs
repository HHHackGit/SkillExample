using System;
using System.Windows;

using NikeStore.Navigation;

namespace NikeStore
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			NavigationManager.AddNavigationService(HeaderRegion.Name, HeaderRegion.NavigationService);
			NavigationManager.AddNavigationService(MainRegion.Name, MainRegion.NavigationService);
		}
	}
}
