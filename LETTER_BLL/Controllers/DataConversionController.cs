using LETTER_BLL.Interfaces;
using LETTER_DAL.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LETTER_BLL.Controllers
{
    public class DataConversionController : IDataConversionController
    {
        string date = string.Empty;
        string month = string.Empty;
        string day = string.Empty;
        string monthChar = "";
        public List<Clients> clientConvert;
        private readonly IWordController _wordController;
        private readonly ILogger _logger;

        public DataConversionController(IWordController wordController, ILogger logger)
        {
            _wordController = wordController;
            _logger = logger;
        }

        public async Task StartupConversion(List<Clients> clients)
        {
            await Task.Run(async () =>
            {
                clientConvert = clients;
                if(clientConvert != null)
                {
                    await Month();
                    await StartingDay();
                    await EndingDay();
                }
            });

            await _wordController.SortingClients(clientConvert);
        }

        public async Task EndingDay()
        {
            decimal sum = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < clientConvert.Count; i++)
                {
                //    if (clientConvert[i].DateEnd.Length > 3)
                //    {
                //        date = clientConvert[i].DateEnd.Remove(10, 9);
                //        month = date.Substring(0, 2);
                //        day = date.Substring(3, 2);
                //        date = date.Replace(month, "day");
                //        date = date.Replace(day, "month");
                //        date = date.Replace("month", month);
                //        date = date.Replace("day", day);
                //        clientConvert[i].DateEnd = date;
                //        day = string.Empty; month = string.Empty;

                //        sum = Math.Round(clientConvert[i].ToPayment, 2);
                //        clientConvert[i].ToPayment = sum;
                //    }
                //    else
                //    {
                //        continue;
                //    }
                }
                return clientConvert;
            });
        }

        public async Task Month()
        {
            await Task.Run(() =>
            {
                for(int i = 0; i < clientConvert.Count; i++)
                {
                    //if (clientConvert[i].DateEnd.Length > 3)
                    //{
                    //    monthChar = clientConvert[i].DateEnd.Remove(2, 17);

                    //    switch (monthChar)
                    //    {
                    //        case "01":
                    //            clientConvert[i].Month = "Январь";
                    //            break;
                    //        case "02":
                    //            clientConvert[i].Month = "Февраль";
                    //            break;
                    //        case "03":
                    //            clientConvert[i].Month = "Март";
                    //            break;
                    //        case "04":
                    //            clientConvert[i].Month = "Апрель";
                    //            break;
                    //        case "05":
                    //            clientConvert[i].Month = "Май";
                    //            break;
                    //        case "06":
                    //            clientConvert[i].Month = "Июнь";
                    //            break;
                    //        case "07":
                    //            clientConvert[i].Month = "Июль";
                    //            break;
                    //        case "08":
                    //            clientConvert[i].Month = "Август";
                    //            break;
                    //        case "09":
                    //            clientConvert[i].Month = "Сентябрь";
                    //            break;
                    //        case "10":
                    //            clientConvert[i].Month = "Октябрь";
                    //            break;
                    //        case "11":
                    //            clientConvert[i].Month = "Ноябрь";
                    //            break;
                    //        case "12":
                    //            clientConvert[i].Month = "Декабрь";
                    //            break;
                    //    }
                    //}
                    //else
                    //{
                    //    clientConvert[i].Month = clientConvert[0].Month;
                    //    continue;
                    //}
                    
                }
            });
        }

        public async Task StartingDay()
        {
            await Task.Run(() =>
            {
                for(int i = 0; i < clientConvert.Count; i++)
                {
                    //if (clientConvert[i].DateStart.Length > 3)
                    //{
                    //    date = clientConvert[i].DateStart.Remove(10, 9);
                    //    month = date.Substring(0, 2);
                    //    day = date.Substring(3, 2);
                    //    date = date.Replace(month, "day");
                    //    date = date.Replace(day, "month");
                    //    date = date.Replace("month", month);
                    //    date = date.Replace("day", day);
                    //    clientConvert[i].DateStart = date;
                    //    day = string.Empty; month = string.Empty;
                    //}
                    //else
                    //{
                    //    continue;
                    //}
                }
                return clientConvert;
            });
            
        }

        
    }
}
