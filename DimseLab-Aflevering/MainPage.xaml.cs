using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DimseLab_Aflevering.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DimseLab_Aflevering
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string _searchString;
        private StringBuilder _sBuilder;

        public MainPage()
        {
            this.InitializeComponent();
            SBuilder = new StringBuilder();
        }

        private void SearchForUsersForEveryLetterTyped(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Back)
            {
                if (SBuilder.Length != 0)
                {
                    SBuilder.Length--;
                }
            }
            else
            {
                SBuilder.Append(e.Key);
                SearchString = SBuilder.ToString();
                ModelController.Instance.SearchForUsers(SearchString);
            }
        }

        private void SearchForDoohickeysByLetter(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Back)
            {
                if (SBuilder.Length != 0)
                {
                    SBuilder.Length--;
                }
            }
            else
            {
                SBuilder.Append(e.Key);
                SearchString = SBuilder.ToString();
                ModelController.Instance.SearchForDoohickeys(SearchString);
            }
        }

        private void EmptySearchString(object sender, RoutedEventArgs e)
        {
            SearchString = "";
            SBuilder.Clear();
        }

        #region Properties

        public string SearchString
        {
            get { return _searchString; }
            set { _searchString = value; }
        }

        public StringBuilder SBuilder
        {
            get { return _sBuilder; }
            set { _sBuilder = value; }
        }

        #endregion

        
    }
}
