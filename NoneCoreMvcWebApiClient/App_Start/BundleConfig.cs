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
            ScriptBundle scriptBndl = new ScriptBundle("~/bundles/scripts");
            scriptBndl.Include("~/lib/jquery/jquery.js").Include("~/lib/jquery/validation/jquery.validate.js").Include("~/lib/jquery/validation/unobtrusive/jquery.validate.unobtrusive.js").Include("~/lib/bootstrap/js/bootstrap.js");

            bundles.Add(scriptBndl);

            scriptBndl = new ScriptBundle("~/bundles/bootstrap");
            scriptBndl.Include("~/lib/bootstrap/css/bootstrap.css");

            bundles.Add(scriptBndl);

            BundleTable.EnableOptimizations = true;

        }
    }
}