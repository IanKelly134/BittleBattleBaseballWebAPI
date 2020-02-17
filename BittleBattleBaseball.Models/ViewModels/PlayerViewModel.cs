using System;

namespace BittleBattleBaseball.Models.ViewModels
{
    public class PlayerViewModel
    {
        public int Id { get; set; }

        public string PlayerName { get; set; }

        public string NickName { get; set; }

        //public string FirstInitial { get; set; }

        //public string LastName { get; set; }

        public string Position { get; set; }       
        
        public string Bats { get; set; }

        //public int Weight { get; set; }

        //public string Height { get; set; }

        public string Throws { get; set; }

        public DateTime DOB { get; set; }

        public string BbRefHref
        {
            get
            {

                if (this.PlayerName != null && this.PlayerName.Contains(" "))
                {
                    string returnVal = "https://www.baseball-reference.com/players/";
                    string lastInitial = PlayerName.Substring(PlayerName.IndexOf(" ") + 1, 1);
                    var lastNameLength = PlayerName.Length - (PlayerName.IndexOf(" ") + 1);
                    string lastName = PlayerName.Substring(PlayerName.IndexOf(" ") + 1, lastNameLength >= 5 ? 5 : lastNameLength);
                    returnVal += lastInitial + "/" + lastName;
                    returnVal = returnVal + PlayerName.Substring(0, 2) + "01.shtml";
                    return returnVal.ToLower();
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// PlayerImageURL
        /// </summary>
        public string PlayerImageURL { get; set; }
    }
}
