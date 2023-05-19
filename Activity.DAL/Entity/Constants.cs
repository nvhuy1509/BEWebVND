using System;
using System.ComponentModel.DataAnnotations;

namespace Activity.DAL.Entity
{
    public static class Constants
    {
        public const string ShortDatePattern = "dd/MM/yyyy";

        public const string FullDatePattern = "dd/MM/yyyy HH:mm:ss";

        public const string PasswordNoChange = "[No Change]";

        public const string PROPERTY_TYPE_SOUND = "SOUND";

        public const string PROPERTY_TYPE_IMAGE = "IMAGE";

        public const string PROPERTY_TYPE_SPINE = "SPINE";

        public const string PAGE_NOT_PERMISSION = "NotPermission";


        public const decimal GameCompletedPercent = (decimal)1;

        public static string ContentImages = "content_images";

        public static class ApiAuthentication
        {
            public const string UserName = "KidyVN";
            public const string Password = "Vk5LaWR5JDIwMjAhQCM="; //base64 VNKidy$2020!@#  
            public const string Secret = "W3TTOKVp9AsFPylOFsxBXM8VogOKzVMS";
        }
        public static class ModuleExtension
        {
            public static string ContentImages = "content_images";
            public static string Image = "IMAGE";
            public static string Sound = "SOUND";
            public static string Spine = "SPINE";
        }
        public static class LocalizedStrings
        {
            [Display(Name = "CREATE_FOR")]
            public const string CreateFor = "Create {0}";
            [Display(Name = "EDIT_FOR")]
            public const string EditFor = "Edit {0}";
            [Display(Name = "MANAGE_FOR")]
            public const string ManageFor = "Manage {0}";
            [Display(Name = "SAVE_SUCCESS")]
            public const string SaveSuccess = "Successfully saved changes.";
            [Display(Name = "CONFIRM_DELETE_RECORD")]
            public const string ConfirmDeleteRecord = "Are you sure that you want to delete this record?";
            [Display(Name = "CONFIRM_DEACTIVE_RECORD")]
            public const string ConfirmDeactiveRecord = "Are you sure that you want to deactive this record?";
            [Display(Name = "CONFIRM_REACTIVE_RECORD")]
            public const string ConfirmReactiveRecord = "Are you sure that you want to reactive this record?";
            [Display(Name = "CONFIRM_UN_BLOCK_RECORD")]
            public const string ConfirmUnBlockRecord = "Are you sure that you want to unblock this record?";
            public static class Validation
            {
                public const string Date = "Vui lòng nhập một ngày hợp lệ.";
                public const string Digits = "Vui lòng chỉ nhập các chữ số.";
                public const string Email = "Vui lòng nhập một địa chỉ email hợp lệ.";
                public const string EqualTo = "Vui lòng nhập lại cùng một giá trị.";
                public const string MaxLength = "Vui lòng nhập không quá {0} ký tự.";
                public const string MinLength = "Vui lòng nhập ít nhất {0} ký tự.";
                public const string Number = "Vui lòng nhập một số hợp lệ.";
                public const string PhoneNumber = "Xin vui lòng nhập một số điện thoại hợp lệ.";
                public const string Range = "Vui lòng nhập giá trị từ {0} đến {1}.";
                public const string RangeLength = "Vui lòng nhập một giá trị có độ dài từ {0} đến {1} ký tự.";
                public const string RangeMax = "Vui lòng nhập giá trị nhỏ hơn hoặc bằng {0}.";
                public const string RangeMin = "Vui lòng nhập giá trị lớn hơn hoặc bằng {0}.";
                public const string Required = "Vui lòng nhập một giá trị.";
                public const string Url = "Vui lòng nhập một URL hợp lệ.";
                public const string CreditCard = "Vui lòng nhập số thẻ tín dụng hợp lệ.";
                public const string LettersOnly = "Vui lòng chỉ nhập các chữ cái.";
                public const string EmailWasExisted = "Email đã tồn tại. Vui lòng sử dụng email khác!";
                public const string PhoneWasExisted = "Số điện thoại đã tồn tại. Vui lòng sử dụng điện thoại khác !";
                public const string UserName = "Vui lòng nhập một tên người dùng hợp lệ.";
                public const string Password = "Vui lòng nhập mật khẩu hoặc xác nhận mật khẩu !.";
                public const string PasswordNotSame = "Mật khẩu và mật khẩu xác nhận không giống nhau !";
                public const string Login = "Vui lòng nhập tên người dùng (email) hoặc mật khẩu hợp lệ.";
                public const string block = "Tài khoản bị khóa";

