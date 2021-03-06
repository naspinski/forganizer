﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Core.Resource;
using System.Reflection;
using System.Web.Mvc;
using Castle.Core;

namespace Forganizer.WebUI
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        WindsorContainer container;

        public WindsorControllerFactory()
        {
            container = new WindsorContainer(new XmlInterpreter(new ConfigResource("castle")));
            var controllerTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                                  where typeof(IController).IsAssignableFrom(t)
                                  select t;
            foreach (Type t in controllerTypes) container.AddComponentLifeStyle(t.FullName, t, LifestyleType.Transient);
        }

        protected override IController GetControllerInstance(Type controllerType)
        {
            try { return (IController)container.Resolve(controllerType); }
            catch (ArgumentNullException ex) { throw new HttpException(404, ex.Message); }
            catch (Exception ex) { throw ex; }
        }
    }
}
