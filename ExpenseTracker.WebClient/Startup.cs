﻿using ExpenseTracker.WebClient.Helpers;
using Microsoft.Owin;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(ExpenseTracker.WebClient.Startup))]

namespace ExpenseTracker.WebClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "mvc",
                Authority = ExpenseTrackerConstants.IdSrv,
                RedirectUri = ExpenseTrackerConstants.ExpenseTrackerClient,
                SignInAsAuthenticationType = "Cookies",
                ResponseType = "code id_token",
                Scope = "openid profile",
                //Optional notifications so we can see what is going on
                Notifications = new OpenIdConnectAuthenticationNotifications()
                {
                    MessageReceived = async n =>
                    {
                        EndpointAndTokenHelper.DecodeAndWrite(n.ProtocolMessage.IdToken);
                    }
                }
            });
        }
 
    }
}