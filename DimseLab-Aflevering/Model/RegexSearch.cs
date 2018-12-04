using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace DimseLab_Aflevering
{
    public class RegexSearch
    {

        /*
        ██▓    ▄▄▄       ██▀███    ██████     ██▒   █▓ ▄▄▄       ██▀███      ██░ ██ ▓█████  ██▀███  
        ▓██▒   ▒████▄    ▓██ ▒ ██▒▒██    ▒    ▓██░   █▒▒████▄    ▓██ ▒ ██▒   ▓██░ ██▒▓█   ▀ ▓██ ▒ ██▒
        ▒██░   ▒██  ▀█▄  ▓██ ░▄█ ▒░ ▓██▄       ▓██  █▒░▒██  ▀█▄  ▓██ ░▄█ ▒   ▒██▀▀██░▒███   ▓██ ░▄█ ▒
        ▒██░   ░██▄▄▄▄██ ▒██▀▀█▄    ▒   ██▒     ▒██ █░░░██▄▄▄▄██ ▒██▀▀█▄     ░▓█ ░██ ▒▓█  ▄ ▒██▀▀█▄  
        ░██████▒▓█   ▓██▒░██▓ ▒██▒▒██████▒▒      ▒▀█░   ▓█   ▓██▒░██▓ ▒██▒   ░▓█▒░██▓░▒████▒░██▓ ▒██▒
        ░ ▒░▓  ░▒▒   ▓▒█░░ ▒▓ ░▒▓░▒ ▒▓▒ ▒ ░      ░ ▐░   ▒▒   ▓▒█░░ ▒▓ ░▒▓░    ▒ ░░▒░▒░░ ▒░ ░░ ▒▓ ░▒▓░
        ░ ░ ▒  ░ ▒   ▒▒ ░  ░▒ ░ ▒░░ ░▒  ░ ░      ░ ░░    ▒   ▒▒ ░  ░▒ ░ ▒░    ▒ ░▒░ ░ ░ ░  ░  ░▒ ░ ▒░
        ░ ░    ░   ▒     ░░   ░ ░  ░  ░          ░░    ░   ▒     ░░   ░     ░  ░░ ░   ░     ░░   ░ 
        ░  ░     ░  ░   ░           ░           ░        ░  ░   ░         ░  ░  ░   ░  ░   ░     
        ░                                                 

            Hvasså kællinger???????

            Her er der lige en gratis gave fra Lars til jer numsehuller der koder! Jeg ved at vi skal søge på noget shit senere, så jeg har brudt sprintens etik og låner jeg hermed noget kode jeg alligevel havde liggende fra tidligere
            Den er gratis og kommenteret, motherfuckers



            "Know what I'm saying?" J-Roc


        */

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
    }
}