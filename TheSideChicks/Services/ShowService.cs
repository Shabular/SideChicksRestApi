using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSideChicks.Services
{
    public class ShowService
    {
        HttpClient httpClient;
        public ShowService()
        {
            httpClient = new HttpClient();
        }

        List<Show> showList = new List<Show>();

        public async Task<List<Show>> GetShows()
        {
            if (showList?.Count > 0)
                return showList;

            return null;
        }

    }
}
