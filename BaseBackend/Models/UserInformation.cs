namespace BaseBackend.Models
{
    public class UserBase
    {
        public string username { get; set; }
        public string password { get; set; }

        public string[] roles { get; set; }
    }

    public class UserInformation : UserBase
    {
        public string email { get; set; }
        public string phone { get; set; }
        public int credits { get; set; }
        public bool isActivated { get; set; }
        public bool isBlocked { get; set; }
        public string adress { get; set; }

        public UserInformation(string username)
        {
            this.username = username;
        }

    }
}
