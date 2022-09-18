using Ganss.Excel;
using LETTER_BLL.Interfaces;
using LETTER_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LETTER_BLL.Controllers
{
    public class RobotController : IRobotController
    {
        private readonly IWordController _wordController;
        public List<Clients> clients;

        public RobotController(IWordController wordController)
        {
            _wordController = wordController;
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
            await _wordController.SortingClients(clients);
        }
    }
}
