using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using TestGame.Core;

namespace TestGameWebApp.Controllers
{
    public class GameController : Controller
    {
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        ////
        //// GET: /Game/

        public JsonResult Index(int bucket1, int bucket2, int target)
        {
             IEnumerable<StateTransition> steps = GameSolver.Solve(bucket1, bucket2, target);

             string result = serializer.Serialize(new { Steps = steps.ToArray(), Message = string.Empty});

             return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
