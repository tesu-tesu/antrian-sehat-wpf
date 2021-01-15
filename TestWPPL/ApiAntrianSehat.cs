using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Velacro.Api;

namespace TestWPPL
{
    public class ApiAntrianSehat
    {
        private static ApiAntrianSehat instance;
        private ApiClient apiClient;

        private ApiAntrianSehat()
        {
            apiClient = new ApiClient("https://api.antrian-sehat.me/api/");
        }


        public static ApiAntrianSehat getInstance()
        {
            if(instance == null)
            {
                instance = new ApiAntrianSehat();
            }
            return instance;
        }

        public ApiClient GetApiClient()
        {
            return this.apiClient;
        }

    }

}
