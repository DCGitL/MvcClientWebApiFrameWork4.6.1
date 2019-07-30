using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace NoneCoreMvcWebApiClient.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add( new ScriptBundle("~/bundles/scripts").Include(
             "~/lib/jquery/jquery.js",
             "~/lib/jquery/validation/jquery.validate*",
              "~/lib/jquery/validation/unobtrusive/jquery.validate.unobtrusive*",
              "~/lib/bootstrap/js/bootstrap.js"
             ));
       

            bundles.Add( new StyleBundle("~/bundles/bootstrap").Include(
             "~/lib/bootstrap/css/bootstrap.css"));

            BundleTable.EnableOptimizations = true;

        }
    }
}