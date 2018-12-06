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
        private string _searchStringUser;
        private string _searchStringDoohickey;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SearchForUsersForEveryLetterTyped(object sender, KeyRoutedEventArgs e)
        {
            StringBuilder SBuilder = new StringBuilder();

            switch (e.Key)
            {
                case VirtualKey.Back:
                    if (SearchStringUser.Length != 0)
                    {
                        SearchStringUser = SearchStringUser.Remove(SearchStringUser.Length - 1);
                    }
                    break;
                case VirtualKey.Accept:
                    break;
                case VirtualKey.Add:
                    break;
                case VirtualKey.Application:
                    break;
                case VirtualKey.Cancel:
                    break;
                case VirtualKey.CapitalLock:
                    break;
                case VirtualKey.Clear:
                    break;
                case VirtualKey.Control:
                    break;
                case VirtualKey.Convert:
                    break;
                case VirtualKey.Decimal:
                    break;
                case VirtualKey.Delete:
                    break;
                case VirtualKey.Divide:
                    break;
                case VirtualKey.End:
                    break;
                case VirtualKey.Enter:
                    break;
                case VirtualKey.Escape:
                    break;
                case VirtualKey.Execute:
                    break;
                case VirtualKey.F1:
                    break;
                case VirtualKey.F2:
                    break;
                case VirtualKey.F3:
                    break;
                case VirtualKey.F4:
                    break;
                case VirtualKey.F5:
                    break;
                case VirtualKey.F6:
                    break;
                case VirtualKey.F7:
                    break;
                case VirtualKey.F8:
                    break;
                case VirtualKey.F9:
                    break;
                case VirtualKey.F10:
                    break;
                case VirtualKey.F11:
                    break;
                case VirtualKey.F12:
                    break;
                case VirtualKey.Scroll:
                    break;
                case VirtualKey.Print:
                    break;
                case VirtualKey.Pause:
                    break;
                case VirtualKey.Insert:
                    break;
                case VirtualKey.Home:
                    break;
                case VirtualKey.PageDown:
                    break;
                case VirtualKey.PageUp:
                    break;
                case VirtualKey.NumberKeyLock:
                    break;
                case VirtualKey.Left:
                    break;
                case VirtualKey.LeftButton:
                    break;
                case VirtualKey.Up:
                    break;
                case VirtualKey.Down:
                    break;
                case VirtualKey.Right:
                    break;
                case VirtualKey.RightButton:
                    break;
                case VirtualKey.RightControl:
                    break;
                case VirtualKey.LeftWindows:
                    break;
                case VirtualKey.RightWindows:
                    break;
                case VirtualKey.RightShift:
                    break;
                case VirtualKey.LeftShift:
                    break;
                case VirtualKey.LeftControl:
                    break;
                case VirtualKey.Tab:
                    break;
                case VirtualKey.Shift:
                    break;
                case VirtualKey.Space:
                    SearchStringUser = SearchStringUser.PadRight(SearchStringUser.Length+1);
                    break;
                default:
                    SBuilder.Append(SearchStringUser + e.Key);
                    SearchStringUser = SBuilder.ToString();
                    break;
            }
            ModelController.Instance.SearchForUsers(SearchStringUser);
        }

        private void SearchForDoohickeysByLetter(object sender, KeyRoutedEventArgs e)
        {
            StringBuilder SBuilder = new StringBuilder();

            switch (e.Key)
            {
                case VirtualKey.Back:
                    if (SearchStringDoohickey.Length != 0)
                    {
                        SearchStringDoohickey = SearchStringDoohickey.Remove(SearchStringDoohickey.Length - 1);
                    }
                    break;
                case VirtualKey.Accept:
                    break;
                case VirtualKey.Add:
                    break;
                case VirtualKey.Application:
                    break;
                case VirtualKey.Cancel:
                    break;
                case VirtualKey.CapitalLock:
                    break;
                case VirtualKey.Clear:
                    break;
                case VirtualKey.Control:
                    break;
                case VirtualKey.Convert:
                    break;
                case VirtualKey.Decimal:
                    break;
                case VirtualKey.Delete:
                    break;
                case VirtualKey.Divide:
                    break;
                case VirtualKey.End:
                    break;
                case VirtualKey.Enter:
                    break;
                case VirtualKey.Escape:
                    break;
                case VirtualKey.Execute:
                    break;
                case VirtualKey.F1:
                    break;
                case VirtualKey.F2:
                    break;
                case VirtualKey.F3:
                    break;
                case VirtualKey.F4:
                    break;
                case VirtualKey.F5:
                    break;
                case VirtualKey.F6:
                    break;
                case VirtualKey.F7:
                    break;
                case VirtualKey.F8:
                    break;
                case VirtualKey.F9:
                    break;
                case VirtualKey.F10:
                    break;
                case VirtualKey.F11:
                    break;
                case VirtualKey.F12:
                    break;
                case VirtualKey.Scroll:
                    break;
                case VirtualKey.Print:
                    break;
                case VirtualKey.Pause:
                    break;
                case VirtualKey.Insert:
                    break;
                case VirtualKey.Home:
                    break;
                case VirtualKey.PageDown:
                    break;
                case VirtualKey.PageUp:
                    break;
                case VirtualKey.NumberKeyLock:
                    break;
                case VirtualKey.Left:
                    break;
                case VirtualKey.LeftButton:
                    break;
                case VirtualKey.Up:
                    break;
                case VirtualKey.Down:
                    break;
                case VirtualKey.Right:
                    break;
                case VirtualKey.RightButton:
                    break;
                case VirtualKey.RightControl:
                    break;
                case VirtualKey.LeftWindows:
                    break;
                case VirtualKey.RightWindows:
                    break;
                case VirtualKey.RightShift:
                    break;
                case VirtualKey.LeftShift:
                    break;
                case VirtualKey.LeftControl:
                    break;
                case VirtualKey.Tab:
                    break;
                case VirtualKey.Shift:
                    break;
                case VirtualKey.Space:
                    SearchStringDoohickey = SearchStringDoohickey.PadRight(SearchStringDoohickey.Length+1);
                    break;
                default:
                    SBuilder.Append(SearchStringDoohickey + e.Key);
                    SearchStringDoohickey = SBuilder.ToString();
                    break;
            }
            ModelController.Instance.SearchForDoohickeys(SearchStringDoohickey);
        }

        private void EmptySearchString(object sender, RoutedEventArgs e)
        {
            
        }

        #region Properties

        public string SearchStringUser
        {
            get { return _searchStringUser; }
            set { _searchStringUser = value; }
        }

        public string SearchStringDoohickey
        {
            get { return _searchStringDoohickey; }
            set { _searchStringDoohickey = value; }
        }

        #endregion


    }
}
