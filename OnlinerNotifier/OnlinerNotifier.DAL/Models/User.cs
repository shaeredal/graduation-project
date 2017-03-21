using System;
using System.Collections.Generic;

namespace OnlinerNotifier.DAL.Models
{
    public class User
    {
        public int Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string AvatarUri { get; set; }

        public string Email { get; set; }

        public string SocialId { get; set; }

        public string ProviderName { get; set; }

        public DateTime NotificationTime { get; set; }

        public ICollection<UserProduct> UserProducts { get; set; }

        public bool EnableNotifications { get; set; }

        public User()
        {
            UserProducts = new List<UserProduct>();
        }
    }
}
