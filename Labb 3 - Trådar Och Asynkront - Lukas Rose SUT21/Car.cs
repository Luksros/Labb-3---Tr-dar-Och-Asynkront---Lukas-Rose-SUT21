using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb_3___Trådar_Och_Asynkront___Lukas_Rose_SUT21
{
    public class Car
    {
        public string Name { get; set; }
        public decimal Speed { get; set; } = 120;
        public decimal Distance { get; set; } = 0;
        public bool NoErrors { get; set; } = true;
        public int ErrorCountdown { get; set; } = 30;
        public int ErrorTimer { get; set; } = 0;
        public string ErrorMessage { get; set; } = "";

        
        public void Drive()
        {
            while (Distance < 3)
            {
                if (NoErrors) { Distance = Distance + ((Speed / 3600m) / 5); }
                Thread.Sleep(200);
            }
            Cars.finishedCount++;
            Cars.LogAddPrint((Cars.finishedCount == 1) ? $"{Name} har gått i mål och vann racet!\n" : $"{Name} har gått i mål, och kom {Cars.finishedCount}:a!\n");        
        }
        public void Refill()
        {
            NoErrors = false;
            ErrorCountdown = 0;
            ErrorTimer = 30;
            while (ErrorCountdown < ErrorTimer)
            {
                ErrorMessage = $"Slut på bensin! Den måste tanka i {ErrorTimer - ErrorCountdown} sek till!";
                ErrorCountdown++;
                Thread.Sleep(1000);
            }
            NoErrors = true;
        }

        public void ChangeTire()
        {
            NoErrors = false;
            ErrorCountdown = 0;
            ErrorTimer = 20;
            while (ErrorCountdown < ErrorTimer)
            {
                ErrorMessage = $"Punktering! Däckbytet dröjer {ErrorTimer - ErrorCountdown} sek till!";
                ErrorCountdown++;
                Thread.Sleep(1000);
            }
            NoErrors = true;
        }

        public void BirdOnWindow()
        {
            NoErrors = false;
            ErrorCountdown = 0;
            ErrorTimer = 10;
            while (ErrorCountdown < ErrorTimer)
            {
                ErrorMessage = $"Fågel på vindrutan! Att tvätta vindrutan tar {ErrorTimer - ErrorCountdown} sek till!";
                ErrorCountdown++;
                Thread.Sleep(1000);
            }
            NoErrors = true;
        }

        public void EngineFault()
        {
            Speed = Speed - 1;
        }
    }
}
