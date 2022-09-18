using LETTER_BLL.Interfaces;
using LETTER_DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ganss.Excel;
using System.Runtime.InteropServices;

namespace LETTER_BLL.Controllers
{
    public class RobotController : IRobotController
    {
        public List<Clients> clients;


        public async Task<List<Clients>> RobotStartReadFile(string value)
        {
            ExcelMapper mapper = new ExcelMapper(PathController.GetFilePath());

            await Task.Run(() =>
            {
                if (value == null)
                {
                    clients = mapper.Fetch<Clients>(sheetName: "Гарантии").ToList();
                    clients.RemoveRange(0, 1);
                }
                else
                {
                    clients = mapper.Fetch<Clients>(sheetName: "Гарантии").Where(cl => cl.Id == value).ToList();
                }
            });

            return clients;
        }

        public Task RobotStartWork()
        {
            throw new NotImplementedException();
        }
    }
}
