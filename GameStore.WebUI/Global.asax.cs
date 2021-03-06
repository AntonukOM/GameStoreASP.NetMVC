﻿using GameStore.Domain.Entities;
using GameStore.WebUI.Infrastructure.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data.Entity;
using GameStore.Domain.Concrete;
using System.IO;

namespace GameStore.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data"));
            //Database.SetInitializer<GameDbContext>(new DropCreateDatabaseIfModelChanges<GameDbContext>());
            Database.SetInitializer<GameDbContext>(null);
        }
    }
}
