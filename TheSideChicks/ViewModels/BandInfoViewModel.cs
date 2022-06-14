using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.ViewModels
{
    public partial class BandInfoViewModel : BaseViewModel
    {


        [ICommand]
        async Task GoToFaceBookAsync()
        {
            try
            {
                Uri uri = new Uri("https://www.facebook.com");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }

        }

        [ICommand]
        async Task GoToInstagramAsync()
        {
            try
            {
                Uri uri = new Uri("https://www.instagram.com");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }

        }
        [ICommand]
        async Task GoEmailAsync()
        {
            try
            {
                var emailMessage = new EmailMessage();
                emailMessage.To = new List<String> { "Bart-Grootoonk@hotmail.com" };
                emailMessage.Subject = "Seeking contact";
                emailMessage.Body = "Type all your questions and we will get back to you soon";

                await Email.ComposeAsync(emailMessage);
            }
            catch (Exception ex)
            {
                // An unexpected error occured. No browser may be installed on the device.
            }

        }
    }
}
