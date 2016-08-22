using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Bookify.App.Sdk.Exceptions;

namespace Bookify.App.iOS.Ui.Helpers
{
    public static class SharedMethods
    {
        public static async Task<T> TryTask<T>(IUserDialogs userDialogs, Func<Task<T>> operation, string badRequest = null, string unauthorized = null, string notFound = null, string defaultMsg = null)
        {
            try
            {
                return await operation();
            }
            catch (HttpResponseException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
            {
                await userDialogs.AlertAsync(badRequest ?? "Der var fejl i det angivne data", "Data fejl");
            }
            catch (HttpResponseException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
            {
                await userDialogs.AlertAsync(unauthorized ?? "Login er krævet for at bruge denne funktion. Log venligst ind", "Login mangler");
            }
            catch (HttpResponseException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                await userDialogs.AlertAsync(notFound ?? "Det anmodne data kunne ikke findes på serveren", "Ikke fundet");
            }
            catch (HttpResponseException)
            {
                await userDialogs.AlertAsync(defaultMsg ?? "En ukendt fejl opstod på serveren. Prøv igen senere", "Fejl");
            }
            catch (Exception)
            {
                await userDialogs.AlertAsync("En ukendt fejl opstod. Prøv igen senere", "Fejl");
            }
            return default(T);
        }

        public static async Task TryTask(IUserDialogs userDialogs, Func<Task> operation, string badRequest = null, string unauthorized = null, string notFound = null, string defaultMsg = null)
        {
            await TryTask(
                userDialogs, 
                async () =>
                {
                    await operation();
                    return true;
                },
                badRequest,
                unauthorized,
                notFound,
                defaultMsg);
        }
    }
}
