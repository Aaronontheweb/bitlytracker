using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using BitlyTracker.Config;
using BitlyTracker.Helpers;
using BitlyTracker.Models;
using BitlyTracker.Service;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;
using BitlyTracker.ViewModels;
using BitlyTracker.Converters;

namespace BitlyTracker
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly IRepository _repository;
        private readonly IBitlyService _bitlyService;

        public ObservableCollection<UserClick> BoundClicks { get; set; }
        public IList<UserClick> Clicks { get; set; }

        // Constructor
        public MainPage()
        {
            _repository = new IsolatedStorageRepository();
            _bitlyService = new BitlyService(BitlyApiSettings.DefaultSettings);

            InitializeComponent();

            // Set the data context of the listbox control to the sample data
            //DataContext = App.ViewModel;
            this.Loaded +=new RoutedEventHandler(MainPage_Loaded);
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //Check to see if a user is logged in or not
            var currentUser = _repository.GetCurrentUser();
            BoundClicks = new ObservableCollection<UserClick>();
            Clicks = new List<UserClick>();

            if (currentUser.GetType() == typeof(MissingBitlyLogin))
            {
                ShowPopup();
            }
            else
            {
                DownloadUserClicks(currentUser);
            }
        }

        #region Login Popup Controls

        private void ShowPopup()
        {
            LoginPopup.IsOpen = true;
            ContentPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void HidePopup()
        {
            LoginPopup.IsOpen = false;
            ContentPanel.Visibility = System.Windows.Visibility.Visible;
        }

        private void HideBrowserUi()
        {
            NavigationPanel.Visibility = System.Windows.Visibility.Collapsed;
            PopupContentPanel.Visibility = System.Windows.Visibility.Visible;
        }

        private void ShowBrowserUi()
        {
            NavigationPanel.Visibility = System.Windows.Visibility.Visible;
            PopupContentPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void SaveUserAndGotoMain(BitlyLogin login)
        {
            if (login.GetType() == typeof(MissingBitlyLogin))
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show("Unable to log you in. Please try again!");
                    HideBrowserUi();
                });

            }
            else
            {
                _repository.SaveUser(login);
                Dispatcher.BeginInvoke(HidePopup);
                DownloadUserClicks(login);
            }
        }

        private void DownloadUserClicks(BitlyLogin login)
        {
            _bitlyService.BeginUserClicks(login, 7, (request, response, userState) =>
            {
                var userClicks = _bitlyService.EndUserClicks(response);
                Clicks = UserClicksConverter.ExtractClicks(userClicks);                

                Dispatcher.BeginInvoke(() =>
                {
                    if (Clicks.GetType() == typeof(MissingBitlyUserClicksResponse))
                    {
                        MessageBox.Show("Unable to download click data from Bit.ly :( ");
                    }
                    else
                    {
                        TotalClicks.Text = "Total Clicks: " + userClicks.data.total_clicks;
                        TotalDays.Text = "Total Days: " + userClicks.data.Days;
                    }

                    
                });
            });
        }

        private void btnOAuthBegin_Click(object sender, RoutedEventArgs e)
        {
            ShowBrowserUi();
            var navigateUrl = _bitlyService.GetAuthorizeUrl();
            browserSignIn.Navigate(new Uri(navigateUrl));
        }

        private void btnCancelLogin_Click(object sender, RoutedEventArgs e)
        {
            HideBrowserUi();
        }

        private void browserSignIn_Navigating(object sender, NavigatingEventArgs e)
        {
            //Don't do anything else if the application isn't be redirected to the URI specified
            if (!e.Uri.ToString().Contains(_bitlyService.Settings.RedirectUrl)) return;

            //Extract the code parameter from the query string and determine if authorization was successful
            var parameters = ParameterHelper.ExtractQueryParameters(e.Uri.Query);

            //Do we have a code parameter waiting for us?
            if (parameters.ContainsKey("code"))
            {
                _bitlyService.BeginGetAccessToken(parameters["code"], (request, response, userState) =>
                {
                    var login = _bitlyService.EndGetAccessToken(response);
                    SaveUserAndGotoMain(login);
                });
            }

        }

        #endregion
    }
}