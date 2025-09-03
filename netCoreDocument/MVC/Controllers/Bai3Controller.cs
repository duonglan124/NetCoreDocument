namespace MVC.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MVC.Models;

    public class Bai3Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Bai3 model)
        {
            double bmi = 0;
            string message = "";

            if (model.Height > 0 && model.Weight > 0)
            {
                bmi = model.Weight / (model.Height * model.Height);

                if (bmi < 18.5)
                {
                    message = $"BMI = {bmi:F2} → Gầy";
                }
                else if (bmi < 24.9)
                {
                    message = $"BMI = {bmi:F2} → Bình thường";
                }
                else if (bmi < 29.9)
                {
                    message = $"BMI = {bmi:F2} → Thừa cân";
                }
                else
                {
                    message = $"BMI = {bmi:F2} → Béo phì";
                }
            }
            else
            {
                message = "Vui lòng nhập chiều cao và cân nặng hợp lệ!";
            }

            ViewBag.Result = message;
            return View();
        }
    }
}
