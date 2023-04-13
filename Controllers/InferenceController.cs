using System.Collections.Generic;
using System.Linq;
using INTEXII.Models;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient.Server;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace INTEXIIModelAPI.Controllers
{
    [ApiController]
    [Route("/score")]
    public class InferenceController : ControllerBase
    {
        private InferenceSession _session;

        public InferenceController(InferenceSession session)
        {
            _session = session;
        }

        [HttpPost]
        public ActionResult Score([FromForm] Prediction data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });
            Tensor<string> wrapping = result.First().AsTensor<string>();
            var prediction = new PredictionResponse { PredictedWrap = wrapping.First() };
            result.Dispose();
            return RedirectToAction("ValueOutput", "Home", prediction);
        }

        [HttpGet]
        public ActionResult Score(PredictionResponse resp)
        {
            return RedirectToAction("Inference/Score", resp);
        }
    }
}
