using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Obgyn.HypertensionCalculator.Pages
{
    public class CalculatorModel : PageModel
    {
        private string ProgramPath = "C:\\OrchardCore\\src\\Obgyn.DEV\\Obgyn.Modules.Cms\\Obgyn.HypertensionCalculator\\Scripts\\input.py";

        [BindProperty]
        public double MaternalAge { get; set; }
        [BindProperty]
        public double GestationalAge { get; set; }
        [BindProperty]
        public double PrepregnancyBmi { get; set; }
        [BindProperty]
        public double MaxSystolicBpLabor { get; set; }
        [BindProperty]
        public double MaxSystolicBp24 { get; set; }
        [BindProperty]
        public double MaxSystolicBp48 { get; set; }
        [BindProperty]
        public double MaxDiastolicBpLabor { get; set; }
        [BindProperty]
        public double MaxDiastolicBp24 { get; set; }
        [BindProperty]
        public double MaxDiastolicBp48 { get; set; }
        [BindProperty]
        public bool Ibuprofen { get; set; }
        [BindProperty]
        public bool HydralazineIv { get; set; }
        [BindProperty]
        public bool LabetalolIv { get; set; }
        [BindProperty]
        public bool LabetalolOral { get; set; }
        [BindProperty]
        public bool NifedipineIr { get; set; }
        [BindProperty]
        public bool NifedipineXr { get; set; }

        public string Result { get; private set; }

        public void OnGet()
        {
            
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(MaxSystolicBp24);
            sb.Append(' ');
            sb.Append(MaxDiastolicBp24);
            sb.Append(' ');
            sb.Append(MaxSystolicBp48);
            sb.Append(' ');
            sb.Append(MaxDiastolicBp48);
            sb.Append(' ');
            sb.Append(PrepregnancyBmi);
            sb.Append(' ');
            sb.Append(GestationalAge);
            sb.Append(' ');
            sb.Append(MaternalAge);
            sb.Append(' ');
            sb.Append(MaxSystolicBpLabor);
            sb.Append(' ');
            sb.Append(MaxDiastolicBpLabor);
            sb.Append(' ');
            sb.Append(Ibuprofen ? 1 : 0);
            sb.Append(' ');
            sb.Append(NifedipineIr ? 1 : 0);
            sb.Append(' ');
            sb.Append(NifedipineXr ? 1 : 0);
            sb.Append(' ');
            sb.Append(LabetalolIv ? 1 : 0);
            sb.Append(' ');
            sb.Append(LabetalolOral ? 1 : 0);
            sb.Append(' ');
            sb.Append(HydralazineIv ? 1 : 0);

            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = "python.exe",
                Arguments = string.Format("{0} {1}", ProgramPath, sb.ToString()),
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                RedirectStandardError = true
            };

            Process process = Process.Start(start);
            process.WaitForExit();
            StreamReader reader = process.StandardOutput;
            StreamReader errorReader = process.StandardError;

            //Result = reader.ReadToEnd() + errorReader.ReadToEnd();

            string temp = reader.ReadToEnd().TrimEnd() + errorReader.ReadToEnd().TrimEnd();

            decimal temp1 = decimal.Parse(temp);

            Result = String.Format("{0:P2}", temp1);

            return Page();
        }
    }
}