                public const string OldPassword = "Tên người dùng hoặc mật khẩu hiện tại không đúng!";
                public const string NewPasswordConfirm = "Mật khẩu mới và xác nhận mật khẩu mới không giống nhau !";
                public const string NewPassword = "Vui lòng nhập mật khẩu mới hoặc xác nhận mật khẩu mới !.";
                public const string ValidatePhoneNumber = "Vui lòng nhập số điện thoại chính xác !.";

                public const string RegisterSuccess = "Đăng ký thành công!.";
                public const string OtpFalse = "Sai mã Otp!.";
                public const string OtpTimeOut = "Hết thời gian Otp.";
                public const string AccountDoesNotExist = "Tài khoản không tồn tại.";
                public const string ForGotPassSuccess = "Gửi yêu cầu quên mật khẩu thành công.";
            }

            [Display(Name = "INVALID_ACCESS_KEY")]
            public const string Invalid_Access_Key = "Không tìm thấy khóa truy cập!";

            [Display(Name = "ERROR_PARRAM_REQUEST")]
            public const string Error_Parram_Request = "Yêu cầu thiếu parram. Vui lòng gửi đúng parram!";

            [Display(Name = "LESSON_NOT_FOUND")]
            public const string Lesson_Not_Found = "Bài học không được tìm thấy !!!";

            [Display(Name = "USER_NOT_ACTIVE_ON_GAME")]
            public const string User_Not_Active_On_Game = "Người dùng không hoạt động trên trò chơi. Vui lòng kích hoạt người dùng trước khi gửi dữ liệu !!!";

            [Display(Name = "USER_UNIT_VALUE_EMPTY")]
            public const string USER_UNIT_VALUE_EMPTY = "Tài khoản của bạn chưa xem được quyển sách này.";

            [Display(Name = "USER_LESSON_VALUE_EMPTY")]
            public const string USER_LESSON_VALUE_EMPTY = "Bài học yêu cầu không có dữ liệu, Vui lòng gửi dữ liệu chính xác !!!";

            [Display(Name = "USER_LEVEL_VALUE_EMPTY")]
            public const string USER_LEVEL_VALUE_EMPTY = "Lớp yêu cầu không có dữ liệu, Vui lòng gửi dữ liệu chính xác !!!";

            [Display(Name = "LESSON_GAME_NOT_MAPPING")]
            public const string LESSON_GAME_NOT_MAPPING = "Trò chơi không liên kết với bài học !!!";

            [Display(Name = "NO_DATA")]
            public const string NO_DATA = "không có dữ liệu !!!";
        }

        public static class FireBase
        {
            public const string FireBaseHttpClient = "FireBaseHttpClient";
        }

        public static class FileUpload
        {
            public const string FileUploadHttpClient = "FireBaseHttpClient";
            public const string ChildPath = @"contact/child";
            public const string UserPath = @"contact/user";
            public const string ImageUrl = @"contact/user";
            public const string Thumbnail = @"Thumbnail";
            public const string MMC_User = @"MMC/User";
            public const string MMC_UserNull = @"MMC";
        }
        public static class SocialNotification
        {
            public const string AndroidValidateSubscriptionUrl = "AndroidValidateSubscriptionUrl";
            public const string IosValidateSubscriptionUrl = "IosValidateSubscriptionUrl";
        }
    }
}
