using System;
namespace Activity.DAL.Entity
{
    public class AppSetting
    {
        public string ImageUrl { get; set; }

        public string FileFolder { get; set; }
        public string ImageFolder { get; set; }

        public string FileUrl { get; set; }

        public string FireBaseUrl { get; set; }
        public string ShareRoomUrl { get; set; }

        public string FireBaseToken { get; set; }

        public int? GameCompletedPercent { get; set; }

        public string AndroidValidateSubscriptionUrl { get; set; }

        public string IosValidateSubscriptionUrl { get; set; }

        public string IosPassword { get; set; }
        public string JwtToken { get; set; }

    }
}
