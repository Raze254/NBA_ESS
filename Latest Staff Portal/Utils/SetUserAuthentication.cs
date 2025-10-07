public static class SetUserAuthentications
{
    //public static void SetUserAuthedication(string UserName, string email, string role)
    //{
    //    try
    //    {
    //        var userModel = new UserViewModel();
    //        userModel.UserName = UserName;
    //        userModel.Email = email;
    //        userModel.RoleName = role;
    //        var userData = string.Format("{0}|{1}|{2}|{3}", userModel.UserName, userModel.UserID, userModel.Email,
    //            userModel.RoleName);
    //        var ticket = new FormsAuthenticationTicket(1, userModel.UserName, DateTime.Now,
    //            DateTime.Now.AddMinutes(1), false, userData);
    //        var encTicket = FormsAuthentication.Encrypt(ticket);

    //        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
    //        Response.Cookies.Add(cookie);
    //    }
    //    catch (Exception ex)
    //    {
    //        ex.Data.Clear();
    //    }
    //}
}
