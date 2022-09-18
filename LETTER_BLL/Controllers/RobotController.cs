using Ganss.Excel;
using LETTER_BLL.Interfaces;
using LETTER_DAL.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LETTER_BLL.Controllers
{
    public class RobotController : IRobotController
    {
        private readonly IDataConversionController _dataConversion;
        private readonly ILogger _logger;
        public List<Clients> clients;

        public RobotController(IDataConversionController dataConversion, ILogger logger)
        {
            _dataConversion = dataConversion;
            _logger = logger;
        }

        public async Task<List<Clients>> RobotStartReadFile(string value)
        {
            ExcelMapper mapper = new ExcelMapper(PathController.GetFilePath()) { HeaderRow = false, MinRowNumber = 2};

            await Task.Run(() =>
            {
                if (value == null)
                {
                    clients = mapper.Fetch<Clients>(sheetName: "Гарантии").ToList();
                    //clients.RemoveRange(0, 1);
                }
                else
                {
                    clients = mapper.Fetch<Clients>(sheetName: "Гарантии").Where(cl => cl.Id == value).ToList();
                }
            });
            await RobotStartWork();
            return clients;
        }

        public async Task RobotStartWork()
        {
            await _dataConversion.StartupConversion(clients);
        }
    }
}
