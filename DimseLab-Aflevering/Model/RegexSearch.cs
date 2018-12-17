using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using DimseLab_Aflevering.Model;

namespace DimseLab_Aflevering
{
    public class RegexSearch 
    {

        /// <summary>
        /// Søger efter brugere
        /// </summary>
        /// <param name="SearchTerm">Søgestring</param>
        /// <param name="inputList">Liste af brugere, der skal søges i</param>
        /// <returns></returns>
        public static ObservableCollection<User> SearchUsers(string SearchTerm, ObservableCollection<User> inputList)
        {
            ObservableCollection<User> FilteredList = new ObservableCollection<User>();

            if (SearchTerm != null)
            {
                string pattern = SearchTerm; //det pattern vi søger efter
                RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled; //en option der gør at vi ignorer forskellen på store og små bogstaver og noget andet gøjl


                foreach (User user in inputList)
                {

                    string text = user.FirstName + " " + user.LastName; //den tekst vi søger i
                    Regex optionRegex = new Regex(pattern, options); //tager pattern og options og laver et Regex object der gør det faktiske arbejde

                    //her sker magien
                    if (optionRegex.IsMatch(text))
                    {
                        FilteredList.Add(user);
                    }


                }
            }

            //returerner en liste med søgeresultater
            return FilteredList;
        }

        /// <summary>
        /// Søger efter dimser
        /// </summary>
        /// <param name="SearchTerm">Søgestring</param>
        /// <param name="inputList">Liste af dimser, der skal søges i</param>
        /// <returns></returns>
        public static ObservableCollection<Doohickey> SearchDoohickeys(string SearchTerm, ObservableCollection<Doohickey> inputList)
        {
            ObservableCollection<Doohickey> FilteredList = new ObservableCollection<Doohickey>();

            if (SearchTerm != null)
            {
                string pattern = SearchTerm; //det pattern vi søger efter
                RegexOptions options = RegexOptions.IgnoreCase | RegexOptions.Compiled; //en option der gør at vi ignorer forskellen på store og små bogstaver og noget andet gøjl


                foreach (Doohickey doohickey in inputList)
                {

                    string text = doohickey.Name; //den tekst vi søger i
                    Regex optionRegex = new Regex(pattern, options); //tager pattern og options og laver et Regex object der gør det faktiske arbejde

                    //her sker magien
                    if (optionRegex.IsMatch(text))
                    {
                        FilteredList.Add(doohickey);
                    }


                }
            }

            //returerner en liste med søgeresultater
            return FilteredList;
        }
    }
}